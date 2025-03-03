﻿using Newtonsoft.Json;

namespace SH5ApiClient.Core.ServerOperations
{
    /// <summary>
    /// Информация о базе данных
    /// </summary>
    public sealed class InfoOperationContent
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [JsonProperty("Ident")]
        public string Ident { get; set; }
        /// <summary>
        /// Размер
        /// </summary>
        [JsonProperty("Size")]
        public string Size { get; set; }
        /// <summary>
        /// Версия
        /// </summary>
        [JsonProperty("Version")]
        public string Version { get; set; }
    }
}
