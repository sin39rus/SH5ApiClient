using Newtonsoft.Json.Linq;

namespace SH5ApiClient.Core.Requests
{
    public class DepartRequest : RequestBase
    {
        private const string procName = "Depart";
        public override OperationBase Operation => new ExecOperation();
        private uint _rid;
        private string _guid;
        public DepartRequest(ConnectionParamSH5 connectionParamSH5, uint rid, string guid) : base(procName, connectionParamSH5)
        {
            _rid = rid;
            _guid = guid;
        }
        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", new JArray(
                    new JObject(
                        new JProperty("head", "106"),
                        new JProperty("original", new JArray("1", "4")),
                        new JProperty("values", new JArray(
                            new JArray(_rid),
                            new JArray("{" + _guid + "}"))))))).ToString();
        }
    }
}
