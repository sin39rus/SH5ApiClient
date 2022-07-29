using Newtonsoft.Json;

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
