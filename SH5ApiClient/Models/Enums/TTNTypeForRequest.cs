namespace SH5ApiClient.Models.Enums
{
    /// <summary>Тип накладной</summary>
    [Flags]
    public enum TTNTypeForRequest
    {
        /// <summary>Приходная накладная GDoc0</summary>
        PurchaseInvoice = 1,
        /// <summary>Возврат от получателя GDoc1</summary>
        ReturnRecipient = 2,
        /// <summary>Расходная накладная GDoc4</summary>
        SalesInvoice = 16,
        /// <summary>Возврат поставщику GDoc5</summary>
        ReturnSupplier = 32,
        /// <summary>Сличительная ведомость GDoc8</summary>
        CollationStatement = 256,
        /// <summary>Акт переработки GDoc10</summary>
        ActProcessing = 1024,
        /// <summary>Внутреннее перемещение GDoc11</summary>
        InternalMovement = 2048,
        /// <summary>Комплектация GDoc12</summary>
        Compdection = 4096,
        /// <summary>Декомплектация GDoc13</summary>
        Decomposition = 8192
    }
}
