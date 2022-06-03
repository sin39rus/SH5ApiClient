using Newtonsoft.Json;

namespace SH5ApiClient.Core.Answears
{
    public class SHStructAnswearContent
    {
        private SHStructAnswearContent() { }

        [JsonProperty("head")]
        public string Head { get; private set; }

        [JsonProperty("SingleRow")]
        public bool SingleRow { get; private set; }

        [JsonProperty("fields")]
        public SHStructAnswearField[] Fields { get; private set; }

        public SHStructAnswearField GetFieldInfo(string fieldName) =>
            Fields.Single(t=>t.Name == fieldName);
        public bool ConteinsFiled(string fieldName) =>
            Fields.Any(t=>t.Name == fieldName);
    }
}
