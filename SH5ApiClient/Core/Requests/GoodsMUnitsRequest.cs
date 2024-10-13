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
    internal class GoodsMUnitsRequest : RequestBase
    {
        public override OperationBase Operation => new ExecOperation();
        private const string procName = "GoodsMUnits";
        private uint _rid;
        public GoodsMUnitsRequest(ConnectionParamSH5 connectionParamSH5, uint rid) : base(procName, connectionParamSH5)
        {
            _rid = rid;
        }
        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", new JArray(
                    new JObject(
                        new JProperty("head", "210"),
                        new JProperty("original", new JArray("1")),
                        new JProperty("values", new JArray()
                            { new JArray(_rid) }
                        ))))).ToString();
        }
    }
}
