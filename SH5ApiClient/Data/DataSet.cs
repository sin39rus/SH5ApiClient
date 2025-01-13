using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using System.Data;
using System.Linq;

namespace SH5ApiClient.Data
{
    public class DataSet : System.Data.DataSet
    {
        public string RawData { get; private set; }
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
                            .Select(t => t.ColumnName))),

                        new JProperty("fields", new JArray(
                            table.Columns.Cast<System.Data.DataColumn>()
                            .Select(t => t.Caption))),

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
            ExecOperation answer = OperationBase.Parse<ExecOperation>(data);
            DataSet dataSet = new DataSet();
            dataSet.RawData = data;
            foreach (var shTable in answer.Content)
            {
                DataTable dataTable = new DataTable(shTable.Head);
                dataSet.Tables.Add(dataTable);

                CreateColumns(shTable, dataTable);
                FillDataRows(shTable, dataTable);
            }
            return dataSet;
        }
        private static void CreateColumns(ExecOperationContent shTable, DataTable dataTable)
        {
            for (int i = 0; i < shTable.Fields.Length; i++)
            {
                dataTable.Columns.Add(new DataColumn
                {
                    ColumnName = shTable.Original[i],
                    Caption = shTable.Fields[i],
                    DataType = typeof(object)
                });
            }
        }
        private static void FillDataRows(ExecOperationContent shTable, DataTable dataTable)
        {
            int columnCount = dataTable.Columns.Count;
            int rowCount = shTable.Values.First().Length;

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var row = dataTable.NewRow();
                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    object value = shTable.Values[columnIndex][rowIndex];
                    row[columnIndex] = value;
                }
                dataTable.Rows.Add(row);
            }
        }
    }
}
