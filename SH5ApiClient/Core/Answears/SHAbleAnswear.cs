using Newtonsoft.Json;

namespace SH5ApiClient.Core.Answears
{
    public class SHAbleAnswear : SHAnswearBase
    {
        [JsonProperty("Version")]
        public string Version { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("procList")]
        public string[] ProcList { get; set; }

        [JsonProperty("allow")]
        public bool[] Allow { get; set; }

        /// <summary>
        /// Разобрать ответ SH
        /// </summary>
        /// <param name="jsonText">Содержимое ответа (json)</param>
        /// <returns>Ответ SH</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static SHAbleAnswear Parse(string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
                throw new ArgumentException($"\"{nameof(jsonText)}\" не может быть пустым или содержать только пробел.", nameof(jsonText));
            SHAbleAnswear? answear = JsonConvert.DeserializeObject<SHAbleAnswear>(jsonText);
            if (answear == null)
                throw new ArgumentException("Ошибка разбора ответа SH.");
            answear.CheckError();

            return answear;
        }
    }
}
