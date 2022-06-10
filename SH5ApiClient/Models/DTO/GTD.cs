namespace SH5ApiClient.Models.DTO
{
    [OriginalName("116")]
    public class GTD
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        public static GTD? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new GTD
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
            };
        }
    }
}
