using Newtonsoft.Json.Linq;
using System.Reflection;

namespace SH5ApiClient.Data
{
    public class DataSet : System.Data.DataSet
    {
        public JProperty ToJson()
        {
            JProperty shTable =
                new JProperty("shTable", new JArray(
                    Tables.Cast<System.Data.DataTable>()
                    .Select(table => new JObject(

                        new JProperty("head", table.TableName),

                        new JProperty("recCount", table.Rows.Count),

                        new JProperty("original", new JArray(
                            table.Columns.Cast<System.Data.DataColumn>()
                            .Select(t => t.Caption))),

                        new JProperty("fields", new JArray(
                            table.Columns.Cast<System.Data.DataColumn>()
                            .Select(t => t.ColumnName))),

                        new JProperty("values", new JArray(
                            new JArray(
                                table.Columns.Cast<System.Data.DataColumn>()
                                .Select(column => new JArray(
                                    table.Rows.Cast<System.Data.DataRow>()
                                    .Select(dataRow => dataRow[column]))))))))));
            return shTable;
        }
        public static DataSet ParseFromJson(string data)
        {
            ExecOperation answear = OperationBase.Parse<ExecOperation>(data);
            DataSet dataSet = new();

            foreach (var shTable in answear.Content)
            {
                System.Data.DataTable dataTable = new(shTable.Head);
                dataSet.Tables.Add(dataTable);

                CreateColumns(shTable, dataTable);
                CreateRows(shTable, dataTable);
                FillDataRows(shTable, dataTable);
            }
            return dataSet;
        }
        private static void CreateColumns(ExecOperationContent shTable, System.Data.DataTable dataTable)
        {
            for (int i = 0; i < shTable.Fields.Length; i++)
            {
                dataTable.Columns.Add(new System.Data.DataColumn
                {
                    ColumnName = shTable.Fields[i],
                    Caption = shTable.Original[i],
                    DataType = typeof(object)
                });
            }
        }
        private static void CreateRows(ExecOperationContent shTable, System.Data.DataTable dataTable)
        {
            dataTable.Rows.AddRange(Enumerable.Range(0, shTable.Values.First().Length)
                .Select(t => dataTable.NewRow())
                .ToArray());
        }
        private static void FillDataRows(ExecOperationContent shTable, System.Data.DataTable dataTable)
        {
            for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
            {
                for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                {
                    object? value = shTable.Values[columnIndex][rowIndex];
                    dataTable.Rows[rowIndex][columnIndex] = value;
                }
            }
        }


        public static T Parse<T>(string data) where T : notnull
        {
            DataSet dataSet = ParseFromJson(data);
            T rootInstance = (T)Activator.CreateInstance(typeof(T))
                ?? throw new ApiClientException($"Не удалось создать экземпляр класса {nameof(T)}");



            foreach (System.Data.DataTable dataTable in dataSet.Tables)
            {
                PropertyInfo property = typeof(T).GetProperties()
                    .SingleOrDefault(t => t.GetCustomAttribute<OriginalNameAttribute>()?.OriginalName == dataTable.TableName)
                    ?? throw new ApiClientException($"У объекта {typeof(T).Name} отсутствует свойство с атрибутом {dataTable.TableName}.");
                var tableInstance = Activator.CreateInstance(property.PropertyType)
                    ?? throw new ApiClientException($"Не удалось создать экземпляр класса {property.PropertyType.Name}");
                property.SetValue(rootInstance, tableInstance);


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
                                    (currentInstance as System.Collections.IDictionary).Add(caption, Convert.ToString(value));
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
                if (value is null || property.PropertyType == typeof(DBNull))
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
                    data = Convert.ToString(value);
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
                else
                {
                    throw new ApiClientException($"Тип свойства {property.PropertyType.Name} не определен.");
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
