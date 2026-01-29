using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Models.DTO.Reports
{
    public class DocsByCorrsReport
    {
        private DocsByCorrsReport(List<DocsByCorrsReportItem> items)
        {
            Items = new ReadOnlyCollection<DocsByCorrsReportItem>(items);
        }
        public DateTime CreateDateTime { get; } = DateTime.Now;
        public ReadOnlyCollection<DocsByCorrsReportItem> Items { get; }

        internal static DocsByCorrsReport Parse(Dictionary<string, string>[] dictionaries)
        {
            var items = dictionaries
                .Select(t => DocsByCorrsReportItem.Parse(t))
                .ToList();
            DocsByCorrsReport report = new DocsByCorrsReport(items);
            return report;
        }
    }
    public class DocsByCorrsReportItem
    {
        /// <summary>Rid корреспондента</summary>
        public uint CorrespondentRid { get; private set; }

        /// <summary>Имя корреспондента</summary>
        public string CorrespondentName { get; private set; }

        /// <summary>Сумма без налогов</summary>
        public decimal SumWithOutTax { get; private set; }

        /// <summary>НДС</summary>
        public decimal NDS { get; private set; }

        /// <summary>НСП</summary>
        public decimal NSP { get; private set; }

        /// <summary>Сумма включая налоги</summary>
        public decimal SumWithTax { get; private set; }

        internal static DocsByCorrsReportItem Parse(Dictionary<string, string> value)
        {
            return new DocsByCorrsReportItem()
            {
                CorrespondentRid = uint.TryParse(value["1"], out uint rid) ? rid : 0,
                CorrespondentName = value["3"],
                SumWithOutTax = decimal.TryParse(value["112\\50"], out decimal sumWithOutTax) ? sumWithOutTax : 0,
                NDS = decimal.TryParse(value["112\\51"], out decimal nds) ? nds : 0,
                NSP = decimal.TryParse(value["112\\52"], out decimal nsp) ? nsp : 0,
                SumWithTax = decimal.TryParse(value["112\\53"], out decimal sumWithTax) ? sumWithTax : 0,
            };
        }
    }
}
