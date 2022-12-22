using SH5ApiClient.Data;
using System.Collections;

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
