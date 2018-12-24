using System.Collections.Generic;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules.Light.Common
{
    public interface ILight
    {
        /// <summary>模組類型</summary>
        ModulesType ModulesType { get; set; }

        /// <summary>參數類型</summary>
        ParameterType ParameterType { get; set; }

        /// <summary>參數路徑</summary>
        string ParameterFolderPath { get; set; }

        /// <summary>參數檔案</summary>
        string DeviceName { get; set; }

        /// <summary>通道數量</summary>
        int ChannelNumber { get; set; }

        /// <summary>通道資訊</summary>
        Dictionary<LightChannel, LightInfo> LightInfoCollection { get; set; }

        /// <summary>
        /// 初始化設定
        /// </summary>
        /// <param name="parameterType">參數類型</param>
        void InitializeConfiguration(ParameterType parameterType);

        /// <summary>
        /// 儲存設定
        /// </summary>
        void SaveConfiguration();

        /// <summary>通道開/關</summary>
        /// <param name="channel">通道</param>
        /// <param name="lightSwitch">開/關</param>
        void SwitchLight(LightChannel channel, bool lightSwitch);

        /// <summary>通道數值設定</summary>
        /// <param name="channel">通道</param>
        /// <param name="lightValue">數值</param>
        void SetLightValue(LightChannel channel, int lightValue);

        /// <summary>通道數值讀取</summary>
        /// <param name="channel">通道</param>
        int GetLightValue(LightChannel channel);

        /// <summary>開啟控制視窗</summary>
        DialogResult ShowLightController();

        /// <summary>開啟命令監視視窗</summary>
        void ShowMonitor();

        /// <summary>開啟設定視窗</summary>
        void ShowConfiguration();
    }
}
