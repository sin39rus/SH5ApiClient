namespace SH5ApiClient.Models.DTO
{
    [OriginalName("102")]
    public class LegalEntity
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>ИНН</summary>
        [OriginalName("2")]
        public string? INN { set; get; }

        public static LegalEntity? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new LegalEntity
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                INN = value.GetValueOrDefault("2"),
                Name = value.GetValueOrDefault("3")
            };
        }
    }
}
