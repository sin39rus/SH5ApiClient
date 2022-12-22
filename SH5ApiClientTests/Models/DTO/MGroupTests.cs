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
    public class MGroupTests
    {
        [TestMethod()]
        public void ParseMGroupTests()
        {
            var result = Options.ApiClient.GetMeasureGroupAsync(1).Result;

            Assert.IsNotNull(result);

            Assert.AreEqual(result.Rid, (uint)1);
            Assert.AreEqual(result.Name, "Весовые");
            Assert.IsNull(result.BaseMeasureUnit);
        }
    }
}
