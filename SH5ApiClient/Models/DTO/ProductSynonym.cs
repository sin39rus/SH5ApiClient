using System.Globalization;

namespace SH5ApiClient.Models.DTO
{
    /// <summary>Синоним товара</summary>
    [OriginalName("255")]
    public class ProductSynonym
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>CF</summary>
        [OriginalName("41")]
        public decimal? CF { set; get; }

        /// <summary>FullName</summary>
        [OriginalName("22")]
        public string? FullName { set; get; }

        /// <summary>Синоним</summary>
        [OriginalName("244")]
        public SynonymMeasureUnit? SynonymMeasureUnit { set; get; }

        public static ProductSynonym? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new ProductSynonym
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                CF = decimal.TryParse(value.GetValueOrDefault("41"), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal fullName) ? fullName : null,
                FullName = value.GetValueOrDefault("22")
            };
        }


        /* 
               Object 255 {
         // Синоним товара: Rid
         tUint32 1 tsKey
         // Синоним товара: CF, FullName
         tDouble 41
         tBob 22[type:Text]
         Object 244 {
            // Ед. изм.: Name
            tStrP 3[255]
         }
      }
         
         */
    }
}
