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
