namespace SH5ApiClient.Models
{
    /// <summary>Валюта</summary>
    [OriginalName("100")]
    public class Currency
    {
        [OriginalName("1")]
        public uint? Rid { get; set; }


        [OriginalName("2")]
        public string? Code { get; set; }


        [OriginalName("3")]
        public string? Name { get; set; }


        public static Currency? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Currency
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Code = value.GetValueOrDefault("2"),
                Name = value.GetValueOrDefault("3")
            };
        }
    }
}
