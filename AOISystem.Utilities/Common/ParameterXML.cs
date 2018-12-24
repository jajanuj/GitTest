using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AOISystem.Utilities.Common
{
    [Serializable]
    public class ParameterXML<T> where T : new() 
    {
        //因為Load時，會去跑參數的Set存取子，因而呼叫WriteAndNotify，但此時正在處理檔案序列化不能寫檔，所以設定此變數
        protected bool _isReadFile = false;

        public ParameterXML()
        {
            this.FolderPath = Application.StartupPath;
            this.FileName = this.GetType().Name;
        }

        public ParameterXML(string folderPath)
        {
            this.FolderPath = folderPath;
            this.FileName = this.GetType().Name;
        }

        public ParameterXML(string folderPath, string fileName)
        {
            this.FolderPath = folderPath;
            this.FileName = fileName;
        }

        public delegate void ParameterChangedHandler(string paraName);

        public event ParameterChangedHandler ParameterChanged;

        [Browsable(false), XmlIgnore()]
        public string FolderPath { get; set; }

        [Browsable(false), XmlIgnore()]
        public string FileName { get; set; }

        public bool Load()
        {
            return Load(this.FolderPath, this.FileName);
        }

        public bool Load(string folderPath)
        {
            return Load(folderPath, this.FileName);
        }

        /// <summary>
        /// 讀取參數檔
        /// </summary>
        /// <returns></returns>
        public bool Load(string folderPath, string fileName)
        {
            this.FolderPath = folderPath;
            this.FileName = fileName;
            string path = string.Format(@"{0}\{1}.xml", folderPath, fileName);
            _isReadFile = true;
            try
            {
                object instance = null;
                if (!File.Exists(path))
                {
                    instance = new T();
                    File.Delete(path);
                }
                else
                {
                    using (FileStream fileStream = new FileStream(path, FileMode.Open))
                    {
                        XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));
                        instance = (T)xmlFormatter.Deserialize(fileStream);
                    }
                }
                PropertyHelper.BlindingInstanceProperty(instance, this);
                PropertyHelper.AvoidPropertyInstanceIsNull(this);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.MessageBoxShow(ex, "SystemHint");
                return false;
            }
            finally
            {
                _isReadFile = false;
            }
        }

        public bool Save()
        {
            return Save(this.FolderPath, this.FileName);
        }

        public bool Save(string folderPath)
        {
            return Save(folderPath, this.FileName);
        }

        /// <summary>
        /// 寫入參數檔
        /// </summary>
        /// <param name="recipeData"></param>
        public bool Save(string folderPath, string fileName)
        {
            this.FolderPath = folderPath;
            this.FileName = fileName;
            string path = string.Format(@"{0}\{1}.xml", folderPath, fileName);

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));
                    xmlFormatter.Serialize(fileStream, this);
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.MessageBoxShow(ex, "SystemHint");
                return false;
            }
        }

        private void BlindingInstanceProperty2(object instance)
        {
            PropertyInfo[] propertyInfos = this.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                XmlIgnoreAttribute xmlIgnoreAttribute =
                        (XmlIgnoreAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(XmlIgnoreAttribute));
                if (xmlIgnoreAttribute != null)
                {
                    continue;
                }
                object value = propertyInfo.GetValue(instance, null);
                propertyInfo.SetValue(this, value, null);
            }
        }

        protected void WriteAndNotify(string paraName)
        {
            if (!_isReadFile)
            {
                try
                {
                    Save();
                    if (ParameterChanged != null)
                    {
                        ParameterChanged(paraName);
                    }
                }
                catch (System.IO.IOException ex)
                {
                    string content = string.Format(@"Error occur when access file: \n{0}\{1}.xml\n\n{2}", this.FolderPath, this.FileName, ex.Message);
                    MessageBox.Show(content, "Error Occur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //public static T ShowDialog<T>(T instance, string title, ref string currentFileFullPath)
        //{
        //    FilteredPropertyGridForm fpgf = new FilteredPropertyGridForm(instance, title, false, 0);
        //    fpgf.OnOpenFile += new FilteredPropertyGridForm.ReadFileHandle((f) => { return ReadFromXml<T>(f); });
        //    fpgf.OnSaveFile += new FilteredPropertyGridForm.WriteFileHandle((f, i) => { WriteToXml(f, (T)i); });
        //    return (T)fpgf.GetResult(ref currentFileFullPath);
        //}

        //public static T ShowDialog<T>(T instance, string title, string currentFileFullPath)
        //{
        //    FilteredPropertyGridForm fpgf = new FilteredPropertyGridForm(instance, title, true, 0);
        //    fpgf.OnOpenFile += new FilteredPropertyGridForm.ReadFileHandle((f) => { return ReadFromXml<T>(f); });
        //    fpgf.OnSaveFile += new FilteredPropertyGridForm.WriteFileHandle((f, i) => { WriteToXml(f, (T)i); });
        //    return (T)fpgf.GetResult(ref currentFileFullPath);
        //}

        //public static T ShowDialog<T>(T instance, string title, int userPermissions, ref string currentFileFullPath)
        //{
        //    FilteredPropertyGridForm fpgf = new FilteredPropertyGridForm(instance, title, false, userPermissions);
        //    fpgf.OnOpenFile += new FilteredPropertyGridForm.ReadFileHandle((f) => { return ReadFromXml<T>(f); });
        //    fpgf.OnSaveFile += new FilteredPropertyGridForm.WriteFileHandle((f, i) => { WriteToXml(f, (T)i); });
        //    return (T)fpgf.GetResult(ref currentFileFullPath);
        //}

        //public static T ShowDialog<T>(T instance, string title, int userPermissions, string currentFileFullPath)
        //{
        //    FilteredPropertyGridForm fpgf = new FilteredPropertyGridForm(instance, title, true, userPermissions);
        //    fpgf.OnOpenFile += new FilteredPropertyGridForm.ReadFileHandle((f) => { return ReadFromXml<T>(f); });
        //    fpgf.OnSaveFile += new FilteredPropertyGridForm.WriteFileHandle((f, i) => { WriteToXml(f, (T)i); });
        //    return (T)fpgf.GetResult(ref currentFileFullPath);
        //}

        //public static T ShowDialog<T>(T instance, string title, Size dialogSize, ref string currentFileFullPath)
        //{
        //    FilteredPropertyGridForm fpgf = new FilteredPropertyGridForm(instance, title, false, 0, dialogSize);
        //    fpgf.OnOpenFile += new FilteredPropertyGridForm.ReadFileHandle((f) => { return ReadFromXml<T>(f); });
        //    fpgf.OnSaveFile += new FilteredPropertyGridForm.WriteFileHandle((f, i) => { WriteToXml(f, (T)i); });
        //    return (T)fpgf.GetResult(ref currentFileFullPath);
        //}

        //public static T ShowDialog<T>(T instance, string title, Size dialogSize, string currentFileFullPath)
        //{
        //    FilteredPropertyGridForm fpgf = new FilteredPropertyGridForm(instance, title, true, 0, dialogSize);
        //    fpgf.OnOpenFile += new FilteredPropertyGridForm.ReadFileHandle((f) => { return ReadFromXml<T>(f); });
        //    fpgf.OnSaveFile += new FilteredPropertyGridForm.WriteFileHandle((f, i) => { WriteToXml(f, (T)i); });
        //    return (T)fpgf.GetResult(ref currentFileFullPath);
        //}

        //public static T ShowDialog<T>(T instance, string title, int userPermissions, Size dialogSize, ref string currentFileFullPath)
        //{
        //    FilteredPropertyGridForm fpgf = new FilteredPropertyGridForm(instance, title, false, userPermissions, dialogSize);
        //    fpgf.OnOpenFile += new FilteredPropertyGridForm.ReadFileHandle((f) => { return ReadFromXml<T>(f); });
        //    fpgf.OnSaveFile += new FilteredPropertyGridForm.WriteFileHandle((f, i) => { WriteToXml(f, (T)i); });
        //    return (T)fpgf.GetResult(ref currentFileFullPath);
        //}

        //public static T ShowDialog<T>(T instance, string title, int userPermissions, Size dialogSize, string currentFileFullPath)
        //{
        //    FilteredPropertyGridForm fpgf = new FilteredPropertyGridForm(instance, title, true, userPermissions, dialogSize);
        //    fpgf.OnOpenFile += new FilteredPropertyGridForm.ReadFileHandle((f) => { return ReadFromXml<T>(f); });
        //    fpgf.OnSaveFile += new FilteredPropertyGridForm.WriteFileHandle((f, i) => { WriteToXml(f, (T)i); });
        //    return (T)fpgf.GetResult(ref currentFileFullPath);
        //}
    }
}
