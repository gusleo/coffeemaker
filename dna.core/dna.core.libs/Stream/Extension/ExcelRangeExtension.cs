using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using System.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace dna.core.libs.Stream.Extension
{
    public static class ExcelRangeExtension
    {
        /// <summary>
        /// Convert ExcelWorksheet to IList of class
        /// </summary>
        public static List<T> ToList<T>(this ExcelWorksheet table, bool FirstRowAsColumnName) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            List<T> result = new List<T>();

            /*for ( int i = 0; i <= table.Columns.Count - 1; i++ )
            {
                string columnName = table.Columns[i].ColumnName;
                table.Columns[i].ColumnName = Regex.Replace(columnName, @"\s+", "");
            }*/
            
            
            int start = FirstRowAsColumnName ? 2 : 1;

            for ( var rowNumber = start; rowNumber <= table.Dimension.End.Row; rowNumber++ )
            {
                var row = table.Cells[rowNumber, 1, rowNumber, table.Dimension.End.Column];
                var item = CreateItemFromRow<T>(row, properties);
                result.Add(item);
            }              
            return result;
        }

        private static T CreateItemFromRow<T>(ExcelRange row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            int index = 0;
            //ignore not mapped attribute
            properties = properties.Where(x => x.GetCustomAttribute<NotMappedAttribute>() == null).ToList();
            
            foreach ( var property in properties )
            {

                string value;
                try
                {
                    value = row.ElementAt(index).Text;
                    if ( property.PropertyType == typeof(System.DayOfWeek) )
                    {
                        DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), value);
                        property.SetValue(item, day, null);
                    }
                    else if ( property.PropertyType == typeof(System.DateTime) || property.PropertyType == typeof(Nullable<System.DateTime>) )
                    {


                        if ( !String.IsNullOrWhiteSpace(value) )
                        {
                            DateTime date = DateTime.Parse(value);
                            property.SetValue(item, date, null);
                        }
                        else
                        {
                            property.SetValue(item, null, null);
                        }

                    }
                    else if ( property.PropertyType == typeof(int) || property.PropertyType == typeof(Nullable<int>) )
                    {
                        if ( value != null )
                        {
                            property.SetValue(item, int.Parse(value), null);
                        }
                        else
                        {
                            property.SetValue(item, null, null);
                        }

                    }
                    else if ( property.PropertyType == typeof(bool) || property.PropertyType == typeof(Nullable<int>) )
                    {
                        if ( value != null )
                        {
                            bool _newValue = false;
                            int dump;
                            if ( int.TryParse(value, out dump) )
                            {
                                _newValue = dump == 0 ? false : true;
                            }
                            else
                            {
                                _newValue = value.ToString().ToLower().Equals("true") ? true : false;
                            }
                            property.SetValue(item, _newValue, null);
                        }
                        else
                            property.SetValue(item, null, null);


                    }
                    else
                    {
                        property.SetValue(item, value, null);
                    }
                }
                catch
                {                    
                    property.SetValue(item, null, null);
                }

                
                index++;
            }
            
            return item;
        }
    }
}
