using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models.Enums;
using System;
using System.IO;
using System.Text;
using System.Linq;

namespace SH5ApiClient.Models.DTO.Tests
{
    [TestClass()]
    public class GDoc11Tests
    {
        [TestMethod()]
        public void ParseGDoc11Test()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\Gdoc11.json", Encoding.UTF8);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            var gDoc11 = GDoc11.Parse(answear);
            Assert.IsNotNull(gDoc11);
            Assert.IsNotNull(gDoc11.Header);
            Assert.IsNotNull(gDoc11.Content);

            var header = gDoc11.Header;
            Assert.AreEqual(header.Rid, (uint)10);
            Assert.AreEqual(header.GUID, "B32CCD68-8979-0177-BAB3-68ED6C31E5AA");
            Assert.AreEqual(header.TTNOptions, TTNOptions.Active | TTNOptions.ActivatedByCntr1 | TTNOptions.ActivatedByCntr0);
            Assert.AreEqual(header.DateStamp, new DateTime(2022, 7, 11));
            Assert.IsNotNull(header.BuhOperation);
            Assert.AreEqual(header.BuhOperation.Rid, 0);
            Assert.AreEqual(header.BuhOperation.Name, "Бух операция 1");

            Assert.IsNotNull(header.Supplier);
            Assert.AreEqual(header.Supplier.Rid, (uint)4194304);
            Assert.AreEqual(header.Supplier.Name, "Склад 1");
            Assert.AreEqual(header.Supplier.SubType, CorrType3.AlcoholProducer);
            Assert.IsNotNull(header.Supplier.CorrespondentSpecification);
            Assert.AreEqual(header.Supplier.CorrespondentSpecification.Rid, 1);
            Assert.AreEqual(header.Supplier.CorrespondentSpecification.Name, "666678678");

            Assert.IsNotNull(header.Recipient);
            Assert.AreEqual(header.Recipient.Rid, (uint)8388608);
            Assert.AreEqual(header.Recipient.Name, "Склад 2");
            Assert.AreEqual(header.Recipient.SubType, CorrType3.AlcoholProducer);
            Assert.IsNotNull(header.Recipient.CorrespondentSpecification);
            Assert.IsNull(header.Recipient.CorrespondentSpecification.Rid);
            Assert.IsNull(header.Recipient.CorrespondentSpecification.Name);

            Assert.AreEqual(header.Name, "123");
            Assert.IsTrue(header.Attributes6.ContainsKey("DocType1C"));
            Assert.IsTrue(header.Attributes6.ContainsKey("DocType1C_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("TTN_number"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Comment"));
            Assert.AreEqual(header.Attributes6["DocType1C"], "2");
            Assert.AreEqual(header.Attributes6["DocType1C_itext_"], "Списание");
            Assert.AreEqual(header.Attributes6["TTN_number"], "Номер ТТН");
            Assert.AreEqual(header.Attributes6["Comment"], "Примечание");

            Assert.IsTrue(header.Attributes7.ContainsKey("PersonAccountable"));
            Assert.IsTrue(header.Attributes7.ContainsKey("SKeeper1"));
            Assert.IsTrue(header.Attributes7.ContainsKey("SKeeper0"));
            Assert.IsTrue(header.Attributes7.ContainsKey("PersonInCharge"));
            Assert.AreEqual(header.Attributes7["PersonAccountable"], "Подотчетное лицо");
            Assert.AreEqual(header.Attributes7["SKeeper1"], "Получил");
            Assert.AreEqual(header.Attributes7["SKeeper0"], "Отпустил");
            Assert.AreEqual(header.Attributes7["PersonInCharge"], "Ответственное лицо");


            Assert.AreEqual(header.MinActiveDate, new DateTime(2022, 7, 11));

            Assert.IsNotNull(header.Creator);
            Assert.AreEqual(header.Creator.Name, "Admin");
            Assert.AreEqual(header.Creator.Rid, (uint)0);
            Assert.AreEqual(header.Creator.Date, new DateTime(2022, 07, 11));
            Assert.AreEqual(header.Creator.Time, (uint)36017);

            Assert.IsNotNull(header.LastUpdater);
            Assert.AreEqual(header.LastUpdater.Name, "Admin");
            Assert.AreEqual(header.LastUpdater.Rid, (uint)0);
            Assert.AreEqual(header.LastUpdater.Date, new DateTime(2022, 07, 11));
            Assert.AreEqual(header.LastUpdater.Time, (uint)36850);


            var content = gDoc11.Content;
            Assert.AreEqual(content.Count(), 2);

            var item1 = content.ElementAt(0);
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.Rid, (uint)17);
            Assert.IsNotNull(item1.GoodsItem);
            Assert.AreEqual(item1.GoodsItem.Rid, (uint)3);
            Assert.AreEqual(item1.GoodsItem.Name, "Товар №1");
            Assert.IsTrue(item1.GoodsItem.Attributes6.ContainsKey("SDInd"));
            Assert.AreEqual(item1.GoodsItem.Attributes6["SDInd"], null);
            Assert.IsNotNull(item1.GoodsItem.MeasureUnit);
            Assert.AreEqual(item1.GoodsItem.MeasureUnit.Rid, (uint)5);
            Assert.AreEqual(item1.GoodsItem.MeasureUnit.Name, "шт.");
            Assert.AreEqual(item1.Currency67, 1.5m);
            Assert.AreEqual(item1.Currency68, 681.825m);
            Assert.AreEqual(item1.Currency69, 68.175m);
            Assert.AreEqual(item1.Currency70, 0);
            Assert.AreEqual(item1.Currency40, 681.825m);
            Assert.AreEqual(item1.Currency41, 68.175m);
            Assert.AreEqual(item1.Currency42, 0);
            Assert.AreEqual(item1.Options, (uint)1);
            Assert.AreEqual(item1.Quantity, 1.5m);
            Assert.IsNull(item1.AmountWeighed);
            Assert.IsTrue(item1.Attributes6.ContainsKey("ExpDate"));
            Assert.AreEqual(item1.Attributes6["ExpDate"], "2022-07-12");

            var item2 = content.ElementAt(1);
            Assert.IsNotNull(item2);
            Assert.AreEqual(item2.Rid, (uint)19);
            Assert.IsNotNull(item2.GoodsItem);
            Assert.AreEqual(item2.GoodsItem.Rid, (uint)4);
            Assert.AreEqual(item2.GoodsItem.Name, "Товар №2");
            Assert.IsTrue(item2.GoodsItem.Attributes6.ContainsKey("SDInd"));
            Assert.AreEqual(item2.GoodsItem.Attributes6["SDInd"], null);
            Assert.IsNotNull(item2.GoodsItem.MeasureUnit);
            Assert.AreEqual(item2.GoodsItem.MeasureUnit.Rid, (uint)4);
            Assert.AreEqual(item2.GoodsItem.MeasureUnit.Name, "Литр");
            Assert.AreEqual(item2.Currency67, 0);
            Assert.AreEqual(item2.Currency68, 0);
            Assert.AreEqual(item2.Currency69, 0);
            Assert.AreEqual(item2.Currency70, 0);
            Assert.AreEqual(item2.Currency40, 178.0m);
            Assert.AreEqual(item2.Currency41, 0);
            Assert.AreEqual(item2.Currency42, 20.0m);
            Assert.AreEqual(item2.Options, (uint)1);
            Assert.AreEqual(item2.Quantity, 1.0m);
            Assert.IsNull(item2.AmountWeighed);
            Assert.IsTrue(item2.Attributes6.ContainsKey("ExpDate"));
            Assert.AreEqual(item2.Attributes6["ExpDate"], null);
        }
    }
}