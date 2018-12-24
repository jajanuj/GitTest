using AOISystem.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AOISystem.Utilities.Common
{
    public class ParameterDictionary
    {
        private static Dictionary<string, string> _parameterDictionary;
        private static IniFile _iniFile = null;

        static ParameterDictionary()
        {
            Initialize();
        }

        public static void Initialize()
        {
            _parameterDictionary = new Dictionary<string, string>();

            _iniFile = new IniFile(SystemDefine.SYSTEM_DATA_FOLDER_PATH, "SystemConfig");
            string[] keyNames = _iniFile.GetKeyNames("Configuration");
            foreach (string keyName in keyNames)
            {
                GetValue(keyName);
            }
        }

        public static string GetValue(string name, bool ignoreSystemConfigError = false)
        {
            string value = string.Empty;
            try
            {
                if (!_parameterDictionary.ContainsKey(name))
                {
                    value = _iniFile.GetString("Configuration", name);

                    if (!ignoreSystemConfigError && value == string.Empty)
                        throw new Exception(string.Format("SystemConfig.ini 檔案找不到\"{0}\"參數", name));

                    _parameterDictionary.Add(name, value);
                }

                return _parameterDictionary[name];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public static void SetValue(string name, string value)
        {
            if (!_parameterDictionary.ContainsKey(name))
            {
                _parameterDictionary.Add(name, value);
            }
            else
            {
                _parameterDictionary[name] = value;
            }
            _iniFile.WriteValue("Configuration", name, value);
        }
    }
}
