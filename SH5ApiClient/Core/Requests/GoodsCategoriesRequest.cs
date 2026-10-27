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
    internal class GoodsCategoriesRequest : RequestBase
    {
        private const string procName = "GoodsCategories";
        public GoodsCategoriesRequest(ConnectionParamSH5 param) : base(procName, param)
        {
            
        }
        public override OperationBase Operation => new ExecOperation();
        
        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName)
                ).ToString();
        }
    }
}
