using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L132.MasterCard;
using System;
using System.ComponentModel;
using U16 = System.UInt16;

namespace AOISystem.Utilities.Modules.Syntek.L132.SlaveModules.ADC
{
    [Serializable]
    internal class ParameterL132A180 : ParameterINI
    {
        private CardSwitchNo cardSwitchNo;
        private RingNoOfCard ringNoOfCard;
        private U16 slaveIP;
        private bool isActive;	//slave是否啟用
        private AdcChannelEnable enableCH0;
        private AdcChannelEnable enableCH1;
        private AdcChannelEnable enableCH2;
        private AdcChannelEnable enableCH3;
        private AdcChannelEnable enableCH4;
        private AdcChannelEnable enableCH5;
        private AdcChannelEnable enableCH6;
        private AdcChannelEnable enableCH7;
        private AdcGain gainCH0;
        private AdcGain gainCH1;
        private AdcGain gainCH2;
        private AdcGain gainCH3;
        private AdcGain gainCH4;
        private AdcGain gainCH5;
        private AdcGain gainCH6;
        private AdcGain gainCH7;

        /// <summary> 主卡旋鈕號碼 </summary>
        [Browsable(true) , Description("主卡旋鈕號碼")]
        public CardSwitchNo CardSwitchNo { get { return cardSwitchNo; } set { cardSwitchNo = value; WriteAndNotify("CardSwitchNo"); } }

        /// <summary> 主卡使用埠號 </summary>
        [Browsable(true) , Description("主卡使用埠號")]
        public RingNoOfCard RingNoOfCard { get { return ringNoOfCard; } set { ringNoOfCard = value; WriteAndNotify("RingNoOfCard"); } }

        /// <summary> 延伸模組站號 </summary>
        [Browsable(true) , Description("延伸模組站號")]
        public U16 SlaveIP { get { return slaveIP; } set { slaveIP = value; WriteAndNotify("SlaveIP"); } }

        /// <summary> 延伸模組啟用訊號 </summary>
        [Browsable(false)]
        public bool IsActive { get { return isActive; } set { isActive = value; WriteAndNotify("IsActive"); } }

        [Browsable(false)]
        public AdcChannelEnable EnableCH0 { get { return enableCH0; } set { enableCH0 = value; WriteAndNotify("EnableCH0"); } }

        [Browsable(false)]
        public AdcChannelEnable EnableCH1 { get { return enableCH1; } set { enableCH1 = value; WriteAndNotify("EnableCH1"); } }

        [Browsable(false)]
        public AdcChannelEnable EnableCH2 { get { return enableCH2; } set { enableCH2 = value; WriteAndNotify("EnableCH2"); } }

        [Browsable(false)]
        public AdcChannelEnable EnableCH3 { get { return enableCH3; } set { enableCH3 = value; WriteAndNotify("EnableCH3"); } }

        [Browsable(false)]
        public AdcChannelEnable EnableCH4 { get { return enableCH4; } set { enableCH4 = value; WriteAndNotify("EnableCH4"); } }

        [Browsable(false)]
        public AdcChannelEnable EnableCH5 { get { return enableCH5; } set { enableCH5 = value; WriteAndNotify("EnableCH5"); } }

        [Browsable(false)]
        public AdcChannelEnable EnableCH6 { get { return enableCH6; } set { enableCH6 = value; WriteAndNotify("EnableCH6"); } }

        [Browsable(false)]
        public AdcChannelEnable EnableCH7 { get { return enableCH7; } set { enableCH7 = value; WriteAndNotify("EnableCH7"); } }

        [Browsable(false)]
        public AdcGain GainCH0 { get { return gainCH0; } set { gainCH0 = value; WriteAndNotify("GainCH0"); } }

        [Browsable(false)]
        public AdcGain GainCH1 { get { return gainCH1; } set { gainCH1 = value; WriteAndNotify("GainCH1"); } }

        [Browsable(false)]
        public AdcGain GainCH2 { get { return gainCH2; } set { gainCH2 = value; WriteAndNotify("GainCH2"); } }

        [Browsable(false)]
        public AdcGain GainCH3 { get { return gainCH3; } set { gainCH3 = value; WriteAndNotify("GainCH3"); } }

        [Browsable(false)]
        public AdcGain GainCH4 { get { return gainCH4; } set { gainCH4 = value; WriteAndNotify("GainCH4"); } }

        [Browsable(false)]
        public AdcGain GainCH5 { get { return gainCH5; } set { gainCH5 = value; WriteAndNotify("GainCH5"); } }

        [Browsable(false)]
        public AdcGain GainCH6 { get { return gainCH6; } set { gainCH6 = value; WriteAndNotify("GainCH6"); } }

        [Browsable(false)]
        public AdcGain GainCH7 { get { return gainCH7; } set { gainCH7 = value; WriteAndNotify("GainCH7"); } }

        public ParameterL132A180(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            isActive = true;
        }
    }
}