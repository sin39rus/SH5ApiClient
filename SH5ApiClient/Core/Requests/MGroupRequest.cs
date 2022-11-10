using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SH5ApiClient.Core.Requests
{
    public class MGroupRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "MGroup";
        private readonly uint _rid;
        public MGroupRequest(ConnectionParamSH5 connectionParamSH5, uint groupRid) : base(procName, connectionParamSH5)
        {
            _rid = groupRid;
        }
        public override OperationBase Operation => new ExecOperation();
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
