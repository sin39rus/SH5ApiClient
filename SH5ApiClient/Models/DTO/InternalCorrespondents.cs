using SH5ApiClient.Data;
using System.Collections;
using System.Collections.Generic;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    internal class InternalCorrespondents : DataExecutable, IEnumerable<InternalCorrespondent>
    {
        [OriginalName("102#1")] 
        public InternalCorrespondent InnerCorrespondent { get; set; }

        [OriginalName("102")]
        private List<InternalCorrespondent> InternalCorrespondentsCollection { set; get; } = new List<InternalCorrespondent>();

        public IEnumerator<InternalCorrespondent> GetEnumerator() =>
            InternalCorrespondentsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            InternalCorrespondentsCollection.GetEnumerator();
    }
}
