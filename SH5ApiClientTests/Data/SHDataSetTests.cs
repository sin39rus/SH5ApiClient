using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SH5ApiClient.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Data.Tests
{
    [TestClass()]
    public class SHDataSetTests
    {
        [TestMethod()]
        public void ParseFromJsonTest()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\Gdoc4.json", Encoding.UTF8);
            DataSet result = DataSet.ParseFromJson(jsonAnswear);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Tables.Count, 2);
            Assert.IsNotNull(result.Tables["112"]);
            Assert.IsNotNull(result.Tables["111"]);

            Assert.AreEqual(result.Tables["111"]?.Columns.Count, 50);
            Assert.AreEqual(result.Tables["112"]?.Columns.Count, 26);

            Assert.AreEqual(result.Tables["111"]?.Rows.Count, 1);
            Assert.AreEqual(result.Tables["112"]?.Rows.Count, 2);
        }

        [TestMethod()]
        public void ToJsonTest()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\Gdoc4.json", Encoding.UTF8);
            string actual = JsonConvert.DeserializeObject(jsonAnswear)?.ToString() ?? throw new AssertFailedException();
            DataSet dataSet = DataSet.ParseFromJson(jsonAnswear);

            JProperty result = dataSet.ToJson();
            string expected = new JObject(
                new JProperty("errorCode", 0),
                new JProperty("errMessage", "OK"),
                new JProperty("Version", "1.12"),
                new JProperty("UserName", "Admin"),
                new JProperty("actionName", "GDoc4"),
                new JProperty("actionType", "Execute"),
                result
                ).ToString();
            Assert.AreEqual(expected, actual);
        }
    }

}