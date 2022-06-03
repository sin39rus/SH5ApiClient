namespace SH5ApiClient.Models.Enums
{
    /// <summary>
    /// Поддрерживаемы типы создаваемых атрибутов
    /// </summary>
    public enum SHAttributeType
    {
#if INSIDEDEBUG || INSIDERELEASE
        tUint16,
#endif
        tInt32,
        tInt64,
        tDouble,
        tLongDate,
        tStrP
    }
}
