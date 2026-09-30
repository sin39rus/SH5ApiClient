using SH5ApiClient.Data;
using System.Collections;
using System.Collections.Generic;
using SH5ApiClient.Infrastructure.Attributes;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("106")]
    internal class Departs : DataExecutable, IEnumerable<Depart>
    {
        [OriginalName("108")]
        private UnusedTable UnusedTable1 { set; get; }

        [OriginalName("106")]
        private List<Depart> DepartCollection { set; get; } = new List<Depart>();

        [OriginalName("106#1")]
        private UnusedTable UnusedTable2 { set; get; }

        public IEnumerator<Depart> GetEnumerator() =>
            DepartCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            DepartCollection.GetEnumerator();

        private class UnusedTable
        {
            [OriginalName("6")]
            public string Field6 { set; get; }

            [OriginalName("239")]
            public string Field239 { set; get; }
        }
    }
}
