namespace SH5ApiClient.Models.DTO
{
    public class GDoc
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public int Rid { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string? GUID { set; get; }

        /// <summary>Тип накладной</summary>
        [OriginalName("5")]
        public TTNType? TTNType { set; get; }

        /// <summary>Опции накладной</summary>
        [OriginalName("33")]
        public TTNOptions? TTNOptions { set; get; }

        /// <summary>DateStamp1</summary>
        [OriginalName("32")]
        public uint? DateStamp1 { set; get; }

        /// <summary>Дата документа</summary>
        [OriginalName("31")]
        public DateTime? DateStamp { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new();

        /// <summary>Поставщик</summary>
        [OriginalName("105")]
        public Сorrespondent? Supplier { set; get; }

        /// <summary>Получатель</summary>
        [OriginalName("105#1")]
        public Сorrespondent? Recipient { set; get; }

        /// <summary>Финансовая информация</summary>
        [OriginalName("112")]
        public FinancialInfo? FinancialInfo { set; get; }

        /// <summary>Счет-фактура</summary>
        [OriginalName("117")]
        public Invoice? Invoice { set; get; }

        /// <summary>Бухгалтерская операция</summary>
        [OriginalName("179")]
        public BuhOperation? BuhOperation { set; get; }

        /// <summary>Договор</summary>
        [OriginalName("172")]
        public Contract? Сontract { set; get; }

        /// <summary>Сумма оплаты 
        /// <para>Так и не разобрался где фигурирует это поле</para></summary> //ToDo:
        [OriginalName("53")]
        public decimal? PaymentAmount { set; get; }

        public static IEnumerable<GDoc> GetGDocsFromSHAnswear(ExecOperationContent answear)
        {
            foreach (Dictionary<string, string>? value in answear.GetValues())
            {
                var gDoc = Parse(value);
                if (gDoc is not null)
                    yield return gDoc;
            }
        }
        public static GDoc Parse(Dictionary<string, string> value)
        {
            return new GDoc
            {
                Rid = int.TryParse(value["1"], out int rid) ? rid : 0,
                GUID = value.GetValueOrDefault("4")?.TrimStart('{').TrimEnd('}'),
                TTNType = Enum.TryParse(typeof(TTNType), value.GetValueOrDefault("5"), out object? ttnType) ? (TTNType?)ttnType : null,
                TTNOptions = Enum.TryParse(typeof(TTNOptions), value.GetValueOrDefault("33"), out object? ttnOptions) ? (TTNOptions?)ttnOptions : null,
                DateStamp1 = uint.TryParse(value.GetValueOrDefault("32"), out uint dateStamp1) ? dateStamp1 : null,
                Name = value.GetValueOrDefault("3"),
                Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\".ToCharArray()), g => g.Value),
                Supplier = Сorrespondent.Parse(value.Where(t => t.Key.StartsWith("105\\")).ToDictionary(t => t.Key.TrimStart("105\\"), g => g.Value)),
                Recipient = Сorrespondent.Parse(value.Where(t => t.Key.StartsWith("105#1\\")).ToDictionary(t => t.Key.TrimStart("105#1\\"), g => g.Value)),
                DateStamp = DateTime.TryParse(value.GetValueOrDefault("31"), out DateTime dateStamp) ? dateStamp : null,
                FinancialInfo = FinancialInfo.Parse(value.Where(t => t.Key.StartsWith("112\\")).ToDictionary(t => t.Key.TrimStart("112\\"), g => g.Value)),
                Invoice = Invoice.Parse(value.Where(t => t.Key.StartsWith("117\\")).ToDictionary(t => t.Key.TrimStart("117\\"), g => g.Value)),
                BuhOperation = BuhOperation.Parse(value.Where(t => t.Key.StartsWith("179\\")).ToDictionary(t => t.Key.TrimStart("179\\"), g => g.Value)),
                Сontract = Contract.Parse(value.Where(t => t.Key.StartsWith("179\\")).ToDictionary(t => t.Key.TrimStart("179\\"), g => g.Value)),
                PaymentAmount = decimal.TryParse(value.GetValueOrDefault("53"), out decimal paymentAmount) ? paymentAmount : null
            };
        }
    }
}
