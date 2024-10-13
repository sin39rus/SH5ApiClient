using Newtonsoft.Json;

namespace SH5ApiClient.Core.ServerOperations
{
    public sealed class InfoOperation : OperationBase
    {
        /// <summary>
        /// Версия API
        /// </summary>
        [JsonProperty("Version")]
        public string ApiVersion { get; set; }
        /// <summary>
        /// Не разобрался, в тестовом было значение 1
        /// </summary>
        [JsonProperty("LinkType")]
        public int LinkType { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        [JsonProperty("Host")]
        public string Host { get; set; }
        /// <summary>
        /// Порт
        /// </summary>
        [JsonProperty("Port")]
        public int Port { get; set; }
        /// <summary>
        /// Строка подключения, пример: "(tcp/ip) 192.168.200.4:7777"
        /// </summary>
        [JsonProperty("LinkDisp")]
        public string LinkDisp { get; set; }
        /// <summary>
        /// Таймаут
        /// </summary>
        [JsonProperty("timeout")]
        public int Timeout { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        /// <summary>
        /// Информация о базе данных
        /// </summary>
        [JsonProperty("DB")]
        public InfoOperationContent DBInfo { get; set; }

        public override string Uri => "sh5info";

        internal override void AfterParse()
        {

        }
    }
}
