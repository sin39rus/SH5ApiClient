using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Infrastructure.Exceptions
{
    public class SH5ApiClientException : Exception
    {
        public SH5ApiClientException()
        {
        }

        public SH5ApiClientException(string? message) : base(message)
        {
        }

        public SH5ApiClientException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected SH5ApiClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
