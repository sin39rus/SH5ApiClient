using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Models.Enums;
using System.Collections.Generic;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Корреспондент SH</summary>
    [OriginalName("105")]
    public sealed class СorrespondentOld
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

        [OriginalName("114")]
        public KPP KPP { set; get; }

        /// <summary>Cрок оплаты приходов</summary>
        [OriginalName("11")]
        public ushort? PaymentIncomeSpan { set; get; }

        /// <summary>Cрок оплаты расходов</summary>
        [OriginalName("12")]
        public ushort? PaymentExpenseSpan { set; get; }
    }
}
