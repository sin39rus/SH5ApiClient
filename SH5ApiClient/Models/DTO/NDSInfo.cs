using SH5ApiClient.Data;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("212")]
    public class NDSInfo : DataExecutable
    {
        [OriginalName("9")]
        public uint? Rate { get; set; }
        public static NDSInfo Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new NDSInfo
            {
                Rate = uint.TryParse(value.GetValueOrDefault("9"), out uint rate) ? (uint?)rate : null
            };
        }
    }
}
