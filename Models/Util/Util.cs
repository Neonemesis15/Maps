using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace Xplora.GIS.Models.Util
{
    public static class Util
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {

            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable("Tabla");
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static string getRutaVirtualdeRutaFisica /*GetVirtualPathFromPhysicalFilePath*/(string rutafisica)
        {
            return rutafisica.Replace(HttpRuntime.AppDomainAppPath, "/").Replace(Path.DirectorySeparatorChar, '/');
        }

        public static DataTable ConvertToDataTable(string[] headers, string[][] content)
        {
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header, Type.GetType("System.String"));
            }

            DataRow dr;

            for (int i = 0; i < content.Length; i++)
            {
                dr = dt.NewRow();
                for (int j = 0; j < content[i].Length; j++)
                {
                    dr[j] = content[i][j];
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

    }
}