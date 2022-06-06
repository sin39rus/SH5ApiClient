using Newtonsoft.Json;

namespace SH5ApiClient.Core.Requests
{
    public class PDocsRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "PDocs";
        public override OperationBase Operation => new ExecOperation();
        public PDocsRequest(ConnectionParamSH5 connectionParam) : base(procName, connectionParam)
        {
        }
        public override string CreateJsonRequest()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
