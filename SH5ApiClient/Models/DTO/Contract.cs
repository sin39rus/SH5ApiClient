namespace SH5ApiClient.Models.DTO
{
    /// <summary>Договор</summary>
    public class Contract
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public int? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Date</summary>
        [OriginalName("31")]
        public DateTime? Date { set; get; }

        public static Contract? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Contract
            {
                Rid = int.TryParse(value.GetValueOrDefault("1"), out int rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
                Date = DateTime.TryParse(value.GetValueOrDefault("31"), out DateTime date) ? date : null
            };
        }
    }
}
