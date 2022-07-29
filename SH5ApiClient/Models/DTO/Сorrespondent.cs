namespace SH5ApiClient.Models.DTO
{
    /// <summary>Корреспондент SH</summary>
    [OriginalName("105")]
    public sealed class Сorrespondent
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>ИНН</summary>
        [OriginalName("2")]
        public string? INN { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string? GUID { set; get; }

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
        public Dictionary<string, string> Attributes6 { set; get; } = new();

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new();

        /// <summary>Атрибуты типа 34</summary>
        [OriginalName("34")]
        public Dictionary<string, string> Attributes34 { set; get; } = new();

        [OriginalName("114")]
        public KPP? KPP { set; get; }

        /// <summary>Cрок оплаты приходов</summary>
        [OriginalName("11")]
        public ushort? PaymentIncomeSpan { set; get; }

        /// <summary>Cрок оплаты расходов</summary>
        [OriginalName("12")]
        public ushort? PaymentExpenseSpan { set; get; }


        public static IEnumerable<Сorrespondent> GetСorrespondentsFromSHAnswear(ExecOperationContent answear)
        {
            foreach (Dictionary<string, string>? value in answear.GetValues())
            {
                var correspondent = Parse(value);
                if (correspondent is not null)
                    yield return correspondent;
            }
        }

        public static Сorrespondent? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Сorrespondent
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                INN = value.GetValueOrDefault("2"),
                Name = value.GetValueOrDefault("3"),
                PaymentIncomeSpan = ushort.TryParse(value.GetValueOrDefault("11"), out ushort paymentIncomeSpan) ? paymentIncomeSpan : null,
                PaymentExpenseSpan = ushort.TryParse(value.GetValueOrDefault("12"), out ushort paymentExpenseSpan) ? paymentExpenseSpan : null,
                GUID = value.GetValueOrDefault("4")?.TrimStart('{').TrimEnd('}'),
                CorrType = Enum.TryParse(typeof(CorrType), value.GetValueOrDefault("5"), out object? corrType) ? (CorrType?)corrType : null,
                CorrTypeEx = Enum.TryParse(typeof(CorrTypeEx), value.GetValueOrDefault("32"), out object? corrTypeEx) ? (CorrTypeEx?)corrTypeEx : null,
                SubType = Enum.TryParse(typeof(CorrType3), value.GetValueOrDefault("31"), out object? subType) ? (CorrType3?)subType : null,
                Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\"), g => g.Value),
                Attributes7 = value.Where(t => t.Key.StartsWith("7\\")).ToDictionary(t => t.Key.TrimStart("7\\"), g => g.Value),
                Attributes34 = value.Where(t => t.Key.StartsWith("34\\")).ToDictionary(t => t.Key.TrimStart("34\\"), g => g.Value),
                KPP = KPP.Parse(value.Where(t => t.Key.StartsWith("114\\")).ToDictionary(t => t.Key.TrimStart("114\\"), g => g.Value))
            };
        }

    }
}
