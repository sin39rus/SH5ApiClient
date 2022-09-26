namespace SH5ApiClient.Models.Enums
{
    /// <summary>Тип ТТН</summary>
    [OriginalName("24")]
    public enum TTNType
    {
        /// <summary>Приходная накладная</summary>
        PurchaseInvoice = 0,

        /// <summary>Возврат от получателя</summary>
        ReturnRecipient = 1,

        /// <summary>Расходная накладная</summary>
        SalesInvoice = 4,

        /// <summary>Возврат поставщику</summary>
        ReturnSupplier = 5,

        /// <summary>Сличительная ведомость</summary>
        CollationStatement = 8,

        /// <summary>Сличительная ведомость излишки/недостачи</summary>
        CollationStatementDiffs = 88,

        /// <summary>Акт переработки</summary>
        ActProcessing = 10,

        /// <summary>Внутреннее перемещение</summary> 
        InternalMovement = 11,

        /// <summary>Комплектация</summary>
        Compdection = 12,

        /// <summary>Декомплектация</summary>
        Decomposition = 13
    }
}
