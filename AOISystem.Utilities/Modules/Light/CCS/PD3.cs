using AOISystem.Utilities.Modules.Light.Common;
using AOISystem.Utilities.Modules.Ports;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules.Light.CCS
{
    public enum PD3Channel : byte
    { 
        L1 = 0, 
        L2 = 1, 
        L3 = 2, 
        L4 = 3, 
        L5 = 4,
        L6 = 5,
        L7 = 6,
        L8 = 7,
        All = 255
    }

    public enum PD3Model
    {
        PD3_5024,
        PD3_10024
    }

    public class PD3 : SerialModulesBase, ILight
    {
        #region field

        //標頭
        private string cmdHeader = "@";

        //命令結束字元
        private string cmdTerminator = "\r\n";

        #endregion field

        #region properties

        public PD3Model ModelName { get; set; }

        public ParameterType ParameterType { get; set; }

        public int ChannelNumber { get; set; }

        public Dictionary<LightChannel, LightInfo> LightInfoCollection { get; set; }

        public delegate void LightValueHandler(LightChannel channel, int lightVal);

        public event LightValueHandler LightValueChanged;

        #endregion properties

        #region construct

        public PD3(ModulesType modulesType, string xmlFilePath, string deviceName)
            : base(modulesType, xmlFilePath, deviceName)
        {
            if (modulesType == ModulesType.CCS_PD3_10024)
            {
                this.ModelName = PD3Model.PD3_10024;
                this.ChannelNumber = 8;
            }
            else //modulesType == ModulesType.CCS_PD3_5024
            {
                this.ModelName = PD3Model.PD3_5024;
                this.ChannelNumber = 4;
            }
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
            if (this.ModelName == PD3Model.PD3_10024)
            {
                InitializeLightChannel(LightChannel.CH5);
                InitializeLightChannel(LightChannel.CH6);
                InitializeLightChannel(LightChannel.CH7);
                InitializeLightChannel(LightChannel.CH8);
            }
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

        private PD3Channel LightChannelTranslate(LightChannel channel)
        {
            return (PD3Channel)channel;
        }

        private LightChannel LightChannelTranslate(PD3Channel channel)
        {
            return (LightChannel)channel;
        }

        #endregion

        #region public method

        public void SwitchLight(PD3Channel channel, bool lightSwitch)
        {
            if (!this.IsInitialized)
            {
                return;
            }
            string cmdContent = string.Format("{0:X}L{1}", channel, lightSwitch ? 1 : 0);
            sendCommand(getCombineCommand(cmdContent));
            if (channel == PD3Channel.All)
            {
                foreach (var item in this.LightInfoCollection)
                {
                    item.Value.Switch = lightSwitch;
                }
            }
            else
            {
                this.LightInfoCollection[LightChannelTranslate(channel)].Switch = lightSwitch; 
            }
        }

        public void SetLightValue(PD3Channel channel, int lightValue)
        {
            if (!this.IsInitialized)
            {
                return;
            }
            string cmdContent = string.Format("{0:X}F{1:D3}", channel, lightValue);
            sendCommand(getCombineCommand(cmdContent));
            if (channel == PD3Channel.All)
            {
                foreach (var item in this.LightInfoCollection)
                {
                    item.Value.ActionValue = lightValue;
                }
            }
            else
            {
                this.LightInfoCollection[LightChannelTranslate(channel)].ActionValue = lightValue;
            }
            //OnLightValueChanged(channel, lightValue);
        }

        public int GetLightValue(PD3Channel channel)
        {
            return this.LightInfoCollection[LightChannelTranslate(channel)].ActionValue;
        }

        public void CheckStatus(PD3Channel channel)
        {
            string cmdContent = string.Format("{0:X}M00", channel);
            sendCommand(getCombineCommand(cmdContent));
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
            string cmdCombined = string.Format("{0}{1}{2}", cmdHeader, cmdContent, "00");
            cmdCombined = string.Format("{0}{1}{2}", cmdCombined, getCheckDigit(cmdCombined), cmdTerminator);
            return ASCIIEncoding.ASCII.GetBytes(cmdCombined).ToList();
        }

        private string getCheckDigit(string cmdCombined)
        {
            string checksum = "";
            int All = 0;
            Char[] chars = cmdCombined.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                All += (int)chars[i];
            }
            checksum = Convert.ToString(All, 16);
            checksum = checksum.Substring(checksum.Length - 2, 2).ToUpper();

            return checksum;
        }

        protected override bool analyzeReceiveData(List<byte> recevedDataList)
        {
            bool success = false;
            string cmdWords = ASCIIEncoding.ASCII.GetString(recevedDataList.ToArray()).Trim();
            if (cmdWords.Contains(cmdHeader) && cmdWords.Length >= 8)
            {
                int index = cmdWords.LastIndexOf(cmdHeader);
                cmdWords = cmdWords.Substring(index, cmdWords.Length - index);
                if (cmdWords.Length == 8)
                {
                    PD3Channel channel;
                    if (Enum.TryParse<PD3Channel>(cmdWords[2].ToString(), out channel))
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
                    success = true;
                }
                if (cmdWords.Contains('?'))
                {
                    throw new Exception("接收到燈箱不合理字串");
                }
                else if (cmdWords.Contains('N'))
                {
                    string NG = cmdWords.Substring(cmdWords.IndexOf('N') + 1, 2);
                    switch (NG)
                    {
                        case "01":
                            throw new Exception("燈箱 Command error");
                        case "02":
                            throw new Exception("燈箱 Checksum error");
                        case "03":
                            throw new Exception("燈箱 Set value out of range error");
                    }
                }
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
