using SH5ApiClient;
using SH5ApiClient.Data;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using System;
using System.Collections;
using System.Text;

namespace ConsoleForTest
{
    internal class Program
    {
        static void Main()
        {
            ////var dd = ModelSHBase.Parse<InternalСorrespondent>(null);
            ConnectionParamSH5 param = new("Admin", "", "127.0.0.1", 9798);
            //ConnectionParamSH5 param = new("Admin", "776417", "192.168.200.5", 9797);
            ApiClient client = new ApiClient(param);
            var fff = client.LoadGDocsAsync(new DateTime(2024,12,18), new DateTime(2024, 12, 19), SH5ApiClient.Models.Enums.TTNTypeForRequest.PurchaseInvoice, SH5ApiClient.Models.Enums.GDocsRequestFilter.ShowInactiveInvoices).Result;
            var nds = client.GetNdsListAsync().Result;
            var ggc = nds.ToList();
            try
            {
                var gg = client.CreateIncomingTTNAsync(DateTime.Now, "12345", 0, 8388609, "comment", new List<GDoc0Item>
                {
                    new GDoc0Item(68,1,416.67M,5)
                    {
                         VatSum = 83.33M,
                         NdsRateValue = nds.Any(t=>t.Rate ==  700) ? (uint)700 : throw new Exception("Ставка НДС 700 не найдена в SH."),
                    }
                }).Result;
            }
            catch (Exception ex)
            {

            }
        }
        private static async Task<Tuple<IEnumerable<MeasureUnit>, uint>> FindVolumeMeasureUnitsGroupeAsync(ApiClient client)
        {
            var measureUnits = await client.LoadMeasureUnitsAsync();
            var measureUnit = measureUnits.Where(t => t.Attributes7.ContainsKey("OKEI")).Where(t => t.Attributes7["OKEI"] == "112");

            if (!measureUnit.Any())
                throw new Exception("Не найдена группа объемных единиц измерения. Единица измерения \"Литр\" должна содержать код ОКЕИ 112.");
            if (measureUnit.Count() > 1)
                throw new Exception("Сразу несколько единиц измерения содержат код ОКЕИ 112. Только одна единица измерения может содержать код ОКЕИ 112.");

            var groupRid = measureUnit.First().MeasureGroup.Rid;
            var measureUnitsInGroup = measureUnits.Where(t => t.MeasureGroup.Rid == groupRid);
            return Tuple.Create(measureUnitsInGroup, groupRid.GetValueOrDefault());
        }
    }
}