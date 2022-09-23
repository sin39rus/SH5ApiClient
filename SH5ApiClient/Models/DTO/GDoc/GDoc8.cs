namespace SH5ApiClient.Models.DTO
{
    /// <summary>Расходная накладная</summary>
    public class GDoc8
    {
        /// <summary>Заголовок накладной</summary>
        [OriginalName("111")]
        public GDocHeader? Header { get; set; }

        /// <summary>Содержимое накладной</summary>
        [OriginalName("112")]
        public IEnumerable<GDocItem?>? Content { get; set; }
        public static GDoc8? Parse(ExecOperation answear)
        {
            ExecOperationContent header = answear.GetAnswearContent("111");
            ExecOperationContent content = answear.GetAnswearContent("112");
            return new GDoc8
            {
                Header = GDocHeader.Parse(header.GetValues()[0]),
                Content = content.GetValues().Select(t => GDocItem.Parse(t))
            };
        }
    }
}
