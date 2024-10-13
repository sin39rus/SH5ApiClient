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
    [OriginalName("213")]
    public class NSPInfo
    {
        [OriginalName("9")]
        public uint? Rate { get; set; }
        public static NSPInfo Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new NSPInfo
            {
                Rate = uint.TryParse(value.GetValueOrDefault("9"), out uint rate) ? (uint?)rate : null
            };
        }
    }
}
