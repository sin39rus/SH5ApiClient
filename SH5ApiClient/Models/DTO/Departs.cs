using SH5ApiClient.Data;
using System.Collections;

namespace SH5ApiClient.Models.DTO
{
    [OriginalName("106")]
    public class Departs : DataExecutable, IEnumerable<Depart>
    {
        [OriginalName("108")]
        private Ignore? Ignore1 { set; get; }
        
        [OriginalName("106")]
        private List<Depart> DepartCollection { set; get; } = new List<Depart>();

        [OriginalName("106#1")]
        private Ignore? Ignore2 { set; get; }
        public IEnumerator<Depart> GetEnumerator() =>
            DepartCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            DepartCollection.GetEnumerator();

        public class Ignore
        {
            [OriginalName("6")]
            public string Temp1 { set; get; }

            [OriginalName("239")]
            public string Temp2 { set; get; }
        }
    }
}
