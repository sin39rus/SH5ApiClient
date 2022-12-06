using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models.Enums;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SH5ApiClient.Models.DTO.Tests
{
    [TestClass()]
    public class GDoc8Tests
    {
        [TestMethod()]
        public void ParseGDoc8Test()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\Gdoc8.json", Encoding.UTF8);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            var gDoc8 = GDoc8.Parse(answear);
            Assert.IsNotNull(gDoc8);
            Assert.IsNotNull(gDoc8.Header);
            Assert.IsNotNull(gDoc8.Content);

            var header = gDoc8.Header;
            Assert.AreEqual(header.Rid, (uint?)58884);
            Assert.AreEqual(header.GUID, "24081730-6AA9-E77C-4C76-AD930C119F3F");
            Assert.AreEqual(header.TTNOptions, TTNOptions.Active | TTNOptions.ActivatedByCntr0);
            Assert.AreEqual(header.DateStamp, new DateTime(2022, 09, 23));
            Assert.IsNotNull(header.BuhOperation);
            Assert.AreEqual(header.BuhOperation.Rid, (uint)0);
            Assert.AreEqual(header.BuhOperation.Name, "Бухгалтерская операция");
            Assert.IsNotNull(header.Supplier);
            Assert.AreEqual(header.Supplier.Rid, (uint)176160809);
            Assert.AreEqual(header.Supplier.Name, " ОТДЕЛ ПРОДАЖ (ООО ЦТО Ассар)");
            Assert.AreEqual(header.Supplier.SubType, CorrType3.AlcoholProducer);
            Assert.IsNotNull(header.Supplier.KPP);
            Assert.AreEqual(header.Supplier.KPP.Rid, (uint)994);
            Assert.AreEqual(header.Supplier.KPP.Name, "12345678");
            Assert.AreEqual(header.Name, "12345");
            Assert.AreEqual(header.Attributes6.Count, 31);
            foreach (var attribute in header.Attributes6)
                Assert.IsNull(attribute.Value);
            Assert.AreEqual(header.Attributes7.Count, 10);
            foreach (var attribute in header.Attributes7)
                Assert.IsNull(attribute.Value);
            Assert.AreEqual(header.MinActiveDate, new DateTime(2022, 09, 23));

            Assert.IsNotNull(header.Creator);
            Assert.AreEqual(header.Creator.Name, "Admin");
            Assert.AreEqual(header.Creator.Rid, (uint)0);
            Assert.AreEqual(header.Creator.Date, new DateTime(2022, 09, 23));
            Assert.AreEqual(header.Creator.Time, (uint)50779);
            Assert.IsNotNull(header.LastUpdater);
            Assert.AreEqual(header.LastUpdater.Name, "Admin");
            Assert.AreEqual(header.LastUpdater.Rid, (uint)0);
            Assert.AreEqual(header.LastUpdater.Date, new DateTime(2022, 09, 23));
            Assert.AreEqual(header.LastUpdater.Time, (uint)51193);

            var item1 = gDoc8.Content.ElementAt(0);
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.Rid, (uint)114381);
            Assert.IsNotNull(item1.GoodsItem);
            Assert.AreEqual(item1.GoodsItem.Rid, (uint)5266);
            Assert.AreEqual(item1.GoodsItem.Name, "Кассовая лента термо-80мм (21м)");
            Assert.AreEqual(item1.GoodsItem.Attributes6.Count, 21);
            foreach (var attribute in item1.GoodsItem.Attributes6)
                Assert.IsNull(attribute.Value);
            Assert.IsNotNull(item1.GoodsItem.MeasureUnit);
            Assert.AreEqual(item1.GoodsItem.MeasureUnit.Rid, (uint)5);
            Assert.AreEqual(item1.GoodsItem.MeasureUnit.Name, "шт");
            Assert.IsNotNull(item1.GoodsItem.DishComposition);
            Assert.IsNull(item1.GoodsItem.DishComposition.Name);
            Assert.IsNull(item1.GoodsItem.DishComposition.Rid);
            Assert.IsNotNull(item1.GoodsItem.DishComposition.DishCompositionVersion);
            Assert.IsNull(item1.GoodsItem.DishComposition.DishCompositionVersion.Name);
            Assert.IsNull(item1.GoodsItem.DishComposition.DishCompositionVersion.Rid);
            Assert.IsNull(item1.GoodsItem.DishComposition.DishCompositionVersion.Version);

            Assert.AreEqual(item1.Options, (uint)3);
            Assert.AreEqual(item1.Quantity, 50m);
            Assert.IsNull(item1.AmountWeighed);
            Assert.AreEqual(item1.Attributes6.Count, 6);

            var item2 = gDoc8.Content.ElementAt(1);
            Assert.IsNotNull(item2);
            Assert.AreEqual(item2.Rid, (uint)114382);
            Assert.IsNotNull(item2.GoodsItem);
            Assert.AreEqual(item2.GoodsItem.Rid, (uint)2418);
            Assert.AreEqual(item2.GoodsItem.Name, "Кассовая лента термо-80мм (72м)");
            Assert.AreEqual(item2.GoodsItem.Attributes6.Count, 21);
            foreach (var attribute in item2.GoodsItem.Attributes6)
                Assert.IsNull(attribute.Value);
            Assert.IsNotNull(item2.GoodsItem.MeasureUnit);
            Assert.AreEqual(item2.GoodsItem.MeasureUnit.Rid, (uint)5);
            Assert.AreEqual(item2.GoodsItem.MeasureUnit.Name, "шт");
            Assert.IsNotNull(item2.GoodsItem.DishComposition);
            Assert.IsNull(item2.GoodsItem.DishComposition.Name);
            Assert.IsNull(item2.GoodsItem.DishComposition.Rid);
            Assert.IsNotNull(item2.GoodsItem.DishComposition.DishCompositionVersion);
            Assert.IsNull(item2.GoodsItem.DishComposition.DishCompositionVersion.Name);
            Assert.IsNull(item2.GoodsItem.DishComposition.DishCompositionVersion.Rid);
            Assert.IsNull(item2.GoodsItem.DishComposition.DishCompositionVersion.Version);

            Assert.AreEqual(item2.Options, (uint)3);
            Assert.AreEqual(item2.Quantity, 300m);
            Assert.IsNull(item2.AmountWeighed);
            Assert.AreEqual(item2.Attributes6.Count, 6);


            var item3 = gDoc8.Content.ElementAt(2);
            Assert.IsNotNull(item3);
            Assert.AreEqual(item3.Rid, (uint)114383);
            Assert.IsNotNull(item3.GoodsItem);
            Assert.AreEqual(item3.GoodsItem.Rid, (uint)5266);
            Assert.AreEqual(item3.GoodsItem.Name, "Кассовая лента термо-80мм (21м)");
            Assert.AreEqual(item3.GoodsItem.Attributes6.Count, 21);
            foreach (var attribute in item3.GoodsItem.Attributes6)
                Assert.IsNull(attribute.Value);
            Assert.IsNotNull(item3.GoodsItem.MeasureUnit);
            Assert.AreEqual(item3.GoodsItem.MeasureUnit.Rid, (uint)5);
            Assert.AreEqual(item3.GoodsItem.MeasureUnit.Name, "шт");
            Assert.IsNotNull(item3.GoodsItem.DishComposition);
            Assert.AreEqual(item3.GoodsItem.DishComposition.Rid, (uint)192);
            Assert.AreEqual(item3.GoodsItem.DishComposition.Name, "Кассовая лента термо-80мм (21м)");
            Assert.IsNotNull(item3.GoodsItem.DishComposition.DishCompositionVersion);
            Assert.AreEqual(item3.GoodsItem.DishComposition.DishCompositionVersion.Rid, (uint)192);
            Assert.IsNull(item3.GoodsItem.DishComposition.DishCompositionVersion.Name);
            Assert.AreEqual(item3.GoodsItem.DishComposition.DishCompositionVersion.Version, (ushort)0);

            Assert.AreEqual(item3.Options, (uint)1);
            Assert.AreEqual(item3.Quantity, 3m);
            Assert.IsNull(item3.AmountWeighed);
            Assert.AreEqual(item3.Attributes6.Count, 6);
        }
    }
}