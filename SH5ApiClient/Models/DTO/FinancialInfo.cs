using System.Globalization;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Финансовая информация</summary>
    public class FinancialInfo
    {
        /// <summary>Закупочная сумма без налогов</summary>
        public double Currency40 { set; get; }
        /// <summary>Закупочная сумма НДС</summary>
        public double Currency41 { set; get; }
        /// <summary>Закупочная сумма НСП</summary>
        public double Currency42 { set; get; }
        /// <summary>Отпускная сумма без налогов</summary>
        public double Currency45 { set; get; }
        /// <summary>Отпускная сумма НДС</summary>
        public double Currency46 { set; get; }
        /// <summary>Отпускная сумма НСП</summary>
        public double Currency47 { set; get; }
        /// <summary>Компенсирующая сумма без налогов</summary>
        public double Currency68  { set; get; }
        /// <summary>Компенсирующая сумму НДС</summary>
        public double Currency69 { set; get; }
        /// <summary>Компенсирующая сумму НСП</summary>
        public double Currency70 { set; get; }
        public static FinancialInfo? Parse(Dictionary<string, string> value)
        {
            if(!value.Any())
                return null;
            return new FinancialInfo
            {
                Currency40 = double.Parse(value.GetValueOrDefault("40") ?? "0", CultureInfo.InvariantCulture),
                Currency41 = double.Parse(value.GetValueOrDefault("41") ?? "0", CultureInfo.InvariantCulture),
                Currency42 = double.Parse(value.GetValueOrDefault("42") ?? "0", CultureInfo.InvariantCulture),
                Currency45 = double.Parse(value.GetValueOrDefault("45") ?? "0", CultureInfo.InvariantCulture),
                Currency46 = double.Parse(value.GetValueOrDefault("46") ?? "0", CultureInfo.InvariantCulture),
                Currency47 = double.Parse(value.GetValueOrDefault("47") ?? "0", CultureInfo.InvariantCulture),
                Currency68 = double.Parse(value.GetValueOrDefault("68") ?? "0", CultureInfo.InvariantCulture),
                Currency69 = double.Parse(value.GetValueOrDefault("69") ?? "0", CultureInfo.InvariantCulture),
                Currency70 = double.Parse(value.GetValueOrDefault("70") ?? "0", CultureInfo.InvariantCulture)
            };
        }
    }
}
