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
    /// <summary>Запрос товара по ID</summary>
    internal class GoodsItemRequest : RequestBase
    {
        private readonly uint _goodItemRid;
        private const string procName = "GoodsItem";

        public GoodsItemRequest(ConnectionParamSH5 connectionParam, uint goodItemRid) : base (procName, connectionParam)
        {
            _goodItemRid = goodItemRid;
        }
        public override OperationBase Operation => 
            new ExecOperation();

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
                            { new JArray(_goodItemRid) }
                        ))))).ToString();
        }
    }
}
