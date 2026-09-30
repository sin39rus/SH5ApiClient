namespace SH5ApiClient.Models.DTO
{
    /// <summary>Информация о сервере SH и подключении к БД.</summary>
    public sealed class ServerInfo
    {
        /// <summary>Версия API.</summary>
        public string ApiVersion { get; set; }

        /// <summary>Тип соединения.</summary>
        public int LinkType { get; set; }

        /// <summary>Адрес хоста.</summary>
        public string Host { get; set; }

        /// <summary>Порт.</summary>
        public int Port { get; set; }

        /// <summary>Строка подключения, пример: "(tcp/ip) 192.168.200.4:7777".</summary>
        public string LinkDisp { get; set; }

        /// <summary>Таймаут.</summary>
        public int Timeout { get; set; }

        /// <summary>Имя пользователя.</summary>
        public string UserName { get; set; }

        /// <summary>Информация о базе данных.</summary>
        public DatabaseInfo Database { get; set; }
    }

    /// <summary>Информация о базе данных StoreHouse.</summary>
    public sealed class DatabaseInfo
    {
        /// <summary>Идентификатор.</summary>
        public string Ident { get; set; }

        /// <summary>Размер.</summary>
        public string Size { get; set; }

        /// <summary>Версия.</summary>
        public string Version { get; set; }
    }
}
