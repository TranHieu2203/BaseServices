using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Base.Common.Helpers
{
    public class EntitiesHelper<T> where T : new()
    {
        public static List<T> GetListObject(DataTable dt)
        {
            List<T> lstObject = new List<T>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T obj = new T();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    int index = IndexOfField(dt.Columns[j].ColumnName);
                    if (index != -1)
                    {
                        PropertyInfo pi = obj.GetType().GetProperties()[index];
                        Type propType = pi.PropertyType;

                        if (propType.IsGenericType && (propType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            propType = propType.GetGenericArguments()[0];
                        }

                        if (propType.IsEnum)
                        {
                            int objectValue = 0;
                            Int32.TryParse(dt.Rows[i][j].ToString(), out objectValue);
                            pi.SetValue(obj, Enum.ToObject(propType, objectValue), null);
                        }
                        else if (dt.Rows[i][j] != DBNull.Value && dt.Columns[j].DataType != propType && dt.Columns[j].DataType == Type.GetType("System.DateTime"))
                        {
                            pi.SetValue(obj, DateTime.Parse(dt.Rows[i][j].ToString()).ToString("yyyy/MM/dd HH:mm:ss"), null);
                        }
                        else if (dt.Columns[j].DataType == propType && dt.Rows[i][j] != DBNull.Value)
                        {
                            pi.SetValue(obj, dt.Rows[i][j], null);
                        }
                        else if ((propType.Name.Equals("Boolean") || propType.Name.Equals("bool")) && dt.Rows[i][j] != DBNull.Value)
                        {
                            pi.SetValue(obj, Convert.ToBoolean(dt.Rows[i][j]), null);
                        }
                        else if (dt.Rows[i][j] != DBNull.Value && dt.Columns[j].DataType != propType)
                        {
                            pi.SetValue(obj, Convert.ToInt32(dt.Rows[i][j]), null);
                        }
                    }
                }

                lstObject.Add(obj);
            }

            return lstObject;
        }

        public static DataTable ConvertToDataTable(IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return null;
            }

            DataTable table = new DataTable();
            foreach (var prop in list[0].GetType().GetProperties())
            {
                Type colType = prop.PropertyType;

                if (colType.IsGenericType && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }

                table.Columns.Add(new DataColumn(prop.Name, colType));
            }

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (DataColumn column in table.Columns)
                {
                    int index = IndexOfField(column.ColumnName, item.GetType());
                    if (index != -1)
                    {
                        PropertyInfo info = item.GetType().GetProperties()[index];
                        Type propType = info.PropertyType;

                        if (propType.IsGenericType && (propType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            propType = propType.GetGenericArguments()[0];
                        }

                        if (propType == column.DataType)
                        {
                            var colValue = info.GetValue(item, null);
                            if (colValue != null)
                            {
                                row[column.ColumnName] = colValue;
                            }
                            else
                            {
                                row[column.ColumnName] = DBNull.Value;
                            }
                        }
                    }
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable ConvertObjectToDataTable(T obj)
        {
            if (obj == null)
            {
                return null;
            }

            DataTable table = new DataTable();
            foreach (var prop in obj.GetType().GetProperties())
            {
                Type colType = prop.PropertyType;

                if (colType.IsGenericType && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }

                table.Columns.Add(new DataColumn(prop.Name, colType));
            }

            DataRow row = table.NewRow();

            foreach (DataColumn column in table.Columns)
            {
                int index = IndexOfField(column.ColumnName, obj.GetType());
                if (index != -1)
                {
                    PropertyInfo info = obj.GetType().GetProperties()[index];
                    Type propType = info.PropertyType;

                    if (propType.IsGenericType && (propType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        propType = propType.GetGenericArguments()[0];
                    }

                    if (propType == column.DataType)
                    {
                        var colValue = info.GetValue(obj, null);
                        if (colValue != null)
                        {
                            row[column.ColumnName] = colValue;
                        }
                        else
                        {
                            row[column.ColumnName] = DBNull.Value;
                        }
                    }
                }
            }

            table.Rows.Add(row);


            return table;
        }

        public static DataTable ConvertObjectToDataTable(T obj, DataTable table)
        {
            if (obj == null)
            {
                return null;
            }


            DataRow row = table.Rows[0];

            foreach (DataColumn column in table.Columns)
            {
                int index = IndexOfField(column.ColumnName, obj.GetType());
                if (index != -1)
                {
                    PropertyInfo info = obj.GetType().GetProperties()[index];
                    Type propType = info.PropertyType;

                    if (propType.IsGenericType && (propType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        propType = propType.GetGenericArguments()[0];
                    }

                    if (propType == column.DataType)
                    {
                        var colValue = info.GetValue(obj, null);
                        if (colValue != null)
                        {
                            row[column.ColumnName] = colValue;
                        }
                        //else
                        //{
                        //    row[column.ColumnName] = DBNull.Value;
                        //}
                    }
                }
            }

            return table;
        }
        private static int IndexOfField(string colName)
        {
            T o = new T();
            PropertyInfo[] pi = o.GetType().GetProperties();
            for (int i = 0; i < pi.Length; i++)
            {
                if (pi[i].Name == colName)
                {
                    return i;
                }
            }

            return -1;
        }


        private static int IndexOfField(string colName, Type originType)
        {
            PropertyInfo[] pi = originType.GetProperties();
            for (int i = 0; i < pi.Length; i++)
            {
                if (pi[i].Name == colName)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
