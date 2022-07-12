using Newtonsoft.Json.Linq;

namespace SH5ApiClient.Core.Requests
{
    public class GGroupsRequest : RequestBase
    {
        private const string procName = "GGroups";
        public GGroupsRequest(ConnectionParamSH5 connectionParam) : base(procName, connectionParam)
        {

        }
        public override OperationBase Operation => new ExecOperation();

        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName)).ToString();
        }

    }
}
