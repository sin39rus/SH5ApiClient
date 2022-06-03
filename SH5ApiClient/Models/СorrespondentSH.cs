

namespace SH5ApiClient.Models
{
    /// <summary>
    /// Корреспондент
    /// </summary>
    public class СorrespondentSH
    {
        /// <summary>
        /// Rid
        /// </summary>
        [OriginalName("1")]
        public int Rid { set; get; }
        /// <summary>
        /// Name
        /// </summary>
        [OriginalName("3")]
        public string? Name { set; get; }
        /// <summary>
        /// ИНН
        /// </summary>
        [OriginalName("2")]
        public string? INN { set; get; }
        /// <summary>
        /// GUID
        /// </summary>
        [OriginalName("4")]
        public string? GUID { set; get; }
        /// <summary>
        /// Тип1
        /// </summary>
        [OriginalName("5")]
        public CorrType? CorrType { set; get; }
        /// <summary>
        /// Тип2
        /// </summary>
        [OriginalName("32")]
        public CorrTypeEx? CorrTypeEx { set; get; }
        public static IEnumerable<СorrespondentSH> GetСorrespondentsFromSHAnswear(SHExecAnswearContent answear)
        {
            foreach (Dictionary<string, string>? value in answear.GetValues())
            {
                yield return Parse(value);
            }
        }

        public static СorrespondentSH Parse(Dictionary<string, string> value)
        {
            return new СorrespondentSH
            {
                Rid = int.TryParse(value["1"], out int rid) ? rid : 0,
                Name = value["3"],
                INN = value["2"],
                GUID = value["4"].TrimStart('{').TrimEnd('}'),
                CorrType = Enum.TryParse(typeof(CorrType), value["5"], out object? corrType) ? (CorrType?)corrType : null,
                CorrTypeEx = Enum.TryParse(typeof(CorrTypeEx), value["32"], out object? corrTypeEx) ? (CorrTypeEx?)corrTypeEx : null
            };
        }

    }
}
