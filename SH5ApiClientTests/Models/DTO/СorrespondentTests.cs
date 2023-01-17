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
        public void ParseCorrespondentsTest()
        {
            var corrs = Options.ApiClient.LoadCorrespondentsAsync().Result;

            var cor0 = corrs.ElementAt(0);
            var cor2 = corrs.ElementAt(2);

            Assert.AreEqual(7, corrs.Count());

            Assert.AreEqual((uint)6, cor0.Rid);
            Assert.AreEqual("{D660060C-13F8-7321-C562-AD2DEA7B0BCF}", cor0.GUID);
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


            Assert.AreEqual((uint)4, cor2.Rid);
            Assert.AreEqual("{BA8E82DD-0690-B0F9-658F-5B803C59FE99}", cor2.GUID);
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
        [TestMethod()]
        public void ParseInternalCorrespondentsTest()
        {
            var corrs = (InternalСorrespondents)Options.ApiClient.LoadInternalCorrespondentsAsync().Result;

            Assert.AreEqual(1, corrs.Count());
            
            Assert.AreEqual((uint)65534, corrs.InnerСorrespondent.MaxCount);
            Assert.AreEqual((uint)0, corrs.InnerСorrespondent.HiddenCount);

            var cor = corrs.First();

            Assert.AreEqual((uint)0, cor.Rid);
            Assert.AreEqual("{605FB3DC-05E7-CF08-A6D5-38AEF2E76216}", cor.GUID);
            Assert.AreEqual((ushort)14, cor.PaymentIncomeSpan);
            Assert.AreEqual((ushort)15, cor.PaymentExpenseSpan);
            Assert.AreEqual("Юридицеское лицо", cor.Name);
            Assert.AreEqual("123123123123", cor.INN);
            Assert.AreEqual("Зав производстом", cor.Attributes7["PrSupervisor"]);
            Assert.AreEqual("Гл бух", cor.Attributes7["Accountant"]);
            Assert.AreEqual("Ген директор", cor.Attributes7["Manager"]);
            Assert.AreEqual("ОКПО", cor.Attributes7["OKPO"]);
            Assert.AreEqual("Физический адрес", cor.Attributes7["PAddr"]);
            Assert.AreEqual("Юридисечкий адрес", cor.Attributes7["RAddr"]);
            Assert.AreEqual("Кос счет", cor.Attributes7["CAcc"]);
            Assert.AreEqual("Расчетный счет", cor.Attributes7["PAcc"]);
            Assert.AreEqual("Банк", cor.Attributes7["Bank"]);
            Assert.AreEqual("Бик", cor.Attributes7["BIK"]);
            Assert.AreEqual("Полное наименование", cor.Attributes7["FullName"]);
            Assert.AreEqual("ПрефД", cor.Attributes7["$ContractNum"]);
            Assert.AreEqual("ПрефП", cor.Attributes7["$PriceLstNum"]);
            Assert.AreEqual("ПрефПД", cor.Attributes7["$PDocNum"]);
            Assert.AreEqual("ПерфСФ", cor.Attributes7["$IDocNum"]);
            Assert.AreEqual("ПрефН", cor.Attributes7["$GDocNum"]);
        }
    }
}