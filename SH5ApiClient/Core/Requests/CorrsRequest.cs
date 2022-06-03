using Newtonsoft.Json;

namespace SH5ApiClient.Core.Requests
{
    public class CorrsRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "Corrs";
        public CorrsRequest(ConnectionParamSH5 connectionParam) : base(procName, connectionParam, ServerOperationType.sh5exec)
        {
        }

        public override string CreateJsonRequest()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
