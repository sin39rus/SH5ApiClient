using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Models.Enums;
using SH5ApiClient.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Товарная группа</summary>
    [OriginalName("209")]
    public class GGroup
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

        /// <summary>Флаги</summary>
        [OriginalName("42")]
        public GoodsItemFlags? Flags { set; get; }

        /// <summary>Группа предок</summary>
        [OriginalName("209#1")]
        public GGroup Parent { set; get; }

        /// <summary>UserGroup</summary>
        [OriginalName("239")]
        public long UserGroup { set; get; }

        /// <summary>Подразделение</summary>
        [OriginalName("106")]
        public Depart Depart { set; get; }

        internal static GGroup Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new GGroup
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? (uint?)rid : null,
                Name = value.GetValueOrDefault("3"),
                GUID = value.GetValueOrDefault("4")
            };
        }
    }
}
