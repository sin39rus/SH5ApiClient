using SH5ApiClient.Data;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Внутреннее перемещение</summary>
    public class GDoc11 : DataExecutable
    {
        /// <summary>Заголовок накладной</summary>
        [OriginalName("111")]
        public GDocHeader? Header { get; set; }

        /// <summary>Содержимое накладной</summary>
        [OriginalName("112")]
        public List<GDocItem?>? Content { get; set; }
        public static GDoc11? Parse(ExecOperation answear)
        {
            ExecOperationContent header = answear.GetAnswearContent("111");
            ExecOperationContent content = answear.GetAnswearContent("112");
            return new GDoc11
            {
                Header = GDocHeader.Parse(header.GetValues()[0]),
                Content = content.GetValues().Select(t => GDocItem.Parse(t)).ToList()
            };
        }
    }
}
