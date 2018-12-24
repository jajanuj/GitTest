using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard;
using System;
using System.ComponentModel;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.Motion
{
    [Serializable]
    public class ParameterCEtherCATMotion : ParameterINI
    {
        #region field

        private ushort _cardNo;
        private ushort nodeNo;
        private MotorMode motorMode;
        private double strVel;
        private double constVel;
        private double endVel;
        private double accVel;
        private double decVel;
        private double tAcc;
        private double tDec;
        private ushort homeMode;
        private double homeOffset;
        private double homeSwitchVel;
        private double homeZeroVel;
        private double homeAccVel;
        private double homeTimeout;
        private double jogMicroSpeed;
        private double jogLowSpeed;
        private double jogMidSpeed;
        private double jogHighSpeed;
        private double jogMaxSpeed;
        private double distPerRole;
        private int pulsePerRole;
        private PulseMode pulseMode;
        private EncMode encMode;
        private bool logicORG;
        private bool logicZ;
        private bool logicSvon;
        private bool logicEL;
        private double inPositionPrecise;
        private int stopCmdWaitSeconds;

        #endregion field

        #region properties

        /// <summary> CardNo 編號 </summary>
        [Browsable(true), Description("CardNo 編號")]
        public ushort CardNo { get { return _cardNo; } set { _cardNo = value; WriteAndNotify("CardNo"); } }

        /// <summary> NodeID 編號 </summary>
        [Browsable(true), Description("NodeID 編號")]
        public ushort NodeNo { get { return nodeNo; } set { nodeNo = value; WriteAndNotify("NodeNo"); } }

        /// <summary> 馬達形態 </summary>
        [Browsable(true), Description("馬達形態")]
        public MotorMode MotorMode { get { return motorMode; } set { motorMode = value; WriteAndNotify("MotorMode"); } }

        /// <summary> 運動初始速度(mm/sec) </summary>
        [Browsable(false)]
        public double StrVel { get { return strVel; } set { strVel = value; WriteAndNotify("StrVel"); } }

        /// <summary> 運動常態速度(mm/sec) </summary>
        [Browsable(false)]
        public double ConstVel { get { return constVel; } set { constVel = value; WriteAndNotify("ConstVel"); } }

        /// <summary> 運動結束速度(mm/sec) </summary>
        [Browsable(false)]
        public double EndVel { get { return endVel; } set { endVel = value; WriteAndNotify("EndVel"); } }

        /// <summary> 加速度(mm/sec^2) </summary>
        [Browsable(false)]
        public double AccVel { get { return accVel; } set { accVel = value; WriteAndNotify("AccVel"); } }

        /// <summary> 減速度(mm/sec^2) </summary>
        [Browsable(false)]
        public double DecVel { get { return decVel; } set { decVel = value; WriteAndNotify("DecVel"); } }

        /// <summary> 運動加速時間(sec) </summary>
        [Browsable(false)]
        public double TAcc { get { return tAcc; } set { tAcc = value; WriteAndNotify("TAcc"); } }

        /// <summary> 運動減速時間(sec) </summary>
        [Browsable(false)]
        public double TDec { get { return tDec; } set { tDec = value; WriteAndNotify("TDec"); } }

        /// <summary> 復歸模式 </summary>
        [Browsable(false)]
        public ushort HomeMode { get { return homeMode; } set { homeMode = value; WriteAndNotify("HomeMode"); } }

        /// <summary> 復歸原點位移(mm) </summary>
        [Browsable(false)]
        public double HomeOffset { get { return homeOffset; } set { homeOffset = value; WriteAndNotify("HomeOffset"); } }

        /// <summary> 復歸Switch速度(mm/sec) </summary>
        [Browsable(false)]
        public double HomeSwitchVel { get { return homeSwitchVel; } set { homeSwitchVel = value; WriteAndNotify("HomeSwitchVel"); } }

        /// <summary> 復歸Zero速度(mm/sec) </summary>
        [Browsable(false)]
        public double HomeZeroVel { get { return homeZeroVel; } set { homeZeroVel = value; WriteAndNotify("HomeZeroVel"); } }

        /// <summary> 復歸加減速(mm/sec^2) </summary>
        [Browsable(false)]
        public double HomeAccVel { get { return homeAccVel; } set { homeAccVel = value; WriteAndNotify("HomeAccVel"); } }

        /// <summary> 復歸Timeout時間(sec) </summary>
        [Browsable(false)]
        public double HomeTimeout { get { return homeTimeout; } set { homeTimeout = value; WriteAndNotify("HomeTimeout"); } }

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

        /// <summary> Pulse輸出模式 </summary>
        [Browsable(false)]
        public PulseMode PulseMode { get { return pulseMode; } set { pulseMode = value; WriteAndNotify("PulseMode"); } }

        /// <summary> Encoder模式 </summary>
        [Browsable(false)]
        public EncMode EncMode { get { return encMode; } set { encMode = value; WriteAndNotify("EncMode"); } }

        /// <summary> LogicORG </summary>
        [Browsable(false)]
        public bool LogicORG { get { return logicORG; } set { logicORG = value; WriteAndNotify("LogicORG"); } }

        /// <summary> LogicZ </summary>
        [Browsable(false)]
        public bool LogicZ { get { return logicZ; } set { logicZ = value; WriteAndNotify("LogicZ"); } }

        /// <summary> LogicSvon </summary>
        [Browsable(false)]
        public bool LogicSvon { get { return logicSvon; } set { logicSvon = value; WriteAndNotify("LogicSvon"); } }

        /// <summary> LogicEL </summary>
        [Browsable(false)]
        public bool LogicEL { get { return logicEL; } set { logicEL = value; WriteAndNotify("LogicEL"); } }

        /// <summary> 到位訊號精度 </summary>
        [Browsable(true), Description("到位訊號精度")]
        public double InPositionPrecise { get { return inPositionPrecise; } set { inPositionPrecise = value; WriteAndNotify("InPositionPrecise"); } }

        /// <summary> 停止指令等待時間(S) </summary>
        [Browsable(true), Description("停止指令等待時間(S)")]
        public int StopCmdWaitSeconds { get { return stopCmdWaitSeconds; } set { stopCmdWaitSeconds = value; WriteAndNotify("StopCmdWaitSeconds"); } }

        #endregion properties

        #region construct

        public ParameterCEtherCATMotion(string folderPath, string fileName)
            : base(folderPath, fileName)
        {
            _cardNo = 16;
            strVel = 0;
            constVel = 100;
            endVel = 0;
            tAcc = 0.1;
            tDec = 0.1;
            accVel = 100;
            decVel = 100;
            homeMode = 28;
            homeSwitchVel = 50;
            homeZeroVel = 5;
            homeAccVel = 100;
            homeOffset = 0;
            homeTimeout = 60;
            jogMicroSpeed = 0.1;
            jogLowSpeed = 1;
            jogMidSpeed = 5;
            jogHighSpeed = 10;
            jogMaxSpeed = 20;
            distPerRole = 1;
            pulsePerRole = 1;
            pulseMode = PulseMode.AB_Phase;
            encMode = EncMode.CW_CCW;
            logicORG = true;
            logicZ = false;
            logicSvon = true;
            inPositionPrecise = 0.001;
            stopCmdWaitSeconds = 0;
        }

        #endregion construct
    }
}