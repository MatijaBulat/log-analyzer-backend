namespace zavrsni_backend.ErrorModels
{
    public class FluentValidationException : AppCustomException
    {
        public FluentValidationException(IList<FluentValidation.Results.ValidationFailure> errors)
            : base($"{string.Join("\n", errors.Select(e => e.ErrorMessage))}") { }
    }
}
