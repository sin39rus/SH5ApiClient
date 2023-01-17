using SH5ApiClient.Data;
using System.Collections;

namespace SH5ApiClient.Models.DTO.GDoc
{
    internal class GDocs : DataExecutable, IEnumerable<GDocHeader>
    {
        [OriginalName("205")]
        private List<GDocHeader> GDocsCollection { set; get; } = new List<GDocHeader>();

        public IEnumerator<GDocHeader> GetEnumerator() =>
            GDocsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GDocsCollection.GetEnumerator();
    }
}
