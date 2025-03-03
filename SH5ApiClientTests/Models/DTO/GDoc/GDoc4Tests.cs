﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class GDoc4Tests
    {
        [TestMethod()]
        public void ParseGDoc4Test()
        {
            var gDoc4 = Options.ApiClient.GetGDoc4Async(4, "{5488EA00-2DED-F0B5-A8AD-5D5047DF5F82}").Result;
            Assert.IsNotNull(gDoc4);
            Assert.IsNotNull(gDoc4.Header);
            Assert.IsNotNull(gDoc4.Content);

            var header = gDoc4.Header;
            Assert.AreEqual(header.Rid, (uint?)4);
            Assert.AreEqual(header.GUID, "{5488EA00-2DED-F0B5-A8AD-5D5047DF5F82}");
            Assert.AreEqual(header.TTNOptions, TTNOptions.Active | TTNOptions.ActivatedByCntr0);
            Assert.AreEqual(header.DateStamp, new DateTime(2022, 05, 31));
            Assert.IsNotNull(header.Currency);
            Assert.AreEqual(header.Currency.Rid, (uint)0);
            Assert.AreEqual(header.Currency.Code, "Рубль");
            Assert.AreEqual(header.CourceBase, 1.0m);
            Assert.AreEqual(header.CourceInvoice, 1.0m);
            Assert.IsNull(header.DueDate);
            Assert.IsNotNull(header.Invoice);
            Assert.IsNull(header.Invoice.Type);
            Assert.IsNull(header.Invoice.Name);
            Assert.IsNull(header.Invoice.Date);
            Assert.IsNull(header.Invoice.Rid);
            Assert.IsNotNull(header.BuhOperation);
            Assert.IsNull(header.BuhOperation.Rid);
            Assert.IsNull(header.BuhOperation.Name);
            Assert.IsNotNull(header.Contract);
            Assert.IsNull(header.Contract.Type);
            Assert.IsNull(header.Contract.Name);
            Assert.IsNull(header.Contract.Date);
            Assert.IsNull(header.Contract.Options);
            Assert.IsNull(header.Contract.PDow);
            Assert.IsNull(header.Contract.Rid);
            Assert.IsNull(header.Contract.PLimit);

            Assert.IsNotNull(header.Supplier);
            Assert.AreEqual(header.Supplier.Rid, (uint)4194304);
            Assert.AreEqual(header.Supplier.Name, "Склад 1");
            Assert.AreEqual(header.Supplier.SubType, CorrType3.AlcoholProducer);
            Assert.IsNotNull(header.Supplier.KPP);
            Assert.IsNull(header.Supplier.KPP.Rid);
            Assert.IsNull(header.Supplier.KPP.Name);

            Assert.IsNotNull(header.Recipient);
            Assert.AreEqual(header.Recipient.Rid, (uint)1);
            Assert.AreEqual(header.Recipient.Name, "Внешний контрагент 2");
            Assert.AreEqual(header.Recipient.SubType, CorrType3.AlcoholProducer);
            Assert.IsNotNull(header.Recipient.KPP);
            Assert.AreEqual(header.Recipient.KPP.Rid, (uint)2);
            Assert.AreEqual(header.Recipient.KPP.Name, "2222222222");

            Assert.AreEqual(header.Name, "4");
            Assert.IsTrue(header.Attributes6.Count == 4);
            Assert.IsTrue(header.Attributes6.ContainsKey("DocType1C"));
            Assert.IsTrue(header.Attributes6.ContainsKey("DocType1C_itext_"));
            Assert.IsTrue(header.Attributes6.ContainsKey("TTN_number"));
            Assert.IsTrue(header.Attributes6.ContainsKey("Comment"));
            Assert.IsTrue(header.Attributes7.Count == 4);
            Assert.IsTrue(header.Attributes7.ContainsKey("PersonAccountable"));
            Assert.AreEqual(header.Attributes7["PersonAccountable"], "cvb sdfg srfg");
            Assert.IsTrue(header.Attributes7.ContainsKey("SKeeper1"));
            Assert.AreEqual(header.Attributes7["SKeeper1"], "sdfg dsfg sdfg");
            Assert.IsTrue(header.Attributes7.ContainsKey("SKeeper0"));
            Assert.AreEqual(header.Attributes7["SKeeper0"], "sdfg sdfgsdfg sdfg");
            Assert.IsTrue(header.Attributes7.ContainsKey("PersonInCharge"));
            Assert.AreEqual(header.Attributes7["PersonInCharge"], "dfg dfg dfg");
            Assert.AreEqual(header.MinActiveDate, new DateTime(2022, 05, 31));

            Assert.IsNotNull(header.Creator);
            Assert.AreEqual(header.Creator.Name, "Admin");
            Assert.AreEqual(header.Creator.Rid, (uint)0);
            Assert.AreEqual(header.Creator.Date, new DateTime(2022, 05, 18));
            Assert.AreEqual(header.Creator.Time, (uint)46004);

            Assert.IsNotNull(header.LastUpdater);
            Assert.AreEqual(header.LastUpdater.Name, "Admin");
            Assert.AreEqual(header.LastUpdater.Rid, (uint)0);
            Assert.AreEqual(header.LastUpdater.Date, new DateTime(2022, 05, 18));
            Assert.AreEqual(header.LastUpdater.Time, (uint)46438);

            var content = gDoc4.Content;
            Assert.AreEqual(content.Count(), 2);

            var item1 = content.ElementAt(0);
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.Rid, (uint)4);
            Assert.IsTrue(item1.Attributes6.ContainsKey("ExpDate"));
            Assert.AreEqual(item1.Attributes6["ExpDate"], null);
            Assert.IsNotNull(item1.GoodsItem);
            Assert.AreEqual(item1.GoodsItem.Rid, (uint)1);
            Assert.AreEqual(item1.GoodsItem.Name, "Тест Диадок");
            Assert.IsNotNull(item1.GoodsItem.Attributes6);
            Assert.AreEqual(item1.GoodsItem.Attributes6.Count, 1);
            Assert.IsTrue(item1.GoodsItem.Attributes6.ContainsKey("SDInd"));
            Assert.AreEqual(item1.GoodsItem.Attributes6["SDInd"], null);
            Assert.IsNotNull(item1.GoodsItem.MeasureUnit);
            Assert.AreEqual(item1.GoodsItem.MeasureUnit.Rid, (uint)1);
            Assert.AreEqual(item1.GoodsItem.MeasureUnit.Name, "Кг");
            Assert.IsNotNull(item1.GoodsItem.ProductSynonym);
            Assert.AreEqual(item1.GoodsItem.ProductSynonym.Rid, (uint)1);
            Assert.AreEqual(item1.GoodsItem.ProductSynonym.CF, 1m);
            Assert.AreEqual(item1.GoodsItem.ProductSynonym.FullName, "Право использования программы для ЭВМ «Контур.Диадок», тарифный план «250 документов»");
            Assert.IsNotNull(item1.SaleNSP);
            Assert.AreEqual(item1.SaleNSP.Rate, (uint)0);
            Assert.IsNotNull(item1.SaleNDS);
            Assert.AreEqual(item1.SaleNDS.Rate, (uint)0);
            Assert.AreEqual(item1.Currency45, 3000m);
            Assert.AreEqual(item1.Currency46, 0);
            Assert.AreEqual(item1.Currency47, 0);
            Assert.AreEqual(item1.Currency67, 1m);
            Assert.AreEqual(item1.Currency68, 1650m);
            Assert.AreEqual(item1.Currency69, 200m);
            Assert.AreEqual(item1.Currency70, 50m);
            Assert.AreEqual(item1.Currency40, 1650m);
            Assert.AreEqual(item1.Currency41, 200m);
            Assert.AreEqual(item1.Currency42, 50m);
            Assert.AreEqual(item1.Options, (uint)1);
            Assert.AreEqual(item1.Quantity, 1m);
            Assert.IsNull(item1.AmountWeighed);

            var item2 = content.ElementAt(1);
            Assert.IsNotNull(item2);
            Assert.AreEqual(item2.Rid, (uint)5);
            Assert.IsTrue(item2.Attributes6.ContainsKey("ExpDate"));
            Assert.AreEqual(item2.Attributes6["ExpDate"], null);
            Assert.IsNotNull(item2.GoodsItem);
            Assert.AreEqual(item2.GoodsItem.Rid, (uint)2);
            Assert.AreEqual(item2.GoodsItem.Name, "Техническое сопровождение ККТ");
            Assert.IsNotNull(item2.GoodsItem.Attributes6);
            Assert.AreEqual(item2.GoodsItem.Attributes6.Count, 1);
            Assert.IsTrue(item2.GoodsItem.Attributes6.ContainsKey("SDInd"));
            Assert.AreEqual(item2.GoodsItem.Attributes6["SDInd"], null);
            Assert.IsNotNull(item2.GoodsItem.MeasureUnit);
            Assert.AreEqual(item2.GoodsItem.MeasureUnit.Rid, (uint)5);
            Assert.AreEqual(item2.GoodsItem.MeasureUnit.Name, "шт.");
            Assert.IsNotNull(item2.GoodsItem.ProductSynonym);
            Assert.AreEqual(item2.GoodsItem.ProductSynonym.Rid, null);
            Assert.AreEqual(item2.GoodsItem.ProductSynonym.CF, null);
            Assert.AreEqual(item2.GoodsItem.ProductSynonym.FullName, null);
            Assert.IsNotNull(item2.SaleNSP);
            Assert.AreEqual(item2.SaleNSP.Rate, (uint)0);
            Assert.IsNotNull(item2.SaleNDS);
            Assert.AreEqual(item2.SaleNDS.Rate, (uint)0);
            Assert.AreEqual(item2.Currency45, 0);
            Assert.AreEqual(item2.Currency46, 0);
            Assert.AreEqual(item2.Currency47, 0);
            Assert.AreEqual(item2.Currency67, 1m);
            Assert.AreEqual(item2.Currency68, 0);
            Assert.AreEqual(item2.Currency69, 0);
            Assert.AreEqual(item2.Currency70, 0);
            Assert.AreEqual(item2.Currency40, 0);
            Assert.AreEqual(item2.Currency41, 0);
            Assert.AreEqual(item2.Currency42, 0);
            Assert.AreEqual(item2.Options, (uint)1);
            Assert.AreEqual(item2.Quantity, 1m);
            Assert.IsNull(item2.AmountWeighed);
        }
    }
}