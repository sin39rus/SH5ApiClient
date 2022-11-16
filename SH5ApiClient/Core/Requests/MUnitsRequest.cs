using Newtonsoft.Json.Linq;

namespace SH5ApiClient.Core.Requests
{
    internal class MUnitsRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "MUnits";

        private readonly uint? _rid;
        public override OperationBase Operation => new ExecOperation();
        internal MUnitsRequest(ConnectionParamSH5 connectionParam, uint? ridMGroup = null) : base(procName, connectionParam)
        {
            _rid = ridMGroup;
        }
        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", new JArray(
                    new JObject(
                        new JProperty("head", "205"),
                        new JProperty("original", new JArray("1")),
                        new JProperty("values", new JArray()
                            { new JArray(_rid) }
                        ))))).ToString();
        }
    }
}
