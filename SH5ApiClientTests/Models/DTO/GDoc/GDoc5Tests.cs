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
    public class GDoc5Tests
    {
        [TestMethod()]
        public void ParseGDoc5Test()
        {
            
            var gDoc5 = Options.ApiClient.GetGDoc5Async(58883, "5427A28F-A71B-0139-621B-0DAF87FAD4BD").Result;
            Assert.IsNotNull(gDoc5);
            Assert.IsNotNull(gDoc5.Header);
            Assert.IsNotNull(gDoc5.Content);

            var header = gDoc5.Header;
            Assert.AreEqual(header.Rid, (uint?)58883);
            Assert.AreEqual(header.GUID, "{5427A28F-A71B-0139-621B-0DAF87FAD4BD}");
            Assert.AreEqual(header.TTNOptions, TTNOptions.Active | TTNOptions.ActivatedByCntr0);
            Assert.AreEqual(header.DateStamp, new DateTime(2022, 09, 22));
            Assert.IsNull(header.DueDate);
            Assert.IsNotNull(header.Invoice);
            Assert.IsNull(header.Invoice.Type);
            Assert.IsNull(header.Invoice.Name);
            Assert.IsNull(header.Invoice.Rid);
            Assert.IsNull(header.Invoice.Date);
            Assert.IsNotNull(header.BuhOperation);
            Assert.IsNull(header.BuhOperation.Name);
            Assert.IsNull(header.BuhOperation.Rid);

            var supplier = header.Supplier;
            Assert.IsNotNull(supplier);
            Assert.AreEqual(supplier.Rid, (uint)176160809);
            Assert.AreEqual(supplier.Name, " ОТДЕЛ ПРОДАЖ (ООО ЦТО Ассар)");
            Assert.AreEqual(supplier.SubType, CorrType3.AlcoholProducer);
            Assert.IsNotNull(supplier.KPP);
            Assert.IsNull(supplier.KPP.Rid);
            Assert.IsNull(supplier.KPP.Name);

            var recipient = header.Recipient;
            Assert.IsNotNull(recipient);
            Assert.AreEqual(recipient.Rid, (uint)2822);
            Assert.AreEqual(recipient.Name, "НТЦ ИЗМЕРИТЕЛЬ (ООО)");
            Assert.AreEqual(recipient.SubType, CorrType3.AlcoholProducer);
            Assert.IsNotNull(recipient.KPP);
            Assert.IsNull(recipient.KPP.Rid);
            Assert.IsNull(recipient.KPP.Name);

            Assert.AreEqual(header.Name, "7044");

            Assert.AreEqual(header.Attributes6.Count, 31);
            foreach (var attribute in header.Attributes6)
                Assert.IsNull(attribute.Value);
            Assert.AreEqual(header.Attributes7.Count, 10);
            foreach (var attribute in header.Attributes7)
                Assert.IsNull(attribute.Value);

            Assert.AreEqual(header.MinActiveDate, new DateTime(2022, 09, 22));

            Assert.IsNotNull(header.Creator);
            Assert.AreEqual(header.Creator.Name, "Admin");
            Assert.AreEqual(header.Creator.Rid, (uint)0);
            Assert.AreEqual(header.Creator.Date, new DateTime(2022, 9, 22));
            Assert.AreEqual(header.Creator.Time, (uint)57244);

            Assert.IsNotNull(header.LastUpdater);
            Assert.AreEqual(header.LastUpdater.Name, "Admin");
            Assert.AreEqual(header.LastUpdater.Rid, (uint)0);
            Assert.AreEqual(header.LastUpdater.Date, new DateTime(2022, 9, 22));
            Assert.AreEqual(header.LastUpdater.Time, (uint)57379);

            var item1 = gDoc5.Content.ElementAt(0);
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.Rid, (uint)114377);
            var goodsItem1 = item1.GoodsItem;
            Assert.IsNotNull(goodsItem1);
            Assert.AreEqual(goodsItem1.Rid, (uint)3761);
            Assert.AreEqual(goodsItem1.Name, "(без ФН) ККМ ШТРИХ-КАРТ-Ф");

            Assert.AreEqual(goodsItem1.Attributes6.Count, 21);
            foreach (var attribute in goodsItem1.Attributes6)
                Assert.IsNull(attribute.Value);
            Assert.IsNotNull(goodsItem1.MeasureUnit);
            Assert.AreEqual(goodsItem1.MeasureUnit.Rid, (uint)5);
            Assert.AreEqual(goodsItem1.MeasureUnit.Name, "шт");
            Assert.IsNotNull(item1.SaleNDS);
            Assert.AreEqual(item1.SaleNDS.Rate, (uint)1);
            Assert.IsNotNull(item1.SaleNSP);
            Assert.AreEqual(item1.SaleNSP.Rate, (uint)3);
            Assert.AreEqual(item1.Currency45, 43000m);
            Assert.AreEqual(item1.Currency46, 0);
            Assert.AreEqual(item1.Currency47, 0);
            Assert.IsNull(item1.Currency67);
            Assert.IsNull(item1.Currency68);
            Assert.IsNull(item1.Currency69);
            Assert.IsNull(item1.Currency70);
            Assert.AreEqual(item1.Currency40, 44000m);
            Assert.AreEqual(item1.Currency41, 0);
            Assert.AreEqual(item1.Currency42, 0);
            Assert.AreEqual(item1.Options, (uint)1);
            Assert.AreEqual(item1.Quantity, 2m);
            Assert.IsNull(item1.AmountWeighed);

            Assert.IsNotNull(goodsItem1.ProductSynonym);
            Assert.IsNull(goodsItem1.ProductSynonym.Rid);
            Assert.IsNull(goodsItem1.ProductSynonym.CF);
            Assert.IsNull(goodsItem1.ProductSynonym.FullName);

            Assert.IsNotNull(item1.RelatedInvoice);
            Assert.IsNotNull(item1.RelatedInvoice.GDocItem);
            Assert.AreEqual(item1.RelatedInvoice.GDocItem.Rid, (uint)113885);
            Assert.AreEqual(item1.RelatedInvoice.Rid, (uint)58640);
            Assert.AreEqual(item1.RelatedInvoice.Name, "984");
            Assert.AreEqual(item1.RelatedInvoice.TTNOptions, TTNOptions.Active | TTNOptions.ActivatedByCntr1);
            Assert.AreEqual(item1.RelatedInvoice.Attributes6.Count, 31);
            Assert.AreEqual(item1.RelatedInvoice.DateStamp, new DateTime(2022, 8, 11));
            Assert.AreEqual(item1.Attributes6.Count, 6);
            foreach(var attr in item1.Attributes6)
                Assert.IsNull(attr.Value);

            var item2 = gDoc5.Content.ElementAt(1);
            Assert.IsNotNull(item2);
            Assert.AreEqual(item2.Rid, (uint)114378);
            var goodsItem2 = item2.GoodsItem;
            Assert.IsNotNull(goodsItem2);
            Assert.AreEqual(goodsItem2.Rid, (uint)5047);
            Assert.AreEqual(goodsItem2.Name, "ШТРИХ-КАРТ-Ф Крышка отсека для бумаги без валик");

            Assert.AreEqual(goodsItem2.Attributes6.Count, 21);
            Assert.IsNotNull(goodsItem2.MeasureUnit);
            Assert.AreEqual(goodsItem2.MeasureUnit.Rid, (uint)5);
            Assert.AreEqual(goodsItem2.MeasureUnit.Name, "шт");
            Assert.IsNotNull(item2.SaleNDS);
            Assert.AreEqual(item2.SaleNDS.Rate, (uint)2);
            Assert.IsNotNull(item2.SaleNSP);
            Assert.AreEqual(item2.SaleNSP.Rate, (uint)4);
            Assert.AreEqual(item2.Currency45, 15000m);
            Assert.AreEqual(item2.Currency46, 0);
            Assert.AreEqual(item2.Currency47, 0);
            Assert.AreEqual(item2.Currency67, 20m);
            Assert.AreEqual(item2.Currency68, 15000m);
            Assert.AreEqual(item2.Currency69, 0m);
            Assert.AreEqual(item2.Currency70, 0m);
            Assert.AreEqual(item2.Currency40, 15000m);
            Assert.AreEqual(item2.Currency41, 0);
            Assert.AreEqual(item2.Currency42, 0);
            Assert.AreEqual(item2.Options, (uint)1);
            Assert.AreEqual(item2.Quantity, 20m);
            Assert.IsNull(item2.AmountWeighed);

            Assert.IsNotNull(goodsItem2.ProductSynonym);
            Assert.IsNull(goodsItem2.ProductSynonym.Rid);
            Assert.IsNull(goodsItem2.ProductSynonym.CF);
            Assert.IsNull(goodsItem2.ProductSynonym.FullName);

            Assert.IsNotNull(item2.RelatedInvoice);
            Assert.IsNotNull(item2.RelatedInvoice.GDocItem);
            Assert.AreEqual(item2.RelatedInvoice.GDocItem.Rid, (uint)113888);
            Assert.AreEqual(item2.RelatedInvoice.Rid, (uint)58640);
            Assert.AreEqual(item2.RelatedInvoice.Name, "984");
            Assert.AreEqual(item2.RelatedInvoice.TTNOptions, TTNOptions.Active | TTNOptions.ActivatedByCntr1);
            Assert.AreEqual(item2.RelatedInvoice.Attributes6.Count, 31);
            Assert.AreEqual(item2.RelatedInvoice.DateStamp, new DateTime(2022, 8, 11));
            Assert.AreEqual(item2.Attributes6.Count, 6);
            foreach (var attr in item2.Attributes6)
                Assert.IsNull(attr.Value);
        }
    }
}