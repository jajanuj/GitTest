using AOISystem.Utilities.Component;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AOISystem.Utilities.IO
{
    public class SimpleCsvHelper
    {
        #region - Export CSV File -
        public static void ExportDataGridViewToCsv(DataGridView dataGridView, string fileName)
        {
            //test to see if the DataGridView has any rows
            if (dataGridView.RowCount > 0)
            {
                string value = "";
                DataGridViewRow dr = new DataGridViewRow();
                StreamWriter swOut = new StreamWriter(fileName);

                //write header rows to csv
                for (int i = 0; i <= dataGridView.Columns.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        swOut.Write(",");
                    }
                    swOut.Write(dataGridView.Columns[i].HeaderText);
                }

                swOut.WriteLine();

                //write DataGridView rows to csv
                for (int j = 0; j <= dataGridView.Rows.Count - 1; j++)
                {
                    if (j > 0)
                    {
                        swOut.WriteLine();
                    }

                    dr = dataGridView.Rows[j];

                    if (!dr.IsNewRow)
                    {
                        for (int i = 0; i <= dataGridView.Columns.Count - 1; i++)
                        {
                            if (i > 0)
                            {
                                swOut.Write(",");
                            }
                            if (dr.Cells[i].ValueType.FullName == "System.DateTime")
	                        {
                                DateTime dateTime = (DateTime)dr.Cells[i].Value;
                                value = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
	                        }
                            else
                            {
                                value = dr.Cells[i].Value.ToString();
                            }
                            //replace comma's with spaces
                            value = value.Replace(',', ' ');
                            //replace embedded newlines with spaces
                            value = value.Replace(Environment.NewLine, " ");

                            swOut.Write(value);
                        }   
                    }
                }
                swOut.Close();
            }
        }

        /// <summary>
        /// Exports the DataGridView to CSV.
        /// </summary>
        /// <param name="dataGridView">The data grid view.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="ColumnName">Name of the column.</param>
        /// <param name="HasColumnName">if set to <c>true</c> [has column name].</param>
        public static void ExportDataGridViewToCsv(DataGridView dataGridView, string FileName, string[] ColumnName, bool HasColumnName)
        {
            string strValue = string.Empty;
            //CSV 匯出的標題 要先塞一樣的格式字串 充當標題
            if (HasColumnName == true)
                strValue = string.Join(",", ColumnName);
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView.Rows[i].Cells.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dataGridView[j, i].Value.ToString()))
                    {
                        if (j > 0)
                            strValue = strValue + "," + dataGridView[j, i].Value.ToString();
                        else
                        {
                            if (string.IsNullOrEmpty(strValue))
                                strValue = dataGridView[j, i].Value.ToString();
                            else
                                strValue = strValue + Environment.NewLine + dataGridView[j, i].Value.ToString();
                        }
                    }
                    else
                    {
                        if (j > 0)
                            strValue = strValue + ",";
                        else
                            strValue = strValue + Environment.NewLine;
                    }
                }
            }
            //存成檔案
            string strFile = FileName;
            if (!string.IsNullOrEmpty(strValue))
            {
                File.WriteAllText(strFile, strValue, Encoding.Unicode);
            }
        }
        /// <summary>
        /// Exports the data table to CSV.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="ColumnName">Name of the column.</param>
        /// <param name="HasColumnName">if set to <c>true</c> [has column name].</param>
        public static void ExportDataTableToCsv(DataTable dt, string FileName, string[] ColumnName, bool HasColumnName)
        {
            string strValue = string.Empty;
            //CSV 匯出的標題 要先塞一樣的格式字串 充當標題
            if (HasColumnName == true)
                strValue = string.Join(",", ColumnName);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                    {
                        if (j > 0)
                            strValue = strValue + "," + dt.Rows[i][j].ToString();
                        else
                        {
                            if (string.IsNullOrEmpty(strValue))
                                strValue = dt.Rows[i][j].ToString();
                            else
                                strValue = strValue + Environment.NewLine + dt.Rows[i][j].ToString();
                        }
                    }
                    else
                    {
                        if (j > 0)
                            strValue = strValue + ",";
                        else
                            strValue = strValue + Environment.NewLine;
                    }
                }

            }
            //存成檔案
            string strFile = FileName;
            if (!string.IsNullOrEmpty(strValue))
            {
                File.WriteAllText(strFile, strValue, Encoding.Unicode);
            }
        }
        /// <summary>Reads the CSV to list.</summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        public static List<string> ReadCsvToList(string FileName)
        {
            FileInfo fi = new FileInfo(FileName);
            if (fi.Exists)
            {
                List<string> result = new List<string>();
                using (StreamReader sr = new StreamReader(FileName, Encoding.Default))
                {
                    while (sr.Peek() >= 0)
                    {
                        result.Add(sr.ReadLine());
                    }
                }
                return result;
            }
            else return null;
        }
        /// <summary>Write List Data to CSV.</summary>
        /// <param name="list"></param>
        /// <param name="FileName"></param>
        public static void ExportDataListToCsv(List<string> dataList, string FileName, string[] ColumnName, bool HasColumnName)
        {
            string strValue = string.Empty;
            //CSV 匯出的標題 要先塞一樣的格式字串 充當標題
            if (HasColumnName == true)
                strValue = string.Join(",", ColumnName) + Environment.NewLine;
            for (int i = 0; i < dataList.Count; i++)
            {
                strValue += dataList[i] + Environment.NewLine;
            }
            //存成檔案
            string strFile = FileName;
            if (!string.IsNullOrEmpty(strValue))
            {
                File.WriteAllText(strFile, strValue, Encoding.Default);
            }
        }
        /// <summary>
        /// Reads the CSV to data table.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="HasColumnName">if set to <c>true</c> [has column name].</param>
        /// <returns></returns>
        public static DataTable ReadCsvToDataTable(string FileName, bool HasColumnName)
        {
            List<string> Input = ReadCsvToList(FileName);
            if (Input != null)
            {
                string[] sep = new string[] { "," };
                DataTable dt = new DataTable();
                int StartCount = (HasColumnName == true) ? 1 : 0;
                string[] ColumnName = Input[0].Split(sep, StringSplitOptions.None);
                for (int i = 0; i < ColumnName.Length; i++)
                    dt.Columns.Add((HasColumnName == true) ? ColumnName[i] : "C" + i.ToString(), typeof(string));
                for (int j = StartCount; j < Input.Count; j++)
                {
                    string[] valuetemp = Input[j].Split(sep, StringSplitOptions.None);
                    dt.Rows.Add(valuetemp);
                }
                return dt;
            }
            else return null;
        }

        private static AutoResetEvent _areCSV = new AutoResetEvent(true);

        public static void AddData(string folderPath, string fileName, string format, params object[] args)
        {
            StreamWriter sw;
            try
            {
                _areCSV.WaitOne();
                string data = string.Format(format, args);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = string.Format(@"{0}\{1}", folderPath, fileName);
                PathHelper.FixFileNameExtension(ref filePath, ".csv");
                sw = new StreamWriter(filePath, true, Encoding.Default);
                sw.WriteLine(data);
                sw.Flush();
                sw.Close();

                sw.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _areCSV.Set();
            }
        }

        public static void AddData(string folderPath, string fileName, params object[] args)
        {
            StringBuilder format = new StringBuilder(args.Length);
            format.Append("{0}");
            for (int i = 1; i < args.Length; i++)
            {
                format.Append(",{" + i.ToString() + "}");
            }
            AddData(folderPath, fileName, format.ToString(), args);
        }
        #endregion -  Export CSV File -

        public static string[] GetStringArrayValueFormParseString(string parseString)
        {
            return parseString.Split(',');
        }

        public static string GetParseStringFormStringArrayValue<T>(T[] array)
        {
            return String.Join(",", array);
        }

        public static int[] GetInt32ArrayValueFormParseString(string parseString)
        {
            string[] parseArray = parseString.Split(',', ';');
            int[] returnValue = new int[parseArray.Length];
            for (int i = 0; i < parseArray.Length; i++)
            {
                returnValue[i] = int.Parse(parseArray[i]);
            }
            return returnValue;
        }

        public static string GetParseStringFormInt32ArrayValue(int[] array)
        {
            string[] parseArray = new string[array.Length];
            for (int i = 0; i < parseArray.Length; i++)
            {
                parseArray[i] = array[i].ToString();
            }
            return String.Join(",", parseArray);
        }

        public static bool[] GetBooleanArrayValueFormParseString(string parseString)
        {
            string[] parseArray = parseString.Split(',', ';');
            bool[] returnValue = new bool[parseArray.Length];
            for (int i = 0; i < parseArray.Length; i++)
            {
                returnValue[i] = bool.Parse(parseArray[i]);
            }
            return returnValue;
        }

        public static string GetParseStringFormBooleanArrayValue(bool[] array)
        {
            string[] parseArray = new string[array.Length];
            for (int i = 0; i < parseArray.Length; i++)
            {
                parseArray[i] = array[i].ToString();
            }
            return String.Join(",", parseArray);
        }

        public static T GetInstanceFormParseString<T>(string parseString) where T : new()  
        {
            string[] parseArray = parseString.Split(',', ';');
            int[] returnValue = new int[parseArray.Length];
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            T t = new T();
            for (int i = 0; i < parseArray.Length; i++)
            {
                PropertyInfo property = propertyInfos[i];
                string stringValue = parseArray[i];
                Type type = property.PropertyType;
                string typeName = property.PropertyType.FullName;
                switch (typeName)
                {
                    case "System.String":
                        property.SetValue(t, stringValue, null);
                        break;
                    case "System.Int16":
                        property.SetValue(t, Int16.Parse(stringValue), null);
                        break;
                    case "System.UInt16":
                        property.SetValue(t, UInt16.Parse(stringValue), null);
                        break;
                    case "System.Int32":
                        property.SetValue(t, Int32.Parse(stringValue), null);
                        break;
                    case "System.UInt32":
                        property.SetValue(t, UInt32.Parse(stringValue), null);
                        break;
                    case "System.Int64":
                        property.SetValue(t, Int64.Parse(stringValue), null);
                        break;
                    case "System.UInt64":
                        property.SetValue(t, UInt64.Parse(stringValue), null);
                        break;
                    case "System.Single":
                        property.SetValue(t, Single.Parse(stringValue), null);
                        break;
                    case "System.Double":
                        property.SetValue(t, Double.Parse(stringValue), null);
                        break;
                    case "System.Boolean":
                        property.SetValue(t, Boolean.Parse(stringValue), null);
                        break;
                    case "HalconDotNet.HTuple":
                        Type typeHTupleTypeConverter = Assembly.Load("AOISystem.Halcon").GetType("AOISystem.Halcon.HPropertyType.HTupleTypeConverter");
                        MethodInfo methodHTupleStringTypeConverter = typeHTupleTypeConverter.GetMethod("HTupleStringTypeConverter", BindingFlags.Public | BindingFlags.Static);
                        object resultValue = methodHTupleStringTypeConverter.Invoke(t, new object[] { stringValue });
                        property.SetValue(t, resultValue, null);
                        break;
                    default:
                        if (type.IsEnum)
                        {
                            property.SetValue(t, Enum.Parse(type, stringValue), null);
                            break;
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException(string.Format("PropertyInfo Convert don't define {0} convet rule.", typeName));
                        }
                }
            }
            return t;
        }
    }
}
