namespace SH5ApiClient.Models
{
    /// <summary>
    /// Параметры подключения к API SH5
    /// </summary>
    [Serializable]
    public class ConnectionParamSH5
    {
        public ConnectionParamSH5(string userName, string password, string address, int port)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"\"{nameof(userName)}\" не может быть пустым или содержать только пробел.", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException($"\"{nameof(address)}\" не может быть пустым или содержать только пробел.", nameof(address));
            }
            if (port < 0 || port > 65535)
            {
                throw new ArgumentException($"\"{nameof(port)}\" не может быть меньше 0 или больше 65535.", nameof(port));
            }
            UserName = userName;
            Password = password;
            Address = address;
            Port = port;
        }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Адрес сервера API
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Порт сервера API
        /// </summary>
        public int Port { get; set; }
    }
}
