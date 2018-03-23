using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Lambda
{
    public static class ExcelExtension
    {
        public static DataTable EnumerableToDataTable<T>(this IEnumerable<T> data, string[] columns, string[] columnNames)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable(typeof(T).Name);
            foreach (var column in columnNames)
            {
                dataTable.Columns.Add(column);
            }
            var type = typeof(T);
            foreach (var item in data)
            {
                var row = dataTable.NewRow();
                for (int i = 0; i < columns.Length; i++)
                {
                    row[i] = type.GetProperty(columns[i]).GetValue(item, null);
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }

        public static async Task<DataTable> EnumerableToDataTableAsync<T>(this List<T> data, string[] columns, string[] columnNames)
        {
            return await Task.Run(() =>
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                DataTable dataTable = new DataTable(typeof(T).Name);
                foreach (var column in columnNames)
                {
                    dataTable.Columns.Add(column);
                }
                var type = typeof(T);
                foreach (var item in data)
                {
                    var row = dataTable.NewRow();
                    for (int i = 0; i < columns.Length; i++)
                    {
                        row[i] = type.GetProperty(columns[i]).GetValue(item, null);
                    }
                    dataTable.Rows.Add(row);
                }
                return dataTable;
            });
        }
    }
}
