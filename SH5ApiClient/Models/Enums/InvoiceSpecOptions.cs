using System;

namespace SH5ApiClient.Models.Enums
{
    [Flags]
    public enum InvoiceSpecOptions
    {
        /// <summary>активная спецификация</summary>
        GdsoActive = 1,
        /// <summary>С/В: запись применяется для накопления излишков/недостач инвентаризации (для активных и неактивных накладных)</summary>
        GdsoInvDiff = 1 << 1,
        /// <summary>взвешен</summary>
        GdsoWeighted = 1 << 2,
        /// <summary>	С/В: использовать комплект (разбить на составляющие). Только для записей без флага gdsoInvIntRec</summary>
        GdsoInvUseCmp = 1 << 3,
        /// <summary>м.б. взвешен</summary>
        GdsoCanWeight = 1 << 4,
        /// <summary>С/В: запись сгенерирована автоматически для накопления излишков/недостач инвентаризации</summary>
        GdsoInvIntRec = 1 << 5,
    }
}
