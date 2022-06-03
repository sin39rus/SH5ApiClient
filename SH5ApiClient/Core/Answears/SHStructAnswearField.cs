using Newtonsoft.Json;

namespace SH5ApiClient.Core.Answears
{
    /// <summary>
    /// Поле
    /// </summary>
    public class SHStructAnswearField
    {
        private SHStructAnswearField() { }

        /// <summary>
        /// Имя
        /// </summary>
        [JsonProperty("path")]
        public string Name { get; private set; }
        /// <summary>
        /// Тип данных
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; private set; }
        /// <summary>
        /// Размер атрибута
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; private set; }
        /// <summary>
        /// Признак атрибута
        /// </summary>
        [JsonProperty("isAttr")]
        public bool IsAttr { get; private set; }
        /// <summary>
        /// Описание
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; private set; }
    }

}
