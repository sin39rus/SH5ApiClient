using SH5ApiClient.Data;
using System.Collections;

namespace SH5ApiClient.Models.DTO.GDoc
{
    internal class GDocs : DataExecutable, IEnumerable<GDocHeader>
    {
        [OriginalName("111")]
        private List<GDocHeader> GDocsCollection { set; get; } = new List<GDocHeader>();

        [OriginalName("108")]
        public Ignore? Ignore1 { set; get; }

        public IEnumerator<GDocHeader> GetEnumerator() =>
            GDocsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GDocsCollection.GetEnumerator();

        public class Ignore
        {
            [OriginalName("1")]
            public DateTime From { set; get; }

            [OriginalName("2")]
            public DateTime To { set; get; }

            [OriginalName("6")]
            public uint Flags { set; get; }

            [OriginalName("111")]
            public Ignore2? Ignore2 { set; get; }

            [OriginalName("100")]
            public Currency? Currency { set; get; }

            [OriginalName("107")]
            public Сorrespondent? Сorrespondent { set; get; } 

            [OriginalName("107#1")]
            public Сorrespondent? Сorrespondent2 { set; get; }

            /// <summary>Создатель</summary>
            [OriginalName("109")]
            public User? Creator { set; get; }
        }

        [OriginalName("111")]
        public class Ignore2
        {
            [OriginalName("6")]
            public uint Flags { set; get; }

            [OriginalName("8")]
            public uint Ignore { set; get; }
        }
    }
}
