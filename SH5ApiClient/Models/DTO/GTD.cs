﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("116")]
    public class GTD
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string Name { set; get; }

        public static GTD Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new GTD
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? (uint?)rid : null,
                Name = value.GetValueOrDefault("3"),
            };
        }

        internal static IEnumerable<GTD> ParseRange(Dictionary<string, string>[] values) =>
            values.Select(t => Parse(t));
    }
}
