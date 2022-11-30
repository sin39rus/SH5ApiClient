using Newtonsoft.Json.Linq;

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
    }
}
