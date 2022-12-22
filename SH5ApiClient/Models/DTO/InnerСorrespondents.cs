using SH5ApiClient.Data;
using System.Collections;

namespace SH5ApiClient.Models.DTO
{
    public class InnerСorrespondents : DataExecutable, IEnumerable<InnerСorrespondent>
    {
        [OriginalName("102#1")] 
        public InnerСorrespondent? InnerСorrespondent { get; set; }

        [OriginalName("102")]
        private List<InnerСorrespondent> InnerСorrespondentsCollection { set; get; } = new List<InnerСorrespondent>();

        public IEnumerator<InnerСorrespondent> GetEnumerator() =>
            InnerСorrespondentsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            InnerСorrespondentsCollection.GetEnumerator();
    }
}
