using SH5ApiClient.Infrastructure.Attributes;
using System.Collections.Generic;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>КПП контрагента</summary>
    [OriginalName("114")]
    public class KPP
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>КПП по умолчанию</summary>
        [OriginalName("9")]
        public bool IsDefault { set; get; }

        /// <summary>Регион</summary>
        [OriginalName("232")]
        public Region Region { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string Name { set; get; }

        /// <summary>Внешний</summary>
        [OriginalName("34")]
        public string ExtCode { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new Dictionary<string, string>();

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new Dictionary<string, string>();

        /// <summary>Атрибуты типа 35</summary>
        [OriginalName("35")]
        public Dictionary<string, string> Attributes35 { set; get; } = new Dictionary<string, string>();

        /// <summary>Контрагент</summary>
        [OriginalName("107")]
        public СorrespondentOld Сorrespondent { set; get; }

        /// <summary>Контрагент</summary>
        [OriginalName("105")]
        public СorrespondentOld Сorrespondent2 { set; get; }
    }
}
