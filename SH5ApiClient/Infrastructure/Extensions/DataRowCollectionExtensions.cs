using System.Collections.Generic;
using System.Data;

namespace SH5ApiClient.Infrastructure.Extensions
{
    public static class DataRowCollectionExtensions
    {
        public static void AddRange(this DataRowCollection collection, IEnumerable<DataRow> rows)
        {
            foreach(DataRow row in rows)
                collection.Add(row);
        }
    }
}
