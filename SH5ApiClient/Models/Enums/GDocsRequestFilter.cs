namespace SH5ApiClient.Models.Enums
{
    /// <summary>
    /// Битовая маска флагов фильтра. 1 - вычислять суммы(накладных). 2 - показывать компенсированные суммы в виде документов.  4 - показывать неактивные накладные. 8 - показывать активные накладные
    /// </summary>
    [Flags]
    public enum GDocsRequestFilter
    {
        /// <summary>
        /// 1 - вычислять суммы(накладных)
        /// </summary>
        CalculateSums = 1,
        /// <summary>
        /// 2 - показывать компенсированные суммы в виде документов
        /// </summary>
        ShowCompensatedAmounts = 2,
        /// <summary>
        /// 4 - показывать неактивные накладные
        /// </summary>
        ShowInactiveInvoices = 4,
        /// <summary>
        /// 8 - показывать активные накладные
        /// </summary>
        ShowActiveInvoices = 8
    }
}
