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
    public class CurrenciesTests
    {
        [TestMethod()]
        public void ParseCurrenciesTests()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\Currencies.json", Encoding.UTF8);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            ExecOperationContent content = answear.GetAnswearContent("100");
            var result = Currency.GetCurrenciesFromSHAnswear(content);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);

            Assert.AreEqual(result.ElementAt(0).Rid, (uint)1);
            Assert.AreEqual(result.ElementAt(0).Code, "у.е.");
            Assert.AreEqual(result.ElementAt(0).Name, "у.е.");
            Assert.AreEqual(result.ElementAt(0).GUID, "172E7210-C7CF-2671-AF87-7840C144651F");
            Assert.IsNotNull(result.ElementAt(0).Attributes7);
            Assert.AreEqual(result.ElementAt(0).Attributes7["PrnCode"], null);
            Assert.AreEqual(result.ElementAt(0).Attributes7["ISO4217"], null);

            Assert.AreEqual(result.ElementAt(1).Rid, (uint)0);
            Assert.AreEqual(result.ElementAt(1).Code, "руб");
            Assert.AreEqual(result.ElementAt(1).Name, "Рубли");
            Assert.AreEqual(result.ElementAt(1).GUID, "71DFCCE9-4BE7-DC03-4526-2D18164F6BC8");
            Assert.IsNotNull(result.ElementAt(1).Attributes7);
            Assert.AreEqual(result.ElementAt(1).Attributes7["PrnCode"], null);
            Assert.AreEqual(result.ElementAt(1).Attributes7["ISO4217"], "643");
        }
    }
}
