using System.Globalization;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Содержимое накладной</summary>
    /// 
    [OriginalName("112")]
    public class GDocItem
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Ставка НДС</summary>
        [OriginalName("212")]
        public NDSInfo? NDSInfo { set; get; }

        /// <summary>Ставка НСП</summary>
        [OriginalName("213")]
        public NSPInfo? NSPInfo { set; get; }

        /// <summary>Товар</summary>
        [OriginalName("210")]
        public Product? Product { get; set; }

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
        public decimal AmountWeighed { set; get; }

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

        /// <summary>Компенсирующая сумма без налогов</summary>
        [OriginalName("68")]
        public decimal Currency68 { set; get; }

        /// <summary>Компенсирующая сумму НДС</summary>
        [OriginalName("69")]
        public decimal Currency69 { set; get; }

        /// <summary>Компенсирующая сумму НСП</summary>
        [OriginalName("70")]
        public decimal Currency70 { set; get; }

        public static GDocItem? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new GDocItem
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                Options = uint.TryParse(value.GetValueOrDefault("32"), out uint options) ? options : null,
                Product = Product.Parse(value.Where(t => t.Key.StartsWith("210\\")).ToDictionary(t => t.Key.TrimStart("210\\"), g => g.Value)),
                GTD = GTD.Parse(value.Where(t => t.Key.StartsWith("116\\")).ToDictionary(t => t.Key.TrimStart("116\\"), g => g.Value)),
                Country = Country.Parse(value.Where(t => t.Key.StartsWith("231\\")).ToDictionary(t => t.Key.TrimStart("231\\"), g => g.Value)),
                NDSInfo = NDSInfo.Parse(value.Where(t => t.Key.StartsWith("212\\")).ToDictionary(t => t.Key.TrimStart("212\\"), g => g.Value)),
                NSPInfo = NSPInfo.Parse(value.Where(t => t.Key.StartsWith("213\\")).ToDictionary(t => t.Key.TrimStart("213\\"), g => g.Value)),
                Quantity = decimal.Parse(value.GetValueOrDefault("31") ?? "0", CultureInfo.InvariantCulture),
                Currency40 = decimal.Parse(value.GetValueOrDefault("40") ?? "0", CultureInfo.InvariantCulture),
                Currency41 = decimal.Parse(value.GetValueOrDefault("41") ?? "0", CultureInfo.InvariantCulture),
                Currency42 = decimal.Parse(value.GetValueOrDefault("42") ?? "0", CultureInfo.InvariantCulture),
                Currency45 = decimal.Parse(value.GetValueOrDefault("45") ?? "0", CultureInfo.InvariantCulture),
                Currency46 = decimal.Parse(value.GetValueOrDefault("46") ?? "0", CultureInfo.InvariantCulture),
                Currency47 = decimal.Parse(value.GetValueOrDefault("47") ?? "0", CultureInfo.InvariantCulture),
                Currency68 = decimal.Parse(value.GetValueOrDefault("68") ?? "0", CultureInfo.InvariantCulture),
                Currency69 = decimal.Parse(value.GetValueOrDefault("69") ?? "0", CultureInfo.InvariantCulture),
                Currency70 = decimal.Parse(value.GetValueOrDefault("70") ?? "0", CultureInfo.InvariantCulture),
                AmountWeighed = decimal.Parse(value.GetValueOrDefault("74") ?? "0", CultureInfo.InvariantCulture)

                //Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                //GUID = value.GetValueOrDefault("4")?.TrimStart('{').TrimEnd('}'),
                //TTNType = Enum.TryParse(typeof(TTNType), value.GetValueOrDefault("5"), out object? ttnType) ? (TTNType?)ttnType : null,
                //TTNOptions = Enum.TryParse(typeof(TTNOptions), value.GetValueOrDefault("33"), out object? ttnOptions) ? (TTNOptions?)ttnOptions : null,
                //DateStamp1 = uint.TryParse(value.GetValueOrDefault("32"), out uint dateStamp1) ? dateStamp1 : null,
                //Name = value.GetValueOrDefault("3"),
                //Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\".ToCharArray()), g => g.Value),
                //Supplier = Сorrespondent.Parse(value.Where(t => t.Key.StartsWith("105\\")).ToDictionary(t => t.Key.TrimStart("105\\"), g => g.Value)),
                //Recipient = Сorrespondent.Parse(value.Where(t => t.Key.StartsWith("105#1\\")).ToDictionary(t => t.Key.TrimStart("105#1\\"), g => g.Value)),
                //Currency = Currency.Parse(value.Where(t => t.Key.StartsWith("100\\")).ToDictionary(t => t.Key.TrimStart("100\\"), g => g.Value)),
                //DateStamp = DateTime.TryParse(value.GetValueOrDefault("31"), out DateTime dateStamp) ? dateStamp : null,
                //CourceBase = double.TryParse(value.GetValueOrDefault("34"), out double courceBase) ? courceBase : null,
                //CourceInvoice = double.TryParse(value.GetValueOrDefault("35"), out double courceInvoice) ? courceInvoice : null,
                //DueDate = DateTime.TryParse(value.GetValueOrDefault("38"), out DateTime dueDate) ? dueDate : null,
                //Invoice = Invoice.Parse(value.Where(t => t.Key.StartsWith("117\\")).ToDictionary(t => t.Key.TrimStart("117\\"), g => g.Value)),
                //BuhOperation = BuhOperation.Parse(value.Where(t => t.Key.StartsWith("179\\")).ToDictionary(t => t.Key.TrimStart("179\\"), g => g.Value)),
                //Сontract = Contract.Parse(value.Where(t => t.Key.StartsWith("172\\")).ToDictionary(t => t.Key.TrimStart("172\\"), g => g.Value)),
                //PaymentAmount = decimal.TryParse(value.GetValueOrDefault("53"), out decimal paymentAmount) ? paymentAmount : null,
                //MinActiveDate = DateTime.TryParse(value.GetValueOrDefault("38"), out DateTime minActiveDate) ? minActiveDate : null,
                //Creator = User.Parse(value.Where(t => t.Key.StartsWith("109\\")).ToDictionary(t => t.Key.TrimStart("109\\"), g => g.Value)),
                //LastUpdater = User.Parse(value.Where(t => t.Key.StartsWith("109#1\\")).ToDictionary(t => t.Key.TrimStart("109#1\\"), g => g.Value))
            };
        }
    }
}
