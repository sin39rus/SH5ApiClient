namespace SH5ApiClient.Models
{
    /// <summary>Валюта</summary>
    [OriginalName("100")]
    public class Currency
    {
        /// <summary>Rid уникальный идентификатор</summary>
        [OriginalName("1")]
        public uint? Rid { get; set; }

        /// <summary>Код валюты</summary>
        [OriginalName("2")]
        public string? Code { get; set; }

        /// <summary>Наименование валюты</summary>
        [OriginalName("3")]
        public string? Name { get; set; }

        /// <summary>Guid</summary>
        [OriginalName("4")]
        public string? GUID { set; get; }

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new();

        public static IEnumerable<Currency> GetCurrenciesFromSHAnswear(ExecOperationContent answear)
        {
            foreach (Dictionary<string, string>? value in answear.GetValues())
            {
                var correspondent = Parse(value);
                if (correspondent is not null)
                    yield return correspondent;
            }
        }
        public static Currency? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Currency
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Code = value.GetValueOrDefault("2"),
                Name = value.GetValueOrDefault("3"),
                GUID = value.GetValueOrDefault("4")?.TrimStart('{').TrimEnd('}'),
                Attributes7 = value.Where(t => t.Key.StartsWith("7\\")).ToDictionary(t => t.Key.TrimStart("7\\".ToCharArray()), g => g.Value)
            };
        }
    }
}
