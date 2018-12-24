using System;
using System.IO;
using System.Reflection;
using System.Resources;
using AOISystem.Utilities.Common;
using AOISystem.Utilities.Forms;
using AOISystem.Utilities.Resources;

namespace AOISystem.Utilities.Modules
{
    public abstract class ModulesBase : IDisposable
    {
        #region private field

        // Flag: Has Dispose already been called?
        private bool disposed = false;

        private const string assemblyName = "AOISystem.Utilities";

        private ModulesType modulesType;
        private ResourceManager languageResourceManager;
        private dynamic parameter;

        #endregion private field

        #region public properties

        public ModulesType ModulesType { get { return modulesType; } set { modulesType = value; } }

        public string ParameterFolderPath { get; set; }

        public string DeviceName { get; set; }

        public bool IsInitialized { get; set; }

        protected ResourceManager LanguageResourceManager { get { return languageResourceManager; } set { languageResourceManager = value; } }

        protected dynamic Parameter { get { return parameter; } set { parameter = value; } }

        #endregion public properties

        #region event methods

        public delegate void ErrorRaisedEventHandler(object sender, int errorCode, string errorMsg);
        public event ErrorRaisedEventHandler ErrorRaised;

        protected void OnErrorRaised(int errorCode, string errorMsg)
        {
            if (ErrorRaised != null)
                ErrorRaised(this, errorCode, errorMsg);
        }

        #endregion event methods

        #region construct

        public ModulesBase(ModulesType modulesType, string parameterFolderPath, string deviceName)
        {
            LanguageResourceManager = ResourceHelper.Language;
            ModulesType = modulesType;
            ParameterFolderPath = parameterFolderPath;
            DeviceName = deviceName;
            parameterInitialize();
        }

        ~ModulesBase()
        {
            Dispose(false);
        }

        #endregion construct

        #region private method

        protected void parameterInitialize()
        {
            bool isModulesParameterClassExisted = checkModulesParameterClass(modulesType);
            bool isSerialModule = checkModuleIsSerial(modulesType);
            bool isSocketModule = checkModuleIsSocket(modulesType);
            //檢查所指定的參數資料夾是否存在
            if (!Directory.Exists(this.ParameterFolderPath))
            {
                Directory.CreateDirectory(ParameterFolderPath);
            }
            string parameterFilePath = string.Format(@"{0}\{1}.ini", this.ParameterFolderPath, this.DeviceName);
            //參數檔不存在--> 訊息提示 --> 顯示對應屬性修改視窗
            if (!File.Exists(parameterFilePath))
            {
                ExceptionHelper.CommonMessageShow(LanguageResourceManager.GetString("DefaultParameter") + "(" + DeviceName + ")", "SystemHint");

                //在LibraryResource中有找到對應的Parameter參數資源
                if (isModulesParameterClassExisted)
                {
                    //[底層] 此部份要測試，可能會有型別問題
                    //如果有自訂參數檔且是Serial模組 用FrmParameterSerialSetting設定屬性
                    if (isSerialModule)
                    {
                        parameter = createGeneralSerialParameter(modulesType, this.ParameterFolderPath, this.DeviceName);
                        ParameterSerialSettingForm parameterSerialSettingForm = new ParameterSerialSettingForm(parameter, this.DeviceName);
                        parameterSerialSettingForm.ShowDialog();
                        ((ParameterINI)parameter).Save();
                    }
                    else if (isSocketModule)
                    {
                        parameter = createGeneralSocketParameter(modulesType, this.ParameterFolderPath, this.DeviceName);
                        ParameterSocketSettingForm parameterSocketSettingForm = new ParameterSocketSettingForm(parameter, this.DeviceName);
                        parameterSocketSettingForm.ShowDialog();
                        ((ParameterINI)parameter).Save();
                    }
                    else
                    {
                        parameter = createCustomParameter(modulesType, this.ParameterFolderPath, this.DeviceName);
                        ParameterINIForm propertyGridForm = new ParameterINIForm(parameter, this.DeviceName);
                        propertyGridForm.ShowDialog();
                        ((ParameterINI)parameter).Save();
                    }
                }
                //在LibraryResource中沒有對應的參數資源
                else
                {
                    //確認是否為serialModule 有些serial通訊模組不會另外自訂參數檔
                    //從LibraryResource讀取預設的ParameterSerial
                    if (isSerialModule)
                    {
                        parameter = createGeneralSerialParameter(modulesType, this.ParameterFolderPath, this.DeviceName);
                        ParameterSerialSettingForm frmParameterSerialSetting = new ParameterSerialSettingForm(parameter, this.DeviceName);
                        frmParameterSerialSetting.ShowDialog();
                        ((ParameterINI)parameter).Save();
                    }
                    else if (isSocketModule)
                    {
                        parameter = createGeneralSocketParameter(modulesType, this.ParameterFolderPath, this.DeviceName);
                        ParameterSocketSettingForm frmParameterSocketSetting = new ParameterSocketSettingForm(parameter, this.DeviceName);
                        frmParameterSocketSetting.ShowDialog();
                        ((ParameterINI)parameter).Save();
                    }
                    //沒有自訂的參數檔 也不是serialModule 先傳回exception 日後依照需要去修正
                    else
                    {
                        throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongClassName") + "(" + this.DeviceName + ")"));
                    }
                }
            }
            //參數檔存在--> 直接讀回該檔案之屬性值
            else
            {
                if (isModulesParameterClassExisted)
                {
                    parameter = createCustomParameter(modulesType, this.ParameterFolderPath, this.DeviceName);
                    ((ParameterINI)parameter).Load();
                }
                else
                {
                    if (isSerialModule)
                    {
                        parameter = createGeneralSerialParameter(modulesType, this.ParameterFolderPath, this.DeviceName);
                        ((ParameterINI)parameter).Load();
                    }
                    else if (isSocketModule)
                    {
                        parameter = createGeneralSocketParameter(modulesType, this.ParameterFolderPath, this.DeviceName);
                        ((ParameterINI)parameter).Load();
                    }
                    else
                    {
                        throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongClassName") + "(" + this.DeviceName + ")"));
                    }
                }
            }
        }

        private object invokeStaticMethod(string assemblyName, string className, string methodName, params object[] argus)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            Type type = assembly.GetType(className);
            MethodInfo targetMethod = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            return targetMethod.Invoke(null, argus);
        }

        private dynamic createCustomParameter(ModulesType modulesType, string parameterFolderPath, string deviceName)
        {
            string className = ResourceHelper.Library.GetString("Parameter" + modulesType.ToString());
            object[] obj = new object[] { parameterFolderPath, deviceName };
            dynamic result = null;
            Type typeofControl = null;
            Assembly tempAssembly = Assembly.Load(assemblyName);
            typeofControl = tempAssembly.GetType(className);

            try
            {
                result = Activator.CreateInstance(typeofControl, obj);
            }
            catch (TargetInvocationException ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return result;
        }

        private dynamic createGeneralSerialParameter(ModulesType modulesType, string parameterFolderPath, string deviceName)
        {
            string className = ResourceHelper.Library.GetString("ParameterSerial");
            object[] obj = new object[] { parameterFolderPath, deviceName };
            dynamic result = null;
            Type typeofControl = null;
            Assembly tempAssembly = Assembly.Load(assemblyName);
            typeofControl = tempAssembly.GetType(className);

            try
            {
                result = Activator.CreateInstance(typeofControl, obj);
            }
            catch (TargetInvocationException ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return result;
        }

        private dynamic createGeneralSocketParameter(ModulesType modulesType, string parameterFolderPath, string deviceName)
        {
            string className = ResourceHelper.Library.GetString("ParameterSocket");
            object[] obj = new object[] { parameterFolderPath, deviceName };
            dynamic result = null;
            Type typeofControl = null;
            Assembly tempAssembly = Assembly.Load(assemblyName);
            typeofControl = tempAssembly.GetType(className);

            try
            {
                result = Activator.CreateInstance(typeofControl, obj);
            }
            catch (TargetInvocationException ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return result;
        }

        private bool checkModuleIsSerial(ModulesType modulesType)
        {
            string className = ResourceHelper.Library.GetString("Module" + modulesType.ToString());
            Type typeofControl = null;
            Assembly tempAssembly = Assembly.Load(assemblyName);
            if (tempAssembly == null)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("AssemblyIsNull")));
            }
            if (className == null)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongClassName") + "(" + modulesType.ToString() + ")"));
            }
            typeofControl = tempAssembly.GetType(className);

            if (typeofControl.BaseType.Name == "SerialModulesBase")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkModuleIsSocket(ModulesType modulesType)
        {
            string className = ResourceHelper.Library.GetString("Module" + modulesType.ToString());
            Type typeofControl = null;
            Assembly tempAssembly = Assembly.Load(assemblyName);
            if (tempAssembly == null)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("AssemblyIsNull")));
            }
            if (className == null)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongClassName") + "(" + modulesType.ToString() + ")"));
            }
            typeofControl = tempAssembly.GetType(className);

            if (typeofControl.BaseType.Name == "SocketModulesBase")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkModulesParameterClass(ModulesType modulesType)
        {
            string className = ResourceHelper.Library.GetString("Parameter" + modulesType.ToString());
            return className != null ? true : false;
        }

        #endregion private method

        #region dispose

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        #endregion
    }
}
