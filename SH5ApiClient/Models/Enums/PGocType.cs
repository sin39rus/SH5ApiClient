using System.ComponentModel;

namespace SH5ApiClient.Models.Enums
{
    /// <summary>
    /// Тип платежного документа
    /// </summary>
    public enum PGocType
    {
        /// <summary>
        /// Приходный платежный документ
        /// </summary>
        [Description("Приходный платежный документ")]
        Incoming,
        /// <summary>
        /// Расходный платежный документ
        /// </summary>
        [Description("Расходный платежный документ")]
        Outgoing,
        /// <summary>
        /// Внутренний платежный документ 
        /// </summary>
        [Description("Внутренний платежный документ")]
        Inside
    }
}
