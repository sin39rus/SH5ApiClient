using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;

namespace SH5ApiClient.Core.Requests
{
    public class GoodsTreeRequest : RequestBase
    {
        public GoodsTreeRequest(ConnectionParamSH5 connectionParam) :
            base("GoodsTree", connectionParam)
        { }

        public override OperationBase Operation =>
            new ExecOperation();

        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName)).ToString();
        }
    }
}
