using SH5ApiClient.Data;

namespace SH5ApiClient.Models
{
    /// <summary>Группа единиц измерения</summary>
    [OriginalName("205")]
    public class MeasureGroup : DataExecutable
    {
        /// <summary>Rid уникальный идентификатор</summary>
        [OriginalName("1")]
        public uint? Rid { get; set; }

        /// <summary>Наименование группы</summary>
        [OriginalName("3")]
        public string? Name { get; set; }

        /// <summary>Базовая единица измерения</summary>
        [OriginalName("206")]
        public MeasureUnit? BaseMeasureUnit { set; get; }
    }
}
