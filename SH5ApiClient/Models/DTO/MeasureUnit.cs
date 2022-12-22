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

        /// <summary>Группа единицы измерения</summary>
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
    }
}
