namespace SH5ApiClient.Models.DTO
{
    /// <summary>Возврат поставщику</summary>
    public class GDoc5
    {
        /// <summary>Заголовок накладной</summary>
        [OriginalName("111")]
        public GDocHeader? Header { get; set; }

        /// <summary>Содержимое накладной</summary>
        [OriginalName("112")]
        public IEnumerable<GDocItem?>? Content { get; set; }
        public static GDoc5? Parse(ExecOperation answear)
        {
            ExecOperationContent header = answear.GetAnswearContent("111");
            ExecOperationContent content = answear.GetAnswearContent("112");
            return new GDoc5
            {
                Header = GDocHeader.Parse(header.GetValues()[0]),
                Content = content.GetValues().Select(t => GDocItem.Parse(t))
            };
        }
    }
}
