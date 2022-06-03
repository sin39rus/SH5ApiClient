using System.Globalization;

namespace SH5ApiClient.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static object AnyParse(this string value, Type type)
        {

            if (type == typeof(decimal) || type == typeof(decimal?))
                return decimal.Parse(value, CultureInfo.InvariantCulture);
            if (type == typeof(int))
                return int.Parse(value);
            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return DateTime.Parse(value);
            if (type == typeof(string))
                return value;
            throw new ArgumentException($"Тип {type.Name} не распознан.");
        }
        public static bool IsINN(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                return false;
            if (value.Length != 10 && value.Length != 12)
                return false;
            if (value.Any(c => !char.IsDigit(c)))
                return false;
            return true;
        }
    }
}
