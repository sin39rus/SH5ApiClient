using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SH5ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SH5ApiClient.Core.ServerOperations
{
    /// <summary>
    /// Ответ SH
    /// </summary>
    public sealed class ExecOperation : OperationBase
    {
        //Словарь заголовков данных
        private Dictionary<string, int> _headersDict = new Dictionary<string, int>();

        /// <summary>
        /// Версия SH API
        /// </summary>
        [JsonProperty("Version")]
        public string Version { get; private set; }

        /// <summary>
        /// Имя пользователя сделавшего запрос
        /// </summary>
        [JsonProperty("UserName")]
        public string UserName { get; private set; }

        /// <summary>
        /// Имя вызываемой процедуры
        /// </summary>
        [JsonProperty("actionName")]
        public string ActionName { get; private set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        [JsonProperty("actionType")]
        public string ActionType { get; private set; }

        /// <summary>
        /// Содержимое ответа
        /// </summary>
        [JsonProperty("shTable")]
        public ExecOperationContent[] Content { get; private set; } = Array.Empty<ExecOperationContent>();

        public override string Uri => "sh5exec";

        /// <summary>
        /// Получение блока данных
        /// </summary>
        /// <param name="dataHeader">Имя заголовка данных</param>
        /// <returns>Блок данных</returns>
        /// <exception cref="ArgumentException"></exception>
        public ExecOperationContent GetAnswearContent(string dataHeader)
        {
            if (!_headersDict.ContainsKey(dataHeader))
                throw new ArgumentException($"Блок данных с заголовком \"{dataHeader}\" отсутствует.", nameof(dataHeader));
            return Content[_headersDict[dataHeader]];
        }
        internal override void AfterParse()
        {
            _headersDict = Content
                .Select((t, count) => new { key = t.Head, value = count })
                .ToDictionary(key => key.key, value => value.value);
            //_headersDict = new Dictionary<string, int>(Content.Select((t, count) => new KeyValuePair<string, int>(t.Head, count)));
        }
        public static string ChangeValue(string inputJsonText, string head, string originalName, object newValue)
        {
            ExecOperation shAnswear = Parse<ExecOperation>(inputJsonText);
            ExecOperationContent shAnswearContent = shAnswear.GetAnswearContent(head);
            int originalNameIndex = shAnswearContent.GetIndexOriginalName(originalName);
            JObject doc = JObject.Parse(inputJsonText);

            JToken value = doc["shTable"]?
                            .Children()
                            .SingleOrDefault(t => t["head"]?.ToString() == head)?
                            ["values"]?
                            .Children().ElementAt(originalNameIndex).First;
            if (doc["shTable"] is null)
                throw new ArgumentNullException(nameof(head), $"Таблица \"{head}\" не найдена.");
            if (value is null)
                throw new ArgumentNullException(nameof(originalName), $"Параметр \"{originalName}\" в таблице \"{head}\" не найден.");
            value?.Replace(new JValue(newValue));
            return doc.ToString();
        }
        public static string ConvertToRequest(string inputJsonText, string head, ConnectionParamSH5 connectionParam, string procName)
        {
            JObject doc = JObject.Parse(inputJsonText);
            if (doc["shTable"] is null)
                throw new Exception("Таблица shTable не найдена. Необходима для преобразования в таблицу Input");
            JToken value = doc["shTable"]?
                            .Children()
                            .SingleOrDefault(t => t["head"]?.ToString() == head);
            if (value is null)
                throw new ArgumentNullException(nameof(head), $"Таблица \"{head}\" не найдена.");
            JObject request = new JObject(
                new JProperty("UserName", connectionParam.UserName),
                new JProperty("Password", connectionParam.Password),
                new JProperty("procName", procName),
                new JProperty("Input", new JArray(value)));
            return request.ToString();
        }
    }
}
