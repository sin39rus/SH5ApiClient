namespace SH5ApiClient.Models.Enums
{
    /// <summary>Флаг товара</summary>
    [OriginalName("42")]
    [Flags]
    public enum GoodsItemFlags
    {
        /// <summary>Рассчитывать калорийность</summary>
        CalculateCalories = 1,

        /// <summary>Использовать весы для инвентаризации </summary>
        UseScales = 1 << 1,

        /// <summary>Алкогольная продукция</summary>
        Alcohol = 1 << 2,

        /// <summary>Сертифицируемый товар</summary>
        Сertified = 1 << 3,

        /// <summary>Не раскручивать движение для построения алкогольной декларации</summary>
        NotCalculateMovement = 1 << 4,
    }
}
