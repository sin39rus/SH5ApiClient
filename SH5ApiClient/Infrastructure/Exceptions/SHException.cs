namespace SH5ApiClient.Infrastructure.Exceptions
{
    public class SHException : Exception
    {
        public SHException()
        {
        }

        public SHException(string? message) : base(message)
        {
        }

        public SHException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
