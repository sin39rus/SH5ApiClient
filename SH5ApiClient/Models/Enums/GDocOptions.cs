using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Models.Enums
{
    /// <summary>Опции накладной для активации ассоциированы с подразделением.
    /// <para>Например, для полной активации расходной накладной, достаточно выставить флаг gdoActivatedByCntr0 («активирована поставщиком»), так как поставщик - подразделение.</para>
    /// <para>Для активации внутренних перемещений , необходимо выставить gdoActivatedByCntr0 и gdoActivatedByCntr1.</para>
    /// <para>Для приходных накладных - gdoActivatedByCntr1(«активирована получателем»).</para>
    /// <para>флаг gdoActive используется для чтения состояния накладной, независимо от типа документа.</para>
    /// </summary>
    [Flags]
    public enum GDocOptions
    {
        /// <summary>активный документ , используется только для чтения</summary>
        gdoActive = 1,
        /// <summary>активирован поставщиком</summary>
        gdoActivatedByCntr0 = 1 << 1,
        /// <summary>активирован получателем</summary>
        gdoActivatedByCntr1 = 1 << 2,

    }
}
