namespace SH5ApiClient.Models
{
    /// <summary>Группа единиц измерения</summary>
    [OriginalName("205")]
    public class MeasureGroup
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

        public static IEnumerable<MeasureGroup> GetMGroupsFromSHAnswear(ExecOperationContent answear)
        {
            foreach (Dictionary<string, string>? value in answear.GetValues())
            {
                var correspondent = Parse(value);
                if (correspondent is not null)
                    yield return correspondent;
            }
        }
        public static MeasureGroup? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new MeasureGroup
            {
                Rid = uint.TryParse(value["1"], out uint rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
                BaseMeasureUnit = MeasureUnit.Parse(value.Where(t => t.Key.StartsWith("206\\")).ToDictionary(t => t.Key.TrimStart("206\\"), g => g.Value)),
            };
        }
    }
}
