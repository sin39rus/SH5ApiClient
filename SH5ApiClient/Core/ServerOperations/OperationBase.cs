using Newtonsoft.Json;
using SH5ApiClient.Infrastructure.Exceptions;
using System;

namespace SH5ApiClient.Core.ServerOperations
{
    public abstract class OperationBase
    {
        public abstract string Uri { get; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("errMessage")]
        public string ErrMessage { get; set; }
        internal void CheckError()
        {
            if (ErrorCode != 0)
            {
                if (ErrMessage == "Ошибка процедуры библиотеки сервера 81.")
                    throw new ServerOperationsException($"Запрос в API SH закончился ошибкой: {ErrMessage} (Возможно документ в SH заблокирован для редактирования.)");
                if(ErrorCode == 1007)
                    throw new ServerOperationsException($"У товара не найдена единица измерения литр.");
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