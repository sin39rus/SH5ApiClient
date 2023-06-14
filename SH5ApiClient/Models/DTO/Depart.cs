using SH5ApiClient.Data;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Подразделение</summary>
    [OriginalName("106")]
    public class Depart : DataExecutable
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string? Guid { set; get; }

        /// <summary>если не равно 0,то означает, что подразделение имеет общие с пользователем группы</summary>
        [OriginalName("31")]
        public short GeneralGroup { set; get; }

        /// <summary>Атрибуты типа 34</summary>
        [OriginalName("34")]
        public Dictionary<string, string> Attributes34 { set; get; } = new();

        /// <summary>Наименование</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Тип подразделения</summary>
        [OriginalName("8")]
        public DepatmenType? DepatmenType { get; set; }

        /// <summary>битовая маска групп подразделения</summary>
        [OriginalName("32")]
        public string? GroupBitmask { set; get; }

        /// <summary>GDocTypeMask</summary>
        [OriginalName("111")]
        public GDocHeader? GDocHeader { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new();

        /// <summary>Юр. лицо (собственное), куда входит подразделение</summary>
        [OriginalName("102")]
        public LegalEntity? LegalEntity { get; set; }

        /// <summary>Предприятие, куда входит подразделение</summary>
        [OriginalName("103")]
        public Company? Company { get; set; }

        /// <summary>Список обособленных подразделений</summary>
        [OriginalName("114")]
        public List<KPP> KPPs { get; set; }

        /// <summary>Список обособленных подразделений</summary>
        [OriginalName("115")]
        public List<AloLicInfo> AloLicInfos { get; set; }
    }
}
