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
            ConnectionParamSH5 param = new("Admin", "", "192.168.200.41", 9798);
            ApiClient client = new ApiClient(param);

            try
            {
                var gg = client.CreateIncomingTTNAsync(null, new DateTime(2025, 01, 01), "12345", 0, 8388609, "comment", true, new List<GDoc0Item>
                {
                    new GDoc0Item(38829,1,416.67M,5)
                    {
                         VatSum = 83.33M,
                         GtdRid = null,
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