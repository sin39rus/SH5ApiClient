using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;

namespace SH5ApiClient.Core.Requests
{
    public class InsGoodRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "InsGood";
        public InsGoodRequest(ConnectionParamSH5 connectionParam, string name, IEnumerable<MeasureUnit> measureUnits) : base(procName, connectionParam)
        {
            Name = name;
            MeasureUnits = measureUnits;
        }

        /// <summary>
        /// Name
        /// </summary>
        [OriginalName("3")]
        public string Name { set; get; }

        /// <summary>Единица измерения</summary>
        public IEnumerable<MeasureUnit> MeasureUnits { set; get; }

        /// <summary>Тип товара</summary>
        [OriginalName("25")]
        public GoodsItemType Type { set; get; } = GoodsItemType.Item;

        [OriginalName("19")]
        public decimal Ignore { set; get; } = 255;

        /// <summary>Товарная группа</summary>
        [OriginalName("200\\1")]
        public string GoodsCategories { set; get; } = "0";

        /// <summary>Бухгалтерская товарная группа></summary>
        [OriginalName("290\\1")]
        public string BGoodsCategories { set; get; } = "0";


        public override OperationBase Operation => new ExecOperation();

        public override string CreateJsonRequest()
        {
            JArray input = new JArray();

            JObject obj210 = new JObject();
            JArray original210 = new JArray();
            JArray values210 = new JArray();


            original210.Add(this.GetOriginalNameAttributeFromProperty(nameof(Name)));
            values210.Add(new JArray(Name));

            original210.Add(this.GetOriginalNameAttributeFromProperty(nameof(Type)));
            values210.Add(new JArray(Type));

            original210.Add(this.GetOriginalNameAttributeFromProperty(nameof(GoodsCategories)));
            values210.Add(new JArray(GoodsCategories));

            original210.Add(this.GetOriginalNameAttributeFromProperty(nameof(BGoodsCategories)));
            values210.Add(new JArray(BGoodsCategories));

            original210.Add(this.GetOriginalNameAttributeFromProperty(nameof(Ignore)));
            values210.Add(new JArray(Ignore));

            original210.Add("209\\1");
            values210.Add(new JArray(1));

            obj210.Add(new JProperty("head", "210"));
            obj210.Add(new JProperty("original", original210));
            obj210.Add(new JProperty("values", new JArray(values210)));


            JObject obj211 = new JObject();
            JArray original211 = new JArray();
            original211.Add("206\\1");
            original211.Add("8");
            original211.Add("41");
            original211.Add("10");

            JArray values211 = new JArray();
            var rids = new JArray();
            var measureUnitType = new JArray();
            var baseRatio = new JArray();
            var weight = new JArray();
            foreach (var measureUnit in MeasureUnits)
            {
                rids.Add(measureUnit.Rid);
                measureUnitType.Add(measureUnit.MeasureUnitType);
                baseRatio.Add(measureUnit.BaseRatio);
                weight.Add(measureUnit.BaseRatio);
            }
            values211.Add(new JArray(rids));
            values211.Add(new JArray(measureUnitType));
            values211.Add(new JArray(baseRatio));
            values211.Add(new JArray(weight));

            obj211.Add(new JProperty("original", original211));
            obj211.Add(new JProperty("values", new JArray(values211)));
            obj211.Add(new JProperty("head", "211#1"));

            input.Add(obj210);
            input.Add(obj211);

            JObject main = new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", input));

            return main.ToString();
        }
    }
}
