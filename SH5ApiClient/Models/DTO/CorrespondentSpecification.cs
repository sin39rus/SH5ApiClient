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

        /// <summary>Контрагент</summary>
        [OriginalName("105")]
        public Сorrespondent? Сorrespondent { set; get; }

        public static CorrespondentSpecification? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new CorrespondentSpecification
            {
                Rid = int.TryParse(value.GetValueOrDefault("1"), out int rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
                Сorrespondent = Сorrespondent.Parse(value.Where(t => t.Key.StartsWith("105\\")).ToDictionary(t => t.Key.TrimStart("105\\"), g => g.Value))
            };
        }
    }
}
