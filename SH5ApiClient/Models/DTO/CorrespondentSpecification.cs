namespace SH5ApiClient.Models.DTO
{
    /// <summary>Спецификация контрагента</summary>
    public class CorrespondentSpecification
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public int? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        public static CorrespondentSpecification? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new CorrespondentSpecification
            {
                Rid = int.TryParse(value.GetValueOrDefault("1"), out int rid) ? rid : null,
                Name = value.GetValueOrDefault("3")
            };
        }
    }
}
