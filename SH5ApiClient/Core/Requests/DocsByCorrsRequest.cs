using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Core.Requests
{
    /// <summary>Отчет Баланс по корреспондентам</summary>
    public class DocsByCorrsRequest : RequestBase
    {
        private const string procName = "DocsByCorrs";
        private readonly DateTime _from;
        private readonly DateTime _to;
        private readonly uint _internalCorrespondentRid;
        public DocsByCorrsRequest(ConnectionParamSH5 connectionParam, DateTime from, DateTime to, uint internalCorrespondentRid) : base(procName, connectionParam)
        {
            _from = from;
            _to = to;
            _internalCorrespondentRid = internalCorrespondentRid;
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
                        new JProperty("head", "108"),
                        new JProperty("original", new JArray("1", "2")),
                        new JProperty("values", new JArray(
                            new JArray(_from.ToString("yyyy-MM-dd")),
                            new JArray(_to.ToString("yyyy-MM-dd"))))),
                    new JObject(
                        new JProperty("head", "102#10"),
                        new JProperty("original", new JArray("1")),
                        new JProperty("values", new JArray(
                               new JArray(_internalCorrespondentRid.ToString()),
                               null //Если не добавить NULL последний массив будет не учтен.
                               ))))))
                .ToString();
        }
    }
}
