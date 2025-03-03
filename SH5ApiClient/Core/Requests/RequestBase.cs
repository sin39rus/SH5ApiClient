﻿using Newtonsoft.Json;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Models;
using System;
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
namespace SH5ApiClient.Core.Requests
{
    public abstract class RequestBase
    {
        private RequestBase() { }

        [JsonIgnore]
        public ConnectionParamSH5 ConnectionParam { private set; get; }
        [JsonIgnore]
        public abstract OperationBase Operation { get; }

        /// <summary>
        /// Запрос в SH API
        /// </summary>
        /// <param name="procName">Имя выполняемой процедуры</param>
        /// <exception cref="ArgumentException"></exception>
        protected RequestBase(string procName, ConnectionParamSH5 connectionParam) : this(connectionParam)
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
        protected RequestBase(ConnectionParamSH5 connectionParam)
        {
            ConnectionParam = connectionParam;
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
