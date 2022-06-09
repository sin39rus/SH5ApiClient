namespace SH5ApiClient.Models.DTO
{
    /// <summary>Договор</summary>
    [OriginalName("172")]
    public class Contract
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Type</summary>
        [OriginalName("5")]
        public ushort? Type { set; get; }

        /// <summary>Options</summary>
        [OriginalName("33")]
        public ushort? Options { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Date</summary>
        [OriginalName("31")]
        public DateTime? Date { set; get; }

        /// <summary>PLimit</summary>
        [OriginalName("51")]
        public ushort? PLimit { set; get; }

        /// <summary>PDow</summary>
        [OriginalName("52")]
        public ushort? PDow { set; get; }


        public static Contract? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Contract
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                Type = ushort.TryParse(value.GetValueOrDefault("5"), out ushort type) ? type : null,
                Options = ushort.TryParse(value.GetValueOrDefault("33"), out ushort options) ? options : null,
                Name = value.GetValueOrDefault("3"),
                Date = DateTime.TryParse(value.GetValueOrDefault("31"), out DateTime date) ? date : null,
                PLimit = ushort.TryParse(value.GetValueOrDefault("51"), out ushort pLimit) ? pLimit : null,
                PDow = ushort.TryParse(value.GetValueOrDefault("52"), out ushort pDow) ? pDow : null,
            };
        }
    }
}
