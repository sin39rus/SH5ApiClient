using SH5ApiClient.Data;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Расходная накладная</summary>
    public class GDoc8 : DataExecutable
    {
        /// <summary>Заголовок накладной</summary>
        [OriginalName("111")]
        public GDocHeader? Header { get; set; }

        /// <summary>Содержимое накладной</summary>
        [OriginalName("112")]
        public List<GDocItem?>? Content { get; set; }
    }
}
