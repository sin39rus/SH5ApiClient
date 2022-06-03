using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace SH5ApiClient.Core.Answears
{
    public abstract class SHAnswearBase
    {
        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errMessage")]
        public string? ErrMessage { get; set; }
        internal void CheckError()
        {
            if (ErrorCode != 0)
                throw new SHException($"Запрос в API SH закончился ошибкой: {ErrMessage}");
        }
    }
}