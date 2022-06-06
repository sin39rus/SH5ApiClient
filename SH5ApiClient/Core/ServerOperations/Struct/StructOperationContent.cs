using Newtonsoft.Json;

namespace SH5ApiClient.Core.ServerOperations
{
    internal sealed class StructOperationContent
    {
        private StructOperationContent() { Head = string.Empty; }

        [JsonProperty("head")]
        public string Head { get; private set; }

        [JsonProperty("SingleRow")]
        public bool SingleRow { get; private set; }

        [JsonProperty("fields")]
        public StructOperationField[] Fields { get; private set; } = Array.Empty<StructOperationField>();

        public StructOperationField GetFieldInfo(string fieldName) =>
            Fields.Single(t => t.Name == fieldName);
        public bool ConteinsFiled(string fieldName) =>
            Fields.Any(t => t.Name == fieldName);
    }
}
