using Newtonsoft.Json;

namespace SH5ApiClient.Core.ServerOperations
{
    internal sealed class EnumOperation : OperationBase
    {
        [JsonProperty("Version")]
        public string? Version { get; set; }

        [JsonProperty("UserName")]
        public string? UserName { get; set; }

        [JsonProperty("actionType")]
        public string? ActionType { get; set; }

        [JsonProperty("head")]
        public string? Head { get; set; }

        [JsonProperty("path")]
        public string? Path { get; set; }

        [JsonProperty("idents")]
        public List<int> Idents { get; set; } = new();

        [JsonProperty("values")]
        public List<string> Values { get; set; } = new();

        public override string Uri => "sh5enum";

        /// <summary>
        /// Разобрать ответ SH
        /// </summary>
        /// <param name="jsonText">Содержимое ответа (json)</param>
        /// <returns>Ответ SH</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static EnumOperation Parse(string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
                throw new ArgumentException($"\"{nameof(jsonText)}\" не может быть пустым или содержать только пробел.", nameof(jsonText));
            EnumOperation? answear = JsonConvert.DeserializeObject<EnumOperation>(jsonText);
            if (answear == null)
                throw new ArgumentException("Ошибка разбора ответа SH.");
            answear.CheckError();
            return answear;
        }

        public Dictionary<int, string> GetValues()
        {
            Dictionary<int, string> values = new();
            for (int x = 0; x < Values.Count; x++)
                values.Add(Idents[x], Values[x]);
            return values;
        }
    }
}
