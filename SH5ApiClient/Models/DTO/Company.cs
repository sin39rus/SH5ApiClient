namespace SH5ApiClient.Models.DTO
{
    /// <summary>Предприятие</summary>
    [OriginalName("103")]
    public class Company
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        public static Company? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Company
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
            };
        }
    }

}
