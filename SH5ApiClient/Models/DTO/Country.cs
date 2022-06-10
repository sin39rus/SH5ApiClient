namespace SH5ApiClient.Models.DTO
{
    [OriginalName("231")]
    public class Country
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        public static Country? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Country
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
            };
        }
    }
}
