using SH5ApiClient.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;

namespace SH5ApiClient.Models.DTO.GDoc
{
    internal class GDocs : DataExecutable, IEnumerable<GDocHeader>
    {
        [OriginalName("111")]
        private List<GDocHeader> GDocsCollection { set; get; } = new List<GDocHeader>();

        [OriginalName("108")]
        public FilterHead Filter { set; get; }

        public IEnumerator<GDocHeader> GetEnumerator() =>
            GDocsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GDocsCollection.GetEnumerator();

        public class FilterHead
        {
            [OriginalName("1")]
            public DateTime From { set; get; }

            [OriginalName("2")]
            public DateTime To { set; get; }

            [OriginalName("6")]
            public uint Flags { set; get; }

            [OriginalName("111")]
            public FilterFlags FlagsDetail { set; get; }

            [OriginalName("100")]
            public Currency Currency { set; get; }

            [OriginalName("107")]
            public Correspondent Correspondent { set; get; }

            [OriginalName("107#1")]
            public Correspondent Correspondent2 { set; get; }

            /// <summary>Создатель</summary>
            [OriginalName("109")]
            public User Creator { set; get; }
        }

        [OriginalName("111")]
        public class FilterFlags
        {
            [OriginalName("6")]
            public uint Flags { set; get; }

            [OriginalName("8")]
            public uint UnusedFlags { set; get; }
        }
    }
}
