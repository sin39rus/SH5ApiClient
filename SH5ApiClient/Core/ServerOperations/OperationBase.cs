using Newtonsoft.Json;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace SH5ApiClient.Core.ServerOperations
{
    public abstract class OperationBase
    {
        public abstract string Uri { get; }
        private const string _errorPattern = @"\d+$";
        private readonly Dictionary<int, string> _errors = ErrorDictionary.GetErrorDictionary();

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errMessage")]
        public string ErrMessage { get; set; }
        internal void CheckError()
        {
            if (ErrorCode != 0) // Возвращается 1 если ответ содержит ошибку
            {
                if (ErrMessage == "Ошибка процедуры библиотеки сервера 81.")
                    throw new ServerOperationsException($"Запрос в API SH закончился ошибкой: {ErrMessage} (Возможно документ в SH заблокирован для редактирования.)");

                Match match = Regex.Match(ErrMessage, _errorPattern);
                if (match.Success && int.TryParse(match.Value, out int errorCode) && _errors.ContainsKey(errorCode))
                    throw new ServerOperationsException($"Запрос в API SH закончился ошибкой ({errorCode}): {_errors[errorCode]}");

                throw new ServerOperationsException($"Запрос в API SH закончился ошибкой: {ErrMessage}");
            }
        }
        /// <summary>Метод выполняемый после операции разбора.</summary>
        internal abstract void AfterParse();

        /// <summary>Разобрать ответ SH</summary>
        /// <param name="jsonText">Содержимое ответа (json)</param>
        /// <returns>Ответ SH</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static T Parse<T>(string jsonText) where T : OperationBase
        {
            if (string.IsNullOrWhiteSpace(jsonText))
                throw new ArgumentException($"\"{nameof(jsonText)}\" не может быть пустым или содержать только пробел.", nameof(jsonText));
            T answear = JsonConvert.DeserializeObject<T>(jsonText);
            if (answear == null)
                throw new ArgumentException("Ошибка разбора ответа SH.");
            answear.CheckError();
            answear.AfterParse();
            return answear;
        }
    }
}