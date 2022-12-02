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

        private class Node
        {
            public Node(string name)
            {
                Name = name;
            }
            public string Name { set; get; }
            public string FieldName { set; get; }
            public string FullName { set; get; }
            public string TableName { get; set; }
            public bool IsObject { set; get; }
            public List<Node> Childs { set; get; } = new List<Node>();
        }

        private static DataSet dataSet;
        public static T Parse<T>(string data)
        {
            dataSet = ParseFromJson(data);
            List<Node> tree = new List<Node>();
            foreach (System.Data.DataTable dataTable in dataSet.Tables)
            {
                Node table = new Node(dataTable.TableName)
                {
                    IsObject = true,
                    TableName = dataTable.TableName
                };
                tree.Add(table);
                foreach (System.Data.DataColumn column in dataTable.Columns)
                {
                    Node currentNode = table;
                    string[] array = column.Caption.Split('\\');
                    for (int i = 0; i < array.Length; i++)
                    {
                        string? caption = array[i];
                        if (currentNode.Childs.Any(t => t.Name == caption))
                        {
                            currentNode = currentNode.Childs.Single(t => t.Name == caption);
                        }
                        else
                        {
                            Node newNode = new Node(caption)
                            {
                                FullName = i < array.Length - 1 ? caption : column.Caption,
                                IsObject = i < array.Length - 1,
                                TableName = dataTable.TableName,
                                FieldName = column.ColumnName
                            };
                            currentNode.Childs.Add(newNode);
                            currentNode = newNode;
                        }
                    }
                }
            }


            T rootInstance = (T)Activator.CreateInstance(typeof(T))
                ?? throw new ApiClientException($"Не удалось создать экземпляр класса {nameof(T)}");


            foreach (var node in tree)
            {
                PropertyInfo property = typeof(T).GetProperties()
                    .SingleOrDefault(t => t.GetCustomAttribute<OriginalNameAttribute>()?.OriginalName == node.Name)
                    ?? throw new ApiClientException($"У объекта {typeof(T).Name} отсутствует свойство с атрибутом {node.Name}.");

                var instance = Activator.CreateInstance(property.PropertyType);
                property.SetValue(rootInstance, instance);
                ReadNode(instance, node);
            }
            return rootInstance;
        }

        private static void ReadNode(object instance, Node root)
        {
            if (instance.IsList())
            {
                for (int i = 0; i < dataSet.Tables[root.TableName]?.Rows.Count; i++)
                {

                    Type genericType = instance.GetType().GenericTypeArguments[0];
                    var genInstance = Activator.CreateInstance(genericType);
                    (instance as System.Collections.IList).Add(genInstance);
                    Rrr(genInstance, root, i);
                }
            }
            else
            {
                Rrr(instance, root, 0);
            }
        }

        private static object Rrr(object instance, Node root, int i)
        {
            foreach (var node in root.Childs)
            {
                object currentInstance = instance;

                if (instance.IsDictionary())
                {
                    string? value = Convert.ToString(dataSet.Tables[node.TableName]?.Rows[i][node.FieldName]);
                    var dict = instance as Dictionary<string, string?> ?? throw new ApiClientException($"Для объекта {nameof(instance)} не верно определен тип словаря.");
                    dict.Add(node.Name, value);
                    continue;
                }

                PropertyInfo property = instance.GetType().GetProperties()
                    .SingleOrDefault(t => t.GetCustomAttribute<OriginalNameAttribute>()?.OriginalName == node.Name)
                    ?? throw new ApiClientException($"У объекта {instance.GetType().Name} отсутствует свойство с атрибутом {node.Name}.");
                if (node.Childs.Count <= 0)
                {
                    var value = dataSet.Tables[node.TableName]?.Rows[i][node.FieldName];
                    SetPropertyValue(currentInstance, property, value);
                    Console.WriteLine($"{node.TableName} {node.FullName}");
                }
                else
                {
                    if (node.IsObject)
                    {
                        currentInstance = Activator.CreateInstance(property.PropertyType);
                        property.SetValue(instance, currentInstance);
                    }
                    ReadNode(currentInstance, node);
                }
            }
            return instance;
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
                else if (property.PropertyType == typeof(decimal?) || property.PropertyType == typeof(decimal))
                {
                    data = Convert.ToDecimal(value);
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
                    data = null;
                }
                else if (property.PropertyType == typeof(TTNOptions?) || property.PropertyType == typeof(TTNOptions))
                {

                    data = Enum.Parse(typeof(TTNOptions), value?.ToString()) ?? null;
                }
                else
                {
                    var ff = value.GetType();
                    var hhh = property.PropertyType;
                    bool ee = ff == hhh;
                    data = null;
                }
                property.SetValue(instance, data);
            }
            catch (Exception)
            {
                var ff = value.GetType();
                throw;
            }

        }
    }
}
