using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Models.Enums;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("210")]
    public class GDoc0Item
    {
        public GDoc0Item(uint rid, decimal amount, decimal purchaseSum, uint measureUnitRid)
        {
            Rid = rid;
            Amount = amount;
            PurchaseSum = purchaseSum;
            MeasureUnitRid = measureUnitRid;
        }
        [OriginalName("210\\1")]
        public uint Rid { get; private set; }

        [OriginalName("210\\206\\1")]
        public uint MeasureUnitRid { get; private set; }

        /// <summary>Ставка НДС</summary>
        [OriginalName("212\\9")]
        public uint NdsRateValue { get; set; }

        /// <summary>Ставка НСП</summary>
        [OriginalName("213\\9")]
        public uint NspRateValue { get; set; }

        /// <summary>Закупочная сумма</summary>
        [OriginalName("40")]
        public decimal PurchaseSum { get; private set; }

        /// <summary>Сумма НДС</summary>
        [OriginalName("41")]
        public decimal VatSum { get; set; }

        /// <summary>Сумма НСП</summary>
        [OriginalName("42")]
        public decimal NspSum { get; set; }

        /// <summary>Количество</summary>
        [OriginalName("31")]
        public decimal Amount { get; private set; }

        /// <summary>ГТД RID</summary>
        [OriginalName("116\\1")]
        public uint? GtdRid { get; set; }

        /// <summary>Опции спецификаций накладных</summary>
        [OriginalName("32")]
        public InvoiceSpecOptions Options { get; set; } = InvoiceSpecOptions.GdsoActive;

    }

}
