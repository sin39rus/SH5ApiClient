using System.Runtime.Serialization;

namespace SH5ApiClient.Infrastructure.Exceptions
{
    [Serializable]
    internal class ApiClientNotImplementedException : NotImplementedException
    {
        public ApiClientNotImplementedException()
        {
        }

        public ApiClientNotImplementedException(string? message) : base(message)
        {
        }

        public ApiClientNotImplementedException(string? message, Exception? inner) : base(message, inner)
        {
        }

        protected ApiClientNotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
