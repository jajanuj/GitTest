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
    public enum DP1024FChannel : byte
    { 
        CN1 = 1, 
        CN2 = 2,
        CN3 = 3,
        CN4 = 4,
        All = 255
    }

    public class DP1024F : SerialModulesBase, ILight
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

        public DP1024F(ModulesType modulesType, string xmlFilePath, string deviceName)
            : base(modulesType, xmlFilePath, deviceName)
        {
            this.ChannelNumber = 4;
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
            InitializeLightChannel(LightChannel.CH3);
            InitializeLightChannel(LightChannel.CH4);
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

        private DP1024FChannel LightChannelTranslate(LightChannel channel)
        {
            if (channel == LightChannel.CHAll)
            {
                return DP1024FChannel.All;
            }
            else
            {
                return (DP1024FChannel)(channel + 1);
            }
        }

        private LightChannel LightChannelTranslate(DP1024FChannel channel)
        {
            if (channel == DP1024FChannel.All)
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

        public void SwitchLight(DP1024FChannel channel, bool lightSwitch)
        {
            if (!this.IsInitialized)
            {
                return;
            }
            if (channel == DP1024FChannel.All)
            {
                foreach (var item in this.LightInfoCollection)
                {
                    SwitchLight(item.Key, lightSwitch);
                }
            }
            else
            {
            string cmdContent = string.Format("{0}{1}000", lightSwitch ? 1 : 2, (byte)channel);
            sendCommand(getCombineCommand(cmdContent));
            this.LightInfoCollection[LightChannelTranslate(channel)].Switch = lightSwitch; 
            }
        }

        public void SetLightValue(DP1024FChannel channel, int lightValue)
        {
            if (!this.IsInitialized)
            {
                return;
            }
            string cmdContent = string.Format("3{0}0{1}", (byte)channel, lightValue.ToString("x2"));//.PadLeft(2, '0'));
            sendCommand(getCombineCommand(cmdContent));
            this.LightInfoCollection[LightChannelTranslate(channel)].ActionValue = lightValue; 
            //OnLightValueChanged(channel, lightValue);
        }

        public int GetLightValue(DP1024FChannel channel)
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

        private List<byte> getCombineCommand(string cmdContent)
        {
            string cmdCombined = string.Format("{0}{1}", ASCIIEncoding.ASCII.GetString(cmdHeader), cmdContent);
            cmdCombined += getCheckDigit(cmdCombined);
            return ASCIIEncoding.ASCII.GetBytes(cmdCombined).ToList();
        }

        private string getCheckDigit(string cmdCombined)
        {
            bool[] bits = BitConverterEx.GetBits((byte)cmdCombined[0]);
            for (int j = 1; j < cmdCombined.Length; j++)
            {
                for (byte i = 0; i < 8; i++)
                {
                    bits[i] ^= BitConverterEx.TestB(cmdCombined[j], i);
                }
            }
            int upperBit = 0;
            int lowerBit = 0;
            for (byte i = 0; i < 4; i++)
            {
                BitConverterEx.SetBit(ref lowerBit, i, bits[i]);
                BitConverterEx.SetBit(ref upperBit, i, bits[i + 4]);
            }
            return string.Format("{0:x}{1:x}", upperBit, lowerBit);
        }

        protected override bool analyzeReceiveData(List<byte> recevedDataList)
        {
            bool success = false;
            string cmdWords = ASCIIEncoding.ASCII.GetString(recevedDataList.ToArray()).Trim();
            if (cmdWords.Contains('$'))
            {
                if (cmdWords.Length >= 8)
                {
                    int index = cmdWords.LastIndexOf('$');
                    cmdWords = cmdWords.Substring(index, cmdWords.Length - index);
                    if (cmdWords.Length == 8)
                    {
                        DP1024FChannel channel;
                        if (Enum.TryParse<DP1024FChannel>(cmdWords[2].ToString(), out channel))
                        {
                            //狀態指令回傳
                            char cmdSeletct = cmdWords[1];
                            byte value = byte.Parse(cmdWords.Substring(4, 2), NumberStyles.HexNumber);
                            if (cmdSeletct == '4')
                            {
                                //目前亮度回傳
                                OnLightValueChanged(LightChannelTranslate(channel), value);
                            }
                        }
                    }   
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
