namespace SH5ApiClient.Models.DTO
{
    /// <summary>Спецификация контрагента</summary>
    [OriginalName("114")]
    public class CorrespondentSpecification
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public int? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new();

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new();

        /// <summary>Контрагент</summary>
        [OriginalName("107")]
        public Сorrespondent? Сorrespondent { set; get; }

        public static CorrespondentSpecification? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new CorrespondentSpecification
            {
                Rid = int.TryParse(value.GetValueOrDefault("1"), out int rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
                Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\".ToCharArray()), g => g.Value),
                Attributes7 = value.Where(t => t.Key.StartsWith("7\\")).ToDictionary(t => t.Key.TrimStart("7\\".ToCharArray()), g => g.Value),
                Сorrespondent = Сorrespondent.Parse(value.Where(t => t.Key.StartsWith("107\\")).ToDictionary(t => t.Key.TrimStart("107\\"), g => g.Value))
            };
        }
    }
}
