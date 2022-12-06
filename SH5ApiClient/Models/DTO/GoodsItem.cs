namespace SH5ApiClient.Models.DTO
{
    /// <summary>Товар</summary>
    [OriginalName("210")]
    public class GoodsItem
    {
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Guid</summary>
        [OriginalName("4")]
        public string? GUID { set; get; }

        /// <summary>Флаги</summary>
        [OriginalName("42")]
        public GoodsItemFlags? Flags { set; get; }

        /// <summary>Срок годности (часов) для сертифицируемых</summary>
        [OriginalName("59")]
        public uint? ExpirationDateForCertified { set; get; }

        /// <summary>Доп.Параметы<para>Отсутствует информация в документации</para></summary>
        [OriginalName("26")]
        public uint? AdditionalParameters { set; get; }

        /// <summary>Тип товара</summary>
        [OriginalName("25")]
        public GoodsItemType? Type { set; get; }

        /// <summary>Процент погрешности при инвентаризации</summary>
        [OriginalName("52")]
        public decimal? PercentageErrorInInventory { set; get; }

        /// <summary>процент обработки 1</summary>
        [OriginalName("50")]
        public decimal? ProcessingPercentage1 { set; get; }

        /// <summary>процент обработки 2</summary>
        [OriginalName("51")]
        public decimal? ProcessingPercentage2 { set; get; }

        /// <summary>Минимальный запас</summary>
        [OriginalName("77")]
        public decimal? MinimumStock { set; get; }

        /// <summary>Максимальный запас</summary>
        [OriginalName("78")]
        public decimal? MaximumStock { set; get; }

        /// <summary>Товарная группа </summary>
        [OriginalName("209")]
        public GGroup? GGroup { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Ед.изм для отчетов</summary>
        [OriginalName("206#1")]
        public MeasureUnit? ReportMeasureUnit { set; get; }

        /// <summary>Ед.изм. для заявок</summary>
        [OriginalName("206#2")]
        public MeasureUnit? RequestMeasureUnit { set; get; }

        /// <summary>Ед.изм. для автодокументов</summary>
        [OriginalName("206#3")]
        public MeasureUnit? AutodocumentsMeasureUnit { set; get; }

        /// <summary>Ед.изм. для комплекта</summary>
        [OriginalName("206#4")]
        public MeasureUnit? KitMeasureUnit { set; get; }

        /// <summary>Индикатор срока хранения (дней)</summary>
        [OriginalName("67")]
        public uint ShelfLifeIndicator { set; get; }




        /// <summary>Калорийность базовой ед.изм</summary>
        [OriginalName("67")]
        public decimal EnergyBaseUnitCalorie { set; get; }

        /// <summary>белки на 100 гр</summary>
        [OriginalName("20")]
        public decimal EnergyProteins { set; get; }

        /// <summary>Жиры  на 100 гр</summary>
        [OriginalName("21")]
        public decimal EnergyFat { set; get; }

        /// <summary>Углевода на 100 гр</summary>
        [OriginalName("22")]
        public decimal EnergyСarbs { set; get; }

        /// <summary>% Этилового спирта</summary>
        [OriginalName("19")]
        public decimal PercentageEthylAlcohol { set; get; }

        /// <summary>ККал</summary>
        [OriginalName("23")]
        public decimal EnergyValue { set; get; }

        /// <summary>Закупка (цена без налогов)</summary>
        [OriginalName("53")]
        public decimal PurchasePriceWithoutTaxes { set; get; }

        /// <summary>Закупка (цена c налогами)</summary>
        [OriginalName("54")]
        public decimal PurchasePriceWithTaxes { set; get; }

        /// <summary>Закупка НДС</summary>
        [OriginalName("212")]
        public NDSInfo? PurchaseNDS { set; get; }

        /// <summary>Закупка НСП</summary>
        [OriginalName("213")]
        public NSPInfo? PurchaseNSP { set; get; }

        /// <summary>Продажа (цена без налогов)</summary>
        [OriginalName("56")]
        public decimal SalePriceWithoutTaxes { set; get; }

        /// <summary>Продажа (цена c налогами)</summary>
        [OriginalName("57")]
        public decimal? SalePriceWithTaxes { set; get; }

        /// <summary>Продажа НДС</summary>
        [OriginalName("212#1")]
        public NDSInfo? SaleNDS { set; get; }

        /// <summary>Продажа НСП</summary>
        [OriginalName("213#1")]
        public NSPInfo? SaleNSP { set; get; }

        /// <summary>Код товара в rkeeper</summary>
        [OriginalName("241")]
        public uint? RKeeperCode { set; get; }

        /// <summary>Маршрут тип докуемнта</summary>
        [OriginalName("24")]
        public TTNType? RouteTTNType { set; get; }

        /// <summary>Маршрут Контрагент</summary>
        [OriginalName("105")]
        public Сorrespondent? RouteСorrespondent { set; get; }
        //ToDo Object106 не разобрался


        /// <summary>Атрибуты типа 6</summary>
        [OriginalName("6")]
        public Dictionary<string, string> Attributes6 { set; get; } = new();

        /// <summary>Атрибуты типа 7</summary>
        [OriginalName("7")]
        public Dictionary<string, string> Attributes7 { set; get; } = new();

        /// <summary>Вид алк.продукции</summary>
        [OriginalName("201")]
        public AlcoholProductType? AlcoholProductType { get; set; }

        /// <summary>Производитель</summary>
        [OriginalName("114")]
        public KPP? Producer { set; get; }

        /// <summary>Единица измерения</summary>
        [OriginalName("206")]
        public MeasureUnit? MeasureUnit { get; set; }

        /// <summary>Синоним товара</summary>
        [OriginalName("255")]
        public ProductSynonym? ProductSynonym { set; get; }

        /// <summary>Комплект</summary>
        [OriginalName("215")]
        public DishComposition? DishComposition { set; get; }

        public static GoodsItem? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new GoodsItem
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                GUID = value.GetValueOrDefault("4")?.TrimStart('{').TrimEnd('}'),
                Flags = Enum.TryParse(typeof(GoodsItemFlags), value.GetValueOrDefault("42"), out object? flags) ? (GoodsItemFlags?)flags : null,
                ExpirationDateForCertified = uint.TryParse(value.GetValueOrDefault("59"), out uint expirationDate) ? expirationDate : null,
                AdditionalParameters = uint.TryParse(value.GetValueOrDefault("26"), out uint additionalParameters) ? additionalParameters : null,
                Type = Enum.TryParse(typeof(GoodsItemType), value.GetValueOrDefault("25"), out object? type) ? (GoodsItemType?)type : null,
                PercentageErrorInInventory = decimal.TryParse(value.GetValueOrDefault("52"), out decimal percentageErrorInInventory) ? percentageErrorInInventory : null,
                ProcessingPercentage1 = decimal.TryParse(value.GetValueOrDefault("50"), out decimal processingPercentage1) ? processingPercentage1 : null,
                ProcessingPercentage2 = decimal.TryParse(value.GetValueOrDefault("51"), out decimal processingPercentage2) ? processingPercentage2 : null,
                MinimumStock = decimal.TryParse(value.GetValueOrDefault("77"), out decimal minimumStock) ? minimumStock : null,
                MaximumStock = decimal.TryParse(value.GetValueOrDefault("78"), out decimal maximumStock) ? maximumStock : null,
                Name = value.GetValueOrDefault("3"),
                Attributes6 = value.Where(t => t.Key.StartsWith("6\\")).ToDictionary(t => t.Key.TrimStart("6\\".ToCharArray()), g => g.Value),
                Attributes7 = value.Where(t => t.Key.StartsWith("7\\")).ToDictionary(t => t.Key.TrimStart("7\\".ToCharArray()), g => g.Value),
                AlcoholProductType = AlcoholProductType.Parse(value.Where(t => t.Key.StartsWith("201\\")).ToDictionary(t => t.Key.TrimStart("201\\"), g => g.Value)),
                GGroup = GGroup.Parse(value.Where(t => t.Key.StartsWith("209\\")).ToDictionary(t => t.Key.TrimStart("209\\"), g => g.Value)),

                MeasureUnit = MeasureUnit.Parse(value.Where(t => t.Key.StartsWith("206\\")).ToDictionary(t => t.Key.TrimStart("206\\"), g => g.Value)),
                ReportMeasureUnit = MeasureUnit.Parse(value.Where(t => t.Key.StartsWith("206#1\\")).ToDictionary(t => t.Key.TrimStart("206#1\\"), g => g.Value)),
                RequestMeasureUnit = MeasureUnit.Parse(value.Where(t => t.Key.StartsWith("206#2\\")).ToDictionary(t => t.Key.TrimStart("206#2\\"), g => g.Value)),
                AutodocumentsMeasureUnit = MeasureUnit.Parse(value.Where(t => t.Key.StartsWith("206#3\\")).ToDictionary(t => t.Key.TrimStart("206#3\\"), g => g.Value)),
                KitMeasureUnit = MeasureUnit.Parse(value.Where(t => t.Key.StartsWith("206#4\\")).ToDictionary(t => t.Key.TrimStart("206#4\\"), g => g.Value)),

                EnergyBaseUnitCalorie = decimal.TryParse(value.GetValueOrDefault("67"), out decimal energyBaseUnitCalorie) ? energyBaseUnitCalorie : 0,
                EnergyProteins = decimal.TryParse(value.GetValueOrDefault("20"), out decimal energyProteins) ? energyProteins : 0,
                EnergyFat = decimal.TryParse(value.GetValueOrDefault("21"), out decimal energyFat) ? energyFat : 0,
                EnergyСarbs = decimal.TryParse(value.GetValueOrDefault("22"), out decimal energyСarbs) ? energyСarbs : 0,
                PercentageEthylAlcohol = decimal.TryParse(value.GetValueOrDefault("19"), out decimal percentageEthylAlcohol) ? percentageEthylAlcohol : 0,
                EnergyValue = decimal.TryParse(value.GetValueOrDefault("23"), out decimal energyValue) ? energyValue : 0,
                PurchasePriceWithoutTaxes = decimal.TryParse(value.GetValueOrDefault("53"), out decimal purchasePriceWithoutTaxes) ? purchasePriceWithoutTaxes : 0,
                PurchasePriceWithTaxes = decimal.TryParse(value.GetValueOrDefault("54"), out decimal purchasePriceWithTaxes) ? purchasePriceWithTaxes : 0,
                SalePriceWithoutTaxes = decimal.TryParse(value.GetValueOrDefault("56"), out decimal salePriceWithoutTaxes) ? salePriceWithoutTaxes : 0,
                SalePriceWithTaxes = decimal.TryParse(value.GetValueOrDefault("57"), out decimal salePriceWithTaxes) ? salePriceWithTaxes : 0,
                PurchaseNDS = NDSInfo.Parse(value.Where(t => t.Key.StartsWith("212\\")).ToDictionary(t => t.Key.TrimStart("212\\"), g => g.Value)),
                PurchaseNSP = NSPInfo.Parse(value.Where(t => t.Key.StartsWith("213\\")).ToDictionary(t => t.Key.TrimStart("213\\"), g => g.Value)),
                SaleNDS = NDSInfo.Parse(value.Where(t => t.Key.StartsWith("212#1\\")).ToDictionary(t => t.Key.TrimStart("212#1\\"), g => g.Value)),
                SaleNSP = NSPInfo.Parse(value.Where(t => t.Key.StartsWith("213#1\\")).ToDictionary(t => t.Key.TrimStart("213#1\\"), g => g.Value)),

                RouteTTNType = Enum.TryParse(typeof(TTNType), value.GetValueOrDefault("24"), out object? routeTTNType) ? (TTNType?)routeTTNType : null,
                RouteСorrespondent = Сorrespondent.Parse(value.Where(t => t.Key.StartsWith("105\\")).ToDictionary(t => t.Key.TrimStart("105\\"), g => g.Value)),
                RKeeperCode = uint.TryParse(value.GetValueOrDefault("241"), out uint rKeeperCode) ? rKeeperCode : null,

                Producer = KPP.Parse(value.Where(t => t.Key.StartsWith("114\\")).ToDictionary(t => t.Key.TrimStart("114\\"), g => g.Value)),
                ProductSynonym = ProductSynonym.Parse(value.Where(t => t.Key.StartsWith("255\\")).ToDictionary(t => t.Key.TrimStart("255\\"), g => g.Value)),
                DishComposition = DishComposition.Parse(value.Where(t => t.Key.StartsWith("215\\")).ToDictionary(t => t.Key.TrimStart("215\\"), g => g.Value)),
            };
        }

        public static IEnumerable<GoodsItem> ParseGoods(ExecOperation answear)
        {
            var values = answear.GetAnswearContent("210").GetValues();
            foreach (var value in values)
            {
                var goodsItem = Parse(value);
                if (goodsItem != null)
                    yield return goodsItem;
            }
        }
    }
}
