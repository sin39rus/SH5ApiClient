namespace SH5ApiClient.Models.DTO
{
    /// <summary>Товар</summary>
    [OriginalName("210")]
    public class Product
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new();

        /// <summary>Вид алк.продукции</summary>
        [OriginalName("201")]
        public AlcoholProductType? AlcoholProductType { get; set; }

        /// <summary>Вид алк.продукции</summary>
        [OriginalName("114")]
        public CorrespondentSpecification? CorrespondentSpecification { set; get; }

        /// <summary>Единица измерения</summary>
        [OriginalName("206")]
        public MeasureUnit? MeasureUnit { get; set; }

        /// <summary>Синоним товара</summary>
        [OriginalName("255")]
        public ProductSynonym? ProductSynonym { set; get; }


        public static Product? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new Product
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                Name = value.GetValueOrDefault("3"),
                Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\".ToCharArray()), g => g.Value),
                AlcoholProductType = AlcoholProductType.Parse(value.Where(t => t.Key.StartsWith("201\\")).ToDictionary(t => t.Key.TrimStart("201\\"), g => g.Value)),
                CorrespondentSpecification = CorrespondentSpecification.Parse(value.Where(t => t.Key.StartsWith("114\\")).ToDictionary(t => t.Key.TrimStart("114\\"), g => g.Value)),
                MeasureUnit = MeasureUnit.Parse(value.Where(t => t.Key.StartsWith("206\\")).ToDictionary(t => t.Key.TrimStart("206\\"), g => g.Value)),
                ProductSynonym = ProductSynonym.Parse(value.Where(t => t.Key.StartsWith("255\\")).ToDictionary(t => t.Key.TrimStart("255\\"), g => g.Value))
            };
        }
    }
}
