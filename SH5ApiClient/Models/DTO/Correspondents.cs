using SH5ApiClient.Data;
using System.Collections;
using System.Collections.Generic;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;

namespace SH5ApiClient.Models.DTO
{
    internal class Correspondents : DataExecutable, IEnumerable<Correspondent>
    {
        [OriginalName("107#1")]
        public Correspondent Correspondent { get; set; }

        [OriginalName("107")]
        private List<Correspondent> InnerCorrespondentsCollection { set; get; } = new List<Correspondent>();

        public IEnumerator<Correspondent> GetEnumerator() =>
            InnerCorrespondentsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            InnerCorrespondentsCollection.GetEnumerator();
    }
}
