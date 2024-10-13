using System;
using System.Collections.Generic;
using System.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Алкогольные лицензии</summary>
    [OriginalName("115")]
    public class AloLicInfo
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Дата начала</summary>
        [OriginalName("31")]
        public DateTime? From { set; get; }

        /// <summary>Дата окончания</summary>
        [OriginalName("32")]
        public DateTime? To { set; get; }

        /// <summary>Номер лицензии</summary>
        [OriginalName("3")]
        public string LicNum { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new Dictionary<string, string>();

        /// <summary>KPP</summary>
        [OriginalName("114")]
        public KPP KPP { set; get; }
        public static IEnumerable<AloLicInfo> GetAloLicInfosFromSHAnswear(ExecOperationContent answear)
        {
            foreach (Dictionary<string, string> value in answear.GetValues())
            {
                var info = Parse(value);
                if (info != null)
                    yield return info;
            }
        }
        public static AloLicInfo Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new AloLicInfo
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? (uint?)rid : null,
                From = DateTime.TryParse(value["31"], out DateTime from) ? (DateTime?)from : null,
                To = DateTime.TryParse(value["32"], out DateTime to) ? (DateTime?)to : null,
                LicNum = value.ContainsValue("3") ? value["3"] : null,
                Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\"), g => g.Value),
                //KPP = KPP.Parse(value.Where(t => t.Key.StartsWith("114\\")).ToDictionary(t => t.Key.TrimStart("114\\"), g => g.Value))
            };
        }
    }
}
