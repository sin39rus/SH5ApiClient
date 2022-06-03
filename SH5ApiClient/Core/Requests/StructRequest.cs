using Newtonsoft.Json;

namespace SH5ApiClient.Core.Requests
{
    public class StructRequest : RequestBase
    {
        public StructRequest(ConnectionParamSH5 connectionParamSH5, string procName) : base(procName, connectionParamSH5, ServerOperationType.sh5struct)
        {

        }
        public override string CreateJsonRequest() =>
            JsonConvert.SerializeObject(this);
    }
}
