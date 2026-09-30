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
            ////var dd = ModelSHBase.Parse<InternalCorrespondent>(null);
            ConnectionParamSH5 param = new("Admin", "776417", "192.168.200.5", 9797);
            ApiClient client = new ApiClient(param);

            try
            {
                var corrs = client.LoadInternalCorrespondentsAsync().Result;
                var cor = corrs.Single(t => t.Rid == 0);
                var date = DateTime.Now + TimeSpan.FromDays(1);
                var docs = client.LoadGDocsAsync(DateTime.Now, date, SH5ApiClient.Models.Enums.TTNTypeForRequest.SalesInvoice).Result;
                var doc1 = docs.Single(t => t.Rid == 124194);
                foreach (var doc in docs)
                {
                    client.GetGDoc4Async(doc.Rid.Value, doc.GUID);
                }
                var fff = client.GetDocsByCorrsReportAsync(date, date, cor, CancellationToken.None).Result;
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