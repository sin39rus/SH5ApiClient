using System.Globalization;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Единица измерения</summary>
    [OriginalName("206")]
    public class MeasureUnit
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Коэффициент пересчета в базовую для  группы единицу изм.</summary>
        [OriginalName("41")]
        public decimal? BaseRatio { set; get; }

        /// <summary>Коэффициент пересчета в базовую для  группы единицу изм.</summary>
        [OriginalName("205")]
        public MeasureGroup? MeasureGroup { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string? GUID { set; get; }

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new();

        /// <summary>Flags</summary>
        [OriginalName("10")]
        public bool? IsBase { set; get; }

        /// <summary>Flags</summary>
        [OriginalName("42")]
        public byte? Flags { set; get; } //ToDo: Типизированный объект, найти описание
        public static MeasureUnit? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new MeasureUnit
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
                BaseRatio = decimal.TryParse(value.GetValueOrDefault("41"), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amountWeighed) ? amountWeighed : null,
                MeasureGroup = MeasureGroup.Parse(value.Where(t => t.Key.StartsWith("205\\")).ToDictionary(t => t.Key.TrimStart("205\\"), g => g.Value)),
                GUID = value.GetValueOrDefault("4")?.TrimStart('{').TrimEnd('}'),
                Attributes7 = value.Where(t => t.Key.StartsWith("7\\")).ToDictionary(t => t.Key.TrimStart("7\\".ToCharArray()), g => g.Value),
                IsBase = value.GetValueOrDefault("10") is null || string.IsNullOrEmpty(value.GetValueOrDefault("10")) ? null : value.GetValueOrDefault("10") == "1",
                Flags = byte.TryParse(value.GetValueOrDefault("42"), out byte flags) ? flags : null,
            };
        }

        public static IEnumerable<MeasureUnit> ParseMUnits(ExecOperationContent answear)
        {
            foreach (Dictionary<string, string>? value in answear.GetValues())
            {
                var depart = Parse(value);
                if (depart is not null)
                    yield return depart;
            }
        }
    }
}
