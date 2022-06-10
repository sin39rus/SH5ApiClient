namespace SH5ApiClient.Models.DTO
{
    /// <summary>Синоним товара</summary>
    [OriginalName("255")]
    public class ProductSynonym
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>FullName</summary>
        [OriginalName("41")]
        public double? FullName { set; get; } //ToDo: Не понял почему этот тип double

        public static ProductSynonym? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new ProductSynonym
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                FullName = double.TryParse(value.GetValueOrDefault("41"), out double fullName) ? fullName : null,
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
