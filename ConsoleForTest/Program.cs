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
            ConnectionParamSH5 param = new("Admin", "776417", "192.168.200.5", 9797);
            ApiClient client = new ApiClient(param);

            try
            {
                //var docs = client.LoadGDocsAsync(new DateTime(2026, 01, 01), new DateTime(2026, 03, 31), SH5ApiClient.Models.Enums.TTNTypeForRequest.SalesInvoice).Result;
                //var doc = docs.Single(t => t.Rid == 113863);
                var shInvoice = client.GetGDoc4Async(113863, "{330DE3F8-67C0-D300-A4E7-410FC0ED7180}").Result;
                shInvoice.ChangeValue("111", "1", shInvoice.Header.Rid, "6\\DocumentLink_EDO", "Test test");
                var fff = client.UpdateGDoc4(shInvoice).Result;
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