using SH5ApiClient.Data;
using System.Collections.Generic;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models.Enums;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Расходная накладная</summary>
    public class GDoc4 : DataExecutable
    {
        /// <summary>Заголовок накладной</summary>
        [OriginalName("111")]
        public GDocHeader Header { get; set; }

        /// <summary>Содержимое накладной</summary>
        [OriginalName("112")]
        public List<GDocItem> Content { get; set; }
    }
}
