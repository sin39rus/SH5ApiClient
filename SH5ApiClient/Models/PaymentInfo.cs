namespace SH5ApiClient.Models
{
    /// <summary>
    /// Информация об оплате
    /// </summary>
    public class PaymentInfo
    {
        /// <param name="amountWithoutVAT">Сумма без НДС</param>
        public PaymentInfo(decimal amountWithoutVAT)
        {
            AmountWithoutVAT = amountWithoutVAT;
        }

        /// <summary>
        /// Ставка НДС в процентах 18% - 1800, 10% - 1000
        /// </summary>
        [OriginalName("212\\9")]
        public int VATRate { init; get; } = 0;
        /// <summary>
        /// Сумма без НДС
        /// </summary>
        [OriginalName("50")]
        public decimal AmountWithoutVAT { private set; get; } = 0;
        /// <summary>
        /// НДС
        /// </summary>
        [OriginalName("51")]
        public decimal VATVolume { init; get; } = 0;
        /// <summary>
        /// НСП
        /// </summary>
        [OriginalName("52")]
        public decimal NSP { init; get; } = 0;
    }
}
