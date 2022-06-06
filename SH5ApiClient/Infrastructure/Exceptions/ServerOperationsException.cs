namespace SH5ApiClient.Infrastructure.Exceptions
{
    public class ServerOperationsException : Exception
    {
        public ServerOperationsException()
        {
        }

        public ServerOperationsException(string? message) : base(message)
        {
        }

        public ServerOperationsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
