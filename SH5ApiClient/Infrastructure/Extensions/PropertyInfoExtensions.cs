using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Infrastructure.Extensions
{
    internal static class PropertyInfoExtensions
    {
        /// <summary>Содержит ли свойство атрибут OriginalName</summary>
        /// <param name="prop">Свойство</param>
        /// <returns>true - Если свойство содержит атрибут OriginalName
        /// <para>false - Если свойство не содержит артибут OriginalName</para></returns>
        internal static bool ContainsOriginalName(this PropertyInfo prop) =>
            prop.GetCustomAttribute<OriginalNameAttribute>() != null;
        public static string GetOriginalName(this PropertyInfo prop)
        {
            OriginalNameAttribute attribute = prop.GetCustomAttribute(typeof(OriginalNameAttribute)) as OriginalNameAttribute
                ?? throw new ArgumentNullException(nameof(prop), $"Свойство \"{nameof(prop.Name)}\" не содержит атрибут \"SHOriginalName\"");
            return attribute.OriginalName;
        }
    }
}
