using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Единица измерения</summary>
    [OriginalName("200")]
    public class Category
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string Name { set; get; }

        internal static Category Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Category
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? (uint?)rid : null,
                Name = value.GetValueOrDefault("3"),
            };
        }
    }
}
