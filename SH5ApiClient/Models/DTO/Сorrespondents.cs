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
    internal class Сorrespondents : DataExecutable, IEnumerable<Сorrespondent>
    {
        [OriginalName("107#1")]
        public Сorrespondent Сorrespondent { get; set; }

        [OriginalName("107")]
        private List<Сorrespondent> InnerСorrespondentsCollection { set; get; } = new List<Сorrespondent>();

        public IEnumerator<Сorrespondent> GetEnumerator() =>
            InnerСorrespondentsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            InnerСorrespondentsCollection.GetEnumerator();
    }
}
