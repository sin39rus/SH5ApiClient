namespace SH5ApiClient.Models.DTO
{
    /// <summary>Версия комплекта</summary>
    [OriginalName("216")]
    public class DishCompositionVersion
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("2")]
        public ushort? Version { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }


        public static DishCompositionVersion? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new DishCompositionVersion
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Version = ushort.TryParse(value["2"], out ushort version) ? version : null,
                Name = value.GetValueOrDefault("3")
            };
        }
    }
}
