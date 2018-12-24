using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L132.MasterCard;
using System;
using System.ComponentModel;
using U16 = System.UInt16;

namespace AOISystem.Utilities.Modules.Syntek.L132.SlaveModules.DAC
{
    [Serializable]
    internal class ParameterL132A104 : ParameterINI
    {
        private CardSwitchNo cardSwitchNo;
        private RingNoOfCard ringNoOfCard;
        private U16 slaveIP;
        private double[] maxVoltage;
        private double[] minVoltage;

        /// <summary> 主卡旋鈕號碼 </summary>
        [Browsable(true) , Description("主卡旋鈕號碼")]
        public CardSwitchNo CardSwitchNo { get { return cardSwitchNo; } set { cardSwitchNo = value; WriteAndNotify("CardSwitchNo"); } }

        /// <summary> 主卡使用埠號 </summary>
        [Browsable(true) , Description("主卡使用埠號")]
        public RingNoOfCard RingNoOfCard { get { return ringNoOfCard; } set { ringNoOfCard = value; WriteAndNotify("RingNoOfCard"); } }

        /// <summary> 延伸模組站號 </summary>
        [Browsable(true) , Description("延伸模組站號")]
        public U16 SlaveIP { get { return slaveIP; } set { slaveIP = value; WriteAndNotify("SlaveIP"); } }

        /// <summary> 最大電壓值 default value=+10v </summary>
        [Browsable(true) , Description("最大電壓值")]
        public double[] MaxVoltage { get { return maxVoltage; } set { maxVoltage = value; } }

        /// <summary> 最小電壓值 default value=-10v</summary>
        [Browsable(true) , Description("最小電壓值")]
        public double[] MinVoltage { get { return minVoltage; } set { minVoltage = value; } }

        public ParameterL132A104(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            maxVoltage = new double[] { 10 , 10 , 10 , 10 };
            minVoltage = new double[] { -10 , -10 , -10 , -10 };
        }
    }
}