using AOISystem.Utilities.Modules.Light.Common;
using AOISystem.Utilities.Modules.Ports;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules.Light.OPT
{
    public class DPA6024B : SerialModulesBase, ILight
    {
        #region field

        //標頭
        private byte[] cmdHeader = new byte[] { 0xFF };

        #endregion field

        #region properties

        public ParameterType ParameterType { get; set; }

        public int ChannelNumber { get; set; }

        public Dictionary<LightChannel, LightInfo> LightInfoCollection { get; set; }

        public delegate void LightValueHandler(LightChannel channel, int lightVal);

        public event LightValueHandler LightValueChanged;

        #endregion properties

        #region construct

        public DPA6024B(ModulesType modulesType, string xmlFilePath, string deviceName)
            : base(modulesType, xmlFilePath, deviceName)
        {
            this.ChannelNumber = 2;
            InitializeConfiguration(ParameterType.System);
        }

        #endregion construct

        #region interface method

        public void InitializeConfiguration(ParameterType parameterType)
        {
            this.ParameterType = parameterType;
            this.LightInfoCollection = new Dictionary<LightChannel, LightInfo>();
            InitializeLightChannel(LightChannel.CH1);
            InitializeLightChannel(LightChannel.CH2);
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

        private DPA6024Channel LightChannelTranslate(LightChannel channel)
        {
            if (channel == LightChannel.CHAll)
            {
                return DPA6024Channel.All;
            }
            else
            {
                return (DPA6024Channel)(channel + 1);
            }
        }

        private LightChannel LightChannelTranslate(DPA6024Channel channel)
        {
            if (channel == DPA6024Channel.All)
            {
                return LightChannel.CHAll;
            }
            else
            {
                return (LightChannel)(channel - 1);
            }
        }

        #endregion

        #region public method

        public void SwitchLight(DPA6024Channel channel, bool lightSwitch)
        {
            if (!this.IsInitialized)
            {
                return;
            }
            if (channel == DPA6024Channel.All)
            {
                foreach (var item in this.LightInfoCollection)
                {
                    SwitchLight(item.Key, lightSwitch);
                }
            }
            else
            {
                List<byte> cmdContent = new List<byte>() { 5, (byte)channel, 0, (byte)(lightSwitch ? 1 : 0) };
                sendCommand(getCombineCommand(cmdContent));
                this.LightInfoCollection[LightChannelTranslate(channel)].Switch = lightSwitch;
            }
        }

        public void SetLightValue(DPA6024Channel channel, int lightValue)
        {
            if (!this.IsInitialized)
            {
                return;
            }
            List<byte> cmdContent = new List<byte>() { 1, (byte)channel, 0, (byte)lightValue };
            sendCommand(getCombineCommand(cmdContent));
            this.LightInfoCollection[LightChannelTranslate(channel)].ActionValue = lightValue;
            //OnLightValueChanged(channel, lightValue);
        }

        public int GetLightValue(DPA6024Channel channel)
        {
            //string cmdContent = string.Format("4{0}000", (byte)channel);
            //sendCommand(getCombineCommand(cmdContent));
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

        private List<byte> getCombineCommand(List<byte> cmdContent)
        {
            List<byte> cmdCombined = new List<byte>();
            cmdCombined.AddRange(cmdHeader);
            cmdCombined.AddRange(cmdContent);
            cmdCombined.Add(getCheckDigit(cmdCombined));
            return cmdCombined;
        }

        private byte getCheckDigit(List<byte> cmdCombined)
        {
            bool[] bits = BitConverterEx.GetBits(cmdCombined[0]);
            for (int j = 1; j < cmdCombined.Count; j++)
            {
                for (byte i = 0; i < 8; i++)
                {
                    bits[i] ^= BitConverterEx.TestB(cmdCombined[j], i);
                }
            }
            return BitConverterEx.ConvertToByte(bits);
        }

        protected override bool analyzeReceiveData(List<byte> recevedDataList)
        {
            bool success = false;
            if (recevedDataList.Count >= 6)
            {
                if (recevedDataList[0] == 0xFF)
                {
                    success = true;
                }
            }
            else if (recevedDataList.Count >= 2)
            {
                if (recevedDataList[0] == 0xF0)
                {
                    if (recevedDataList[1] == 0x00)
                    {
                        OnErrorRaised(1000, "通訊異常");
                    }
                    else if (recevedDataList[1] == 0x01)
                    {
                        OnErrorRaised(-1, "過流保護");
                    }
                    else if (recevedDataList[1] == 0x02)
                    {
                        OnErrorRaised(-1, "短路保護");
                    }
                    else if (recevedDataList[1] == 0x03)
                    {
                        OnErrorRaised(-1, "過壓保護");
                    }
                }
                else if (recevedDataList[0] == 0x00)
                {
                    OnLightValueChanged(LightChannel.CH1, recevedDataList[1]);
                }
                success = true;
            }
            return success;
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
