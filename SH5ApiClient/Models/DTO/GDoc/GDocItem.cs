using System.Globalization;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Содержимое накладной</summary>
    [OriginalName("112")]
    public class GDocItem
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Ставка НДС</summary>
        [OriginalName("212")] // GDoc4 212#1
        public NDSInfo? PurchaseNDS { set; get; }

        /// <summary>Ставка НСП</summary>
        [OriginalName("213")] // GDoc4 213#1
        public NSPInfo? PurchaseNSP { set; get; }

        /// <summary>Ставка НДС Расходная накладная</summary>
        [OriginalName("212#1")]
        public NDSInfo? SaleNDS { set; get; }

        /// <summary>Ставка НСП Расходная накладная</summary>
        [OriginalName("213#1")]
        public NSPInfo? SaleNSP { set; get; }

        /// <summary>Товар (Расход в акте переработки)</summary>
        [OriginalName("210")]
        public GoodsItem? GoodsItem { get; set; }

        /// <summary>Товар (Приход в акте переработки)</summary>
        [OriginalName("112#1")]
        public GDocItem? GDocItemComing { get; set; }

        /// <summary>Страна</summary>
        [OriginalName("231")]
        public Country? Country { set; get; }

        /// <summary>Грузовая таможенная декларация (ГТД)</summary>
        [OriginalName("116")]
        public GTD? GTD { set; get; }

        /// <summary>Количество </summary>
        [OriginalName("31")]
        public decimal Quantity { set; get; }

        /// <summary>Опции спецификаций накладных </summary>
        [OriginalName("32")]
        public uint? Options { set; get; } //ToDo: Тип который надо определить

        /// <summary>Количество взвешенного (в гр)</summary>
        [OriginalName("74")]
        public decimal? AmountWeighed { set; get; }

        /// <summary>Закупочная сумма без налогов</summary>
        [OriginalName("40")]
        public decimal Currency40 { set; get; }

        /// <summary>Закупочная сумма НДС</summary>
        [OriginalName("41")]
        public decimal Currency41 { set; get; }

        /// <summary>Закупочная сумма НСП</summary>
        [OriginalName("42")]
        public decimal Currency42 { set; get; }

        /// <summary>Отпускная сумма без налогов</summary>
        [OriginalName("45")]
        public decimal Currency45 { set; get; }

        /// <summary>Отпускная сумма НДС</summary>
        [OriginalName("46")]
        public decimal Currency46 { set; get; }

        /// <summary>Отпускная сумма НСП</summary>
        [OriginalName("47")]
        public decimal Currency47 { set; get; }

        /// <summary>Компенсированное количество и суммы (сумма, НДС, НСП)</summary>
        [OriginalName("67")]
        public decimal? Currency67 { get; set; }

        /// <summary>Компенсирующая сумма без налогов</summary>
        [OriginalName("68")]
        public decimal? Currency68 { set; get; }

        /// <summary>Компенсирующая сумму НДС</summary>
        [OriginalName("69")]
        public decimal? Currency69 { set; get; }

        /// <summary>Компенсирующая сумму НСП</summary>
        [OriginalName("70")]
        public decimal? Currency70 { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new();

        /// <summary>Связанная накладная (возврат поставщику)</summary>
        [OriginalName("111#1")]
        public GDocHeader? RelatedInvoice { set; get; }
        public static GDocItem? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new GDocItem
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                Options = uint.TryParse(value.GetValueOrDefault("32"), out uint options) ? options : null,
                GoodsItem = GoodsItem.Parse(value.Where(t => t.Key.StartsWith("210\\")).ToDictionary(t => t.Key.TrimStart("210\\"), g => g.Value)),
                GDocItemComing = Parse(value.Where(t => t.Key.StartsWith("112#1\\")).ToDictionary(t => t.Key.TrimStart("112#1\\"), g => g.Value)),
                GTD = GTD.Parse(value.Where(t => t.Key.StartsWith("116\\")).ToDictionary(t => t.Key.TrimStart("116\\"), g => g.Value)),
                Country = Country.Parse(value.Where(t => t.Key.StartsWith("231\\")).ToDictionary(t => t.Key.TrimStart("231\\"), g => g.Value)),
                PurchaseNDS = NDSInfo.Parse(value.Where(t => t.Key.StartsWith("212\\")).ToDictionary(t => t.Key.TrimStart("212\\"), g => g.Value)),
                SaleNDS = NDSInfo.Parse(value.Where(t => t.Key.StartsWith("212#1\\")).ToDictionary(t => t.Key.TrimStart("212#1\\"), g => g.Value)),
                PurchaseNSP = NSPInfo.Parse(value.Where(t => t.Key.StartsWith("213\\")).ToDictionary(t => t.Key.TrimStart("213\\"), g => g.Value)),
                SaleNSP = NSPInfo.Parse(value.Where(t => t.Key.StartsWith("213#1\\")).ToDictionary(t => t.Key.TrimStart("213#1\\"), g => g.Value)),
                Quantity = decimal.Parse(value.GetValueOrDefault("31") ?? "0", CultureInfo.InvariantCulture),
                Currency40 = decimal.Parse(value.GetValueOrDefault("40") ?? "0", CultureInfo.InvariantCulture),
                Currency41 = decimal.Parse(value.GetValueOrDefault("41") ?? "0", CultureInfo.InvariantCulture),
                Currency42 = decimal.Parse(value.GetValueOrDefault("42") ?? "0", CultureInfo.InvariantCulture),
                Currency45 = decimal.Parse(value.GetValueOrDefault("45") ?? "0", CultureInfo.InvariantCulture),
                Currency46 = decimal.Parse(value.GetValueOrDefault("46") ?? "0", CultureInfo.InvariantCulture),
                Currency47 = decimal.Parse(value.GetValueOrDefault("47") ?? "0", CultureInfo.InvariantCulture),
                Currency67 = decimal.TryParse(value.GetValueOrDefault("67"), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal currency67) ? currency67 : null,
                Currency68 = decimal.TryParse(value.GetValueOrDefault("68"), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal currency68) ? currency68 : null,
                Currency69 = decimal.TryParse(value.GetValueOrDefault("69"), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal currency69) ? currency69 : null,
                Currency70 = decimal.TryParse(value.GetValueOrDefault("70"), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal currency70) ? currency70 : null,
                Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\".ToCharArray()), g => g.Value),
                AmountWeighed = decimal.TryParse(value.GetValueOrDefault("74"), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amountWeighed) ? amountWeighed : null,
                RelatedInvoice = GDocHeader.Parse(value.Where(t => t.Key.StartsWith("111#1\\")).ToDictionary(t => t.Key.TrimStart("111#1\\"), g => g.Value)),
            };
        }
    }
}
