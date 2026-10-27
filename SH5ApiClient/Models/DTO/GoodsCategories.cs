using SH5ApiClient.Data;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("200")]
    public class GoodsCategories : DataExecutable
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string Name { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string GUID { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new Dictionary<string, string>();

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new Dictionary<string, string>();

        internal static IEnumerable<GoodsCategories> ParseRange(Dictionary<string, string>[] values) =>
            values.Select(t => Parse(t));
        private static GoodsCategories Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new GoodsCategories
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? (uint?)rid : null,
                Name = value.GetValueOrDefault("3"),
                GUID = value.GetValueOrDefault("4"),
                Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\".ToCharArray()), g => g.Value),
                Attributes7 = value.Where(t => t.Key.StartsWith("7\\")).ToDictionary(t => t.Key.TrimStart("7\\".ToCharArray()), g => g.Value),
            };
        }
    }
}
