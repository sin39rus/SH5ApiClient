using Newtonsoft.Json;

namespace SH5ApiClient.Core.ServerOperations
{
    public abstract class OperationBase
    {
        public abstract string Uri { get; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errMessage")]
        public string? ErrMessage { get; set; }
        internal void CheckError()
        {
            if (ErrorCode != 0)
                throw new ServerOperationsException($"Запрос в API SH закончился ошибкой: {ErrMessage}");
        }
    }
}