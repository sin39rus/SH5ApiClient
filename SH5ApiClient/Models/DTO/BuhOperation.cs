using System.Collections.Generic;
using System.Linq;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Бух. операция</summary>
    [OriginalName("179")]
    public class BuhOperation
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string Name { set; get; }

        public static BuhOperation Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new BuhOperation
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? (uint?)rid : null,
                Name = value.GetValueOrDefault("3")
            };
        }
    }
}
