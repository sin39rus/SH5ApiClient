using SH5ApiClient.Data;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("107")]
    public class Сorrespondent : DataExecutable
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string Name { set; get; }

        /// <summary>ИНН</summary>
        [OriginalName("2")]
        public string INN { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string GUID { set; get; }

        /// <summary>Тип1</summary>
        [OriginalName("5")]
        public CorrType? CorrType { set; get; }

        /// <summary>Тип3</summary>
        [OriginalName("31")]
        public CorrType3? SubType { set; get; }

        /// <summary>Тип2</summary>
        [OriginalName("32")]
        public CorrTypeEx? CorrTypeEx { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new Dictionary<string, string>();

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new Dictionary<string, string>();

        /// <summary>Атрибуты типа 34</summary>
        [OriginalName("34")]
        public Dictionary<string, string> Attributes34 { set; get; } = new Dictionary<string, string>();

        /// <summary>Атрибуты типа 35</summary>
        [OriginalName("35")]
        public Dictionary<string, string> Attributes35 { set; get; } = new Dictionary<string, string>();

        /// <summary>Атрибуты типа 36</summary>
        [OriginalName("36")]
        public Dictionary<string, string> Attributes36 { set; get; } = new Dictionary<string, string>();

        /// <summary>Rid</summary>
        [OriginalName("37")]
        public uint? AttrsMask { set; get; }

        /// <summary>Подразделение</summary>
        [OriginalName("106")]
        public Depart Depart { set; get; }

        public static Сorrespondent Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Сorrespondent
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? (uint?)rid : null,
                Name = value.GetValueOrDefault("3"),
            };
        }
    }
}
