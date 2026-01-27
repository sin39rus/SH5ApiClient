using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Data;
using SH5ApiClient.Infrastructure.Attributes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SH5ApiClient.Models.DTO.Reports
{
    public class GDocsExReport : DataExecutable
    {
        [OriginalName("111")]
        private ReadOnlyCollection<GDocHeader> Headers { set; get; } = new ReadOnlyCollection<GDocHeader>(new List<GDocHeader>());

        [OriginalName("112")]
        private ReadOnlyCollection<GDocItem> Content { set; get; } = new ReadOnlyCollection<GDocItem>(new List<GDocItem>());

        public static GDocsExReport Parse(ExecOperation answear)
        {
            ExecOperationContent header = answear.GetAnswearContent("111");
            ExecOperationContent content = answear.GetAnswearContent("112");
            return new GDocsExReport
            {
                Headers = new ReadOnlyCollection<GDocHeader>(header.GetValues().Select(t => GDocHeader.Parse(t)).ToList()),
                Content = new ReadOnlyCollection<GDocItem>(content.GetValues().Select(t => GDocItem.Parse(t)).ToList())
            };
        }
    }
}
