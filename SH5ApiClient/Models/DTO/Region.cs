using System.Collections.Generic;
using System.Linq;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("232")]
    public class Region
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        public static Region Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Region
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? (uint?)rid: null
            };
        }
    }
}
