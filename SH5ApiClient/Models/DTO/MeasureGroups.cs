using SH5ApiClient.Data;
using System.Collections;
using System.Collections.Generic;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    public class MeasureGroups : DataExecutable, IEnumerable<MeasureGroup>
    {
        [OriginalName("205")]
        private List<MeasureGroup> MGroupsCollection { set; get; } = new List<MeasureGroup>();

        public IEnumerator<MeasureGroup> GetEnumerator() =>
            MGroupsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            MGroupsCollection.GetEnumerator();
    }
}
