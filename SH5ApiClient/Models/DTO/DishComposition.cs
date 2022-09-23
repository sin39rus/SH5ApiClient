namespace SH5ApiClient.Models.DTO
{
    /// <summary>Комплект</summary>
    [OriginalName("215")]
    public class DishComposition
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Версия комплекта</summary>
        [OriginalName("216")]
        public DishCompositionVersion? DishCompositionVersion { get; set; }
        public static DishComposition? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new DishComposition
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
                DishCompositionVersion = DishCompositionVersion.Parse(value.Where(t => t.Key.StartsWith("216\\")).ToDictionary(t => t.Key.TrimStart("216\\"), g => g.Value)),
            };
        }
    }
}
