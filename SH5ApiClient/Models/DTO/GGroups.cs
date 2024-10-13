using SH5ApiClient.Data;
using System.Collections;
using System.Collections.Generic;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;

namespace SH5ApiClient.Models.DTO
{
    public class GGroups : DataExecutable, IEnumerable<GGroup>
    {
        [OriginalName("209")]
        private List<GGroup> GGroupsCollection { set; get; } = new List<GGroup>();

        public IEnumerator<GGroup> GetEnumerator() =>
            GGroupsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GGroupsCollection.GetEnumerator();
    }
}
