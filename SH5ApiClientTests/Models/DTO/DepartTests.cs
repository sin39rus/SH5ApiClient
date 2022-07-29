using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models.Enums;
using System.IO;
using System.Linq;
using System.Text;

namespace SH5ApiClient.Models.DTO.Tests
{
    [TestClass()]
    public class DepartTests
    {
        [TestMethod()]
        public void ParseDepartTest()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\Depart.json", Encoding.UTF8);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            ExecOperationContent content = answear.GetAnswearContent("106");
            var dep = Depart.Parse(content.GetValues()[0]);
            Assert.IsNotNull(dep);

            dep.KPPs = KPP.GetKPPsFromSHAnswear(answear.GetAnswearContent("114"));

            Assert.AreEqual((uint)4194304, dep.Rid);
            Assert.AreEqual("4B506473-BBED-0168-3416-8AE60ACEEE3F", dep.Guid);
            Assert.AreEqual("Склад 1", dep.Name);
            Assert.AreEqual(DepatmenType.Warehouse | DepatmenType.Trade | DepatmenType.Production, dep.DepatmenType);
            Assert.IsNotNull(dep.LegalEntity);
            Assert.AreEqual((uint)0, dep.LegalEntity.Rid);
            Assert.AreEqual("Юридицеское лицо", dep.LegalEntity.Name);
            Assert.AreEqual("123123123123", dep.LegalEntity.INN);
            Assert.IsNotNull(dep.Company);
            Assert.AreEqual((uint)0, dep.Company.Rid);
            Assert.AreEqual("Наименование предприятия 1", dep.Company.Name);


            Assert.IsNotNull(dep.KPPs);
            Assert.AreEqual(2, dep.KPPs.Count());
            var kpp1 = dep.KPPs.ElementAt(0);

            Assert.IsNotNull(kpp1);

            Assert.AreEqual((uint)6, kpp1?.Rid);
            Assert.IsTrue(kpp1.IsDefault);
            Assert.IsNotNull(kpp1.Region);
            Assert.AreEqual((uint)39, kpp1.Region.Rid);
            Assert.AreEqual("777777777", kpp1.Name);
            Assert.AreEqual("Вид деятельности2", kpp1.Attributes6["LicActType"]);
            Assert.AreEqual(null, kpp1.Attributes7["EMail"]);
            Assert.AreEqual(null, kpp1.Attributes7["Phone"]);
            Assert.AreEqual(null, kpp1.Attributes7["PAddr"]);
            Assert.AreEqual(null, kpp1.Attributes7["FullName"]);
            Assert.AreEqual("555555555555", kpp1.ExtCode);
            Assert.AreEqual(null, kpp1.Attributes35["Port"]);
            Assert.AreEqual(null, kpp1.Attributes35["Host"]);
        }
    }
}