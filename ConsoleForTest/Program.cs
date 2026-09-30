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
                var corrs = client.LoadInternalCorrespondentsAsync().Result;
                var cor = corrs.Single(t => t.Rid == 0);
                var date = DateTime.Now + TimeSpan.FromDays(1);
                var docs = client.LoadGDocsAsync(date, date, SH5ApiClient.Models.Enums.TTNTypeForRequest.SalesInvoice).Result;
                var doc1 = docs.Single(t => t.Rid == 124194);
                var doc2 = docs.Single(t => t.Rid == 122797);
                client.GetGDoc4Async(doc1.Rid.Value, doc1.GUID);
                client.GetGDoc4Async(doc2.Rid.Value, doc1.GUID);
                var fff = client.GetDocsByCorrsReportAsync(date, date, cor, CancellationToken.None).Result;
                //var docs = client.LoadGDocsAsync(new DateTime(2026, 01, 01), new DateTime(2026, 03, 31), SH5ApiClient.Models.Enums.TTNTypeForRequest.SalesInvoice).Result;
                //var doc = docs.Single(t => t.Rid == 113863);

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