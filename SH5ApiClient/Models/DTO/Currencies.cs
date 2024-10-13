using SH5ApiClient.Data;
using System.Collections;
using System.Collections.Generic;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("100")]
    public class Currencies : DataExecutable, IEnumerable<Currency>
    {
        [OriginalName("100")]
        private List<Currency> CurrencyCollection { get; set; } = new List<Currency>();

        public IEnumerator<Currency> GetEnumerator() =>
            CurrencyCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            CurrencyCollection.GetEnumerator();
    }
}
