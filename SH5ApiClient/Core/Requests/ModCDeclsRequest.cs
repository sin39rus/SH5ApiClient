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
    internal class ModCDeclsRequest : RequestBase
    {
        private const string procName = "ModCDecls";
        private IEnumerable<string> _gtdNumbers;
        public override OperationBase Operation => new ExecOperation();
        public ModCDeclsRequest(ConnectionParamSH5 connectionParamSH5, IEnumerable<string> gtdNumbers) : base(procName, connectionParamSH5)
        {
            _gtdNumbers = gtdNumbers;
        }
        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", new JArray(
                    new JObject(
                        new JProperty("head", "116"),
                        new JProperty("original", new JArray("3")),
                        new JProperty("values", new JArray(new JArray(_gtdNumbers), new JObject())
                        ))))).ToString();
        }
    }
}
