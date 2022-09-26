namespace SH5ApiClient.Models.DTO
{
    /// <summary>КПП контрагента</summary>
    [OriginalName("114")]
    public class KPP
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>КПП по умолчанию</summary>
        [OriginalName("9")]
        public bool IsDefault { set; get; }

        /// <summary>Регион</summary>
        [OriginalName("232")]
        public Region? Region { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Внешний</summary>
        [OriginalName("34")]
        public string? ExtCode { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new();

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new();

        /// <summary>Атрибуты типа 35</summary>
        [OriginalName("35")]
        public Dictionary<string, string> Attributes35 { set; get; } = new();

        /// <summary>Контрагент</summary>
        [OriginalName("107")]
        public Сorrespondent? Сorrespondent { set; get; }

        /// <summary>Контрагент</summary>
        [OriginalName("105")]
        public Сorrespondent? Сorrespondent2 { set; get; }

        public static IEnumerable<KPP> GetKPPsFromSHAnswear(ExecOperationContent answear)
        {
            foreach (Dictionary<string, string>? value in answear.GetValues())
            {
                var kpp = Parse(value);
                if (kpp is not null)
                    yield return kpp;
            }
        }

        public static KPP? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new KPP
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                IsDefault = int.TryParse(value.GetValueOrDefault("9"), out int tmp) && tmp == 2,
                Region = Region.Parse(value.Where(t => t.Key.StartsWith("232\\")).ToDictionary(t => t.Key.TrimStart("232\\"), g => g.Value)),
                Name = value.GetValueOrDefault("3"),
                ExtCode = value.GetValueOrDefault("34"),
                Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\".ToCharArray()), g => g.Value),
                Attributes7 = value.Where(t => t.Key.StartsWith("7\\")).ToDictionary(t => t.Key.TrimStart("7\\".ToCharArray()), g => g.Value),
                Attributes35 = value.Where(t => t.Key.StartsWith("35\\")).ToDictionary(t => t.Key.TrimStart("35\\".ToCharArray()), g => g.Value),
                Сorrespondent = Сorrespondent.Parse(value.Where(t => t.Key.StartsWith("107\\")).ToDictionary(t => t.Key.TrimStart("107\\"), g => g.Value)),
                Сorrespondent2 = Сorrespondent.Parse(value.Where(t => t.Key.StartsWith("105\\")).ToDictionary(t => t.Key.TrimStart("105\\"), g => g.Value))
            };
        }
    }
}
