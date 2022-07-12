using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.ServerOperations;
using System.IO;
using System.Linq;
using System.Text;

namespace SH5ApiClient.Models.DTO.Tests
{
    [TestClass()]
    public class GGroupsTests
    {
        [TestMethod()]
        public void ParseGGroupsTest()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\GGroups.json", Encoding.UTF8);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            var groups = GGroup.ParseGGroups(answear);

            Assert.IsNotNull(groups);
            Assert.AreEqual(40, groups.Count());
            Assert.AreEqual(1, groups.Count(t => t?.Parent?.Rid is null));
            Assert.AreEqual(8, groups.Count(t => t?.Parent?.Name == "Кухня"));
            Assert.AreEqual(6, groups.Count(t => t?.Parent?.Name == "Меню ресторана"));
            Assert.AreEqual(4, groups.Count(t => t?.Parent?.Name == "Бар"));
            Assert.AreEqual(1, groups.Count(t => t?.Parent?.Name == "Пиво"));
            Assert.AreEqual(1, groups.Count(t => t?.Parent?.Name == "Кофе"));
            Assert.AreEqual(1, groups.Count(t => t?.Parent?.Name == "Посуда"));
            Assert.AreEqual(14, groups.Count(t => t?.Parent?.Name == "Товары от поставщиков (Продукты)"));
            Assert.AreEqual(4, groups.Count(t => t?.Parent?.Name == "Товарные группы"));

            var item1 = groups.ElementAt(0);
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.Rid, (uint)44);
            Assert.AreEqual(item1.GUID, "808C377E-2324-8102-A319-816F572B7229");
            Assert.AreEqual(item1.Name, "Супы");
            Assert.IsNotNull(item1.Parent);
            Assert.AreEqual(item1.Parent.Rid, (uint)12);
            Assert.AreEqual(item1.Parent.GUID, "C9CA537A-805E-0B83-182A-14CF8AE406DD");
            Assert.AreEqual(item1.Parent.Name, "Кухня");
            Assert.IsNotNull(item1?.Parent?.Parent?.Parent?.Parent);



            var item2 = groups.ElementAt(6);
            Assert.IsNotNull(item2);
            Assert.AreEqual(item2.Rid, (uint)39);
            Assert.AreEqual(item2.GUID, "39250389-49E8-26C8-E0B6-DB55D60014C7");
            Assert.AreEqual(item2.Name, "Напитки");
            Assert.IsNotNull(item2.Parent);
            Assert.AreEqual(item2.Parent.Rid, (uint)3);
            Assert.AreEqual(item2.Parent.GUID, "ED156383-12FE-FB54-4F0E-D6A28CA87B8A");
            Assert.AreEqual(item2.Parent.Name, "Товары от поставщиков (Продукты)");
            Assert.IsNotNull(item2?.Parent?.Parent?.Parent);
        }
    }
}