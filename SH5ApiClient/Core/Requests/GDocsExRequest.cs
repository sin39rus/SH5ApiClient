using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.DTO.GDoc;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Core.Requests
{
    internal class GDocsExRequest : RequestBase
    {
        private const string procName = "GDocsEx";
        private readonly TTNTypeForRequest _ttnType;
        private readonly GDocsRequestFilter _filter;
        private readonly Depart[] _departs;
        private DateTime? _from;
        private DateTime? _to;
        public override OperationBase Operation =>
            new ExecOperation();
        public GDocsExRequest(DateTime? from, DateTime? to, ConnectionParamSH5 connectionParam, TTNTypeForRequest ttnType, GDocsRequestFilter filter, params Depart[] departs) : base(procName, connectionParam)
        {
            _ttnType = ttnType;
            _filter = filter;
            _departs = departs;
            _from = from;
            _to = to;
        }
        public override string CreateJsonRequest()
        {
            var object108 = _from != null && _to != null
                ? new JObject(
                        new JProperty("head", "108"),
                        new JProperty("original", new JArray("1", "2", "6", "111\\8")),
                        new JProperty("values", new JArray(
                            new JArray(_from.GetValueOrDefault().ToString("yyyy-MM-dd")),
                            new JArray(_to.GetValueOrDefault().ToString("yyyy-MM-dd")),
                            new JArray((int)_filter),
                            new JArray((int)_ttnType))))
                : new JObject(
                        new JProperty("head", "108"),
                        new JProperty("original", new JArray("6", "111\\8")),
                        new JProperty("values", new JArray(
                            new JArray((int)_filter),
                            new JArray((int)_ttnType))));
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", new JArray(
                    object108,
                    new JObject(
                        new JProperty("head", "106#10"),
                        new JProperty("original", new JArray("1")),
                        new JProperty("values", new JArray(new JArray(_departs.Select(t => t.Rid.ToString())), null))))))
                .ToString();
        }
    }
}
