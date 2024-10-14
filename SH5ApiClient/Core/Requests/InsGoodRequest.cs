using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;
using System;

namespace SH5ApiClient.Core.Requests
{
    public class InsGoodRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "InsGood";
        public InsGoodRequest(ConnectionParamSH5 connectionParam, string name, MeasureUnit measureUnit) : base(procName, connectionParam)
        {
            Name = name;
            MeasureUnit = measureUnit;
        }

        /// <summary>
        /// Name
        /// </summary>
        [OriginalName("3")]
        public string Name { set; get; }

        /// <summary>Единица измерения</summary>
        public MeasureUnit MeasureUnit { set; get; }

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
            JArray values211 = new JArray();

            original211.Add("206\\1");
            values211.Add(new JArray(MeasureUnit.Rid));
            original211.Add("206\\3");
            values211.Add(new JArray("Литры"));

            original211.Add("8");
            values211.Add(new JArray(MeasureUnitType.Base | MeasureUnitType.Report | MeasureUnitType.Request | MeasureUnitType.AutoDocuments | MeasureUnitType.Calculations));
            original211.Add("41");
            values211.Add(new JArray(1));
            original211.Add("10");
            values211.Add(new JArray(1));


            obj211.Add(new JProperty("head", "211#1"));
            obj211.Add(new JProperty("original", original211));
            obj211.Add(new JProperty("values", new JArray(values211)));

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
