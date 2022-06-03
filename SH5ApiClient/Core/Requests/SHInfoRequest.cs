using Newtonsoft.Json;

namespace SH5ApiClient.Core.Requests
{
    public class SHInfoRequest : RequestBase
    {
        public SHInfoRequest(ConnectionParamSH5 connectionParamSH5) : base(connectionParamSH5, ServerOperationType.sh5info)
        {

        }
        public override string CreateJsonRequest() =>
            JsonConvert.SerializeObject(this);
    }
}
