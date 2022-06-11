using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models.Enums;
using System.IO;
using System.Linq;
using System.Text;

namespace SH5ApiClient.Models.DTO.Tests
{
    [TestClass()]
    public class СorrespondentTests
    {
        [TestMethod()]
        public void ParseTest()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\Correspondents.json", Encoding.UTF8);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            ExecOperationContent content = answear.GetAnswearContent("107");
            var coors = Сorrespondent.GetСorrespondentsFromSHAnswear(content).ToList();

            var cor0 = coors[0];
            var cor2 = coors[2];

            Assert.AreEqual(7, coors.Count);

            Assert.AreEqual(6, cor0.Rid);
            Assert.AreEqual("D660060C-13F8-7321-C562-AD2DEA7B0BCF", cor0.GUID);
            Assert.AreEqual(null, cor0.SubType);
            Assert.AreEqual(CorrTypeEx.Special, cor0.CorrTypeEx);
            Assert.AreEqual(CorrType.Selling, cor0.CorrType);
            Assert.AreEqual("Спец. корреспондент 7", cor0.Name);
            Assert.AreEqual("1231231231", cor0.INN);
            Assert.AreEqual(8, cor0.Attributes34.Count);
            Assert.AreEqual(null, cor0.Attributes34["Accountant"]);
            Assert.AreEqual(null, cor0.Attributes34["Manager"]);
            Assert.AreEqual(null, cor0.Attributes34["OKPO"]);
            Assert.AreEqual(null, cor0.Attributes34["RAddr"]);
            Assert.AreEqual(null, cor0.Attributes34["CAcc"]);
            Assert.AreEqual(null, cor0.Attributes34["PAcc"]);
            Assert.AreEqual(null, cor0.Attributes34["Bank"]);
            Assert.AreEqual(null, cor0.Attributes34["BIK"]);


            Assert.AreEqual(4, cor2.Rid);
            Assert.AreEqual("BA8E82DD-0690-B0F9-658F-5B803C59FE99", cor2.GUID);
            Assert.AreEqual(null, cor2.SubType);
            Assert.AreEqual(CorrTypeEx.Organization, cor2.CorrTypeEx);
            Assert.AreEqual(CorrType.InternalCorrespondent, cor2.CorrType);
            Assert.AreEqual("Внутренний контрагент 5", cor2.Name);
            Assert.AreEqual("2222222222", cor2.INN);
            Assert.AreEqual(8, cor2.Attributes34.Count);
            Assert.AreEqual("Гл Бухгалтер5", cor2.Attributes34["Accountant"]);
            Assert.AreEqual("Ген Директор5", cor2.Attributes34["Manager"]);
            Assert.AreEqual("ОКПО5", cor2.Attributes34["OKPO"]);
            Assert.AreEqual("Юр адрес5", cor2.Attributes34["RAddr"]);
            Assert.AreEqual("Кор счет5", cor2.Attributes34["CAcc"]);
            Assert.AreEqual("Расчетный счет5", cor2.Attributes34["PAcc"]);
            Assert.AreEqual("Банк5", cor2.Attributes34["Bank"]);
            Assert.AreEqual("Бик5", cor2.Attributes34["BIK"]);
        }
    }
}