using SH5ApiClient.Data;
using System.Collections;

namespace SH5ApiClient.Models.DTO
{
    internal class MeasureUnits : DataExecutable, IEnumerable<MeasureUnit>
    {
        [OriginalName("205")]
        public MeasureGroup? Group { get; set; }

        [OriginalName("206")]
        private List<MeasureUnit> MeasureUnitsCollection { set; get; } = new List<MeasureUnit>();

        public IEnumerator<MeasureUnit> GetEnumerator() =>
            MeasureUnitsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            MeasureUnitsCollection.GetEnumerator();
    }
}
