using System;
using System.Collections.Generic;
using System.Linq;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Результат проверки прав на выполнение процедур.</summary>
    public sealed class ProcedurePermissions
    {
        private readonly IReadOnlyDictionary<string, bool> _permissions;

        /// <summary>Версия ответа.</summary>
        public string Version { get; }

        /// <summary>Имя пользователя.</summary>
        public string UserName { get; }

        /// <summary>Имена проверенных процедур.</summary>
        public IEnumerable<string> ProcedureNames => _permissions.Keys;

        internal ProcedurePermissions(string version, string userName, IEnumerable<string> procedureNames, IEnumerable<bool> allow)
        {
            Version = version;
            UserName = userName;
            var names = procedureNames?.ToList() ?? new List<string>();
            var flags = allow?.ToList() ?? new List<bool>();
            var map = new Dictionary<string, bool>(StringComparer.Ordinal);
            for (int i = 0; i < names.Count; i++)
                map[names[i]] = i < flags.Count && flags[i];
            _permissions = map;
        }

        /// <summary>Проверить разрешение по имени процедуры.</summary>
        /// <param name="procedureName">Имя процедуры</param>
        /// <returns>true, если пользователю разрешено использовать процедуру; иначе false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Процедура не входила в запрос проверки.</exception>
        public bool CheckPermission(string procedureName)
        {
            if (!_permissions.TryGetValue(procedureName, out bool allowed))
                throw new ArgumentOutOfRangeException(nameof(procedureName), $"Процедура {procedureName} не найдена.");
            return allowed;
        }
    }
}
