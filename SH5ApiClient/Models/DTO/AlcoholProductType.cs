namespace SH5ApiClient.Models.DTO
{
    /// <summary>Тип алкогольной продукции</summary>
    [OriginalName("201")]
    public class AlcoholProductType
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Flags</summary>
        [OriginalName("42")]
        public byte? Flags { set; get; } //ToDo: Типизированный объект, найти описание

        /// <summary>Code</summary>
        [OriginalName("2")]
        public string? Code { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        public static AlcoholProductType Parse(Dictionary<string, string> value)
        {
            return new AlcoholProductType
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                Flags = byte.TryParse(value.GetValueOrDefault("42"), out byte flags) ? flags : null,
                Code = value.GetValueOrDefault("2"),
                Name = value.GetValueOrDefault("3")
            };
        }
    }
}
