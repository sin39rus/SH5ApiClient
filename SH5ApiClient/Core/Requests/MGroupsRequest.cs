using Newtonsoft.Json;

namespace SH5ApiClient.Core.Requests
{
    public class MGroupsRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "MGroups";
        public MGroupsRequest(ConnectionParamSH5 connectionParamSH5) : base(procName, connectionParamSH5) { }
        public override OperationBase Operation => new ExecOperation();
        public override string CreateJsonRequest() =>
            JsonConvert.SerializeObject(this);
    }
}
