using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zavrsni_backend.Models.DTO;
using zavrsni_backend.Services.Interfaces;

namespace zavrsni_backend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserDTO userDto, CancellationToken cancellationToken)
        {
            var userToken = await _userService.LoginUser(userDto, cancellationToken);

            return userToken is null ? NotFound() : Ok(userToken);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDto, CancellationToken cancellationToken)
        {
            var userToken = await _userService.RegisterUser(userDto, cancellationToken);

            return userToken is null ? NotFound() : Ok(userToken);
        }

        //[HttpGet("me"), Authorize]
        //public ActionResult<string> GetMe()
        //{
        //    var userName = "Matija";
        //    return Ok(userName);
        //}
    }
}
