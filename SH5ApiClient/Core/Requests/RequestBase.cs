using Newtonsoft.Json;
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
namespace SH5ApiClient.Core.Requests
{
    public abstract class RequestBase
    {
        private RequestBase() { }

        [JsonIgnore]
        public ConnectionParamSH5 ConnectionParam { private set; get; }
        [JsonIgnore]
        public ServerOperationType ServerOperationType { private set; get; }

        /// <summary>
        /// Запрос в SH API
        /// </summary>
        /// <param name="procName">Имя выполняемой процедуры</param>
        /// <exception cref="ArgumentException"></exception>
        protected RequestBase(string procName, ConnectionParamSH5 connectionParam, ServerOperationType operationType) : this(connectionParam, operationType)
        {
            if (string.IsNullOrEmpty(procName))
                throw new ArgumentException($"\"{nameof(procName)}\" не может быть пустым или содержать только пробел.", nameof(procName));
            ProcName = procName;

        }
        /// <summary>
        /// Запрос в SH API
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <exception cref="ArgumentException"></exception>
        protected RequestBase(ConnectionParamSH5 connectionParam, ServerOperationType operationType)
        {
            ConnectionParam = connectionParam;
            ServerOperationType = operationType;
            UserName = connectionParam.UserName;
            Password = connectionParam.Password;
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [JsonProperty("UserName")]
        [OriginalName("UserName")]
        public string UserName { get; private set; } = string.Empty;

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [JsonProperty("Password")]
        [OriginalName("Password")]
        public string Password { get; private set; } = string.Empty;

        /// <summary>
        /// Имя выполняемой процедуры
        /// </summary>
        [JsonProperty("procName")]
        [OriginalName("procName")]
        public string ProcName { get; private protected set; } = string.Empty;

        /// <summary>
        /// Создать запрос API
        /// </summary>
        /// <returns></returns>
        public abstract string CreateJsonRequest();
    }
}
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
