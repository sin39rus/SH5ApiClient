using Newtonsoft.Json;

namespace SH5ApiClient.Core.Requests
{
    public class LEntitiesRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "LEntities";
        public LEntitiesRequest(ConnectionParamSH5 connectionParam) : base(procName, connectionParam, ServerOperationType.sh5exec)
        {
        }

        public override string CreateJsonRequest()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
