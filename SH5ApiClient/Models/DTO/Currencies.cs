using SH5ApiClient.Data;
using System.Collections;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("100")]
    public class Currencies : DataExecutable, IEnumerable<Currency>
    {
        [OriginalName("100")]
        public List<Currency> CurrencyCollection { get; set; } = new List<Currency>();

        public IEnumerator<Currency> GetEnumerator() =>
            CurrencyCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            CurrencyCollection.GetEnumerator();
    }
}
