namespace SH5ApiClient.Models.DTO
{
    public abstract class ModelSHBase
    {
        public static T Parse<T>(Dictionary<string, string> values) where T : ModelSHBase
        {
            T result = Activator.CreateInstance(typeof(T)) as T ?? throw new Exception($"Не удалось создать экземпляр объекта {nameof(T)}.");
            IEnumerable<System.Reflection.PropertyInfo>? properties = typeof(T).GetProperties().Where(t => t.GetCustomAttributes(typeof(OriginalNameAttribute), true).Any());

            foreach (var prop in properties)
            {
                //result.
            }

            return null;
        }
    }
}
