using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L132.MasterCard;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using U16 = System.UInt16;

namespace AOISystem.Utilities.Modules.Syntek.L132.SlaveModules.IO
{
    [Serializable]
    internal class ParameterL132DIO : ParameterINI
    {
        private CardSwitchNo cardSwitchNo;
        private RingNoOfCard ringNoOfCard;
        private U16 slaveIP;
        private bool isActive;

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
        [DataMember()]
        public bool IsActive { get { return isActive; } set { isActive = value; WriteAndNotify("IsActive"); } }

        public ParameterL132DIO(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            isActive = true;
        }
    }
}