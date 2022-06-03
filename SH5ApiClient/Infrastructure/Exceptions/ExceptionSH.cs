namespace SH5ApiClient.Infrastructure.Exceptions
{
    public class ExceptionSH : Exception
    {
        public ExceptionSH()
        {
        }

        public ExceptionSH(string? message) : base(message)
        {
        }

        public ExceptionSH(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
