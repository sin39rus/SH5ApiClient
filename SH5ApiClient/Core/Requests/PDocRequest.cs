using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;
using System;

namespace SH5ApiClient.Core.Requests
{
    public class PDocRequest : RequestBase
    {
        private readonly int _rid;
        private readonly string _guid;
        public override OperationBase Operation => new ExecOperation();
        public PDocRequest(PGocType docType, ConnectionParamSH5 connectionParam, int rid, string guid) : base(connectionParam)
        {
            if (!Guid.TryParse(guid, out Guid _))
                throw new ArgumentException("Не корректное значение параметра guid");
            _rid = rid;
            _guid = $"{{{guid}}}";
            switch(docType)
            {
                case PGocType.Incoming:
                    ProcName = "PDoc0";
                    break;
                case PGocType.Outgoing:
                    ProcName = "PDoc1";
                    break;
                case PGocType.Inside:
                    ProcName = "PDoc2";
                    break;
                default: 
                    throw new NotImplementedException($"Не известный тип платежного документа \"{docType}\".");

            }
        }

        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", new JArray(
                    new JObject(
                        new JProperty("head", "119"),
                        new JProperty("original", new JArray("1", "4")),
                        new JProperty("values", new JArray(
                            new JArray(_rid),
                            new JArray(_guid))))))).ToString();
        }
    }
}
