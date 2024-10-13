using SH5ApiClient.Data;
using System.Collections.Generic;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Собственный корреспондент</summary>
    
    [OriginalName("102")]
    public class InternalСorrespondent : DataExecutable
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string GUID { set; get; }

        /// <summary>Cрок оплаты приходов</summary>
        [OriginalName("11")]
        public ushort? PaymentIncomeSpan { set; get; }

        /// <summary>Cрок оплаты расходов</summary>
        [OriginalName("12")]
        public ushort? PaymentExpenseSpan { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string Name { set; get; }

        /// <summary>ИНН</summary>
        [OriginalName("2")]
        public string INN { set; get; }

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new Dictionary<string, string>();

        /// <summary>MaxCount</summary>
        [OriginalName("239")]
        public uint? MaxCount { set; get; }

        /// <summary>HiddenCount</summary>
        [OriginalName("240")]
        public uint? HiddenCount { set; get; }
    }
}
