using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Models.DTO.Tests
{
    [TestClass()]
    public class MGroupsTests
    {
        [TestMethod()]
        public void ParseMGroupsTests()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\MGroups.json", Encoding.UTF8);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            ExecOperationContent content = answear.GetAnswearContent("205");
            var result = MeasureGroup.GetMGroupsFromSHAnswear(content);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 4);

            Assert.AreEqual(result.ElementAt(0).Rid, (uint)1);
            Assert.AreEqual(result.ElementAt(0).Name, "Весовые");
            Assert.IsNotNull(result.ElementAt(0).BaseMeasureUnit);
            Assert.AreEqual(result.ElementAt(0).BaseMeasureUnit?.Rid, (uint)1);
            Assert.AreEqual(result.ElementAt(0).BaseMeasureUnit?.Name, "кг.");

            Assert.AreEqual(result.ElementAt(1).Rid, (uint)2);
            Assert.AreEqual(result.ElementAt(1).Name, "Объемые");
            Assert.IsNotNull(result.ElementAt(1).BaseMeasureUnit);
            Assert.AreEqual(result.ElementAt(1).BaseMeasureUnit?.Rid, (uint)3);
            Assert.AreEqual(result.ElementAt(1).BaseMeasureUnit?.Name, "л");

            Assert.AreEqual(result.ElementAt(2).Rid, (uint)3);
            Assert.AreEqual(result.ElementAt(2).Name, "Штучные");
            Assert.IsNotNull(result.ElementAt(2).BaseMeasureUnit);
            Assert.AreEqual(result.ElementAt(2).BaseMeasureUnit?.Rid, (uint)5);
            Assert.AreEqual(result.ElementAt(2).BaseMeasureUnit?.Name, "шт");

            Assert.AreEqual(result.ElementAt(3).Rid, (uint)4);
            Assert.AreEqual(result.ElementAt(3).Name, "Порционные");
            Assert.IsNotNull(result.ElementAt(3).BaseMeasureUnit);
            Assert.AreEqual(result.ElementAt(3).BaseMeasureUnit?.Rid, (uint)6);
            Assert.AreEqual(result.ElementAt(3).BaseMeasureUnit?.Name, "порция");
        }
    }
}
