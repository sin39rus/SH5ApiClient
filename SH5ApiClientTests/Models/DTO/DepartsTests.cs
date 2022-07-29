using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models.Enums;
using System.IO;
using System.Linq;
using System.Text;

namespace SH5ApiClient.Models.DTO.Tests
{
    [TestClass()]
    public class DepartsTests
    {
        [TestMethod()]
        public void ParseDepartsTest()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\Departs.json", Encoding.UTF8);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            ExecOperationContent content = answear.GetAnswearContent("106");
            var departs = Depart.GetDepartsFromSHAnswear(content);

            Assert.AreEqual(2, departs.Count());

            var dep1 = departs.ElementAt(0);
            var dep2 = departs.ElementAt(1);

            Assert.IsNotNull(dep1);
            Assert.IsNotNull(dep2);


            Assert.AreEqual((uint)8388608, dep1.Rid);
            Assert.AreEqual("31ADB89D-1C0F-B3E3-6DA0-F4535DCED25E", dep1.Guid);
            Assert.AreEqual("Склад 2", dep1.Name);
            Assert.AreEqual(DepatmenType.Warehouse, dep1.DepatmenType);
            Assert.IsNotNull(dep1.LegalEntity);
            Assert.AreEqual((uint)0, dep1.LegalEntity.Rid);
            Assert.AreEqual("Юридицеское лицо", dep1.LegalEntity.Name);
            Assert.AreEqual("123123123123", dep1.LegalEntity.INN);
            Assert.IsNotNull(dep1.Company);
            Assert.AreEqual((uint)0, dep1.Company.Rid);
            Assert.AreEqual("Наименование предприятия 1", dep1.Company.Name);


            Assert.AreEqual((uint)4194304, dep2.Rid);
            Assert.AreEqual("4B506473-BBED-0168-3416-8AE60ACEEE3F", dep2.Guid);
            Assert.AreEqual("Склад 1", dep2.Name);
            Assert.AreEqual(DepatmenType.Warehouse | DepatmenType.Trade | DepatmenType.Production, dep2.DepatmenType);
            Assert.IsNotNull(dep2.LegalEntity);
            Assert.AreEqual((uint)0, dep2.LegalEntity.Rid);
            Assert.AreEqual("Юридицеское лицо", dep2.LegalEntity.Name);
            Assert.AreEqual("123123123123", dep2.LegalEntity.INN);
            Assert.IsNotNull(dep2.Company);
            Assert.AreEqual((uint)0, dep2.Company.Rid);
            Assert.AreEqual("Наименование предприятия 1", dep2.Company.Name);
        }
    }
}