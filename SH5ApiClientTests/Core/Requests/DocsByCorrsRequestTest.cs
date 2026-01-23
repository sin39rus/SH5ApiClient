using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Core.Requests.Tests
{
    [TestClass()]
    public class DocsByCorrsRequestTest
    {
        [TestMethod()]
        public void Create()
        {
            DocsByCorrsRequest request = new DocsByCorrsRequest(Options.connectionParamSH5, DateTime.Now, DateTime.Now, 1);
            var json = request.CreateJsonRequest();
        }
    }
}
