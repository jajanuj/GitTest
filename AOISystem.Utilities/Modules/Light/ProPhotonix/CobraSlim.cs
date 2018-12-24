using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Light.Common;
using AOISystem.Utilities.Modules.Ports;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules.Light.ProPhotonix
{
    public enum CobraSlimChannel : byte
    { 
        CN1 = 0, 
        All = 255
    }

    public class CobraSlim : SocketModulesBase, ILight
    {
        #region field

        //標頭
        private byte[] cmdHeader = new byte[] { 0x24 }; // --> $

        //命令結束字元
        private byte[] cmdTerminator = new byte[] { 0x0D, 0x0A };// --> \r\n

        #endregion field

        #region properties

        public ParameterType ParameterType { get; set; }

        public int ChannelNumber { get; set; }

        public Dictionary<LightChannel, LightInfo> LightInfoCollection { get; set; }

        public delegate void LightValueHandler(LightChannel channel, int lightVal);

        public event LightValueHandler LightValueChanged;

        #endregion properties

        #region construct

        public CobraSlim(ModulesType modulesType, string xmlFilePath, string deviceName)
            : base(modulesType, xmlFilePath, deviceName)
        {
            this.ChannelNumber = 1;
            InitializeConfiguration(ParameterType.System);
        }

        #endregion construct

        #region interface method

        public void InitializeConfiguration(ParameterType parameterType)
        {
            this.ParameterType = parameterType;
            this.LightInfoCollection = new Dictionary<LightChannel, LightInfo>();
            InitializeLightChannel(LightChannel.CH1);
        }

        public void SaveConfiguration()
        {
            foreach (var lightInfo in this.LightInfoCollection)
            {
                lightInfo.Value.DefaultValue = lightInfo.Value.ActionValue;
            }
        }

        public void SwitchLight(LightChannel channel, bool lightSwitch)
        {
            SwitchLight(LightChannelTranslate(channel), lightSwitch);
        }

        public void SetLightValue(LightChannel channel, int lightValue)
        {
            SetLightValue(LightChannelTranslate(channel), lightValue);
        }

        public int GetLightValue(LightChannel channel)
        {
            return GetLightValue(LightChannelTranslate(channel));
        }

        private CobraSlimChannel LightChannelTranslate(LightChannel channel)
        {
            return (CobraSlimChannel)channel;
        }

        private LightChannel LightChannelTranslate(CobraSlimChannel channel)
        {
            return (LightChannel)channel;
        }

        #endregion

        #region public method

        public void SwitchLight(CobraSlimChannel channel, bool lightSwitch)
        {
            if (!this.IsInitialized)
            {
                return;
            }
            if (channel == CobraSlimChannel.All)
            {
                foreach (var item in this.LightInfoCollection)
                {
                    SwitchLight(item.Key, lightSwitch);
                }
            }
            else
            {
                string cmdContent = string.Format("GSS={0}", lightSwitch ? 1 : 0);
                if (SendReceive(cmdContent))
                {
                    this.LightInfoCollection[LightChannelTranslate(channel)].Switch = lightSwitch; 
                }
                //if (SendReceive(cmdContent) && this.ResponseString == "1")
                //{
                //    this.LightInfoCollection[LightChannelTranslate(channel)].Switch = lightSwitch; 
                //}
                //else
                //{
                //    OnErrorRaised(-1, ExceptionHelper.GetFullCurrentMethod(this.ErrorMessage + "(" + DeviceName + ")"));
                //}
            }
        }

        public void SetLightValue(CobraSlimChannel channel, int lightValue)
        {
            if (!this.IsInitialized)
            {
                return;
            }
            string cmdContent = string.Format("GLI={0}", lightValue);
            if (SendReceive(cmdContent))
            {
                this.LightInfoCollection[LightChannelTranslate(channel)].ActionValue = lightValue;
            }
            //if (SendReceive(cmdContent) && this.ResponseString == "1")
            //{
            //    this.LightInfoCollection[LightChannelTranslate(channel)].ActionValue = lightValue;
            //    //OnLightValueChanged(channel, lightValue);
            //}
            //else
            //{
            //    OnErrorRaised(-1, ExceptionHelper.GetFullCurrentMethod(this.ErrorMessage + "(" + DeviceName + ")"));
            //}
        }

        public int GetLightValue(CobraSlimChannel channel)
        {
            return this.LightInfoCollection[LightChannelTranslate(channel)].ActionValue;
        }

        public DialogResult ShowLightController()
        {
            LightControllerForm lightControllerForm = new LightControllerForm(this);
            return lightControllerForm.ShowDialog();
        }

        #endregion public method

        #region private method

        private void InitializeLightChannel(LightChannel channel)
        {
            if (!this.LightInfoCollection.ContainsKey(channel))
            {
                this.LightInfoCollection.Add(channel, new LightInfo(this, channel));
            }
            SetLightValue(channel, this.LightInfoCollection[channel].DefaultValue);
        }

        private void OnLightValueChanged(LightChannel channel, int value)
        {
            if (LightValueChanged != null)
            {
                LightValueChanged(channel, value);
            }
        }

        #endregion private method
    }
}
