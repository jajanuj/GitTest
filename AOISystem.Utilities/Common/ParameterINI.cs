using AOISystem.Utilities.Component;
using AOISystem.Utilities.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AOISystem.Utilities.Common
{
    [Serializable]
    public class ParameterINI
    {
        //因為Load時，會去跑參數的Set存取子，因而呼叫WriteAndNotify，但此時正在處理檔案序列化不能寫檔，所以設定此變數
        protected bool _isReadFile = false;

        public ParameterINI()
        {
            this.FolderPath = Application.StartupPath;
            this.FileName = this.GetType().Name;
            this.SectionName = "Configuration";
        }

        public ParameterINI(string folderPath)
        {
            this.FolderPath = folderPath;
            this.FileName = this.GetType().Name;
            this.SectionName = "Configuration";
        }

        public ParameterINI(string folderPath, string fileName)
        {
            this.FolderPath = folderPath;
            this.FileName = fileName;
            this.SectionName = "Configuration";
        }

        public ParameterINI(string folderPath, string fileName, string sectionName)
        {
            this.FolderPath = folderPath;
            this.FileName = fileName;
            this.SectionName = sectionName;
        }

        public delegate void ParameterChangedHandler(string paraName);

        public event ParameterChangedHandler ParameterChanged;

        [Browsable(false), NonMember()]
        public string FolderPath { get; set; }

        [Browsable(false), NonMember()]
        public string FileName { get; set; }

        [Browsable(false), NonMember()]
        public string SectionName { get; set; }

        public bool Load()
        {
            return Load(this.FolderPath, this.FileName, this.SectionName);
        }

        public bool Load(string folderPath)
        {
            return Load(folderPath, this.FileName, this.SectionName);
        }

        public bool Load(string folderPath, string fileName)
        {
            return Load(folderPath, fileName, this.SectionName);
        }

        public bool Load(string folderPath, string fileName, string sectionName)
        {
            this.FolderPath = folderPath;
            this.FileName = fileName;
            this.SectionName = sectionName;
            _isReadFile = true;
            try
            {
                IniFile iniFile = new IniFile(folderPath, fileName);
                if (iniFile.GetSectionNames().Where(x => x == sectionName).Count() == 0)
                {
                    //找不到參數自動寫入預設值
                    Save(folderPath, fileName, sectionName);
                }
                PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var property in properties)
                {
                    NonMemberAttribute nonMemberAttribute =
                        (NonMemberAttribute)Attribute.GetCustomAttribute(property, typeof(NonMemberAttribute));
                    if (nonMemberAttribute != null)
                    {
                        continue;
                    }

                    string defaultValue = string.Empty;
                    object readInstanceValue = property.GetValue(this, null);
                    if (readInstanceValue != null)
                    {
                        defaultValue = PropertyHelper.PropertyInfoGetValue(this, property);
                        if (defaultValue == null)
                        {
                            //HalconDotNet.HObject不進行讀寫
                            continue;
                        }
                    }
                    DefaultValueAttribute defaultValueAttribute =
                        (DefaultValueAttribute)Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute));
                    if (defaultValueAttribute != null)
                    {
                        defaultValue = defaultValueAttribute.Value.ToString();
                    }
                    string readValue = iniFile.GetString(sectionName, property.Name, defaultValue);
                    if (string.IsNullOrEmpty(readValue))
                    {
                        continue;
                    }
                    PropertyHelper.PropertyInfoSetValue(this, property, readValue);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("ParameterINI.Load({0}, {1}, {2}) Error", folderPath, fileName, sectionName));
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
            return Save(this.FolderPath, this.FileName, this.SectionName);
        }

        public bool Save(string folderPath)
        {
            return Save(folderPath, this.FileName, this.SectionName);
        }

        public bool Save(string folderPath, string fileName)
        {
            return Save(folderPath, fileName, this.SectionName);
        }

        public bool Save(string folderPath, string fileName, string sectionName)
        {
            this.FolderPath = folderPath;
            this.FileName = fileName;
            this.SectionName = sectionName;
            try
            {
                fileName = string.IsNullOrEmpty(fileName) ? this.GetType().Name : fileName;
                IniFile iniFile = new IniFile(folderPath, fileName);
                PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo property in properties)
                {
                    NonMemberAttribute attribute =
                        (NonMemberAttribute)Attribute.GetCustomAttribute(property, typeof(NonMemberAttribute));
                    if (attribute != null)
                    {
                        continue;
                    }
                    object value = property.GetValue(this, null);
                    if (value == null)
                    {
                        continue;
                    }
                    string valueString = PropertyHelper.PropertyInfoGetValue(this, property);
                    if (valueString != null)
                    {
                        iniFile.WriteValue(sectionName, property.Name, valueString);   
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.MessageBoxShow(ex, "SystemHint");
                return false;
            }
        }

        public bool RebulidIni()
        {
            return RebulidIni(this.FolderPath, this.FileName, this.SectionName);
        }

        public bool RebulidIni(string folderPath)
        {
            return RebulidIni(folderPath, this.FileName, this.SectionName);
        }

        public bool RebulidIni(string folderPath, string fileName)
        {
            return RebulidIni(folderPath, fileName, this.SectionName);
        }

        public bool RebulidIni(string folderPath, string fileName, string sectionName)
        {
            try
            {
                fileName = string.IsNullOrEmpty(fileName) ? this.GetType().Name : fileName;
                IniFile iniFile = new IniFile(folderPath, fileName);
                //讀取IniFile所有Key與Value
                List<KeyValuePair<string, string>> keyValuePairs = iniFile.GetSectionValuesAsList(sectionName);
                //刪除IniFile所有Key與Value
                foreach (var item in keyValuePairs)
                {
                    iniFile.DeleteKey(sectionName, item.Key);
                }
                //寫回IniFile參數
                Save(folderPath, fileName, sectionName);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHelper.MessageBoxShow(ex, "SystemHint");
                return false;
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
                    string content = string.Format(@"Error occur when access file: \n{0}\{1}.ini\n\n{2}", this.FolderPath, this.FileName, ex.Message);
                    MessageBox.Show(content, "Error Occur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
