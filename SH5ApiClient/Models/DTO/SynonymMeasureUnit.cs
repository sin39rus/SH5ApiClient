﻿using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models.Enums;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Единица измерения для синонима</summary>
    [OriginalName("244")]
    public class SynonymMeasureUnit
    {
        /// <summary>Name</summary>
        [OriginalName("3")]
        public string FullName { set; get; }
    }
}
