using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Data;
using SH5ApiClient.Models.Enums;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SH5ApiClient.Models.DTO.Tests
{
    [TestClass()]
    public class GDoc10Tests
    {
        [TestMethod()]
        public void ParseGDoc10Test()
        {
            string jsonAnswear = File.ReadAllText(@"..\..\..\Models\DataForTests\Gdoc10.json", Encoding.UTF8);
            var gDoc10 = DataExecutable.Parse<GDoc10>(jsonAnswear);
            Assert.IsNotNull(gDoc10);
            Assert.IsNotNull(gDoc10.Header);
            Assert.IsNotNull(gDoc10.Content);

            var header = gDoc10.Header;
            Assert.AreEqual(header.Rid, (uint?)58882);
            Assert.AreEqual(header.GUID, "{ABCBA85D-F881-498B-5ABC-D8CF88382D20}");
            Assert.AreEqual(header.TTNOptions, TTNOptions.Active | TTNOptions.ActivatedByCntr0 | TTNOptions.ActivatedByCntr1);
            Assert.AreEqual(header.DateStamp, new DateTime(2022, 09, 22));
            Assert.IsNotNull(header.BuhOperation);
            Assert.IsNull(header.BuhOperation.Rid);
            Assert.IsNull(header.BuhOperation.Name);


            Assert.IsNotNull(header.Supplier);
            Assert.AreEqual(header.Supplier.Rid, (uint)167772199);
            Assert.AreEqual(header.Supplier.Name, "!!РЕЕСТР ДОГОВОРОВ (ООО ЦТО Ассар)");
            Assert.AreEqual(header.Supplier.SubType, CorrType3.AlcoholProducer);
            Assert.IsNotNull(header.Supplier.KPP);
            Assert.IsNull(header.Supplier.KPP.Rid);
            Assert.IsNull(header.Supplier.KPP.Name);

            Assert.IsNotNull(header.Recipient);
            Assert.AreEqual(header.Recipient.Rid, (uint)83886083);
            Assert.AreEqual(header.Recipient.Name, "_АКТЫ КТО (ИП)");
            Assert.AreEqual(header.Recipient.SubType, CorrType3.AlcoholProducer);
            Assert.IsNotNull(header.Recipient.KPP);
            Assert.IsNull(header.Recipient.KPP.Rid);
            Assert.IsNull(header.Recipient.KPP.Name);

            Assert.AreEqual(header.Name, "1");

            Assert.AreEqual(header.Attributes6.Count, 31);
            Assert.IsTrue(header.Attributes6.ContainsKey("SbisSubdivisionID"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_EDO"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_EDO_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_TerminationDate"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Termination"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Termination_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_AdditionalDate"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_AdditionalNumber"));
            Assert.IsTrue(header.Attributes6.ContainsKey("CommentManager"));
            Assert.IsTrue(header.Attributes6.ContainsKey("TTN_number"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Manager_Released"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Manager_Released_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("DocumentReceived"));
            Assert.IsTrue(header.Attributes6.ContainsKey("DocumentReceived_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Type"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Type_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Exhibiting"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Exhibiting_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Billing"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Billing_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("DocumentMonth"));
            Assert.IsTrue(header.Attributes6.ContainsKey("DocumentMonth_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Engineer"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Engineer_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Date"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_DeliveryAddress"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Contract_Number"));
            Assert.IsTrue(header.Attributes6.ContainsKey("DocType1C"));
            Assert.IsTrue(header.Attributes6.ContainsKey("DocType1C_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("CommentEngineer"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Comment"));

            Assert.AreEqual(header.Attributes7.Count, 10);
            Assert.IsTrue(header.Attributes7.ContainsKey("a1"));
            Assert.IsTrue(header.Attributes7.ContainsKey("a2"));
            Assert.IsTrue(header.Attributes7.ContainsKey("PersonAccountable"));
            Assert.IsTrue(header.Attributes7.ContainsKey("SKeeper1"));
            Assert.IsTrue(header.Attributes7.ContainsKey("SKeeper0"));
            Assert.IsTrue(header.Attributes7.ContainsKey("a3"));
            Assert.IsTrue(header.Attributes7.ContainsKey("a4"));
            Assert.IsTrue(header.Attributes7.ContainsKey("PersonInCharge"));
            Assert.IsTrue(header.Attributes7.ContainsKey("a6"));
            Assert.IsTrue(header.Attributes7.ContainsKey("a99"));

            Assert.AreEqual(header.MinActiveDate, new DateTime(2022, 9, 22));

            Assert.IsNotNull(header.Creator);
            Assert.AreEqual(header.Creator.Name, "Admin");
            Assert.AreEqual(header.Creator.Rid, (uint)0);
            Assert.AreEqual(header.Creator.Date, new DateTime(2022, 9, 22));
            Assert.AreEqual(header.Creator.Time, (uint)38873);

            Assert.IsNotNull(header.LastUpdater);
            Assert.AreEqual(header.LastUpdater.Name, "Admin");
            Assert.AreEqual(header.LastUpdater.Rid, (uint)0);
            Assert.AreEqual(header.LastUpdater.Date, new DateTime(2022, 9, 22));
            Assert.AreEqual(header.LastUpdater.Time, (uint)38907);

            var content = gDoc10.Content;
            Assert.AreEqual(content.Count(), 1);
            var item1 = content.First();
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.Rid, (uint)114375);
            Assert.IsNotNull(item1.GoodsItem);
            Assert.AreEqual(item1.GoodsItem.Rid, (uint)651);
            Assert.AreEqual(item1.GoodsItem.Name, "Точка доступа D-Link DAP-3310");
            Assert.AreEqual(item1.GoodsItem.Attributes6.Count, 21);
            Assert.IsNotNull(item1.GoodsItem.MeasureUnit);
            Assert.AreEqual(item1.GoodsItem.MeasureUnit.Rid, (uint)5);
            Assert.AreEqual(item1.GoodsItem.MeasureUnit.Name, "шт");
            Assert.AreEqual(item1.Currency67, 2m);
            Assert.AreEqual(item1.Currency68, 0m);
            Assert.AreEqual(item1.Currency69, 0m);
            Assert.AreEqual(item1.Currency70, 0m);
            Assert.AreEqual(item1.Currency40, 0m);
            Assert.AreEqual(item1.Currency41, 0m);
            Assert.AreEqual(item1.Currency42, 0m);
            Assert.AreEqual(item1.Options, (uint)1);
            Assert.AreEqual(item1.Quantity, 2m);
            Assert.IsNull(item1.AmountWeighed);

            var item2 = item1.GDocItemComing;
            Assert.IsNotNull(item2?.GoodsItem?.MeasureUnit);
            Assert.AreEqual(item2.GoodsItem.Rid, (uint)672);
            Assert.AreEqual(item2.GoodsItem.Name, "Тач панель ELO Touch (в ассортименте) 15\"");
            Assert.AreEqual(item2.GoodsItem.Attributes6.Count, 21);
            Assert.AreEqual(item2.GoodsItem.MeasureUnit.Rid, (uint)5);
            Assert.AreEqual(item2.GoodsItem.MeasureUnit.Name, "шт");
            Assert.IsNotNull(item2.GoodsItem.Producer);
            Assert.IsNotNull(item2.GoodsItem.AlcoholProductType);


            Assert.AreEqual(item2.Options, (uint)1);
            Assert.AreEqual(item2.Quantity, 3m);
            Assert.IsNull(item2.AmountWeighed);
            Assert.AreEqual(item1.Attributes6.Count, 6);

            Assert.IsTrue(item1.Attributes6.ContainsKey("ExpDate"));
            Assert.IsTrue(item1.Attributes6.ContainsKey("defaultPrice"));
            Assert.IsTrue(item1.Attributes6.ContainsKey("product_Line_Filter"));
            Assert.IsTrue(item1.Attributes6.ContainsKey("product_Line_DiscountsSize"));
            Assert.IsTrue(item1.Attributes6.ContainsKey("product_InstallationLocation"));
            Assert.IsTrue(item1.Attributes6.ContainsKey("product_Line_Comment"));
        }
    }
}