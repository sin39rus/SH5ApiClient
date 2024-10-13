using Newtonsoft.Json;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;

namespace SH5ApiClient.Core.Requests
{
    public class DepartsRequest : RequestBase
    {
        private const string procName = "Departs";
        public override OperationBase Operation => new ExecOperation();
        public DepartsRequest(ConnectionParamSH5 connectionParamSH5) : base(procName, connectionParamSH5)
        {

        }
        public override string CreateJsonRequest() =>
            JsonConvert.SerializeObject(this);
    }
}
