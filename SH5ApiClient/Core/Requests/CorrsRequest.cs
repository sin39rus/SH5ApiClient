using Newtonsoft.Json;

namespace SH5ApiClient.Core.Requests
{
    public class CorrsRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "Corrs";
        public CorrsRequest(ConnectionParamSH5 connectionParam) : base(procName, connectionParam)
        {
        }

        public override OperationBase Operation => new ExecOperation();

        public override string CreateJsonRequest()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
