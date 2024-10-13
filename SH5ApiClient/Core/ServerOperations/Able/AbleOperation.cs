using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SH5ApiClient.Core.ServerOperations
{
    /// <summary>Запрос наличия прав для выполнения процедуры.</summary>
    public sealed class AbleOperation : OperationBase
    {
        [JsonProperty("Version")]
        public string Version { get; private set; }

        [JsonProperty("UserName")]
        public string UserName { get; private set; }

        [JsonProperty("procList")]
        public IEnumerable<string> ProcList { get; private set; } = Array.Empty<string>();

        [JsonProperty("allow")]
        public IEnumerable<bool> Allow { get; private set; } = Array.Empty<bool>();

        public override string Uri => "sh5able";

        /// <summary>Проверить разрешение по имени процедуры.</summary>
        /// <param name="procedureName">Имя процедуры</param>
        /// <returns>true - если пользователю разрешено использовать процедуру.<para>false - если использовать процедуру нельзя.</para></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool CheckPermission(string procedureName)
        {
            int procIndex = ProcList.ToList().IndexOf(procedureName);
            if (procIndex == -1)
                throw new ArgumentOutOfRangeException($"Процедура {procedureName} не найдена.");
            else
                return Allow.ElementAt(procIndex);
        }

        internal override void AfterParse()
        {

        }
    }
}
