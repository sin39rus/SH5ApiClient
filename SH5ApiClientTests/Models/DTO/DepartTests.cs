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
            Assert.IsNotNull(dep.KPPs);
            dep.AloLicInfos = AloLicInfo.GetAloLicInfosFromSHAnswear(answear.GetAnswearContent("115"));
            Assert.IsNotNull(dep.AloLicInfos);

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


            Assert.AreEqual(2, dep.KPPs.Count());
            var kpp1 = dep.KPPs.ElementAt(0);
            var kpp2 = dep.KPPs.ElementAt(1);


            Assert.IsNotNull(kpp1);
            Assert.IsNotNull(kpp2);

            Assert.AreEqual((uint)6, kpp1.Rid);
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
            
            Assert.AreEqual((uint)1, kpp2.Rid);
            Assert.IsFalse(kpp2.IsDefault);
            Assert.IsNotNull(kpp2.Region);
            Assert.AreEqual((uint)44, kpp2.Region.Rid);
            Assert.AreEqual("666678678", kpp2.Name);
            Assert.AreEqual("Вид деятельности", kpp2.Attributes6["LicActType"]);
            Assert.AreEqual(null, kpp2.Attributes7["EMail"]);
            Assert.AreEqual(null, kpp2.Attributes7["Phone"]);
            Assert.AreEqual(null, kpp2.Attributes7["PAddr"]);
            Assert.AreEqual(null, kpp2.Attributes7["FullName"]);
            Assert.AreEqual("123123123123", kpp2.ExtCode);
            Assert.AreEqual(null, kpp2.Attributes35["Port"]);
            Assert.AreEqual(null, kpp2.Attributes35["Host"]);


            Assert.AreEqual(2, dep.AloLicInfos.Count());
            var alcInfo1 = dep.AloLicInfos.ElementAt(0);
            var alcInfo2 = dep.AloLicInfos.ElementAt(1);


            Assert.AreEqual((uint)2, alcInfo1.Rid);
            Assert.AreEqual(new System.DateTime(2022, 7, 25), alcInfo1.From);
            Assert.AreEqual(new System.DateTime(2022, 7, 29), alcInfo1.To);
            Assert.AreEqual("Номер лиц", alcInfo1.LicNum);
            Assert.AreEqual("Кем выдана лиц", alcInfo1.Attributes6["LicDep"]);
            Assert.AreEqual((uint)1, alcInfo1?.KPP?.Rid);
            
            Assert.AreEqual((uint)1, alcInfo2.Rid);
            Assert.AreEqual(new System.DateTime(2022, 6, 1), alcInfo2.From);
            Assert.AreEqual(new System.DateTime(2022, 6, 30), alcInfo2.To);
            Assert.AreEqual("Номер лиц", alcInfo2.LicNum);
            Assert.AreEqual("Кем выдана", alcInfo2.Attributes6["LicDep"]);
            Assert.AreEqual((uint)1, alcInfo2?.KPP?.Rid);
        }
    }
}