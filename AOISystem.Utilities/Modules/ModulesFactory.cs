using System;
using System.ComponentModel;
using System.Reflection;
using AOISystem.Utilities.Common;
using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Resources;

namespace AOISystem.Utilities.Modules
{
    public enum ModulesType
    {
        /// <summary> Syntek 單軸控制 </summary>
        L122_M1X1,
        /// <summary> Syntek 四軸控制 </summary>
        L122_M2X4,
        /// <summary> Syntek DI / DO 控制 </summary>
        L122_DIO,        
        /// <summary> Syntek ADC Converter </summary>
        L122_A180,
        /// <summary> Syntek DAC Converter </summary>
        L122_A104,
        /// <summary> Syntek 單軸控制 </summary>
        L132_M1X1,
        /// <summary> Syntek DI / DO 控制 </summary>
        L132_DIO,
        /// <summary> Syntek ADC Converter </summary>
        L132_A180,
        /// <summary> Syntek DAC Converter </summary>
        L132_A104,
        /// <summary> Syntek M314 </summary>
        M314,
        /// <summary> Syntek DI 6022 控制 </summary>
        CEtherCATDI6022,
        /// <summary> Syntek DO 7062 控制 </summary>
        CEtherCATDO7062,
        /// <summary> Syntek Motion 控制 </summary>
        CEtherCATMotion,
        /// <summary> Syntek ADC 8124 控制 </summary>
        CEtherCATADC8124,
        /// <summary> Syntek DAC 9144 控制 </summary>
        CEtherCATDAC9144,
        /// <summary> OPT DPA6024 燈箱控制器</summary>
        OPT_DPA6024,
        /// <summary> OPT DPA6024 B 燈箱控制器</summary>
        OPT_DPA6024B,
        /// <summary> OPT DP1024F 燈箱控制器</summary>
        OPT_DP1024F,
        /// <summary> CCS PD3 10024 燈箱控制器</summary>
        CCS_PD3_10024,
        /// <summary> CCS PD3 5024 燈箱控制器</summary>
        CCS_PD3_5024,
        /// <summary> Pro Photonix CobraSlim 燈箱控制器</summary>
        ProPhotonix_CobraSlim,
        /// <summary> PN 模組</summary>
        PN100BI
    }

    public class ModulesFactory
    {
        private static readonly string AssemblyName = "AOISystem.Utilities";

        public static dynamic CreateModule(ModulesType modulesType, string xmlFilePath, string deviceName)
        {
            string className = ResourceHelper.Library.GetString("Module" + modulesType.ToString());
            object[] obj = new object[] { (object)modulesType, (object)xmlFilePath, (object)deviceName };
            dynamic result = null;
            Type typeofControl = null;
            Assembly tempAssembly = Assembly.Load(AssemblyName);
            if (tempAssembly == null)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("AssemblyIsNull")));
            }
            if (className == null)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongClassName") + " ( " + deviceName + " : " + modulesType.ToString() + " )"));
            }

            typeofControl = tempAssembly.GetType(className);

            if (typeofControl == null)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongClassName") + " ( " + deviceName + " : " + modulesType.ToString() + " )"));
            }

            try
            {
                result = Activator.CreateInstance(typeofControl, obj);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException is Exception)
                {
                    throw new Exception(ex.InnerException.Message + " ( " + deviceName + " : " + modulesType.ToString() + " )");
                }
            }
            return result;
        }

        /// <summary>
        /// 設計工具所需的變數，須先由MainForm取得。
        /// </summary>
        public static IContainer Components;

        /// <summary>
        /// 流程控制管理
        /// </summary>
        public static FlowControlHelper FlowControlHelper = new FlowControlHelper();
    }
}
