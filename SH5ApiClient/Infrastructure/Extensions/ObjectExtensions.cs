using System.Reflection;

namespace SH5ApiClient.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static string GetOriginalNameAttributeFromField(this object obj, string nameFiled)
        {
            FieldInfo filed = obj.GetType().GetField(nameFiled, BindingFlags.NonPublic | BindingFlags.Instance)
                ?? throw new ArgumentNullException(nameof(nameFiled), $"Объект \"{nameof(obj)}\" не содержит поля \"{nameof(nameFiled)}\"");
            var attributes = filed.GetCustomAttributes(typeof(OriginalNameAttribute), false);
            if (attributes.Length == 0)
                throw new ArgumentException($"Поле \"{nameof(nameFiled)}\" не содержит атрибут \"SHOriginalName\"");
            OriginalNameAttribute attribute = (OriginalNameAttribute)attributes[0];
            return attribute.OriginalName;
        }
        public static string GetOriginalNameAttributeFromProperty(this object obj, string propertyName)
        {
            PropertyInfo prop = obj.GetType().GetProperty(propertyName)
                ?? throw new ArgumentNullException(nameof(propertyName), $"Объект \"{nameof(obj)}\" не содержит свойства \"{nameof(propertyName)}\"");
            OriginalNameAttribute attribute = prop.GetCustomAttribute(typeof(OriginalNameAttribute)) as OriginalNameAttribute
                ?? throw new ArgumentNullException(nameof(propertyName), $"Свойство \"{nameof(propertyName)}\" не содержит атрибут \"SHOriginalName\"");
            return attribute.OriginalName;
        }
    }
}
