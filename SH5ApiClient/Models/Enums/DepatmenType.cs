using System;

namespace SH5ApiClient.Models.Enums
{
    /// <summary>Тип подразделения</summary>
    [Flags]
    public enum DepatmenType
    {
        /// <summary>Склад</summary>
        Warehouse = 1,

        /// <summary>Производство</summary>
        Production = 2,

        /// <summary>Торговля</summary>
        Trade = 4
    }
}
