using Newtonsoft.Json.Linq;

namespace SH5ApiClient.Core.Requests
{
    public class PDocRequest : RequestBase
    {
        private readonly int _rid;
        private readonly string _guid;
        public PDocRequest(PGocType docType, ConnectionParamSH5 connectionParam, int rid, string guid) : base(connectionParam, ServerOperationType.sh5exec)
        {
            if (!Guid.TryParse(guid, out Guid _))
                throw new ArgumentException("Не корректное значение параметра guid");
            _rid = rid;
            _guid = $"{{{guid}}}";
            ProcName = docType switch
            {
                PGocType.Incoming => "PDoc0",
                PGocType.Outgoing => "PDoc1",
                PGocType.Inside => "PDoc2",
                _ => throw new NotImplementedException($"Не известный тип платежного документа \"{docType}\"."),
            };
        }

        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", new JArray(
                    new JObject(
                        new JProperty("head", "119"),
                        new JProperty("original", new JArray("1", "4")),
                        new JProperty("values", new JArray(
                            new JArray(_rid),
                            new JArray(_guid))))))).ToString();
        }
    }
}
