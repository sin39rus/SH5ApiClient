using System.Collections.Generic;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Валюта</summary>
    [OriginalName("100")]
    public class Currency
    {
        /// <summary>Rid уникальный идентификатор</summary>
        [OriginalName("1")]
        public uint? Rid { get; set; }

        /// <summary>Код валюты</summary>
        [OriginalName("2")]
        public string Code { get; set; }

        /// <summary>Наименование валюты</summary>
        [OriginalName("3")]
        public string Name { get; set; }

        /// <summary>Guid</summary>
        [OriginalName("4")]
        public string GUID { set; get; }

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new Dictionary<string, string>();
    }
}
