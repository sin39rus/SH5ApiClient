namespace SH5ApiClient.Models.Enums
{
    /// <summary>
    /// Тип накладной
    /// </summary>
    [Flags]
    public enum TTNTypeForRequest
    {
        /// <summary>
        /// Приходная накладная
        /// </summary>
        PurchaseInvoice = 1,
        /// <summary>
        /// Возврат от получателя
        /// </summary>
        ReturnRecipient = 2,
        /// <summary>
        /// Расходная накладная
        /// </summary>
        SalesInvoice = 16,
        /// <summary>
        /// Возврат поставщику
        /// </summary>
        ReturnSupplier = 32,
        /// <summary>
        /// Сличительная ведомость
        /// </summary>
        CollationStatement = 256,
        /// <summary>
        /// Акт переработки
        /// </summary>
        ActProcessing = 1024,
        /// <summary>
        /// Внутреннее перемещение
        /// </summary>
        InternalMovement = 2048,
        /// <summary>
        /// Комплектация
        /// </summary>
        Compdection = 4096,
        /// <summary>
        /// Декомплектация
        /// </summary>
        Decomposition = 8192
    }
}
