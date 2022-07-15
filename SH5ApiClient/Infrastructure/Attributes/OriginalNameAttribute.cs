namespace SH5ApiClient.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = false)]
    internal sealed class OriginalNameAttribute : Attribute
    {
        internal string OriginalName { private set; get; }
        internal OriginalNameAttribute(string originalName)
        {
            OriginalName = originalName;
        }
    }
}
