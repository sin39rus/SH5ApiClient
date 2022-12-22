using SH5ApiClient.Data;
using System.Collections;

namespace SH5ApiClient.Models.DTO
{
    internal class Сorrespondents : DataExecutable, IEnumerable<Сorrespondent>
    {
        [OriginalName("107#1")]
        public Сorrespondent? Сorrespondent { get; set; }

        [OriginalName("107")]
        private List<Сorrespondent> InnerСorrespondentsCollection { set; get; } = new List<Сorrespondent>();

        public IEnumerator<Сorrespondent> GetEnumerator() =>
            InnerСorrespondentsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            InnerСorrespondentsCollection.GetEnumerator();
    }
}
