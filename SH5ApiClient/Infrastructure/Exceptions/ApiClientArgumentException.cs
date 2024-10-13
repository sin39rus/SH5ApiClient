using System;
using System.Runtime.Serialization;

namespace SH5ApiClient.Infrastructure.Exceptions
{

    [Serializable]
    internal class ApiClientArgumentException : ArgumentException
    {
        public ApiClientArgumentException()
        {
        }

        public ApiClientArgumentException(string message) : base(message)
        {
        }

        public ApiClientArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ApiClientArgumentException(string message, string paramName) : base(message, paramName)
        {
        }

        public ApiClientArgumentException(string message, string paramName, Exception innerException) : base(message, paramName, innerException)
        {
        }

        protected ApiClientArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
