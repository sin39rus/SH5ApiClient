using Newtonsoft.Json;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;

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
