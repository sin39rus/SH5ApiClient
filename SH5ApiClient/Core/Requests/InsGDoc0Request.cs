using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace SH5ApiClient.Core.Requests
{
    /// <summary>Создание приходной накладной</summary>
    internal class InsGDoc0Request : RequestBase
    {
        /// <summary>Опции заголовка накладной</summary>
        [OriginalName("33")]
        public GDocOptions Options { get; private set; } = 0;

        /// <summary>Дата накладной</summary>
        [OriginalName("31")]
        public DateTime TimeStamp { get; private set; }

        /// <summary>Номер накладной</summary>
        [OriginalName("255")]
        public string Number { get; private set; }

        /// <summary>Rid валюты</summary>
        [OriginalName("100\\1")]
        public uint СurrencyRid { get; private set; } = 0;

        /// <summary>Курс валюты (единица базовой)</summary>
        [OriginalName("34")]
        public uint CurrencyRate { private set; get; } = 1;

        /// <summary>к единице валюты накладной</summary>
        [OriginalName("35")]
        public uint InvoiceCurrencyRate { private set; get; } = 1;

        /// <summary>Поставщик RID</summary>
        [OriginalName("105\\1")]
        public uint SupplierRid { get; private set; }

        /// <summary>Получатель RID</summary>
        [OriginalName("105#1\\1")]
        public uint ConsigneeRid { get; private set; }

        [OriginalName("6\\TTN_number")]
        public string ExtNumber { get; private set; }

        [OriginalName("6\\Comment")]
        public string Comment { get; private set; }

        /// <summary>Содержимое накладной</summary>
        [OriginalName("112")]
        public IEnumerable<GDoc0Item> Items { get; private set; }

        public InsGDoc0Request(ConnectionParamSH5 connectionParamSH5, DateTime timeStamp, string number, uint supplierRid, uint consigneeRid, string comment, IEnumerable<GDoc0Item> items) : base("InsGDoc0", connectionParamSH5)
        {
            TimeStamp = timeStamp;
            ExtNumber = number;
            SupplierRid = supplierRid;
            ConsigneeRid = consigneeRid;
            Items = items;
            Comment = comment;
        }
        public override OperationBase Operation =>
            new ExecOperation();

        public override string CreateJsonRequest()
        {
            JArray input = new JArray();

            JObject obj111 = new JObject();
            JArray original111 = new JArray();
            JArray values111 = new JArray();

            original111.Add(this.GetOriginalNameAttributeFromProperty(nameof(ExtNumber)));
            values111.Add(new JArray(ExtNumber));

            original111.Add(this.GetOriginalNameAttributeFromProperty(nameof(Comment)));
            values111.Add(new JArray(Comment));

            original111.Add(this.GetOriginalNameAttributeFromProperty(nameof(Options)));
            values111.Add(new JArray(Options));

            original111.Add(this.GetOriginalNameAttributeFromProperty(nameof(TimeStamp)));
            values111.Add(new JArray(TimeStamp.ToString("yyyy-MM-dd")));

            original111.Add(this.GetOriginalNameAttributeFromProperty(nameof(SupplierRid)));
            values111.Add(new JArray(SupplierRid));

            original111.Add(this.GetOriginalNameAttributeFromProperty(nameof(ConsigneeRid)));
            values111.Add(new JArray(ConsigneeRid));

            original111.Add(this.GetOriginalNameAttributeFromProperty(nameof(СurrencyRid)));
            values111.Add(new JArray(СurrencyRid));

            original111.Add(this.GetOriginalNameAttributeFromProperty(nameof(CurrencyRate)));
            values111.Add(new JArray(CurrencyRate));

            original111.Add(this.GetOriginalNameAttributeFromProperty(nameof(InvoiceCurrencyRate)));
            values111.Add(new JArray(InvoiceCurrencyRate));

            obj111.Add(new JProperty("head", "111"));
            obj111.Add(new JProperty("original", original111));
            obj111.Add(new JProperty("values", new JArray(values111)));
            input.Add(obj111);

            JObject obj112 = new JObject();
            JArray original112 = new JArray();
            JArray values112 = new JArray();


            original112.Add(Items.First().GetOriginalNameAttributeFromProperty(nameof(GDoc0Item.Rid)));
            values112.Add(new JArray(Items.Select(t => t.Rid)));
            original112.Add(Items.First().GetOriginalNameAttributeFromProperty(nameof(GDoc0Item.PurchaseSum)));
            values112.Add(new JArray(Items.Select(t => t.PurchaseSum)));
            original112.Add(Items.First().GetOriginalNameAttributeFromProperty(nameof(GDoc0Item.VatSum)));
            values112.Add(new JArray(Items.Select(t => t.VatSum)));
            original112.Add(Items.First().GetOriginalNameAttributeFromProperty(nameof(GDoc0Item.NspSum)));
            values112.Add(new JArray(Items.Select(t => t.NspSum)));
            original112.Add(Items.First().GetOriginalNameAttributeFromProperty(nameof(GDoc0Item.Amount)));
            values112.Add(new JArray(Items.Select(t => t.Amount)));
            original112.Add(Items.First().GetOriginalNameAttributeFromProperty(nameof(GDoc0Item.MeasureUnitRid)));
            values112.Add(new JArray(Items.Select(t => t.MeasureUnitRid)));

            original112.Add(Items.First().GetOriginalNameAttributeFromProperty(nameof(GDoc0Item.NdsRateValue)));
            values112.Add(new JArray(Items.Select(t => t.NdsRateValue)));
            original112.Add(Items.First().GetOriginalNameAttributeFromProperty(nameof(GDoc0Item.NspRateValue)));
            values112.Add(new JArray(Items.Select(t => t.NspRateValue)));
            original112.Add(Items.First().GetOriginalNameAttributeFromProperty(nameof(GDoc0Item.Options)));
            values112.Add(new JArray(Items.Select(t => t.Options)));


            obj112.Add(new JProperty("head", "112"));
            obj112.Add(new JProperty("original", original112));
            obj112.Add(new JProperty("values", new JArray(values112)));
            input.Add(obj112);

            JObject main = new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", input));
            return main.ToString();
        }
    }

}
