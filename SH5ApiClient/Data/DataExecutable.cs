﻿using Newtonsoft.Json.Linq;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models.Enums;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SH5ApiClient.Data
{
    public abstract class DataExecutable
    {
        protected DataSet DataSet { private set; get; } = new DataSet();
        public static Task<T> ParseAsync<T>(string data, CancellationToken cancellationToken) where T : DataExecutable
            => Task.Run(() => { return Parse<T>(data, cancellationToken); });
        public static T Parse<T>(string data, CancellationToken cancellationToken = new CancellationToken()) where T : DataExecutable
        {
            T rootInstance = (T)Activator.CreateInstance(typeof(T))
                ?? throw new ApiClientException($"Не удалось создать экземпляр класса {nameof(T)}");
            rootInstance.DataSet = DataSet.ParseFromJson(data);

            foreach (System.Data.DataTable dataTable in rootInstance.DataSet.Tables)
            {
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException();
                object tableInstance = rootInstance;

                PropertyInfo property = typeof(T).GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .SingleOrDefault(t => t.GetCustomAttribute<OriginalNameAttribute>()?.OriginalName == dataTable.TableName);
                if (property != null)
                {
                    tableInstance = Activator.CreateInstance(property.PropertyType)
                        ?? throw new ApiClientException($"Не удалось создать экземпляр класса {property.PropertyType.Name}");
                    property.SetValue(rootInstance, tableInstance);
                }

                foreach (System.Data.DataRow dataRow in dataTable.Rows)
                {
                    if (cancellationToken.IsCancellationRequested)
                        throw new OperationCanceledException();
                    object rowInstance = tableInstance;
                    if (tableInstance.IsList())
                    {
                        Type genericType = tableInstance.GetType().GenericTypeArguments[0];
                        rowInstance = Activator.CreateInstance(genericType) ?? throw new ApiClientException($"Не удалось создать экземпляр класса {nameof(genericType)}");
                        (tableInstance as System.Collections.IList)?.Add(rowInstance);
                    }

                    foreach (System.Data.DataColumn column in dataTable.Columns)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            throw new OperationCanceledException();
                        try
                        {
                            var value = dataRow[column.ColumnName];
                            object currentInstance = rowInstance;
                            string[] array = column.ColumnName.Split('\\');
                            for (int i = 0; i < array.Length; i++)
                            {
                                string caption = array[i];
                                if (currentInstance.IsDictionary())
                                {
                                    string dictValue = value is DBNull ? null : Convert.ToString(value);
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

        /// <summary>Изменить данные в таблице SH</summary>
        /// <param name="dataTableName">Имя таблицы</param>
        /// <param name="rowIdNameColumn">Ключевая колонка строки</param>
        /// <param name="rowId">Ключ строки</param>
        /// <param name="valueNameColumn">Наименование колонки данных</param>
        /// <param name="value">Новое значение</param>
        /// <exception cref="ApiClientArgumentException"></exception>
        public void ChangeValue(string dataTableName, string rowIdNameColumn, object rowId, string valueNameColumn, object value)
        {
            DataTable dataTable = DataSet.Tables[dataTableName] ?? throw new ApiClientArgumentException($"Таблица с именем: \"{dataTableName}\" не найдена.", nameof(dataTableName));
            DataColumn columnRowId = dataTable.Columns[rowIdNameColumn] ?? throw new ApiClientArgumentException($"Колонка идентификатора строки с именем: \"{rowIdNameColumn}\" не найдена.", nameof(rowIdNameColumn));
            DataColumn valueColumn = dataTable.Columns[valueNameColumn] ?? throw new ApiClientArgumentException($"Колонка данных с именем: \"{valueNameColumn}\" не найдена.", nameof(valueNameColumn));
            DataRow row = dataTable.Rows.Cast<DataRow>().SingleOrDefault(t => t[columnRowId].ToString() == rowId.ToString()) ?? throw new ApiClientArgumentException($"Строка с инентификатором: \"{rowId}\" в колонке \"{rowIdNameColumn}\" не найдена.", nameof(rowId)); ;

            row.BeginEdit();
            row[valueColumn] = value;
            row.AcceptChanges();
        }
        public virtual JArray ToJson()
        {
            JArray tables = new JArray();
            foreach (DataTable table in DataSet.Tables)
            {
                if (table.Rows.Count == 0)
                    continue;
                JProperty head = new JProperty("head", table.TableName);
                JProperty recCount = new JProperty("recCount", table.Rows.Count);
                JArray originals = new JArray();
                JArray fields = new JArray();
                JArray values = new JArray();
                JArray statuses = new JArray(Enumerable.Range(0, table.Rows.Count).Select(t => "Modify"));

                foreach (DataColumn column in table.Columns)
                {
                    originals.Add(column.Caption);
                    fields.Add(column.ColumnName);

                    JArray rowArray = new JArray();
                    foreach (DataRow row in table.Rows)
                    {
                        object value = row[column] is DBNull ? null : row[column];
                        rowArray.Add(value);
                    }
                    values.Add(rowArray);
                }

                tables.Add(
                    new JObject(
                        head,
                        recCount,
                        new JProperty("original", originals),
                        new JProperty("fields", fields),
                        new JProperty("values", values),
                        new JProperty("status", statuses)
                        ));
            }
            return tables;
        }
        private static bool IsObject(string[] array, int i)
        {
            return i < array.Length - 1;
        }
        private static void SetPropertyValue(object instance, PropertyInfo property, object value)
        {
            if (instance is null) throw new ArgumentNullException(nameof(instance));
            if (property is null) throw new ArgumentNullException(nameof(property));

            object data;
            try
            {
                if (value is null || value is DBNull || property.PropertyType == typeof(DBNull))
                {
                    data = null;
                }
                else if (property.PropertyType == typeof(ulong?))
                {
                    data = value is DBNull ? null : (ulong?)Convert.ToUInt64(value);
                }
                else if (property.PropertyType == typeof(ulong))
                {
                    data = Convert.ToUInt64(value);
                }
                else if (property.PropertyType == typeof(long?))
                {
                    data = value is DBNull ? null : (long?)Convert.ToInt64(value);
                }
                else if (property.PropertyType == typeof(long))
                {
                    data = Convert.ToInt64(value);
                }
                else if (property.PropertyType == typeof(uint?))
                {
                    data = value is DBNull ? null : (uint?)Convert.ToUInt32(value);
                }
                else if (property.PropertyType == typeof(uint))
                {
                    data = Convert.ToUInt32(value);
                }
                else if (property.PropertyType == typeof(int?))
                {
                    data = value is DBNull ? null : (int?)Convert.ToInt32(value);
                }
                else if (property.PropertyType == typeof(int))
                {
                    data = Convert.ToInt32(value);
                }
                else if (property.PropertyType == typeof(ushort?))
                {
                    data = value is DBNull ? null : (ushort?)Convert.ToUInt16(value);
                }
                else if (property.PropertyType == typeof(ushort))
                {
                    data = Convert.ToUInt16(value);
                }
                else if (property.PropertyType == typeof(byte?))
                {
                    data = value is DBNull ? null : (byte?)Convert.ToByte(value);
                }
                else if (property.PropertyType == typeof(byte))
                {
                    data = Convert.ToByte(value);
                }
                else if (property.PropertyType == typeof(short?))
                {
                    data = value is DBNull ? null : (short?)Convert.ToInt16(value);
                }
                else if (property.PropertyType == typeof(short))
                {
                    data = Convert.ToInt16(value);
                }
                else if (property.PropertyType == typeof(decimal?) || property.PropertyType == typeof(decimal))
                {
                    data = value is DBNull ? null : (decimal?)Convert.ToDecimal(value);
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
                    data = value is DBNull ? null : (DateTime?)Convert.ToDateTime(value);
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
