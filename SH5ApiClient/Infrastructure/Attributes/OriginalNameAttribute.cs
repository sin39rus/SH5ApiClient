namespace SH5ApiClient.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = false)]
    internal sealed class OriginalNameAttribute : Attribute
    {
        internal string OriginalName { private set; get; }
        internal OriginalNameAttribute(string originalName)
        {
            if (string.IsNullOrEmpty(originalName))
            {
                throw new ArgumentException($"\"{nameof(originalName)}\" не может быть неопределенным или пустым.", nameof(originalName));
            }

            OriginalName = originalName;
        }
    }
}
