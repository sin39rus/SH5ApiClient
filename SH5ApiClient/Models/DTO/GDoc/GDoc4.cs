using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Расходная накладная</summary>
    public class GDoc4
    {
        /// <summary>Заголовок накладной</summary>
        [OriginalName("111")]
        public GDocHeader? Header { get; set; }

        /// <summary>Содержимое накладной</summary>
        [OriginalName("112")]
        public IEnumerable<GDocItem?>? Content { get; set; }
        public static GDoc4? Parse(ExecOperation answear)
        {
            ExecOperationContent header = answear.GetAnswearContent("111");
            ExecOperationContent content = answear.GetAnswearContent("112");
            return new GDoc4
            {
                Header = GDocHeader.Parse(header.GetValues()[0]),
                Content = content.GetValues().Select(t => GDocItem.Parse(t))
            };
        }
    }
}
