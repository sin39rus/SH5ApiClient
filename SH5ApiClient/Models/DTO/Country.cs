using System.Collections.Generic;
using System.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("231")]
    public class Country
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string Name { set; get; }

        public static Country Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Country
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? (uint?)rid : null,
                Name = value.GetValueOrDefault("3"),
            };
        }
    }
}
