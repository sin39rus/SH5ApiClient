using Newtonsoft.Json;

namespace SH5ApiClient.Core.Answears
{
    public sealed class SHInfoAnswear : SHAnswearBase
    {
        private SHInfoAnswear() { }
        
        /// <summary>
        /// Версия API
        /// </summary>
        [JsonProperty("Version")]
        public string? ApiVersion { get; set; }
        /// <summary>
        /// Не разобрался, в тестовом было значение 1
        /// </summary>
        [JsonProperty("LinkType")]
        public int LinkType { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        [JsonProperty("Host")]
        public string? Host { get; set; }
        /// <summary>
        /// Порт
        /// </summary>
        [JsonProperty("Port")]
        public int Port { get; set; }
        /// <summary>
        /// Строка подключения, пример: "(tcp/ip) 192.168.200.4:7777"
        /// </summary>
        [JsonProperty("LinkDisp")]
        public string? LinkDisp { get; set; }
        /// <summary>
        /// Таймаут
        /// </summary>
        [JsonProperty("timeout")]
        public int Timeout { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [JsonProperty("UserName")]
        public string? UserName { get; set; }
        /// <summary>
        /// Информация о базе данных
        /// </summary>
        [JsonProperty("DB")]
        public SHDBInfo? DBInfo { get; set; }

        /// <summary>
        /// Разобрать ответ SH
        /// </summary>
        /// <param name="jsonText">Содержимое ответа (json)</param>
        /// <returns>Ответ SH</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static SHInfoAnswear Parse(string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
                throw new ArgumentException($"\"{nameof(jsonText)}\" не может быть пустым или содержать только пробел.", nameof(jsonText));
            SHInfoAnswear? answear = JsonConvert.DeserializeObject<SHInfoAnswear>(jsonText);
            if (answear == null)
                throw new Exception("Ошибка разбора ответа SH.");
            answear.CheckError();
            return answear;
        }
    }
}
