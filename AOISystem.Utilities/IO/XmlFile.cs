using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

namespace AOISystem.Utilities.IO
{
    /// <summary>
    /// XmlFile 的摘要描述
    /// </summary>
    public class XmlFile
    {
        #region 讀寫xml檔案
        /// <summary>
        /// 寫入xml檔案，檔案存在會新增節點.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="Header">The header.</param>
        /// <param name="NodeName">Name of the node.</param>
        /// <param name="NodeValue">The node value.</param>
        public static void WriteXml(string FileName, string[] Header, string[] NodeName, string[] NodeValue)
        {

            if (!File.Exists(FileName))
            {
                XmlDocument xmlDoc = new XmlDocument();

                XmlNode node = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                xmlDoc.AppendChild(node);
                List<XmlElement> HeaderNode = new List<XmlElement>();
                for (int j = 0; j < Header.Length; j++)
                {
                    HeaderNode.Add(xmlDoc.CreateElement(Header[j]));
                    if (j == 0) xmlDoc.AppendChild(HeaderNode[j]);
                    else HeaderNode[j - 1].AppendChild(HeaderNode[j]);
                }
                for (int i = 0; i < NodeName.Length; i++)
                {
                    XmlElement temp = xmlDoc.CreateElement(NodeName[i]);
                    temp.InnerText = NodeValue[i];
                    HeaderNode[Header.Length - 1].AppendChild(temp);
                }

                xmlDoc.Save(FileName);
            }

            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(FileName);
                string SelectNode = string.Empty;
                if (Header.Length == 1)
                    SelectNode = "/" + Header[0].ToString();
                else
                {
                    for (int q = 0; q < Header.Length - 1; q++)
                        SelectNode += "/" + Header[q].ToString();
                }
                XmlNode node = xmlDoc.SelectSingleNode(SelectNode);
                XmlElement xmlFinalNode = xmlDoc.CreateElement(Header[Header.Length - 1].ToString());
                node.AppendChild(xmlFinalNode);


                for (int i = 0; i < NodeName.Length; i++)
                {
                    XmlElement temp = xmlDoc.CreateElement(NodeName[i]);
                    temp.InnerText = NodeValue[i];
                    xmlFinalNode.AppendChild(temp);
                }
                xmlDoc.Save(FileName);
            }
        }

        /// <summary>
        /// 寫入xml檔案，檔案存在則新增.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="Header">The header.</param>
        /// <param name="NodeName">Name of the node.</param>
        /// <param name="NodeValue">The node value.</param>
        public static void AppendXml(string FileName, string[] Header, string[] NodeName, string[] NodeValue)
        {

            if (!File.Exists(FileName))
            {
                XmlDocument xmlDoc = new XmlDocument();

                XmlNode node = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                xmlDoc.AppendChild(node);
                List<XmlElement> HeaderNode = new List<XmlElement>();
                for (int j = 0; j < Header.Length; j++)
                {
                    HeaderNode.Add(xmlDoc.CreateElement(Header[j]));
                    if (j == 0) xmlDoc.AppendChild(HeaderNode[j]);
                    else HeaderNode[j - 1].AppendChild(HeaderNode[j]);
                }
                for (int i = 0; i < NodeName.Length; i++)
                {
                    XmlElement temp = xmlDoc.CreateElement(NodeName[i]);
                    temp.InnerText = NodeValue[i];
                    HeaderNode[Header.Length - 1].AppendChild(temp);
                }

                xmlDoc.Save(FileName);
            }

            else
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(FileName);
                string SelectNode = string.Empty;
                if (Header.Length == 1)
                    SelectNode = "/" + Header[0].ToString();
                else
                {
                    for (int q = 0; q < Header.Length - 1; q++)
                        SelectNode += "/" + Header[q].ToString();
                }
                XmlNode node = xmlDoc.SelectSingleNode(SelectNode);
                XmlElement[] Result = new XmlElement[NodeName.Length];
                for (int j = 0; j < Result.Length; j++)
                {
                    XmlElement xmlFinalNode = xmlDoc.CreateElement(NodeName[j].ToString());
                    xmlFinalNode.InnerText = NodeValue[j].ToString();
                    node.AppendChild(xmlFinalNode);
                }
                xmlDoc.Save(FileName);
            }
        }
        /// <summary>
        /// Sets the XML to string array  spliter use "\t" .
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="Header">The header.</param>
        /// <param name="NodeName">Name of the node.</param>
        /// <returns></returns>
        public static List<string> SetXmlToStringArray(string FileName, string[] Header, string[] NodeName, bool IsSameNodeName)
        {
            List<string> result = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(FileName);
            string SelectNode = string.Empty;
            for (int q = 0; q < Header.Length - 1; q++)
                SelectNode += "/" + Header[q].ToString();
            XmlNode node = doc.SelectSingleNode(SelectNode);
            XmlNodeList nodelist = node.SelectNodes(Header[Header.Length - 1].ToString());
            foreach (XmlNode tempNode in nodelist)
            {
                string TempString = string.Empty;
                if (!IsSameNodeName)
                {
                    for (int i = 0; i < NodeName.Length; i++)
                    {

                        TempString += tempNode.SelectSingleNode(NodeName[i]).InnerText + "\t";

                    }
                }
                else
                    TempString += tempNode.InnerText + "\t";
                result.Add(TempString);
            }
            return result;
        }
        #endregion
        #region xml與dataset，datatable應用
        /// <summary>
        /// xml字串寫入dataset.
        /// </summary>
        /// <param name="XmlString">The XML string.</param>
        /// <returns></returns>
        public static DataSet SetXmlStringToDataSet(string XmlString)
        {
            /*    
             * string xmlString = 
                   "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + 
                   "<employees xmlns=\"http://schemas.microsoft.com/vsto/samples\">" + 
                       "<employee>" + 
                           "<name>Karina Leal</name>" + 
                           "<hireDate>1999-04-01</hireDate>" + 
                           "<title>Manager</title>" + 
                       "</employee>" + 
                   "</employees>"; */


            StringReader reader = new StringReader(XmlString);
            DataSet AuthorsDataSet = new DataSet();
            AuthorsDataSet.ReadXml(reader);
            return AuthorsDataSet;
        }

        /// <summary>
        /// xml檔案寫入dataset.
        /// </summary>
        /// <param name="XmlFile">The XML file.</param>
        /// <returns></returns>
        public static DataSet SetXmlToDataSet(string XmlFile)
        {
            DataSet AuthorsDataSet = new DataSet();
            AuthorsDataSet.ReadXml(XmlFile);
            return AuthorsDataSet;

        }
        /// <summary>
        /// xml檔案寫入datatable.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        public static DataTable SetXmlToDataTable(string FileName)
        {
            DataTable dt = new DataTable();
            dt.ReadXml(FileName);
            return dt;
        }
        /// <summary>
        /// xml字串寫入dataTable.
        /// </summary>
        /// <param name="XmlString">The XML string.</param>
        /// <returns></returns>
        public static DataTable SetXmlStringToDataTable(string XmlString)
        {
            /*    
             * string xmlString = 
                   "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + 
                   "<employees xmlns=\"http://schemas.microsoft.com/vsto/samples\">" + 
                       "<employee>" + 
                           "<name>Karina Leal</name>" + 
                           "<hireDate>1999-04-01</hireDate>" + 
                           "<title>Manager</title>" + 
                       "</employee>" + 
                   "</employees>"; */


            StringReader reader = new StringReader(XmlString);
            DataTable AuthorsDataSet = new DataTable();
            AuthorsDataSet.ReadXml(reader);
            return AuthorsDataSet;
        }
        /// <summary>
        /// datatable寫入xml檔案.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="dt">The dt.</param>
        public static void SetDataTableToXmlFile(string FileName, DataTable dt)
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
            dt.WriteXml(FileName);
        }
        /// <summary>
        /// datatable寫入xml字串.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string SetDataTableToXmlString(DataTable dt)
        {
            StringWriter sw = new StringWriter();
            dt.WriteXml(sw);
            return sw.ToString();
        }
        /// <summary>
        /// dataset寫入xml檔案.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="dt">The dt.</param>
        public static void SetDataSetToXmlFile(string FileName, DataSet dt)
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
            dt.WriteXml(FileName);
        }
        /// <summary>
        /// dataset寫入xml字串.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string SetDataTableToXmlString(DataSet dt)
        {
            StringWriter sw = new StringWriter();
            dt.WriteXml(sw);
            return sw.ToString();
        }

        #endregion
        #region 節點操作
        /// <summary>
        /// Gets the node list.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        public static XmlNodeList GetNodeList(string fileName, string Path)
        {
            XmlNodeList NodeList = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                NodeList = doc.SelectNodes(Path);

            }
            catch//(Exception ex)
            {
                //throw ex;
            }
            return NodeList;
        }

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        public static XmlNode GetNode(string fileName, string Path)
        {
            XmlNode Node = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                Node = doc.SelectSingleNode(Path);

            }
            catch
            { }
            return Node;
        }

        /// <summary>
        /// Reads the decimal.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        public static decimal ReadDecimal(string fileName, string Path)
        {
            decimal dec = 0;
            try
            {
                XmlNodeList list = GetNodeList(fileName, Path);

                foreach (XmlNode node in list)
                {
                    if (node.InnerXml == null || node.InnerXml.Length == 0)
                    {
                    }
                    else
                    {
                        dec += Convert.ToDecimal(node.InnerXml);
                    }
                }
            }
            catch
            { }
            return dec;
        }

        /// <summary>
        /// Reads the decimal string.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        public static string ReadDecimalString(string fileName, string Path)
        {
            decimal dec = 0;
            string strDec = String.Empty;
            try
            {
                XmlNodeList list = GetNodeList(fileName, Path);

                foreach (XmlNode node in list)
                {
                    if (node.InnerXml == null || node.InnerXml.Length == 0)
                    {
                    }
                    else
                    {
                        dec += Convert.ToDecimal(node.InnerXml);
                    }
                }
                if (dec == 0)
                    strDec = dec.ToString();
                else
                    strDec = dec.ToString("##,###,###,###,###,###");

            }
            catch
            { }
            return strDec;
        }

        /// <summary>
        /// Reads the nodes value.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        public static string ReadNodesValue(string fileName, string Path)
        {
            string strDec = String.Empty;
            try
            {
                XmlNodeList list = GetNodeList(fileName, Path);

                foreach (XmlNode node in list)
                {
                    if (node.InnerXml == null)
                    {
                        strDec = "";
                    }
                    else
                    {
                        strDec += node.InnerXml;
                    }
                }
            }
            catch { }
            return strDec;
        }

        /// <summary>
        /// Reads the nodes.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        public static string[] ReadNodes(string fileName, string Path)
        {
            XmlNodeList list = GetNodeList(fileName, Path);
            string[] arrList = new string[list.Count];
            int i = 0;
            foreach (XmlNode node in list)
            {
                if (node.InnerXml == null)
                {
                    arrList[i] = "";
                }
                else
                {
                    arrList[i] = node.InnerXml;
                }
                i++;
            }
            return arrList;
        }

        /// <summary>
        /// Deletes the node.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="Path">The path.</param>
        public static void DeleteNode(string fileName, string Path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNodeList list = doc.SelectNodes(Path);
            if (list != null)
            {
                XmlNode node = list.Item(0);
                if (node != null)
                {
                    node.ParentNode.RemoveChild(node);
                    doc.Save(fileName);
                }
            }
        }

        /// <summary>
        /// Gets the node list with XML.
        /// </summary>
        /// <param name="XmlString">The XML string.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        public static XmlNodeList GetNodeListWithXml(string XmlString, string Path)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XmlString);
            XmlNodeList list = doc.SelectNodes(Path);
            return list;
        }

        /// <summary>
        /// Gets the node with XML.
        /// </summary>
        /// <param name="XmlString">The XML string.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        public static XmlNode GetNodeWithXml(string XmlString, string Path)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XmlString);
            XmlNode node = doc.SelectSingleNode(Path);
            return node;
        }
        #endregion

        #region - Serialize / Deserialize -
        public static string Serialize<T>(T value)
        {
            if (value == null)
            {
                return null;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false);
            settings.Indent = false;
            settings.OmitXmlDeclaration = false;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, value);
                }
                return textWriter.ToString();
            }
        }

        public static T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlReaderSettings settings = new XmlReaderSettings();

            using (StringReader textReader = new StringReader(xml))
            {
                using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }

        public static void SerializeToFile<T>(string path, T value)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));
                xmlFormatter.Serialize(fileStream, value);
            }
            //using (FileStream fs = File.Create(path))
            //{
            //    string xmlData = Serialize<T>(value);
            //    byte[] info = new UTF8Encoding(true).GetBytes(xmlData);
            //    fs.Write(info, 0, info.Length);
            //}
        }

        public static T DeserializeFromFile<T>(string path) where T : new()
        {
            if (!File.Exists(path))
            {
                return new T();
            }
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));
                return (T)xmlFormatter.Deserialize(fileStream);
            }
            //using (FileStream fs = File.OpenRead(path))
            //{
            //    byte[] b = new byte[1024];
            //    UTF8Encoding temp = new UTF8Encoding(true);
            //    StringBuilder sb = new StringBuilder();
            //    while (fs.Read(b, 0, b.Length) > 0)
            //    {
            //        sb.Append(temp.GetString(b));
            //    }
            //    return Deserialize<T>(sb.ToString());
            //}
        }
        #endregion - Serialize / Deserialize -
    }
}
