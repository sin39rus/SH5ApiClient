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
            //ConnectionParamSH5 param = new("Admin", "776417", "192.168.200.5", 9797);
            ApiClient client = new ApiClient(param);

            var measureUnits = FindVolumeMeasureUnitsGroupeAsync(client).Result;
            var items = measureUnits.Item1;
            var groupId = measureUnits.Item2;
            client.CreateMeasureUnitAsync("Бут 1,5л", 1.5m, groupId).Wait();

            MeasureUnit? measureUnit = NewMethod(client);
            client.CreateGoodAsync("Тестовый товар", new List<MeasureUnit>
            {
                new MeasureUnit
                {
                    Rid = measureUnit.Rid,
                    MeasureUnitType = MeasureUnitType.Base | MeasureUnitType.Report | MeasureUnitType.Request | MeasureUnitType.AutoDocuments | MeasureUnitType.Calculations,
                    BaseRatio = 1,
                },
                new MeasureUnit
                {
                    Rid = 3,
                    BaseRatio = 0.5m
                },
            }).Wait();
        }

        private static MeasureUnit? NewMethod(ApiClient client)
        {
            var measureUnits = client.LoadMeasureUnitsAsync().Result;
            var measureUnit = measureUnits.Where(t => t.Attributes7.ContainsKey("OKEI")).SingleOrDefault(t => t.Attributes7["OKEI"] == "112");
            return measureUnit;
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
            var measureUnitsInGroup = measureUnits.Where(t=>t.MeasureGroup.Rid == groupRid);
            return Tuple.Create(measureUnitsInGroup, groupRid.GetValueOrDefault());
        }
    }
}