using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Core.Requests
{
    internal class CDeclsRequest : RequestBase
    {
        private const string procName = "CDecls";
        public override OperationBase Operation => new ExecOperation();
        public CDeclsRequest(ConnectionParamSH5 connectionParamSH5) : base(procName, connectionParamSH5) { }
        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName)).ToString();
        }
    }
}
