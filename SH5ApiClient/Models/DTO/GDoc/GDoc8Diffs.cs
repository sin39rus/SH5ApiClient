﻿using SH5ApiClient.Data;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Сличительная ведомость излишки/недостачи</summary>
    public class GDoc8Diffs : DataExecutable
    {
        /// <summary>Заголовок накладной</summary>
        [OriginalName("111")]
        public GDocHeader? Header { get; set; }

        /// <summary>Содержимое накладной</summary>
        [OriginalName("112")]
        public List<GDocItem?>? Content { get; set; }
    }
}
