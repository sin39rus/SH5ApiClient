using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Core.Answears
{
    //ToDo написать тесты
    public class SHStructAnswear : SHAnswearBase
    {
        //Словарь заголовков данных
        private Dictionary<string, int> _headersDict = new();
        private SHStructAnswear() { }

        [JsonProperty("Version")]
        public string Version { get; private set; }

        [JsonProperty("UserName")]
        public string UserName { get; private set; }

        [JsonProperty("actionName")]
        public string ActionName { get; private set; }

        [JsonProperty("actionType")]
        public string ActionType { get; private set; }

        [JsonProperty("shTable")]
        public SHStructAnswearContent[] Content { get; private set; }
        public static SHStructAnswear Parse(string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
                throw new ArgumentException($"\"{nameof(jsonText)}\" не может быть пустым или содержать только пробел.", nameof(jsonText));
            SHStructAnswear? answear = JsonConvert.DeserializeObject<SHStructAnswear>(jsonText);
            if (answear == null)
                throw new ExceptionSH("Ошибка разбора ответа SH.");
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
        public SHStructAnswearContent GetAnswearContent(string dataHeader)
        {
            if (!_headersDict.ContainsKey(dataHeader))
                throw new ArgumentException($"Блок данных с заголовком \"{dataHeader}\" отсутсвует.", nameof(dataHeader));
            return Content[_headersDict[dataHeader]];
        }
    }


}
