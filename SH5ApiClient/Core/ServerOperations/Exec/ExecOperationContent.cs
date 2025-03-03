﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace SH5ApiClient.Core.ServerOperations
{
    /// <summary>
    /// Блок данных
    /// </summary>
    public sealed class ExecOperationContent
    {
        //Данные блока
        private Dictionary<string, string>[] _values = Array.Empty<Dictionary<string, string>>();

        /// <summary>
        /// Заголовок данных
        /// </summary>
        [JsonProperty("head")]
        public string Head { get; private set; } = string.Empty;

        /// <summary>
        /// Количество записей в ответе
        /// </summary>
        [JsonProperty("recCount")]
        public int RecCount { get; private set; } = -1;

        /// <summary>
        /// Оригинальные поля SH
        /// </summary>
        [JsonProperty("original")]
        public string[] Original { get; private set; } = Array.Empty<string>();

        /// <summary>
        /// Наименование полей SH
        /// </summary>
        [JsonProperty("fields")]
        public string[] Fields { get; private set; } = Array.Empty<string>();

        /// <summary>
        /// Значение полей SH
        /// </summary>
        [JsonProperty("values")]
        public object[][] Values { get; private set; } = Array.Empty<object[]>();

        /// <summary>
        /// Получить данные запроса
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string>[] GetValues()
        {
            return _values;
        }
        /// <summary>
        /// Получить индекс поля
        /// </summary>
        /// <param name="originalName"></param>
        /// <returns>Индекс параметра OriginalName</returns>
        public int GetIndexOriginalName(string originalName)
        {
            int index = Original.ToList().IndexOf(originalName);
            if (index == -1)
                throw new Exception($"Параметр {originalName} не найден.");
            else
                return index;
        }

        /// <summary>
        /// Метод выполняемый после десериализации
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (RecCount == -1)
                RecCount = Values[0].Length;
            TransformValues();
        }

        /// <summary>
        /// Преобразование данных для удобста работы.
        /// </summary>
        private void TransformValues()
        {
            _values = new Dictionary<string, string>[RecCount];
            for (int x = 0; x < RecCount; x++)
            {
                _values[x] = new Dictionary<string, string>();
                for (int y = 0; y < Original.Length; y++)
                    _values[x].Add(Original[y], Values[y][x]?.ToString());
            }
        }
    }

}
