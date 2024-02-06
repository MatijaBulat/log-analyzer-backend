using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using zavrsni_backend.ErrorModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace zavrsni_backend.Middlewares
{
    public class ExceptionMiddleware
    {
        RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var custom_message = "";

                switch (error)
                {
                    case UnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        custom_message = $"{error.Message}";
                        break;
                    case NotImplementedException:
                        response.StatusCode = (int)HttpStatusCode.NotImplemented;
                        custom_message = $"{error.Message}";
                        break;
                    case FluentValidationException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        custom_message = $"{error.Message}";
                        break;
                    case SystemException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        custom_message = $"{e.Message}";
                        break;
                    case AppCustomException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        custom_message = $"{e.Message}";
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        custom_message = $"An error occurred while processing your request.";
                        break;
                }
                await response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = response.StatusCode,
                    Message = custom_message
                }.ToString());
            }
        }
    }
}
