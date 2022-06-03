using Newtonsoft.Json;

namespace SH5ApiClient.Core.Requests
{
    public class PDocsRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "PDocs";
        public PDocsRequest(ConnectionParamSH5 connectionParam) : base(procName, connectionParam, ServerOperationType.sh5exec)
        {
        }
        public override string CreateJsonRequest()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
