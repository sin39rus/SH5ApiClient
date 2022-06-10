namespace SH5ApiClient.Models.DTO
{
    /// <summary>Единица измерения</summary>
    [OriginalName("206")]
    public class MeasureUnit
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        public static MeasureUnit? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new MeasureUnit
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
            };
        }
    }
}
