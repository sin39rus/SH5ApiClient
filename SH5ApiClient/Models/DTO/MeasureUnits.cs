using SH5ApiClient.Data;
using SH5ApiClient.Infrastructure.Attributes;
using System.Collections;
using System.Collections.Generic;

namespace SH5ApiClient.Models.DTO
{
    internal class MeasureUnits : DataExecutable, IEnumerable<MeasureUnit>
    {
        [OriginalName("210")]
        private GoodsItem GoodsItem { set; get; }

        [OriginalName("205")]
        public MeasureGroup Group { get; set; }

        [OriginalName("206")]
        private List<MeasureUnit> MeasureUnitsCollection { set; get; } = new List<MeasureUnit>();

        public IEnumerator<MeasureUnit> GetEnumerator() =>
            MeasureUnitsCollection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            MeasureUnitsCollection.GetEnumerator();
    }
}
