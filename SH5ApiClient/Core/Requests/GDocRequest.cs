using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Core.Requests
{
    public class GDocRequest : RequestBase
    {
        private readonly int _rid;
        private readonly string _guid;
        public GDocRequest(TTNType ttnType, ConnectionParamSH5 connectionParam, int rid, string guid) : base(connectionParam)
        {
            if (!Guid.TryParse(guid, out Guid _))
                throw new ArgumentException("Не корректное значение параметра guid");
            _rid = rid;
            _guid = $"{{{guid}}}";
            ProcName = ttnType switch
            {
                TTNType.PurchaseInvoice => "GDoc0",
                TTNType.ReturnRecipient => "GDoc1",
                TTNType.SalesInvoice => "GDoc4",
                TTNType.InternalMovement => "GDoc11",
                TTNType.ActProcessing => "GDoc10",
                TTNType.ReturnSupplier => "GDoc5",
                TTNType.CollationStatement => "GDoc8",
                TTNType.Compdection => "GDoc12",
                TTNType.Decomposition => "GDoc13",
                _ => throw new NotImplementedException($"Не известный тип накладной \"{ttnType}\".")
            };
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
