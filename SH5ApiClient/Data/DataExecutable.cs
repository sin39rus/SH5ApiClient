using System.Reflection;

namespace SH5ApiClient.Data
{
    public abstract class DataExecutable
    {
        protected DataSet DataSet { private set; get; } = new DataSet();

        public static T Parse<T>(string data) where T : DataExecutable
        {
            T rootInstance = (T?)Activator.CreateInstance(typeof(T))
                ?? throw new ApiClientException($"Не удалось создать экземпляр класса {nameof(T)}");
            rootInstance.DataSet = DataSet.ParseFromJson(data);

            foreach (System.Data.DataTable dataTable in rootInstance.DataSet.Tables)
            {
                object tableInstance = rootInstance;

                PropertyInfo property = typeof(T).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .SingleOrDefault(t => t.GetCustomAttribute<OriginalNameAttribute>()?.OriginalName == dataTable.TableName);
                if (property is not null)
                {
                    tableInstance = Activator.CreateInstance(property.PropertyType)
                        ?? throw new ApiClientException($"Не удалось создать экземпляр класса {property.PropertyType.Name}");
                    property.SetValue(rootInstance, tableInstance);
                }

                foreach (System.Data.DataRow dataRow in dataTable.Rows)
                {
                    object rowInstance = tableInstance;
                    if (tableInstance.IsList())
                    {
                        Type genericType = tableInstance.GetType().GenericTypeArguments[0];
                        rowInstance = Activator.CreateInstance(genericType) ?? throw new ApiClientException($"Не удалось создать экземпляр класса {nameof(genericType)}");
                        (tableInstance as System.Collections.IList)?.Add(rowInstance);
                    }

                    foreach (System.Data.DataColumn column in dataTable.Columns)
                    {
                        try
                        {
                            var value = dataRow[column.ColumnName];
                            object currentInstance = rowInstance;
                            string[] array = column.Caption.Split('\\');
                            for (int i = 0; i < array.Length; i++)
                            {
                                string? caption = array[i];
                                if (currentInstance.IsDictionary())
                                {
                                    string? dictValue = value is DBNull ? null : Convert.ToString(value);
                                    (currentInstance as System.Collections.IDictionary).Add(caption, dictValue);
                                    continue;
                                }
                                PropertyInfo rowProperty = currentInstance.GetType().GetProperties()
                                    .SingleOrDefault(t => t.GetCustomAttribute<OriginalNameAttribute>()?.OriginalName == caption)
                                    ?? throw new ApiClientException($"У объекта {currentInstance.GetType().Name} отсутствует свойство с атрибутом {caption}.");
                                if (IsObject(array, i))
                                {
                                    if (rowProperty.GetValue(currentInstance) is null)
                                    {
                                        object newInstance = Activator.CreateInstance(rowProperty.PropertyType);
                                        rowProperty.SetValue(currentInstance, newInstance);
                                        currentInstance = newInstance;
                                    }
                                    else
                                        currentInstance = rowProperty.GetValue(currentInstance);
                                }
                                else
                                {
                                    SetPropertyValue(currentInstance, rowProperty, value);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
            return rootInstance;
        }

        private static bool IsObject(string[] array, int i)
        {
            return i < array.Length - 1;
        }
        private static void SetPropertyValue(object instance, PropertyInfo property, object? value)
        {
            if (instance is null) throw new ArgumentNullException(nameof(instance));
            if (property is null) throw new ArgumentNullException(nameof(property));

            object? data;
            try
            {
                if (value is null || value is DBNull || property.PropertyType == typeof(DBNull))
                {
                    data = null;
                }
                else if (property.PropertyType == typeof(ulong?))
                {
                    data = value is DBNull ? null : Convert.ToUInt64(value);
                }
                else if (property.PropertyType == typeof(ulong))
                {
                    data = Convert.ToUInt64(value);
                }
                else if (property.PropertyType == typeof(long?))
                {
                    data = value is DBNull ? null : Convert.ToInt64(value);
                }
                else if (property.PropertyType == typeof(long))
                {
                    data = Convert.ToInt64(value);
                }
                else if (property.PropertyType == typeof(uint?))
                {
                    data = value is DBNull ? null : Convert.ToUInt32(value);
                }
                else if (property.PropertyType == typeof(uint))
                {
                    data = Convert.ToUInt32(value);
                }
                else if (property.PropertyType == typeof(int?))
                {
                    data = value is DBNull ? null : Convert.ToInt32(value);
                }
                else if (property.PropertyType == typeof(int))
                {
                    data = Convert.ToInt32(value);
                }
                else if (property.PropertyType == typeof(ushort?))
                {
                    data = value is DBNull ? null : Convert.ToUInt16(value);
                }
                else if (property.PropertyType == typeof(ushort))
                {
                    data = Convert.ToUInt16(value);
                }
                else if (property.PropertyType == typeof(byte?))
                {
                    data = value is DBNull ? null : Convert.ToByte(value);
                }
                else if (property.PropertyType == typeof(byte))
                {
                    data = Convert.ToByte(value);
                }
                else if (property.PropertyType == typeof(short?))
                {
                    data = value is DBNull ? null : Convert.ToInt16(value);
                }
                else if (property.PropertyType == typeof(short))
                {
                    data = Convert.ToInt16(value);
                }
                else if (property.PropertyType == typeof(decimal?) || property.PropertyType == typeof(decimal))
                {
                    data = value is DBNull ? null : Convert.ToDecimal(value);
                }
                else if (property.PropertyType == typeof(string))
                {
                    data = value is DBNull ? null : Convert.ToString(value);
                }
                else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                {
                    data = Convert.ToBoolean(value);
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    data = Convert.ToDateTime(value);
                }
                else if (property.PropertyType == typeof(DateTime?))
                {
                    data = value is DBNull ? null : Convert.ToDateTime(value);
                }
                else if (property.PropertyType == typeof(TTNOptions?) || property.PropertyType == typeof(TTNOptions))
                {
                    data = Enum.Parse(typeof(TTNOptions), value?.ToString()) ?? null;
                }
                else if (property.PropertyType == typeof(CorrType3?) || property.PropertyType == typeof(CorrType3))
                {
                    data = Enum.Parse(typeof(CorrType3), value?.ToString()) ?? null;
                }
                else if (property.PropertyType == typeof(TTNType?) || property.PropertyType == typeof(TTNType))
                {
                    data = Enum.Parse(typeof(TTNType), value?.ToString()) ?? null;
                }
                else if (property.PropertyType == typeof(DepatmenType?) || property.PropertyType == typeof(DepatmenType))
                {
                    data = Enum.Parse(typeof(DepatmenType), value?.ToString()) ?? null;
                }
                else if (property.PropertyType == typeof(GoodsItemFlags?) || property.PropertyType == typeof(GoodsItemFlags))
                {
                    data = Enum.Parse(typeof(GoodsItemFlags), value?.ToString()) ?? null;
                }
                else if (property.PropertyType == typeof(CorrType?) || property.PropertyType == typeof(CorrType))
                {
                    data = Enum.Parse(typeof(CorrType), value?.ToString()) ?? null;
                }
                else if (property.PropertyType == typeof(CorrTypeEx?) || property.PropertyType == typeof(CorrTypeEx))
                {
                    data = Enum.Parse(typeof(CorrTypeEx), value?.ToString()) ?? null;
                }
                else
                {
                    throw new ApiClientException($"Тип свойства {property.Name} не определен.");
                }
                property.SetValue(instance, data);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
