using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L122.MasterCard;
using System;
using System.ComponentModel;
using U16 = System.UInt16;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.Motion
{
    [Serializable]
    internal class ParameterL122M1X1 : ParameterINI
    {
        #region field

        private CardSwitchNo cardSwitchNo;
        private RingNoOfCard ringNoOfCard;
        private HomeMode homeMode;
        private U16 slaveIP;
        private bool isActive;
        private double strVelA;
        private double devVelA;
        private double accVelA;
        private double decVelA;
        private double strVelM;
        private double devVelM;
        private double accVelM;
        private double decVelM;
        private double oPRDevVelH;
        private double creepStrVelH;
        private double creepDevVelH;
        private double creepAccVelH;
        private double creepDecVelH;
        private double offsetH;
        private double jogMicroSpeed;
        private double jogLowSpeed;
        private double jogMidSpeed;
        private double jogHighSpeed;
        private double jogMaxSpeed;
        private double distPerRole;
        private int pulsePerRole;
        private double maxVel;
        private PulseMode pulseMode;
        private EncMode encMode;
        private EncDirection encDir;
        private double clearDelay;
        private bool logicALM;
        private bool logicINP;
        private bool logicERC;
        private bool logicORG;
        private bool logicZ;
        private bool logicSD;
        private bool logicLTC;
        private bool enabled;
        private bool softLimitEnabled;
        private double softLimitN;
        private double softLimitP;

        #endregion field

        #region properties

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

        /// <summary> 復歸模式 </summary>
        [Browsable(false)]
        public HomeMode HomeMode { get { return homeMode; } set { homeMode = value; WriteAndNotify("HomeMode"); } }

        /// <summary> 自動初速(mm/sec) </summary>
        [Browsable(false)]
        public double StrVelA { get { return strVelA; } set { strVelA = value; WriteAndNotify("StrVelA"); } }

        /// <summary> 自動恆速(mm/sec) </summary>
        [Browsable(false)]
        public double DevVelA { get { return devVelA; } set { devVelA = value; WriteAndNotify("DevVelA"); } }

        /// <summary> 自動加速度(sec) </summary>
        [Browsable(false)]
        public double AccVelA { get { return accVelA; } set { accVelA = value; WriteAndNotify("AccVelA"); } }

        /// <summary> 自動減速度(sec) </summary>
        [Browsable(false)]
        public double DecVelA { get { return decVelA; } set { decVelA = value; WriteAndNotify("DecVelA"); } }

        /// <summary> 手動初速(mm/sec) </summary>
        [Browsable(false)]
        public double StrVelM { get { return strVelM; } set { strVelM = value; WriteAndNotify("StrVelM"); } }

        /// <summary> 手動恆速(mm/sec) </summary>
        [Browsable(false)]
        public double DevVelM { get { return devVelM; } set { devVelM = value; WriteAndNotify("DevVelM"); } }

        /// <summary> 手動加速度(sec) </summary>
        [Browsable(false)]
        public double AccVelM { get { return accVelM; } set { accVelM = value; WriteAndNotify("AccVelM"); } }

        /// <summary> 手動減速度(sec) </summary>
        [Browsable(false)]
        public double DecVelM { get { return decVelM; } set { decVelM = value; WriteAndNotify("DecVelM"); } }

        /// <summary> 復歸速度(mm/sec) </summary>
        [Browsable(false)]
        public double OPRDevVelH { get { return oPRDevVelH; } set { oPRDevVelH = value; WriteAndNotify("OPRDevVelH"); } }

        /// <summary> 復歸Creep初速(mm/sec) </summary>
        [Browsable(false)]
        public double CreepStrVelH { get { return creepStrVelH; } set { creepStrVelH = value; WriteAndNotify("CreepStrVelH"); } }

        /// <summary> 復歸Creep恆速(mm/sec) </summary>
        [Browsable(false)]
        public double CreepDevVelH { get { return creepDevVelH; } set { creepDevVelH = value; WriteAndNotify("CreepDevVelH"); } }

        /// <summary> 復歸Creep加速度(sec) </summary>
        [Browsable(false)]
        public double CreepAccVelH { get { return creepAccVelH; } set { creepAccVelH = value; WriteAndNotify("CreepAccVelH"); } }

        /// <summary> 復歸Creep減速度(sec)  </summary>
        [Browsable(false)]
        public double CreepDecVelH { get { return creepDecVelH; } set { creepDecVelH = value; WriteAndNotify("CreepDecVelH"); } }

        /// <summary> 軟體原點 (mm) :Convert.ToUInt16 </summary>
        [Browsable(false)]
        public double OffsetH { get { return offsetH; } set { offsetH = value; WriteAndNotify("OffsetH"); } }

        /// <summary> JOG 微速 </summary>
        [Browsable(false)]
        public double JogMicroSpeed { get { return jogMicroSpeed; } set { jogMicroSpeed = value; WriteAndNotify("JogMicroSpeed"); } }

        /// <summary> JOG 低速 </summary>
        [Browsable(false)]
        public double JogLowSpeed { get { return jogLowSpeed; } set { jogLowSpeed = value; WriteAndNotify("JogLowSpeed"); } }

        /// <summary> JOG 中速 </summary>
        [Browsable(false)]
        public double JogMidSpeed { get { return jogMidSpeed; } set { jogMidSpeed = value; WriteAndNotify("JogMidSpeed"); } }

        /// <summary> JOG 高速 </summary>
        [Browsable(false)]
        public double JogHighSpeed { get { return jogHighSpeed; } set { jogHighSpeed = value; WriteAndNotify("JogHighSpeed"); } }

        /// <summary> JOG 極速 </summary>
        [Browsable(false)]
        public double JogMaxSpeed { get { return jogMaxSpeed; } set { jogMaxSpeed = value; WriteAndNotify("JogMaxSpeed"); } }

        /// <summary> 導程 </summary>
        [Browsable(false)]
        public double DistPerRole { get { return distPerRole; } set { distPerRole = value; WriteAndNotify("DistPerRole"); } }

        /// <summary> 每轉Pulse量 </summary>
        [Browsable(false)]
        public int PulsePerRole { get { return pulsePerRole; } set { pulsePerRole = value; WriteAndNotify("PulsePerRole"); } }

        /// <summary> 最大速度 </summary>
        [Browsable(false)]
        public double MaxVel { get { return maxVel; } set { maxVel = value; WriteAndNotify("MaxVel"); } }

        /// <summary> Pulse輸出模式 </summary>
        [Browsable(false)]
        public PulseMode PulseMode { get { return pulseMode; } set { pulseMode = value; WriteAndNotify("PulseMode"); } }

        /// <summary> Encoder模式 </summary>
        [Browsable(false)]
        public EncMode EncMode { get { return encMode; } set { encMode = value; WriteAndNotify("EncMode"); } }

        /// <summary> Encoder方向 </summary>
        [Browsable(false)]
        public EncDirection EncDir { get { return encDir; } set { encDir = value; WriteAndNotify("EncDir"); } }

        /// <summary> 復歸完成延遲 </summary>
        [Browsable(false)]
        public double ClearDelay { get { return clearDelay; } set { clearDelay = value; WriteAndNotify("ClearDelay"); } }

        /// <summary> LogicALM </summary>
        [Browsable(false)]
        public bool LogicALM { get { return logicALM; } set { logicALM = value; WriteAndNotify("LogicALM"); } }

        /// <summary> LogicINP </summary>
        [Browsable(false)]
        public bool LogicINP { get { return logicINP; } set { logicINP = value; WriteAndNotify("LogicINP"); } }

        /// <summary> LogicERC </summary>
        [Browsable(false)]
        public bool LogicERC { get { return logicERC; } set { logicERC = value; WriteAndNotify("LogicERC"); } }

        /// <summary> LogicORG </summary>
        [Browsable(false)]
        public bool LogicORG { get { return logicORG; } set { logicORG = value; WriteAndNotify("LogicORG"); } }

        /// <summary> LogicZ </summary>
        [Browsable(false)]
        public bool LogicZ { get { return logicZ; } set { logicZ = value; WriteAndNotify("LogicZ"); } }

        /// <summary> LogicSD </summary>
        [Browsable(false)]
        public bool LogicSD { get { return logicSD; } set { logicSD = value; WriteAndNotify("LogicSD"); } }

        /// <summary> LogicLTC </summary>
        [Browsable(false)]
        public bool LogicLTC { get { return logicLTC; } set { logicLTC = value; WriteAndNotify("LogicLTC"); } }

        /// <summary> 軸啟用訊號 </summary>
        [Browsable(false)]
        public bool Enabled { get { return enabled; } set { enabled = value; } }

        [Browsable(false)]
        public bool SoftLimitEnabled { get { return softLimitEnabled; } set { softLimitEnabled = value; WriteAndNotify("SoftLimitEnabled"); } }

        [Browsable(false)]
        public double SoftLimitN { get { return softLimitN; } set { softLimitN = value; WriteAndNotify("SoftLimitN"); } }

        [Browsable(false)]
        public double SoftLimitP { get { return softLimitP; } set { softLimitP = value; WriteAndNotify("SoftLimitP"); } }

        #endregion properties

        #region construct

        public ParameterL122M1X1(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            isActive = true;
            strVelA = 50;
            devVelA = 100;
            accVelA = 0.1;
            decVelA = 0.1;
            strVelM = 5;
            devVelM = 10;
            accVelM = 0.1;
            decVelM = 0.1;
            creepStrVelH = 1;
            creepDevVelH = 5;
            creepAccVelH = 0.1;
            creepDecVelH = 0.1;
            oPRDevVelH = 50;
            offsetH = 0;
            jogMicroSpeed = 0.1;
            jogLowSpeed = 1;
            jogMidSpeed = 5;
            jogHighSpeed = 10;
            jogMaxSpeed = 20;
            distPerRole = 1;
            pulsePerRole = 1;
            maxVel = 20000;
            homeMode = HomeMode.OnePoint;
            pulseMode = PulseMode.AB_Phase;
            encMode = EncMode.CW_CCW;
            encDir = 0;
            logicORG = true;
            logicZ = false;
            logicSD = true;
            logicLTC = true;
            clearDelay = 1;
            softLimitEnabled = false;
        }

        #endregion construct
    }
}