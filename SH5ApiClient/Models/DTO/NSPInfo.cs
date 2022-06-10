namespace SH5ApiClient.Models.DTO
{
    [OriginalName("213")]
    public class NSPInfo
    {
        [OriginalName("9")]
        public uint? Rate { get; set; }
        public static NSPInfo? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new NSPInfo
            {
                Rate = uint.TryParse(value.GetValueOrDefault("40"), out uint rate) ? rate : null
            };
        }
    }
}
