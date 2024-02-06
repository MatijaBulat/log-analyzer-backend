namespace zavrsni_backend.ErrorModels
{
    public class AppCustomException : Exception
    {
        public AppCustomException() : base() { }

        public AppCustomException(string message) : base(message) { }

        public AppCustomException(string message, params object[] args) : base(String.Format(message, args)) { }
    }
}
