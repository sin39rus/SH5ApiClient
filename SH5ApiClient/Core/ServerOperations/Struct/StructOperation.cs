using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SH5ApiClient.Core.ServerOperations
{
    //ToDo написать тесты
    internal sealed class StructOperation : OperationBase
    {
        //Словарь заголовков данных
        private Dictionary<string, int> _headersDict = new Dictionary<string, int>();

        [JsonProperty("Version")]
        public string Version { get; private set; }

        [JsonProperty("UserName")]
        public string UserName { get; private set; }

        [JsonProperty("actionName")]
        public string ActionName { get; private set; }

        [JsonProperty("actionType")]
        public string ActionType { get; private set; }

        [JsonProperty("shTable")]
        public StructOperationContent[] Content { get; private set; } = Array.Empty<StructOperationContent>();

        public override string Uri => "sh5struct";
        internal override void AfterParse()
        {
            _headersDict = Content
                .Select((t, count) => new { Key = t.Head, Value = count })
                .ToDictionary(t => t.Key, t => t.Value);

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
