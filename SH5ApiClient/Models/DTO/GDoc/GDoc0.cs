namespace SH5ApiClient.Models.DTO
{
    /// <summary>Приходная накладная</summary>
    public class GDoc0
    {
        /// <summary>Заголовок накладной</summary>
        [OriginalName("111")]
        public GDoc? Header { get; set; }

        /// <summary>Содержимое накладной</summary>
        [OriginalName("112")]
        public IEnumerable<GDocItem?>? Content { get; set; }

        public static GDoc0? Parse(ExecOperation answear)
        {
            ExecOperationContent header = answear.GetAnswearContent("111");
            ExecOperationContent content = answear.GetAnswearContent("112");
            return new GDoc0
            {
                Header = GDoc.Parse(header.GetValues()[0]),
                Content = content.GetValues().Select(t=> GDocItem.Parse(t))
            };
        }
    }
}
