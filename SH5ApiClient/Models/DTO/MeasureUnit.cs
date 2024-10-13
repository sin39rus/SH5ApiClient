using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Единица измерения</summary>
    [OriginalName("206")]
    public class MeasureUnit
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string Name { set; get; }

        /// <summary>Коэффициент пересчета в базовую для  группы единицу изм.</summary>
        [OriginalName("41")]
        public decimal? BaseRatio { set; get; }

        /// <summary>Группа единицы измерения</summary>
        [OriginalName("205")]
        public MeasureGroup MeasureGroup { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string GUID { set; get; }

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new Dictionary<string, string>();

        /// <summary>Flags</summary>
        [OriginalName("10")]
        public bool? IsBase { set; get; }

        /// <summary>Flags</summary>
        [OriginalName("255")]
        public Ignore Ignore2 { set; get; }

        /// <summary>Flags</summary>
        [OriginalName("42")]
        public uint? Flags { set; get; } //ToDo: Типизированный объект, найти описание

        internal static MeasureUnit Parse(Dictionary<string, string> value)
        {
                if (!value.Any())
                    return null;
                return new MeasureUnit
                {
                    Rid = uint.TryParse(value["1"], out uint rid) ? (uint?)rid : null,
                    Name = value.GetValueOrDefault("3")
                };
        }

        [OriginalName("255")]
        public class Ignore
        {
            /// <summary>Rid</summary>
            [OriginalName("1")]
            public uint? Rid { set; get; }

            /// <summary>Коэффициент пересчета в базовую для  группы единицу изм.</summary>
            [OriginalName("41")]
            public decimal? BaseRatio { set; get; }

            /// <summary>ИНН</summary>
            [OriginalName("2")]
            public string INN { set; get; }

            /// <summary>Углевода на 100 гр</summary>
            [OriginalName("22")]
            public decimal EnergyСarbs { set; get; }

            [OriginalName("244")]
            public Ignore2 Ignore3 { set; get; }

            [OriginalName("244")]
            public class Ignore2
            {
                /// <summary>Rid</summary>
                [OriginalName("1")]
                public uint? Rid { set; get; }

                /// <summary>GUID</summary>
                [OriginalName("4")]
                public string GUID { set; get; }

                /// <summary>Name</summary>
                [OriginalName("3")]
                public string Name { set; get; }


                /// <summary>ИНН</summary>
                [OriginalName("2")]
                public string INN { set; get; }
            }
        }
    }
}
