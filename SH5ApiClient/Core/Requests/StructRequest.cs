using Newtonsoft.Json;

namespace SH5ApiClient.Core.Requests
{
    public class StructRequest : RequestBase
    {
        public StructRequest(ConnectionParamSH5 connectionParamSH5, string procName) : base(procName, connectionParamSH5)
        {

        }

        public override OperationBase Operation => new StructOperation();

        public override string CreateJsonRequest() =>
            JsonConvert.SerializeObject(this);
    }
}
