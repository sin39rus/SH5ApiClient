using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;
using SH5ApiClient.Models.Enums;
using System;

namespace SH5ApiClient.Core.Requests
{
    public class GDocRequest : RequestBase
    {
        private readonly uint _rid;
        private readonly string _guid;
        public GDocRequest(ConnectionParamSH5 connectionParam, TTNType ttnType, uint rid, string guid) : base(connectionParam)
        {
            if (!Guid.TryParse(guid, out Guid _newGuid))
                throw new ArgumentException("Не корректное значение параметра guid");
            _rid = rid;
            _guid = $"{{{_newGuid}}}";
            ProcName = GetProcName(ttnType);
            
        }
        public static string GetProcName(TTNType ttnType)
        {
            switch (ttnType)
            {
                case TTNType.PurchaseInvoice:
                    return "GDoc0";
                case TTNType.ReturnRecipient:
                    return "GDoc1";
                case TTNType.SalesInvoice:
                    return "GDoc4";
                case TTNType.InternalMovement:
                    return "GDoc11";
                case TTNType.ActProcessing:
                    return "GDoc10";
                case TTNType.ReturnSupplier:
                    return "GDoc5";
                case TTNType.CollationStatement:
                    return "GDoc8";
                case TTNType.CollationStatementDiffs:
                    return "GDoc8Diffs";
                case TTNType.Compdection:
                    return "GDoc12";
                case TTNType.Decomposition:
                    return "GDoc13";
                default:
                    throw new NotImplementedException($"Не известный тип накладной \"{ttnType}\".");
            }
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
                        new JProperty("head", "111"),
                        new JProperty("original", new JArray("1", "4")),
                        new JProperty("values", new JArray(
                            new JArray(_rid),
                            new JArray(_guid))))))).ToString();
        }
    }
}
