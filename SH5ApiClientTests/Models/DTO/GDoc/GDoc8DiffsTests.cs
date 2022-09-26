using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.ServerOperations;
using System.IO;
using System.Linq;
using System.Text;

namespace SH5ApiClient.Models.DTO.Tests
{
    [TestClass()]
    public class GDoc8DiffsTests
    {
        [TestMethod()]
        public void ParseGDoc8DiffsTest()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\GDoc8Diffs.json", Encoding.UTF8);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            var gDoc8Diffs = GDoc8Diffs.Parse(answear);
            Assert.IsNotNull(gDoc8Diffs);
            Assert.IsNotNull(gDoc8Diffs.Content);

            Assert.AreEqual(gDoc8Diffs.Content.Count(), 2);

            var item1 = gDoc8Diffs.Content.ElementAt(0);
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.Rid, (uint)114381);
            Assert.IsNotNull(item1.GoodsItem);
            Assert.AreEqual(item1.GoodsItem.Rid, (uint)5266);
            Assert.AreEqual(item1.GoodsItem.Name, "Кассовая лента термо-80мм (21м)");
            Assert.AreEqual(item1.GoodsItem.Attributes6.Count, 21);
            foreach (var attribute in item1.GoodsItem.Attributes6)
                Assert.IsNull(attribute.Value);
            Assert.IsNotNull(item1.GoodsItem.BaseMeasureUnit);
            Assert.AreEqual(item1.GoodsItem.BaseMeasureUnit.Rid, (uint)5);
            Assert.AreEqual(item1.GoodsItem.BaseMeasureUnit.Name, "шт");
            Assert.IsNotNull(item1.GoodsItem.Producer);
            Assert.IsNull(item1.GoodsItem.Producer.Rid);
            Assert.IsNull(item1.GoodsItem.Producer.Name);
            Assert.IsNotNull(item1.GoodsItem.Producer.Сorrespondent2);
            Assert.IsNull(item1.GoodsItem.Producer.Сorrespondent2.Rid);
            Assert.IsNull(item1.GoodsItem.Producer.Сorrespondent2.Name);
            Assert.IsNotNull(item1.GoodsItem.AlcoholProductType);
            Assert.IsNull(item1.GoodsItem.AlcoholProductType.Rid);
            Assert.IsNull(item1.GoodsItem.AlcoholProductType.Flags);
            Assert.IsNull(item1.GoodsItem.AlcoholProductType.Code);
            Assert.IsNull(item1.GoodsItem.AlcoholProductType.Name);

            Assert.AreEqual(item1.Currency55, -3142m);
            Assert.AreEqual(item1.Currency50, -116254m);
            Assert.AreEqual(item1.Currency51, 0m);
            Assert.AreEqual(item1.Currency52, 0m);

            Assert.AreEqual(item1.Quantity, 3192m);
            Assert.AreEqual(item1.Currency40, 118104m);
            Assert.AreEqual(item1.Currency41, 0m);
            Assert.AreEqual(item1.Currency42, 0m);

            Assert.IsNull(item1.Currency57);
            Assert.IsNotNull(item1.PurchaseNDS);
            Assert.IsNull(item1.PurchaseNDS.Rate);
            Assert.IsNotNull(item1.PurchaseNSP);
            Assert.IsNull(item1.PurchaseNSP.Rate);

            var item2 = gDoc8Diffs.Content.ElementAt(1);
            Assert.IsNotNull(item2);
            Assert.AreEqual(item2.Rid, (uint)114382);
            Assert.IsNotNull(item2.GoodsItem);
            Assert.AreEqual(item2.GoodsItem.Rid, (uint)2418);
            Assert.AreEqual(item2.GoodsItem.Name, "Кассовая лента термо-80мм (72м)");
            Assert.AreEqual(item2.GoodsItem.Attributes6.Count, 21);
            foreach (var attribute in item2.GoodsItem.Attributes6)
                Assert.IsNull(attribute.Value);
            Assert.IsNotNull(item2.GoodsItem.BaseMeasureUnit);
            Assert.AreEqual(item2.GoodsItem.BaseMeasureUnit.Rid, (uint)5);
            Assert.AreEqual(item2.GoodsItem.BaseMeasureUnit.Name, "шт");
            Assert.IsNotNull(item2.GoodsItem.Producer);
            Assert.IsNull(item2.GoodsItem.Producer.Rid);
            Assert.IsNull(item2.GoodsItem.Producer.Name);
            Assert.IsNotNull(item2.GoodsItem.Producer.Сorrespondent2);
            Assert.IsNull(item2.GoodsItem.Producer.Сorrespondent2.Rid);
            Assert.IsNull(item2.GoodsItem.Producer.Сorrespondent2.Name);
            Assert.IsNotNull(item2.GoodsItem.AlcoholProductType);
            Assert.IsNull(item2.GoodsItem.AlcoholProductType.Rid);
            Assert.IsNull(item2.GoodsItem.AlcoholProductType.Flags);
            Assert.IsNull(item2.GoodsItem.AlcoholProductType.Code);
            Assert.IsNull(item2.GoodsItem.AlcoholProductType.Name);

            Assert.AreEqual(item2.Currency55, 12m);
            Assert.AreEqual(item2.Currency50, 1140m);
            Assert.AreEqual(item2.Currency51, 0m);
            Assert.AreEqual(item2.Currency52, 0m);

            Assert.AreEqual(item2.Quantity, 288m);
            Assert.AreEqual(item2.Currency40, 27360m);
            Assert.AreEqual(item2.Currency41, 0m);
            Assert.AreEqual(item2.Currency42, 0m);

            Assert.IsNull(item2.Currency57);
            Assert.IsNotNull(item2.PurchaseNDS);
            Assert.IsNull(item2.PurchaseNDS.Rate);
            Assert.IsNotNull(item2.PurchaseNSP);
            Assert.IsNull(item2.PurchaseNSP.Rate);
        }
    }
}