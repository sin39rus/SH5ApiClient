using Newtonsoft.Json;

namespace SH5ApiClient.Core.ServerOperations
{
    //ToDo написать тесты
    internal sealed class StructOperation : OperationBase
    {
        //Словарь заголовков данных
        private Dictionary<string, int> _headersDict = new();

        [JsonProperty("Version")]
        public string Version { get; private set; }

        [JsonProperty("UserName")]
        public string UserName { get; private set; }

        [JsonProperty("actionName")]
        public string ActionName { get; private set; }

        [JsonProperty("actionType")]
        public string ActionType { get; private set; }

        [JsonProperty("shTable")]
        public StructOperationContent[] Content { get; private set; }

        public override string Uri => "sh5struct";

        public static StructOperation Parse(string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
                throw new ArgumentException($"\"{nameof(jsonText)}\" не может быть пустым или содержать только пробел.", nameof(jsonText));
            StructOperation? answear = JsonConvert.DeserializeObject<StructOperation>(jsonText);
            if (answear == null)
                throw new ServerOperationsException("Ошибка разбора ответа SH.");
            answear._headersDict = new(answear.Content.Select((t, count) => new KeyValuePair<string, int>(t.Head, count)));
            answear.CheckError();
            return answear;
        }
        /// <summary>
        /// Получение блока данных
        /// </summary>
        /// <param name="dataHeader">Имя заголовка данных</param>
        /// <returns>Блок данных</returns>
        /// <exception cref="ArgumentException"></exception>
        public StructOperationContent GetAnswearContent(string dataHeader)
        {
            if (!_headersDict.ContainsKey(dataHeader))
                throw new ArgumentException($"Блок данных с заголовком \"{dataHeader}\" отсутсвует.", nameof(dataHeader));
            return Content[_headersDict[dataHeader]];
        }
    }


}
