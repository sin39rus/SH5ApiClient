namespace SH5ApiClient.Models.DTO
{
    /// <summary>Счет-фактура</summary>
    [OriginalName("117")]
    public class Invoice
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Type</summary>
        [OriginalName("5")]
        public uint? Type { set; get; } //ToDo: Возможно есть класификатор типов для счета фактуры.

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Date</summary>
        [OriginalName("31")]
        public DateTime? Date { set; get; }


        public static Invoice? Parse(Dictionary<string, string> value)
        {
            if(!value.Any())
                return null;
            return new Invoice
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                Type = uint.TryParse(value.GetValueOrDefault("5"), out uint type) ? type : null,
                Name = value.GetValueOrDefault("3"),
                Date = DateTime.TryParse(value.GetValueOrDefault("31"), out DateTime date) ? date : null
            };
        }
    }
}
