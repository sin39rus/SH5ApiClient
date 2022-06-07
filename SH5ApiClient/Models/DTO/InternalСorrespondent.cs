namespace SH5ApiClient.Models.DTO
{
    /// <summary>Внутренний корреспондент SH</summary>
    public sealed class InternalСorrespondent
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public int Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>ИНН</summary>
        [OriginalName("2")]
        public string? INN { set; get; }

        /// <summary>GUID</summary>
        [OriginalName("4")]
        public string? GUID { set; get; }

        internal static IEnumerable<InternalСorrespondent> GetСorrespondentsFromSHAnswear(ExecOperationContent answear)
        {
            foreach (var value in answear.GetValues())
            {
                yield return new InternalСorrespondent
                {
                    Rid = int.TryParse(value["1"], out int rid) ? rid : 0,
                    Name = value["3"],
                    INN = value["2"],
                    GUID = value["4"].TrimStart('{').TrimEnd('}')
                };
            }
        }
    }
}
