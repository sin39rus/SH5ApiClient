namespace SH5ApiClient.Models.Enums
{
    /// <summary>Опции накладных</summary>
    [Flags]
    public enum TTNOptions
    {
        /// <summary>Активный документ , используется только для чтения</summary>
        Active = 1,
        
        /// <summary>Активирован поставщиком</summary>
        ActivatedByCntr0 = 2,
        
        /// <summary>Активирован получателем</summary>
        ActivatedByCntr1 = 4,

        /// <summary>Не известная опция, доумент появляется дубликатом при создании из Честного знака</summary> //ToDo: разобратся
        Unknown = 32771
        
    }
}
