using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace SH5ApiClient.Core.Requests.Tests
{
    [TestClass()]
    public class CorrsRequestTests
    {
        [TestMethod()]
        public void CreateJsonRequestTest()
        {
            CorrsRequest corrsRequest = new(Options.connectionParamSH5);
            string actual = File.ReadAllText(@"..\..\..\Core\Requests\DataForTests\CorrsRequest.json");
            string expected = corrsRequest.CreateJsonRequest();
            Assert.AreEqual(expected, actual);
        }
    }
}