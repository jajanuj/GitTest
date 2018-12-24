using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L122.MasterCard;
using System;
using System.ComponentModel;
using U16 = System.UInt16;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.DAC
{
    [Serializable]
    internal class ParameterL122A104 : ParameterINI
    {
        private CardSwitchNo cardSwitchNo;
        private RingNoOfCard ringNoOfCard;
        private U16 slaveIP;

        /// <summary> 主卡旋鈕號碼 </summary>
        [Browsable(true) , Description("主卡旋鈕號碼")]
        public CardSwitchNo CardSwitchNo { get { return cardSwitchNo; } set { cardSwitchNo = value; WriteAndNotify("CardSwitchNo"); } }

        /// <summary> 主卡使用埠號 </summary>
        [Browsable(true) , Description("主卡使用埠號")]
        public RingNoOfCard RingNoOfCard { get { return ringNoOfCard; } set { ringNoOfCard = value; WriteAndNotify("RingNoOfCard"); } }

        /// <summary> 延伸模組站號 </summary>
        [Browsable(true) , Description("延伸模組站號")]
        public U16 SlaveIP { get { return slaveIP; } set { slaveIP = value; WriteAndNotify("SlaveIP"); } }

        /// <summary> CN0 最大電壓值 default value=+10v </summary>
        [Browsable(true), Description("CN0 最大電壓值")]
        public double MaxVoltageCN0 { get; set; }

        /// <summary> CN0 最小電壓值 default value=-10v </summary>
        [Browsable(true), Description("CN0 最小電壓值")]
        public double MinVoltageCN0 { get; set; }

        /// <summary> CN1 最大電壓值 default value=+10v </summary>
        [Browsable(true), Description("CN1 最大電壓值")]
        public double MaxVoltageCN1 { get; set; }

        /// <summary> CN1 最小電壓值 default value=-10v </summary>
        [Browsable(true), Description("CN1 最小電壓值")]
        public double MinVoltageCN1 { get; set; }

        /// <summary> CN2 最大電壓值 default value=+10v </summary>
        [Browsable(true), Description("CN2 最大電壓值")]
        public double MaxVoltageCN2 { get; set; }

        /// <summary> CN2 最小電壓值 default value=-10v </summary>
        [Browsable(true), Description("CN2 最小電壓值")]
        public double MinVoltageCN2 { get; set; }

        /// <summary> CN3 最大電壓值 default value=+10v </summary>
        [Browsable(true), Description("CN3 最大電壓值")]
        public double MaxVoltageCN3 { get; set; }

        /// <summary> CN3 最小電壓值 default value=-10v </summary>
        [Browsable(true), Description("CN3 最小電壓值")]
        public double MinVoltageCN3 { get; set; }

        public ParameterL122A104(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            this.MaxVoltageCN0 = 10;
            this.MinVoltageCN0 = -10;
            this.MaxVoltageCN1 = 10;
            this.MinVoltageCN1 = -10;
            this.MaxVoltageCN2 = 10;
            this.MinVoltageCN2 = -10;
            this.MaxVoltageCN3 = 10;
            this.MinVoltageCN3 = -10;
        }
    }
}