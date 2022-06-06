using Newtonsoft.Json;

namespace SH5ApiClient.Core.Requests
{
    public class LEntitiesRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "LEntities";
        public override OperationBase Operation => new ExecOperation();
        public LEntitiesRequest(ConnectionParamSH5 connectionParam) : base(procName, connectionParam)
        {
        }

        public override string CreateJsonRequest()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
