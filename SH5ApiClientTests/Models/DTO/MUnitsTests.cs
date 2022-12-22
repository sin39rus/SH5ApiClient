using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SH5ApiClient.Models.DTO.Tests
{
    [TestClass()]
    public class MUnitsTests
    {
        [TestMethod()]
        public void ParseMUnitsTests()
        {
            var result = Options.ApiClient.LoadMeasureUnitsAsync().Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 16);
            Assert.AreEqual(result.Count(t => t.IsBase is not null && t.IsBase.Value), 4);
            Assert.AreEqual(result.Count(t => t.MeasureGroup is not null && t.MeasureGroup.Name == "Штучные"), 11);
            Assert.AreEqual(result.Count(t => t.MeasureGroup is not null && t.MeasureGroup.Name == "Весовые"), 2);
            Assert.AreEqual(result.Count(t => t.MeasureGroup is not null && t.MeasureGroup.Name == "Объемые"), 2);
            Assert.AreEqual(result.Count(t => t.MeasureGroup is not null && t.MeasureGroup.Name == "Порционные"), 1);

            var item1 = result.ElementAt(0);
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.Rid, (uint)16);
            Assert.AreEqual(item1.GUID, "{582A0A8F-BDA2-5212-2CF7-CF8D7CF063EB}");
            Assert.AreEqual(item1.BaseRatio, 1.0m);
            Assert.IsNotNull(item1.MeasureGroup);
            Assert.AreEqual(item1.MeasureGroup.Rid, (uint)3);
            Assert.AreEqual(item1.MeasureGroup.Name, "Штучные");
            Assert.AreEqual(item1.Name, "ч.");
            Assert.IsTrue(item1.Attributes7.ContainsKey("OKEI"));
            Assert.AreEqual(item1.Attributes7["OKEI"], null);
            Assert.IsFalse(item1.IsBase);
            Assert.AreEqual(item1.Flags, (byte)0);


        }
    }
}
