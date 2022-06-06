﻿using Newtonsoft.Json;

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

        public Dictionary<int, string> GetValues()
        {
            Dictionary<int, string> values = new();
            for (int x = 0; x < Values.Count; x++)
                values.Add(Idents[x], Values[x]);
            return values;
        }

        internal override void AfterParse()
        {

        }
    }
}
