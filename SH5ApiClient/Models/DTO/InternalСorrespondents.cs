using SH5ApiClient.Data;
using System.Collections;

namespace SH5ApiClient.Models.DTO
{
    public class InternalСorrespondents : DataExecutable, IEnumerable<InternalСorrespondent>
    {
        [OriginalName("102#1")] 
        public InternalСorrespondent? InnerСorrespondent { get; set; }

        [OriginalName("102")]
        private List<InternalСorrespondent> InternalСorrespondentsCollection { set; get; } = new List<InternalСorrespondent>();

        public IEnumerator<InternalСorrespondent> GetEnumerator() =>
            InternalСorrespondentsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            InternalСorrespondentsCollection.GetEnumerator();
    }
}
