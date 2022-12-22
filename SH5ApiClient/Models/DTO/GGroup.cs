namespace SH5ApiClient.Models.DTO
{
    /// <summary>Товарная группа</summary>
    [OriginalName("209")]
    public class GGroup
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string? GUID { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new();

        /// <summary>Флаги</summary>
        [OriginalName("42")]
        public GoodsItemFlags? Flags { set; get; }

        /// <summary>Группа предок</summary>
        [OriginalName("209#1")]
        public GGroup? Parent { set; get; }

        //public static GGroup? Parse(Dictionary<string, string> value)
        //{
        //    if (!value.Any())
        //        return null;
        //    return new GGroup
        //    {
        //        Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
        //        Name = value.GetValueOrDefault("3"),
        //        GUID = value.GetValueOrDefault("4")?.TrimStart('{').TrimEnd('}'),
        //        Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\".ToCharArray()), g => g.Value),
        //        Parent = Parse(value.Where(t => t.Key.StartsWith("209#1\\")).ToDictionary(t => t.Key.TrimStart("209#1\\"), g => g.Value))
        //    };
        //}


    }
}
