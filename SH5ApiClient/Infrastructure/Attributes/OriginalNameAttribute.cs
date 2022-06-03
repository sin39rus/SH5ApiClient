namespace AddonCore.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    internal sealed class OriginalNameAttribute : Attribute
    {
        internal string OriginalName { private set; get; }
        internal OriginalNameAttribute(string originalName)
        {
            OriginalName = originalName;
        }
    }
}
