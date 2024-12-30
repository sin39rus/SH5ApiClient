using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;

namespace SH5ApiClient.Core.Requests
{
    public class CorrsRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "Corrs2";
        public CorrsRequest(ConnectionParamSH5 connectionParam) : base(procName, connectionParam)
        {

        }

        public override OperationBase Operation => new ExecOperation();

        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", new JArray(
                    new JObject(
                        new JProperty("head", "107#1"),
                        new JProperty("original", new JArray("37")),
                        new JProperty("values", new JArray()
                            {new JArray("2") }))))).ToString();
        }
    }
}
