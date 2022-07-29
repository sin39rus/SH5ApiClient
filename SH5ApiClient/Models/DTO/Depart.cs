namespace SH5ApiClient.Models.DTO
{
    /// <summary>Подразделение</summary>
    [OriginalName("106")]
    public class Depart
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string? Guid { set; get; }

        /// <summary>Атрибуты типа 34</summary>
        [OriginalName("34")]
        public Dictionary<string, string> Attributes34 { set; get; } = new();

        /// <summary>Наименование</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Тип подразделения</summary>
        [OriginalName("8")]
        public DepatmenType? DepatmenType { get; set; }

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
        public IEnumerable<KPP> KPPs { get; set; } = Array.Empty<KPP>();

        /// <summary>Список обособленных подразделений</summary>
        [OriginalName("115")]
        public IEnumerable<AloLicInfo> AloLicInfos { get; set; } = Array.Empty<AloLicInfo>();

        //ToDo Не описаны 3 поля "31" "111\\8" "32"


        public static IEnumerable<Depart> GetDepartsFromSHAnswear(ExecOperationContent answear)
        {
            foreach (Dictionary<string, string>? value in answear.GetValues())
            {
                var depart = Parse(value);
                if (depart is not null)
                    yield return depart;
            }
        }

        public static Depart? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Depart
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
                Guid = value.GetValueOrDefault("4")?.TrimStart('{').TrimEnd('}'),
                Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\"), g => g.Value),
                Attributes34 = value.Where(t => t.Key.StartsWith("34\\")).ToDictionary(t => t.Key.TrimStart("34\\"), g => g.Value),
                DepatmenType = Enum.TryParse(typeof(DepatmenType), value.GetValueOrDefault("8"), out object? depatmenType) ? (DepatmenType?)depatmenType : null,
                LegalEntity = LegalEntity.Parse(value.Where(t => t.Key.StartsWith("102\\")).ToDictionary(t => t.Key.TrimStart("102\\"), g => g.Value)),
                Company = Company.Parse(value.Where(t => t.Key.StartsWith("103\\")).ToDictionary(t => t.Key.TrimStart("103\\"), g => g.Value))
            };
        }
    }
}
