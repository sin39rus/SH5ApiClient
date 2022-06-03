namespace SH5ApiClient.Models.Enums
{
    /// <summary>
    /// Вид платежа
    /// </summary>
    public enum PaymentType
    {
        /// <summary>
        /// Наличные
        /// </summary>
        Cash = 0,
        /// <summary>
        /// Безналичные
        /// </summary>
        NonCash = 1,
        /// <summary>
        /// Кредитные карты
        /// </summary>
        CreditCards = 2,
        /// <summary>
        /// Прочее
        /// </summary>
        Other = 3
    }
}
