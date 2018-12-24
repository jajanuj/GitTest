using AOISystem.Utilities.Common;
using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Logging;
using AOISystem.Utilities.Modules.Syntek.M314.Library;
using AOISystem.Utilities.Resources;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using I16 = System.Int16;
using U16 = System.UInt16;

namespace AOISystem.Utilities.Modules.Syntek.M314
{
    public enum ComparatorDirection : ushort
    {
        Normal ,
        Inverse
    }

    public enum CompareOutputSrc : ushort
    {
        CMOS_3_3V ,
        SourceAxisDO_24V ,
        Axis_4_DIR_OR_PLS ,

        // DIR / PLS ?
        Axis_4_DIR_PLS
    }

    public enum AxisNo : ushort
    {
        Axis0,
        Axis1,
        Axis2,
        Axis3
    }

    public enum CompareChannel : ushort
    {
        Channel0 ,
        Channel1
    }

    public enum CardSwitchNo : ushort
    {
        Card0,
        Card1,
        Card2,
        Card3,
        Card4,
        Card5,
        Card6,
        Card7
    }

    public enum RingNoOfCard : ushort
    {
        CN1 ,
        CN2
    }

    public enum HomeMode : ushort
    {
        OnePoint = 0 ,
        TwoPoint = 1 ,
        ThreePoint = 2
    }

    public enum SoftLimitStopType : byte
    {
        INT = 0 ,
        StopImmediately = 1 ,
        SlowStop = 2
    }

    public enum EncDirection : ushort
    {
        NonInverse = 0 ,
        Inverse = 1
    }

    public enum LTCSignalSource
    {
        DI ,
        EZ ,
        ORG ,
        MEL
    }

    public enum EncMode : ushort
    {
        X1 ,
        X2 ,
        X4
    }

    public enum PulseMode : ushort
    {
        OUT_DIR_OUT_Falling_edge_DIR_high_level ,
        OUT_DIR_OUT_Rising_edge_DIR_high_level ,
        OUT_DIR_OUT_Falling_edge_DIR_low_level ,
        OUT_DIR_OUT_Rising_edge_DIR_low_level ,
        CW_CCW_Falling_edge ,
        CW_CCW_Rising_edge ,
        AB_Phase ,
        BA_Phase
    }

    public class M314 : ModulesBase , IDisposable, IMotion
    {
        #region struct

        internal struct M314Status
        {
            public bool RDY;
            public bool ALM;
            public bool LimitP;
            public bool LimitN;
            public bool ORG;
            public bool DIR;
            public bool EMG;
            public bool _24V;
            public bool ERC;
            public bool EZ;
            public bool _5V;
            public bool Latch;
            public bool SD;
            public bool INP;
            public bool SVON;
            public bool RALM;
            public bool Home;
        }

        #endregion struct

        #region private field

        private double maxSpeed;
        private static bool isNeedCreate = false;
        private static List<byte> cardOpenedNum = new List<byte>();

        private U16[] aryAxis;
        private M314Status status;
        private Thread systemScanThread;
        private M314HomeFlow m314HomeFlow;
        private ParameterM314 axisPara;
        private bool keyOfIOStatus;
        private bool isHomeMoving;
        private bool isDoubleHomeMoving;
        private int nPos;
        private U16 busy;

        //標記物件是否已被釋放
        private bool disposed = false;

        private FlowStatus flowResult;

        private static PCI_M314 m314;

        private bool softLimitEnabled;

        private double softLimitN;

        private double softLimitP;

        #endregion private field

        #region properties

        /// <summary> M1X1模組軸參 </summary>
        public ParameterM314 AxisPara { get { return axisPara; } set { axisPara = value; } }

        /// <summary> M1X1模組狀態點資訊 </summary>
        internal M314Status Status { get { return status; } }

        /// <summary> 取得目前位置 (Command Counter) </summary>
        public double Position { /*set { setPos(value); }*/ get { return getPos(); } }
        public double Position2 { /*set { setPos(value); }*/ get { return getPos2(); } }

        /// <summary> 取得編碼器位置 (Position Counter) </summary>
        public double Encoder {/* set { setENC(value); }*/ get { return getENC(); } }
        public double Encoder2 {/* set { setENC(value); }*/ get { return getENC2(); } }

        /// <summary> 取得目前速度 (Current Speed) </summary>
        public double Speed {/* set { setENC(value); }*/ get { return getSpeed(); } }
        public double Speed2 {/* set { setENC(value); }*/ get { return getSpeed2(); } }

        ///<summary> 取得錯誤命令次數 (Error Count) </summary>
        public double ErrorCount { /*set { setENC(value); } */get { return getErrorCount(); } }
        public double ErrorCount2 { /*set { setENC(value); } */get { return getErrorCount2(); } }

        /// <summary> 取得原點復歸的旗標 </summary>
        public bool IsHome { get { return status.Home; } set { status.Home = value; } }

        /// <summary> 取得原點Sensor訊號 </summary>
        public bool IsORG { get { return status.ORG; } }

        /// <summary> 取得正極限訊號 </summary>
        public bool IsLimitP { get { return status.LimitP; } }

        /// <summary> 取得負極限訊號 </summary>
        public bool IsLimitN { get { return status.LimitN; } }

        /// <summary> 取得警報訊號 </summary>
        public bool IsAlarm { get { return status.ALM; } }

        /// <summary> 取得到位訊號 </summary>
        public bool IsINP { get { return status.INP; } }

        /// <summary> 軸是否啟用(紀錄到xml中) </summary>
        public bool IsActive { get { return axisPara.IsActive; } set { axisPara.IsActive = value; } }

        /// <summary> 軸是否啟用 </summary>
        public bool Enabled { get { return axisPara.Enabled; } set { axisPara.Enabled = value; } }

        /// <summary> M1X1模組是否在移動中 </summary>
        public bool IsBusy
        {
            get
            {
                U16 busyData = new U16();
                if (m314.Library.CS_m314_motion_done((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , ref busyData) == 0)
                {
                    busy = busyData;
                    return busyData != 0 ? true : false;
                }
                else
                {
                    return busy != 0 ? true : false;
                }
            }
        }

        /// <summary> 軸是否到位 </summary>
        public bool IsReached
        {
            get
            {
                //int cmdPos = 0;
                //m314.Library.CS_m314_get_command((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , ref cmdPos);
                //double inpPrecise = 0;
                //if (axisPara.DistPerRole != 0)
                //{
                //    inpPrecise = axisPara.InPositionPrecise / axisPara.DistPerRole * axisPara.PulsePerRole;
                //}
                //return !this.IsBusy && (Math.Abs(nPos - cmdPos) <= inpPrecise);

                //double enc = getENC2();
                //double inpPrecise = 0;
                //if (axisPara.DistPerRole != 0)
                //{
                //    inpPrecise = axisPara.InPositionPrecise / axisPara.DistPerRole * axisPara.PulsePerRole;
                //}
                //return !this.IsBusy && (Math.Abs(nPos - enc) <= inpPrecise);

                double cmd = getPos2();
                double enc = getENC2();
                double inpPrecise = 0;
                if (axisPara.DistPerRole != 0)
                {
                    inpPrecise = axisPara.InPositionPrecise / axisPara.DistPerRole * axisPara.PulsePerRole;
                }
                return !this.IsBusy && (/*(Math.Abs(nPos - cmd) <= inpPrecise) || (Math.Abs(nPos - enc) <= inpPrecise) || */this.IsINP);
            }
        }

        public FlowStatus FlowResult { get { return flowResult; } }

        public bool IsHomeMoving { get { return isHomeMoving; } set { isHomeMoving = value; } }

        public bool IsDoubleHomeMoving { get { return isDoubleHomeMoving; } set { isDoubleHomeMoving = value; } }

        public Action<string> BuildLog { set; get; }

        public Action<string> Complete { set; get; }

        public Action<string> Fail { set; get; }

        public bool IsSoftLimitN { get { return Encoder <= softLimitN; } }

        public bool IsSoftLimitP { get { return Encoder >= softLimitP; } }

        #endregion properties

        #region construct

        public M314(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            maxSpeed = 1000;
            flowResult = FlowStatus.Initialize;
            m314 = PCI_M314.GetInstance();
            aryAxis = new U16[2];
            axisPara = Parameter as ParameterM314;
            m314CardInitialize();
            setHardwareScan();
            taskInitialize();
            axisPara.ParameterChanged += new ParameterINI.ParameterChangedHandler((paraName) => SetMotion());
            ServoOn(CmdStatus.ON);
            ResetPos();
            SetMotion();
            softLimitEnabled = axisPara.SoftLimitEnabled;
            softLimitN = axisPara.SoftLimitN;
            softLimitP = axisPara.SoftLimitP;
        }

        ~M314()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            //告訴GC此物件的Finalize方法不再需要呼叫
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            // 當物件已經被解構時，不再執行
            if (disposed)
                return;
            if (isDisposing)
            {
                //在這裡釋放託管資源
                //只在使用者呼叫Dispose方法時執行
            }
            //在這裡釋放非託管資源
            if (m314 != null)
            {
                m314.Library.CS_m314_close((U16)axisPara.CardSwitchNo);
            }
            //標記物件已被釋放
            disposed = true;
        }

        #endregion construct

        #region private method

        private void axisParaInitialize()
        {
            axisPara.StrVelA = 50;
            axisPara.DevVelA = 100;
            axisPara.AccVelA = 0.1;
            axisPara.DecVelA = 0.1;

            axisPara.StrVelM = 5;
            axisPara.DevVelM = 10;
            axisPara.AccVelM = 0.1;
            axisPara.DecVelM = 0.1;

            axisPara.OPRDevVelH = 10;
            axisPara.CreepStrVelH = 20;
            axisPara.CreepDevVelH = 30;
            axisPara.CreepAccVelH = 0.1;
            axisPara.CreepDecVelH = 0.1;
            axisPara.OffsetH = 0;

            axisPara.JogMicroSpeed = 0.1;
            axisPara.JogLowSpeed = 1;
            axisPara.JogMidSpeed = 5;
            axisPara.JogHighSpeed = 10;
            axisPara.JogMaxSpeed = 20;

            axisPara.DistPerRole = 1;
            axisPara.PulsePerRole = 1;

            axisPara.HomeMode = HomeMode.OnePoint;
            axisPara.PulseMode = PulseMode.CW_CCW_Rising_edge;
            axisPara.EncMode = EncMode.X4;

            axisPara.EncDir = 0;
            axisPara.LogicORG = true;
            axisPara.LogicEZ = false;
            axisPara.LogicSD = true;
            axisPara.LogicLTC = true;
            axisPara.ClearDelay = 1;
            axisPara.LTCSignalSource = 0;
        }

        private void m314CardInitialize()
        {
            if (cardOpenedNum.Count == 0)
            {
                createCard();
                cardOpenedNum.Add((byte)axisPara.CardSwitchNo);
            }
            else
            {
                isNeedCreate = true;
                foreach (var item in cardOpenedNum)
                {
                    if (item == (byte)axisPara.CardSwitchNo)
                    {
                        isNeedCreate = false;
                    }
                }

                if (isNeedCreate)
                {
                    createCard();
                    cardOpenedNum.Add((byte)axisPara.CardSwitchNo);
                }
            }
            U16 cardNo = 999;
            m314.Library.CS_m314_get_cardno((U16)axisPara.CardSwitchNo , ref cardNo);
            if (cardNo != (U16)axisPara.CardSwitchNo)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("InitializeErrorM314")));
            }
        }

        private void createCard()
        {
            U16 exitsCard = 0;
            I16 rc;
            rc = m314.Library.CS_m314_open(ref exitsCard);

            if (exitsCard == 0)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("DidntFindM314")));
            }
            rc = m314.Library.CS_m314_initial_card((U16)axisPara.CardSwitchNo);
            if (rc != 0)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("InitializeErrorM314")));
            }
        }

        //設定掃描M1X1的Thread
        private void setHardwareScan()
        {
            FlowControl flowControl = ModulesFactory.FlowControlHelper.GetFlowControl("SYNTEKMotion");
            FlowBase flowBase = new FlowBase(this.DeviceName, systemScan);
            flowControl.AddFlowBase(flowBase);
            flowBase.Start();

            //systemScanThread = new Thread(systemScan);
            //systemScanThread.IsBackground = true;
            //systemScanThread.Start();
            keyOfIOStatus = true;
        }

        //流程器初始化
        private void taskInitialize()
        {
            FlowControl flowControl = ModulesFactory.FlowControlHelper.GetFlowControl("SYNTEKMotion");
            m314HomeFlow = new M314HomeFlow(this);
            flowControl.AddFlowBase(m314HomeFlow);
        }

        //scan m1x1 slave IO status
        private void systemScan(FlowVar fv)
            {
            //while (true)
            //{
                if (keyOfIOStatus)
                {
                    U16 status = 0;
                    m314.Library.CS_m314_get_io_status((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , ref status);
                    this.status.RDY = BitConverterEx.TestB(status, 0);
                    this.status.ALM = BitConverterEx.TestB(status, 1);
                    this.status.LimitP = BitConverterEx.TestB(status, 2);
                    this.status.LimitN = BitConverterEx.TestB(status, 3);
                    this.status.ORG = BitConverterEx.TestB(status, 4);
                    this.status.DIR = BitConverterEx.TestB(status, 5);
                    this.status.EMG = BitConverterEx.TestB(status, 6);
                    this.status._24V = BitConverterEx.TestB(status, 7);
                    this.status.ERC = BitConverterEx.TestB(status, 8);
                    this.status.EZ = BitConverterEx.TestB(status, 9);
                    this.status._5V = BitConverterEx.TestB(status, 10);
                    this.status.Latch = BitConverterEx.TestB(status, 11);
                    this.status.SD = BitConverterEx.TestB(status, 12);
                    this.status.INP = BitConverterEx.TestB(status, 13);
                    this.status.SVON = BitConverterEx.TestB(status, 14);
                    this.status.RALM = BitConverterEx.TestB(status, 15);

                    if (this.status.ALM || this.status.EMG)
                        this.status.Home = false;

                    //Thread.Sleep(15);
            }
            //}
        }

        public void SetMotion()
        {
            m314.Library.CS_m314_set_alm((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , Convert.ToInt16(axisPara.LogicALM) , 0);
            m314.Library.CS_m314_set_inp((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0 , Convert.ToInt16(axisPara.LogicINP));
            m314.Library.CS_m314_set_erc((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0);
            m314.Library.CS_m314_set_erc_on((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0);
            m314.Library.CS_m314_set_sd((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0 , Convert.ToInt16(axisPara.LogicSD) , 0);
            m314.Library.CS_m314_set_org((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , Convert.ToInt16(axisPara.LogicORG));
            m314.Library.CS_m314_set_ez((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , Convert.ToInt16(axisPara.LogicEZ));
            m314.Library.CS_m314_set_ltc_src((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , (I16)axisPara.LTCSignalSource);
            m314.Library.CS_m314_set_ltc_logic((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , Convert.ToInt16(axisPara.LogicLTC));
            m314.Library.CS_m314_set_feedback_src((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0);   //0:External Feedback, 1:Command Pulse
            //m314.Library.CS_m314_set_a_move_feedback_src((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0);    //0:Command Counter, 1:Position Counter
            m314.Library.CS_m314_set_a_move_feedback_src((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, 1);
            m314.Library.CS_m314_set_pls_outfastmode((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 1);
            m314.Library.CS_m314_set_pls_outmode((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , (I16)axisPara.PulseMode);
            m314.Library.CS_m314_set_pls_iptmode((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , (I16)axisPara.EncMode , (I16)axisPara.EncDir);
            m314.Library.CS_m314_set_ell((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , Convert.ToInt16(axisPara.LogicEL));
        }

        private double pulseToMM(int pulse)
        {
            if (axisPara.PulsePerRole == 0)
            {
                axisPara.PulsePerRole = 1;
            }
            return pulse / axisPara.PulsePerRole * axisPara.DistPerRole;
        }

        private double pulseToMM(double pulse)
        {
            if (axisPara.PulsePerRole == 0)
            {
                axisPara.PulsePerRole = 1;
            }
            return pulse / axisPara.PulsePerRole * axisPara.DistPerRole;
        }

        private int mmToPulse(double mm)
        {
            if (axisPara.DistPerRole != 0)
            {
                return (int)(mm / axisPara.DistPerRole * axisPara.PulsePerRole);
            }
            else
            {
                return 0;
            }
        }

        //get command counter
        private double getPos()
        {
            int cmdPos = new int();
            m314.Library.CS_m314_get_command((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , ref cmdPos);
            double dCmdPos = Convert.ToDouble(cmdPos);
            if (axisPara.PulsePerRole != 0)
            {
                return pulseToMM(dCmdPos);
            }
            else
            {
                return 0;
            }
        }

        private double getPos2()
        {
            int cmdPos = new int();
            m314.Library.CS_m314_get_command((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, ref cmdPos);
            return cmdPos;
        }

        //get encode，取得feedback的位置(光學尺)
        private double getENC()
        {
            double feedBackPos = new double();
            m314.Library.CS_m314_get_position((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , ref feedBackPos);
            return pulseToMM(feedBackPos);
        }

        private double getENC2()
        {
            double feedBackPos = new double();
            m314.Library.CS_m314_get_position((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, ref feedBackPos);
            return feedBackPos;
        }

        //get current speed
        private double getSpeed()
        {
            double speed = new int();
            m314.Library.CS_m314_get_current_speed((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, ref speed);
            if (axisPara.PulsePerRole != 0)
            {
                return pulseToMM(speed);
            }
            else
            {
                return 0;
            }
        }

        private double getSpeed2()
        {
            double speed = new int();
            m314.Library.CS_m314_get_current_speed((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, ref speed);
            return speed;
        }

        private double getErrorCount()
        {
            I16 error = 0;
            m314.Library.CS_m314_get_error_counter((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, ref error);
            if (axisPara.PulsePerRole != 0)
            {
                return pulseToMM((double)error);
            }
            else
            {
                return 0;
            }
        }

        private double getErrorCount2()
        {
            I16 error = 0;
            m314.Library.CS_m314_get_error_counter((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, ref error);
            return error;
        }

        private void doHomeLim()
        {
            isHomeMoving = true;
            m314.Library.CS_m314_set_home_config((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 3 , Convert.ToInt16(axisPara.LogicORG) , Convert.ToInt16(axisPara.LogicEZ) , 1);
            m314.Library.CS_m314_set_home_offset_position((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0);
            m314.Library.CS_m314_set_home_finish_reset((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0);
            m314.Library.CS_m314_home_move((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0 , mmToPulse(axisPara.CreepDevVelH) , axisPara.CreepAccVelH , 1);
        }

        private void doHomeOrg()
        {
            isHomeMoving = true;
            m314.Library.CS_m314_set_home_config((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 1 , Convert.ToInt16(axisPara.LogicORG) , Convert.ToInt16(axisPara.LogicEZ) , 0);
            m314.Library.CS_m314_home_move((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0 , mmToPulse(axisPara.CreepDevVelH) , axisPara.CreepAccVelH , 1);
        }

        private double checkOverMaxSpeed(double speed)
        {
            if (speed > maxSpeed)
            {
                return maxSpeed;
            }
            else
            {
                return speed;
            }
        }

        #endregion private method

        #region public method

        public void SetSoftLimit(bool enabled , double limitN , double limitP)
        {
            softLimitEnabled = enabled;
            softLimitN = limitN;
            softLimitP = limitP;
        }

        public void SetSoftLimit(bool enabled)
        {
            softLimitEnabled = enabled;
        }

        private bool isAllowMove(double dest)
        {
            if (softLimitEnabled)
            {
                if (dest < softLimitN)
                {
                    return false;
                }
                else if (dest > softLimitP)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool isAllowMove(RotationDirection dir)
        {
            if (softLimitEnabled)
            {
                if (Encoder > softLimitP && dir == RotationDirection.CW)
                {
                    return false;
                }
                else if (Encoder < softLimitN && dir == RotationDirection.CCW)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// M1X1 裝置啟停
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns> 命令是否成功 </returns>
        public void ServoOn(CmdStatus option)
        {
            I16 rc = -1;
            if (axisPara != null)
            {
                if (option == CmdStatus.OFF)
                {
                    axisPara.Enabled = false;
                    status.Home = false;
                    rc = m314.Library.CS_m314_set_servo((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0);
                }
                if (option == CmdStatus.ON)
                {
                    if (axisPara.IsActive)
                    {
                        axisPara.Enabled = true;
                        rc = m314.Library.CS_m314_set_servo((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 1);
                        m314.Library.CS_m314_disable_soft_limit((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo);
                    }
                }
            }
        }

        /// <summary>
        /// 清除警報
        /// </summary>
        public void ResetAlarm()
        {
            if (axisPara.IsActive && axisPara.Enabled )
            {
                m314.Library.CS_m314_set_ralm((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, 1);
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(1000);
                    m314.Library.CS_m314_set_ralm((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, 0);
                });
            }
        }

        /// <summary>
        /// 絕對移動 (需選擇使用軸參的手動速度或是自動速度)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="inAuto">手動或自動速度</param>
        public bool AbsolueMove(double dest , SpeedMode inAuto = SpeedMode.Auto)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                if (isAllowMove(dest))
                {
                    nPos = mmToPulse(dest);
                    if (inAuto == SpeedMode.Manual)
                    {
                        rc = m314.Library.CS_m314_start_ta_move((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , nPos ,
                            mmToPulse(axisPara.StrVelM) , mmToPulse(checkOverMaxSpeed(axisPara.DevVelM)) , axisPara.AccVelM , axisPara.DecVelM);
                    }
                    if (inAuto == SpeedMode.Auto)
                    {
                        rc = m314.Library.CS_m314_start_ta_move((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , nPos ,
                            mmToPulse(axisPara.StrVelA) , mmToPulse(checkOverMaxSpeed(axisPara.DevVelA)) , axisPara.AccVelA , axisPara.DecVelA);
                    }
                }
            }
            return rc == 0 ? true : false;
        }

        /// <summary>
        /// 絕對移動 (需要自定義速度，初速為恆速1/2，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="speed"> 速度值 </param>
        /// <param name="s_curve"> T-Curve = true, S-Curve = false </param>
        public bool AbsolueMove(double dest, double speed, bool curve = true)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                if (isAllowMove(dest))
                {
                    nPos = mmToPulse(dest);
                    if (curve)
            {
                        rc = m314.Library.CS_m314_start_ta_move((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, nPos,
                            mmToPulse(axisPara.StrVelM), mmToPulse(checkOverMaxSpeed(speed)), axisPara.AccVelM, axisPara.DecVelM);
            }
            else
            {
                    rc = m314.Library.CS_m314_start_sa_move((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, nPos,
                        mmToPulse(axisPara.StrVelM), mmToPulse(checkOverMaxSpeed(speed)), axisPara.AccVelM, axisPara.DecVelM);
                }
            }
            }
            return rc == 0 ? true : false;
        }

        public bool RelativeMove(double dest , SpeedMode inAuto = SpeedMode.Auto)
        {
            return AbsolueMove(getPos() + dest , inAuto);
        }

        public bool RelativeMove(double dest , double speed)
        {
            return AbsolueMove(getPos() + dest , speed);
        }

        /// <summary>
        /// 連續移動
        /// </summary>
        /// <param name="dir">移動方向</param>
        /// <param name="speed">速度值</param>
        /// <returns></returns>
        public bool ContinuousMove(RotationDirection dir, double speed)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                if (isAllowMove(dir))
                {
                    if (isAllowMove(dir))
                    {
                        byte bDir;
                        if (dir == RotationDirection.CW)
                        {
                            bDir = 0;
                        }
                        else
                        {
                            bDir = 1;
                        }
                        rc = m314.Library.CS_m314_tv_move((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, mmToPulse(checkOverMaxSpeed(speed)), mmToPulse(checkOverMaxSpeed(speed)), axisPara.AccVelM, bDir);
                    }
                }
            }
            return rc == 0 ? true : false;
        }

        /// <summary>
        /// 連續移動 (需選擇使用軸參的手動速度或是自動速度)
        /// </summary>
        /// <param name="dir">移動方向</param>
        /// <param name="inAuto">手動或自動速度</param>
        /// <returns></returns>
        public bool ContinuousMove(RotationDirection dir, SpeedMode inAuto = SpeedMode.Auto)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                if (isAllowMove(dir))
                {
                    if (isAllowMove(dir))
                    {
                        byte bDir;
                        if (dir == RotationDirection.CW)
                        {
                            bDir = 0;
                        }
                        else
                        {
                            bDir = 1;
                        }
                        if (inAuto == SpeedMode.Auto)
                        {
                            rc = m314.Library.CS_m314_tv_move((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, mmToPulse(checkOverMaxSpeed(axisPara.StrVelA)), mmToPulse(checkOverMaxSpeed(axisPara.DevVelA)), axisPara.AccVelA, bDir);
                        }
                        else
                        {
                            rc = m314.Library.CS_m314_tv_move((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, mmToPulse(checkOverMaxSpeed(axisPara.StrVelM)), mmToPulse(checkOverMaxSpeed(axisPara.DevVelM)), axisPara.AccVelM, bDir);
                        }
                    }
                }
            }
            return rc == 0 ? true : false;
        }

        public bool ArcM()
        {
            throw new NotImplementedException("someday will be complete");
            /*
            procedure M314_M1.ArcM(CirY_M1: M314_M1; StartPoint, MidPoint, EndPoint: VPoint2D; _Speed: Double);
            var
              X0, X1, X2, X3, Y0, Y1, Y2, Y3, Thi1, Thi2: Double;
              CWFlag: Integer;
              CenterPoint: VPoint2D;
              IsCW: Boolean;
            begin
              X1 := StartPoint.X; Y1 := StartPoint.Y;
              X2 := MidPoint.X; Y2 := MidPoint.Y;
              X3 := EndPoint.X; Y3 := EndPoint.Y;

              x0 := ((y3 - y1) * (y2 * y2 - y1 * y1 + x2 * x2 - x1 * x1) + (y2 - y1) * (y1 * y1 - y3 * y3 + x1 * x1 - x3 * x3)) / (2 * (x2 - x1) * (y3 - y1) - 2 * (x3 - x1) * (y2 - y1));
              y0 := ((x3 - x1) * (x2 * x2 - x1 * x1 + y2 * y2 - y1 * y1) + (x2 - x1) * (x1 * x1 - x3 * x3 + y1 * y1 - y3 * y3)) / (2 * (y2 - y1) * (x3 - x1) - 2 * (y3 - y1) * (x2 - x1));

              CenterPoint := _Point2D(X0, Y0);
              Thi1 := Line_Thi(CenterPoint, StartPoint);
              Thi2 := Line_Thi(CenterPoint, MidPoint);
              if ((Thi1 >= 0) and (Thi2 >= 0)) or ((Thi1 < 0) and (Thi2 < 0)) then
                IsCW := (Thi1 > Thi2)
              else
                IsCW := (Thi1 >= 0);

              if IsCW then
                CWFlag := 0
              else
                CWFlag := 1;

              LineAxis[0] := AxisNO;
              LineAxis[1] := CirY_M1.AxisNO;
              nPos := mmToPulse(X3);
              with Param do
                D_m314_start_ta_arc3_xy(_Owner.CardNo, LineAxis[0], mmToPulse(X0), mmToPulse(Y0), mmToPulse(X3), mmToPulse(Y3),
                  CWFlag, mmToPulse(_Speed), mmToPulse(_Speed), A_AccVel, A_DecVel);
            end;
            */
        }

        public bool JogConsiderSoftLimit(JogSpeed jogSpeed , RotationDirection dir)
        {
            I16 rc = -1;
            bool result = false;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                double speed = 0;
                switch (jogSpeed)
                {
                    case JogSpeed.Micro:
                        speed = axisPara.JogMicroSpeed;
                        break;

                    case JogSpeed.Low:
                        speed = axisPara.JogLowSpeed;
                        break;

                    case JogSpeed.Mid:
                        speed = axisPara.JogMidSpeed;
                        break;

                    case JogSpeed.High:
                        speed = axisPara.JogHighSpeed;
                        break;

                    case JogSpeed.Max:
                        speed = axisPara.JogMaxSpeed;
                        break;
                }
                if (softLimitEnabled)
                {
                    if (dir == RotationDirection.CW)
                    {
                        result = AbsolueMove(softLimitP , speed);
                    }
                    else
                    {
                        result = AbsolueMove(softLimitN , speed);
                    }
                }
                else
                {
                    if (isAllowMove(dir))
                    {
                        byte bDir;
                        if (dir == RotationDirection.CW)
                        {
                            bDir = 0;
                        }
                        else
                        {
                            bDir = 1;
                        }
                        rc = m314.Library.CS_m314_tv_move((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0 , mmToPulse(checkOverMaxSpeed(speed)) , axisPara.AccVelM , bDir);
                        result = rc == 0 ? true : false;
                    }
                }
            }
            return result;
        }

        public bool Jog(JogSpeed jogSpeed , RotationDirection dir)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                double speed = 0;
                switch (jogSpeed)
                {
                    case JogSpeed.Micro:
                        speed = axisPara.JogMicroSpeed;
                        break;

                    case JogSpeed.Low:
                        speed = axisPara.JogLowSpeed;
                        break;

                    case JogSpeed.Mid:
                        speed = axisPara.JogMidSpeed;
                        break;

                    case JogSpeed.High:
                        speed = axisPara.JogHighSpeed;
                        break;

                    case JogSpeed.Max:
                        speed = axisPara.JogMaxSpeed;
                        break;
                }
                if (isAllowMove(dir))
                {
                    //FlowCompareCmdAndPos flow = new FlowCompareCmdAndPos(this , dir , BuildLog , Complete , Fail);
                    //flow.Start();
                    byte bDir;
                    if (dir == RotationDirection.CW)
                    {
                        bDir = 0;
                    }
                    else
                    {
                        bDir = 1;
                    }
                    rc = m314.Library.CS_m314_tv_move((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0 , mmToPulse(checkOverMaxSpeed(speed)) , axisPara.AccVelM , bDir);
                }
            }
            return rc == 0 ? true : false;
        }

        public bool JogM(double speed , RotationDirection dir)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                if (!this.IsBusy)
                {
                    if (isAllowMove(dir))
                    {
                        byte bDir;
                        if (dir == RotationDirection.CW)
                        {
                            bDir = 0;
                        }
                        else
                        {
                            bDir = 1;
                        }
                        rc = m314.Library.CS_m314_tv_move((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0 , mmToPulse(checkOverMaxSpeed(speed)) , axisPara.AccVelM , bDir);
                    }
                }
                else
                {
                    rc = m314.Library.CS_m314_v_change((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , mmToPulse(checkOverMaxSpeed(speed)) , axisPara.AccVelM);
                }
            }
            return rc == 0 ? true : false;
        }

        public void Stop(StopType type , bool isStopTask = true)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled)
            {
                if (isDoubleHomeMoving)
                {
                    m314.Library.CS_m314_disable_home_move((U16)axisPara.CardSwitchNo , 0 , 0.1);
                    m314.Library.CS_m314_disable_home_move((U16)axisPara.CardSwitchNo , 1 , 0.1);
                }
                if (isHomeMoving)
                {
                    m314.Library.CS_m314_disable_home_move((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0.1);
                }

                if (isStopTask)
                {
                    //20150327 FlowControl暫不實作
                    //if (FlowBase.IsRunning("AOISystem.Utilities.Modules.Syntek.M314.M314+FlowDoubleHome" + "." + DeviceName))
                    //{
                    //    FlowBase.SetCompleteByName("AOISystem.Utilities.Modules.Syntek.M314.M314+FlowDoubleHome" + "." + DeviceName);
                    //}
                    //if (FlowBase.IsRunning("AOISystem.Utilities.Modules.Syntek.M314.M314+FlowHome" + "." + DeviceName))
                    //{
                    //    FlowBase.SetCompleteByName("AOISystem.Utilities.Modules.Syntek.M314.M314+FlowHome" + "." + DeviceName);
                    //}
                    //if (FlowBase.IsRunning("AOISystem.Utilities.Modules.Syntek.M314.M314+FlowCompareCmdAndPos" + "." + DeviceName))
                    //{
                    //    FlowBase.SetCompleteByName("AOISystem.Utilities.Modules.Syntek.M314.M314+FlowCompareCmdAndPos" + "." + DeviceName);
                    //}
                }
                m314.Library.CS_m314_disable_home_move((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0.1);
                if (type == StopType.Emergency)
                {
                    rc = m314.Library.CS_m314_emg_stop((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo);
                }
                if (type == StopType.SlowDown)
                {
                    rc = m314.Library.CS_m314_sd_stop((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0.3);
                }
                if (type == StopType.CmdWait)
                {
                    //if (this.IsReached || nPos == 0)
                    if (!this.IsBusy || axisPara.StopCmdWaitSeconds == 0)
                    {
                        Stop(StopType.SlowDown, isStopTask);
                    }
                    else
                    {
                        Task.Factory.StartNew(() =>
                        {
                            //if (!SpinWait.SpinUntil(() => this.IsReached, 3000))
                            if (!SpinWait.SpinUntil(() => !this.IsBusy, (int)(axisPara.StopCmdWaitSeconds * 1000)))
                            {
                                NotifyLogger.Post("Motor {0} CmdWait Stop Timeout", this.DeviceName);
                            }
                            Stop(StopType.SlowDown, isStopTask);
                        });
                    }
                }
            }
        }

        public bool TraceStart(M314 slaveM1)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                aryAxis[0] = (U16)axisPara.AxisNo;
                aryAxis[1] = (U16)slaveM1.axisPara.AxisNo;
                rc = m314.Library.CS_m314_enable_tracing_axis((U16)axisPara.CardSwitchNo , ref aryAxis[0] , 1);
            }
            return rc == 0 ? true : false;
        }

        public bool TraceStop(M314 slaveM1)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                aryAxis[0] = (U16)axisPara.AxisNo;
                aryAxis[1] = (U16)slaveM1.axisPara.AxisNo;
                rc = m314.Library.CS_m314_enable_tracing_axis((U16)axisPara.CardSwitchNo , ref aryAxis[0] , 0);
            }
            return rc == 0 ? true : false;
        }

        public void ResetPos()
        {
            m314.Library.CS_m314_set_position((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0);
            m314.Library.CS_m314_set_command((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , 0);
        }

        public int GetLatchPos()
        {
            I16 rc = -1;
            double pos = 0;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                rc = m314.Library.CS_m314_get_ltc_position((U16)axisPara.CardSwitchNo , (U16)axisPara.AxisNo , ref pos);
                return rc == 0 ? Convert.ToInt32(Math.Truncate(pos)) : -1;
            }
            else
            {
                return -1;
            }
        }

        public int GetTriggerCount(CompareChannel channel)
        {
            I16 rc = -1;
            int cnt = 0;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                rc = m314.Library.CS_m314_get_trigger_cnt((U16)axisPara.CardSwitchNo , (U16)channel , ref cnt);
                return rc == 0 ? cnt : -1;
            }
            else
            {
                return -1;
            }
        }

        public bool SetCompareConfig(CompareChannel channel , AxisNo axixNo , CompareOutputSrc outputSrc , double outputPulseWidth)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                U16 toMicroSecond = Convert.ToUInt16(Math.Truncate((outputPulseWidth / 3.2) + 1));
                rc = m314.Library.CS_m314_set_trigger_src((U16)axisPara.CardSwitchNo , (U16)channel , (U16)axixNo , (U16)outputSrc , toMicroSecond);
            }
            return rc == 0 ? true : false;
        }

        public static void SetCompareConfig(CardSwitchNo cardSwitchNo , CompareChannel channel , AxisNo axixNo , CompareOutputSrc outputSrc , double outputPulseWidth)
        {
            U16 toMicroSecond = Convert.ToUInt16(Math.Truncate((outputPulseWidth / 3.2) + 1));
            I16 rc = m314.Library.CS_m314_set_trigger_src((U16)cardSwitchNo , (U16)channel , (U16)axixNo , (U16)outputSrc , toMicroSecond);
        }

        //Change the compare output voltage level on the fly
        public bool SetCompareVoltage(CompareChannel channel , bool sw)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                rc = m314.Library.CS_m314_cmp_gpio((U16)axisPara.CardSwitchNo , (U16)channel , Convert.ToUInt16(sw));
            }
            return rc == 0 ? true : false;
        }

        public static void SetCompareVoltage(CardSwitchNo cardSwitchNo , CompareChannel channel , bool sw)
        {
            I16 rc = m314.Library.CS_m314_cmp_gpio((U16)cardSwitchNo , (U16)channel , Convert.ToUInt16(sw));
        }

        public bool CompareOneShut(CompareChannel channel)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                rc = m314.Library.CS_m314_cmp_oneshut((U16)axisPara.CardSwitchNo , (U16)channel);
            }
            return rc == 0 ? true : false;
        }

        public static void CompareOneShut(CardSwitchNo cardSwitchNo , CompareChannel channel)
        {
            I16 rc = m314.Library.CS_m314_cmp_oneshut((U16)cardSwitchNo , (U16)channel);
        }

        public bool CompareEnable(CompareChannel channel , bool sw)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                rc = m314.Library.CS_m314_set_trigger_enable((U16)axisPara.CardSwitchNo , (U16)channel , Convert.ToUInt16(sw));
            }
            return rc == 0 ? true : false;
        }

        public static void CompareEnable(CardSwitchNo cardSwitchNo , CompareChannel channel , bool sw)
        {
            I16 rc = m314.Library.CS_m314_set_trigger_enable((U16)cardSwitchNo , (U16)channel , Convert.ToUInt16(sw));
        }

        public bool SetToggleFixed(CompareChannel channel , int start , int end , uint interval , bool firstStatus)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                rc = m314.Library.CS_m314_position_cmp_level((U16)axisPara.CardSwitchNo , (U16)channel , start , end , interval , Convert.ToUInt16(firstStatus));
                CompareEnable(channel , true);
            }
            return rc == 0 ? true : false;
        }

        public static void SetToggleFixed(CardSwitchNo cardSwitchNo , CompareChannel channel , int start , int end , uint interval , bool firstStatus)
        {
            I16 rc = m314.Library.CS_m314_position_cmp_level((U16)cardSwitchNo , (U16)channel , start , end , interval , Convert.ToUInt16(firstStatus));
        }

        public bool SetTriggerHighSpeed(CompareChannel channel , int start , ComparatorDirection dirs , ushort interval , uint triggerCnt)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                rc = m314.Library.CS_m314_position_cmp_high_speed((U16)axisPara.CardSwitchNo , (U16)channel , start , Convert.ToUInt16(dirs) , interval , triggerCnt);
                CompareEnable(channel , true);
            }
            return rc == 0 ? true : false;
        }

        public static void SetTriggerHighSpeed(CardSwitchNo cardSwitchNo , CompareChannel channel , int start , ComparatorDirection dirs , ushort interval , uint triggerCnt)
        {
            I16 rc = rc = m314.Library.CS_m314_position_cmp_high_speed((U16)cardSwitchNo , (U16)channel , start , Convert.ToUInt16(dirs) , interval , triggerCnt);
            M314.CompareEnable(cardSwitchNo , channel , true);
        }

        public bool SetTriggerFixed(CompareChannel channel , int start , int end , uint interval)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                rc = m314.Library.CS_m314_position_cmp((U16)axisPara.CardSwitchNo , (U16)channel , start , end , interval);
                CompareEnable(channel , true);
            }
            return rc == 0 ? true : false;
        }

        public bool SetTriggerTable(CompareChannel channel , int[] data)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                switch (channel)
                {
                    case CompareChannel.Channel0:
                        rc = m314.Library.CS_m314_position_cmp_table((U16)axisPara.CardSwitchNo , (U16)channel , data , data.Length , 0);
                        break;

                    case CompareChannel.Channel1:
                        rc = m314.Library.CS_m314_position_cmp_table((U16)axisPara.CardSwitchNo , (U16)channel , data , data.Length , 0xc0000);
                        break;
                }
                CompareEnable(channel , true);
            }
            return rc == 0 ? true : false;
        }

        /// <summary>
        /// 啟動原點復歸 (參數定義在軸參中)
        /// </summary>
        public void Home()
        {
            if (axisPara.IsActive && axisPara.Enabled )
            {
                this.IsHome = false;
                m314HomeFlow.Restart();
            }
        }

        /// <summary> 同動復歸 </summary>
        /// <remarks> 因為調整同動的軟體限制軸號在0和1所以同動軸一定要在該卡的0與1軸</remarks>
        public void DoubleHome(M314 slaveM1)
        {
            //20150327 FlowControl暫不實作
            //if (!FlowBase.IsRunning("AOISystem.Utilities.Modules.Syntek.M314+FlowDoubleHome"))
            //{
            //    FlowDoubleHome flow = new FlowDoubleHome(this , slaveM1 , BuildLog , Complete , Fail);
            //    flow.Start(DeviceName + "Double Home Start");
            //}
            //else
            //{
            //    MessageBox.Show(ExceptionHelper.GetFullCurrentMethod(LanguageResourceManager.GetString("CommandBusy") + "(" + DeviceName + ")"), ResourceHelper.Language.GetString("SystemHint"), MessageBoxButtons.OK, MessageBoxIcon.None);
            //}
        }

        /// <summary>
        /// Shows the M314 Configuration Dialog.
        /// </summary>
        public DialogResult ConfigurationShowDialog()
        {
            M314Form m314Form = new M314Form(this);
            m314Form.Text = DeviceName;
            return m314Form.ShowDialog();
        }

        /// <summary>
        /// Shows the M314 Configuration
        /// </summary>
        public void ConfigurationShow(FormClosingEventHandler action = null)
        {
            FormHelper.OpenUniqueForm(DeviceName, () =>
            {
                M314Form m314Form = new M314Form(this);
                m314Form.Text = DeviceName;
                m314Form.Name = DeviceName;
                m314Form.Show();
                if (action != null)
                {
                    m314Form.FormClosing += action;
                }
            });
        }

        /// <summary>
        /// Shows the M314 Slim Configuration
        /// </summary>
        public void ConfigurationSlimShow(FormClosingEventHandler action = null)
        {
            FormHelper.OpenUniqueForm(DeviceName + "Slim", () =>
            {
                M314SlimForm m314SlimForm = new M314SlimForm(this);
                m314SlimForm.Text = DeviceName;
                m314SlimForm.Name = DeviceName + "Slim";
                m314SlimForm.Show();
                if (action != null)
                {
                    m314SlimForm.FormClosing += action;
                }
            });
        }

        public void ResetDoubleHome()
        {
            axisPara.DoubleHomeIsInit = true;
        }

        public double GetJogSpeed(JogSpeed jogSpeed)
        {
            switch (jogSpeed)
            {
                case JogSpeed.Micro:
                    return axisPara.JogMicroSpeed;

                case JogSpeed.Low:
                    return axisPara.JogLowSpeed;

                case JogSpeed.Mid:
                    return axisPara.JogMidSpeed;

                case JogSpeed.High:
                    return axisPara.JogHighSpeed;

                case JogSpeed.Max:
                    return axisPara.JogMaxSpeed;

                default:
                    return axisPara.JogMicroSpeed;
            }
        }

        public static int MMToPulse(M314 axis , double mm)
        {
            if (axis.AxisPara.DistPerRole != 0)
            {
                return (int)(mm / axis.AxisPara.DistPerRole * axis.AxisPara.PulsePerRole);
            }
            else
            {
                return 0;
            }
        }

        public static double PulseToMM(M314 axis , double pulse)
        {
            if (axis.AxisPara.PulsePerRole == 0)
            {
                axis.AxisPara.PulsePerRole = 1;
            }
            return pulse / axis.AxisPara.PulsePerRole * axis.AxisPara.DistPerRole;
        }

        public static void DoHomeLim(M314 axis)
        {
            m314.Library.CS_m314_set_home_config((U16)axis.AxisPara.CardSwitchNo , (U16)axis.AxisPara.AxisNo , 3 , Convert.ToInt16(axis.AxisPara.LogicORG) , Convert.ToInt16(axis.AxisPara.LogicEZ) , 1);
            m314.Library.CS_m314_set_home_offset_position((U16)axis.AxisPara.CardSwitchNo , (U16)axis.AxisPara.AxisNo , 0);
            m314.Library.CS_m314_set_home_finish_reset((U16)axis.AxisPara.CardSwitchNo , (U16)axis.AxisPara.AxisNo , 0);
            m314.Library.CS_m314_home_move((U16)axis.AxisPara.CardSwitchNo , (U16)axis.AxisPara.AxisNo , 0 , M314.MMToPulse(axis , axis.AxisPara.CreepDevVelH) , axis.AxisPara.CreepAccVelH , 1);
        }

        public static void DoHomeOrg(M314 axis)
        {
            m314.Library.CS_m314_set_home_config((U16)axis.AxisPara.CardSwitchNo , (U16)axis.AxisPara.AxisNo , 1 , Convert.ToInt16(axis.AxisPara.LogicORG) , Convert.ToInt16(axis.AxisPara.LogicEZ) , 0);
            m314.Library.CS_m314_home_move((U16)axis.AxisPara.CardSwitchNo , (U16)axis.AxisPara.AxisNo , 0 , M314.MMToPulse(axis , axis.AxisPara.CreepDevVelH) , axis.AxisPara.CreepAccVelH , 1);
        }

        //set command counter
        public void SetCommandCounter(double position)
        {
            m314.Library.CS_m314_set_command((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, mmToPulse(position));
        }

        //set encode
        public void SetPositionCounter(double position)
        {
            m314.Library.CS_m314_set_position((U16)axisPara.CardSwitchNo, (U16)axisPara.AxisNo, mmToPulse(position));
        }

        #endregion public method

        #region flow

        private class M314HomeFlow : FlowBase
        {
            private M314 _axis;

            public M314HomeFlow(M314 axis)
            {
                this.Name = axis.DeviceName + "HomeFlow";

                _axis = axis;
            }

            public override void Flow()
            {
                if (Timer3.ElapsedMilliseconds > _axis.AxisPara.HomeTimeout)
                {
                    ChangeStep1("901", string.Format("Timeout at Step : {0},Desc : {1}", Step1, Description));
                }
                if (_axis.IsAlarm)
                {
                    ChangeStep1("901", string.Format("Axis alarm at Step : {0},Desc : {1}", Step1, Description));
                }
                switch (Step1)
                {
                    #region flow init

                    case "0":
                        _axis.SetSoftLimit(false);
                        _axis.Stop(StopType.SlowDown, false);
                        _axis.ServoOn(CmdStatus.ON);
                        ChangeStep1("1", "Axis Home Start");
                        Timer3.Restart();
                        break;

                    case "1":
                        if (!_axis.IsBusy)
                        {
                            switch (_axis.AxisPara.HomeMode)
                            {
                                case HomeMode.OnePoint:
                                    ChangeStep1("1000", "Home Start(One Point)");
                                    break;

                                case HomeMode.TwoPoint:
                                    ChangeStep1("2000", "Home Start(Two Point)");
                                    break;

                                case HomeMode.ThreePoint:
                                    ChangeStep1("3000", "Home Start(Three Point)");
                                    break;
                            }
                        }
                        break;

                    #endregion flow init

                    #region OnePoint

                    case "1000":
                        //不在原點上
                        if (!_axis.IsORG)
                        {
                            _axis.JogM(_axis.AxisPara.OPRDevVelH, RotationDirection.CCW);
                        }
                        ChangeStep1("1010", "Axis Jog Negative");
                        break;

                    case "1010":
                        if (_axis.IsORG)
                        {
                            _axis.Stop(StopType.SlowDown, false);
                            Timer1.Restart();
                            ChangeStep1("1020", "Axis On ORG");
                        }
                        break;

                    case "1020":
                        if (Timer1.ElapsedMilliseconds > 100)
                        {
                            _axis.JogM(5, RotationDirection.CW);
                            Timer1.Restart();
                            ChangeStep1("1030", "Axis Jog Positive");
                        }
                        break;

                    case "1030":
                        if (!_axis.IsORG)
                        {
                            _axis.Stop(StopType.SlowDown, false);
                            M314.DoHomeOrg(_axis);
                            Timer1.Restart();
                            ChangeStep1("7000", "M314 Home Move");
                        }
                        break;

                    #endregion OnePoint

                    #region TwoPoint

                    case "2000":
                        if (_axis.IsLimitN)
                        {
                            _axis.JogM(_axis.AxisPara.OPRDevVelH, RotationDirection.CW);
                            Timer1.Restart();
                            ChangeStep1("2010", "Axis Jog Positive");
                        }
                        break;

                    case "2010":
                        if (!_axis.IsLimitN)
                        {
                            _axis.JogM(_axis.AxisPara.OPRDevVelH, RotationDirection.CCW);
                        }
                        ChangeStep1("2020", "Axis Jog Negative");
                        break;

                    case "2020":
                        if (_axis.IsLimitN)
                        {
                            _axis.Stop(StopType.SlowDown, false);
                            Timer1.Restart();
                            ChangeStep1("2030", "Axis On LimitN");
                        }
                        break;

                    case "2030":
                        if (Timer1.ElapsedMilliseconds > 100)
                        {
                            M314.DoHomeLim(_axis);
                            ChangeStep1("7000", "M314 Home Move");
                        }
                        break;

                    #endregion TwoPoint

                    #region ThreePoint

                    case "3000":
                        if (/*!_axis.IsORG &&*/ !_axis.IsLimitN)
                        {
                            _axis.JogM(_axis.AxisPara.OPRDevVelH, RotationDirection.CCW);
                        }
                        ChangeStep1("3010", "Axis Jog Negative");
                        break;

                    case "3010":
                        if (/*_axis.IsORG ||*/ _axis.IsLimitN)
                        {
                            _axis.Stop(StopType.SlowDown, false);
                            Timer1.Restart();
                            string str;
                            if (_axis.IsORG)
                            {
                                str = string.Format("{0} Axis On ORG ", _axis.DeviceName);
                            }
                            else if (_axis.IsLimitN)
                            {
                                str = string.Format("{0} Axis On LimitN", _axis.DeviceName);
                            }
                            else
                            {
                                str = string.Format("{0} Axis On ORG and Axis On LimitN", _axis.DeviceName);
                            }
                            ChangeStep1("3020", str);
                        }
                        break;

                    case "3020":
                        if (Timer1.ElapsedMilliseconds > 100)
                        {
                            _axis.JogM(_axis.AxisPara.CreepDevVelH, RotationDirection.CW);
                            ChangeStep1("3030", _axis.DeviceName + "Axis Jog Positive");
                        }
                        break;

                    case "3030":
                        if (_axis.IsORG)
                        {
                            ChangeStep1("3040", _axis.DeviceName + "Axis On ORG");
                        }
                        break;

                    case "3040":
                        if (!_axis.IsORG)
                        {
                            _axis.Stop(StopType.SlowDown, false);
                            Timer1.Restart();
                            ChangeStep1("3050", _axis.DeviceName + "Axis out of ORG");
                        }
                        break;

                    case "3050":
                        if (Timer1.ElapsedMilliseconds > 100)
                        {
                            M314.DoHomeOrg(_axis);
                            ChangeStep1("7000", _axis.DeviceName + "M314 Home Move");
                        }
                        break;

                    #endregion ThreePoint

                    #region flow continue

                    case "7000":
                        if (Timer1.ElapsedMilliseconds > 500 && !_axis.IsBusy)
                        {
                            ChangeStep1("7010", _axis.DeviceName + "M314 Home Move");
                        }
                        break;

                    case "7010":
                        _axis.ResetPos();
                        _axis.AbsolueMove(_axis.AxisPara.OffsetH, SpeedMode.Manual);
                        ChangeStep1("7020", "Move to soft ORG");
                        break;

                    case "7020":
                        if (_axis.IsReached)
                        {
                            Timer1.Restart();
                            ChangeStep1("7030", "Axis Home Delay");
                        }
                        break;

                    case "7030":
                        if (Timer1.ElapsedMilliseconds >= _axis.AxisPara.ClearDelay * 1000)
                        {
                            _axis.ResetPos();
                            ChangeStep1("801", "Axis Home Complete");
                        }
                        break;

                    #endregion flow continue

                    #region flow end

                    //flow finish
                    case "801":
                        //_axis.SetSoftLimit(true, _axis.softLimitN, _axis.softLimitP);
                        _axis.IsHome = true;
                        _axis.IsHomeMoving = false;
                        Stop("Axis Home Complete");
                        break;

                    //flow trip
                    case "901":
                        //_axis.SetSoftLimit(true, _axis.softLimitN, _axis.softLimitP);
                        _axis.IsHome = false;
                        _axis.IsHomeMoving = false;
                        Fail("Axis Home Fail");
                        break;

                    #endregion flow end
                }
            }
        }

        //private class FlowDoubleHome : FlowBase
        //{
        //    private static PCI_M314 m314 = PCI_M314.GetInstance();
        //    private M314 masterAxis;
        //    private M314 slaveAxis;

        //    public FlowDoubleHome(M314 masterAxis , M314 slaveAxis , Action<string> BuildLog , Action<string> Complete , Action<string> Fail)
        //    {
        //        this.masterAxis = masterAxis;
        //        this.slaveAxis = slaveAxis;
        //        if (Fail != null)
        //        {
        //            this.Failed += (msg) => { Fail(msg); };
        //        }
        //        if (BuildLog != null)
        //        {
        //            this.BuildLog += (msg) => { BuildLog(msg); };
        //        }
        //        if (Complete != null)
        //        {
        //            this.Completed += (msg) => { Complete(msg); };
        //        }
        //    }

        //    public override void Flow()
        //    {
        //        if (Timer3.ElapsedMilliseconds > masterAxis.AxisPara.DoubleHomeTimeout)
        //        {
        //            StepChange("901" , string.Format("Timeout at Step : {0},Desc : {1}" , Step , Description));
        //        }
        //        if (masterAxis.IsAlarm || slaveAxis.IsAlarm)
        //        {
        //            StepChange("901" , string.Format("Axis alarm at Step : {0},Desc : {1}" , Step , Description));
        //        }
        //        // 同動復歸
        //        // 因為調整同動的軟體限制軸號在0和1所以同動軸一定要在該卡的0與1軸
        //        switch (Step)
        //        {
        //            case "0":
        //                masterAxis.SetSoftLimit(false);
        //                slaveAxis.SetSoftLimit(false);
        //                m314.Library.CS_m314_disable_soft_limit(0 , 0);
        //                masterAxis.Stop(StopType.SlowDown , false);
        //                masterAxis.ServoOn(CmdStatus.ON);
        //                masterAxis.IsHome = false;
        //                slaveAxis.IsHome = false;
        //                masterAxis.TraceStart(slaveAxis);
        //                StepChange("1" , "Double Home Start");
        //                break;

        //            case "1":
        //                if (!masterAxis.IsBusy)
        //                {
        //                    masterAxis.JogM(masterAxis.AxisPara.OPRDevVelH , RotationDirection.CCW);
        //                    StepChange("2" , "Axis Jog Negative");
        //                }
        //                break;

        //            case "2":
        //                if (masterAxis.IsLimitN)
        //                {
        //                    masterAxis.Stop(StopType.Emergency , false);
        //                    slaveAxis.Stop(StopType.Emergency , false);
        //                    Timer1.Restart();
        //                    StepChange("3" , "Axis On LimitN");
        //                }
        //                break;

        //            case "3":
        //                if (Timer1.ElapsedMilliseconds > 5000)
        //                {
        //                    //必須先將跟隨停止否則無法ClearCurrentPos
        //                    masterAxis.TraceStop(slaveAxis);
        //                    masterAxis.ResetPos();
        //                    slaveAxis.ResetPos();
        //                    StepChange("4" , "M314 Home Move");
        //                }
        //                break;

        //            case "4":
        //                masterAxis.TraceStart(slaveAxis);
        //                m314.Library.CS_m314_set_ltc_logic(0 , 0 , 0);
        //                m314.Library.CS_m314_set_ltc_src(0 , 0 , 2);
        //                m314.Library.CS_m314_set_ltc_logic(0 , 1 , 0);
        //                m314.Library.CS_m314_set_ltc_src(0 , 1 , 2);
        //                m314.Library.CS_m314_set_home_config(0 , 0 , 7 , 0 , 0 , 0);
        //                m314.Library.CS_m314_set_home_offset_position(0 , 0 , 0);
        //                m314.Library.CS_m314_set_home_finish_reset(0 , 0 , 0);

        //                masterAxis.IsDoubleHomeMoving = true;
        //                m314.Library.CS_m314_home_move(0 , 0 , M314.MMToPulse(masterAxis , masterAxis.AxisPara.CreepStrVelH) , M314.MMToPulse(masterAxis , masterAxis.AxisPara.CreepDevVelH) , masterAxis.AxisPara.CreepAccVelH , 1);
        //                StepChange("5" , "M314 Home Move");
        //                break;

        //            case "5":
        //                if (!masterAxis.IsBusy)
        //                {
        //                    StepChange("6" , "M314 Home Move");
        //                }
        //                break;

        //            case "6":
        //                masterAxis.TraceStop(slaveAxis);
        //                Timer1.Restart();
        //                StepChange("7" , "M314 Home Move");
        //                break;

        //            case "7":
        //                if (Timer1.ElapsedMilliseconds > 200)
        //                {
        //                    if (masterAxis.AxisPara.DoubleHomeIsInit)
        //                    {
        //                        double ltcPositionMaster = 0;
        //                        double ltcPositionSlave = 0;
        //                        m314.Library.CS_m314_get_ltc_position(0 , 0 , ref ltcPositionMaster);
        //                        m314.Library.CS_m314_get_ltc_position(0 , 1 , ref ltcPositionSlave);
        //                        masterAxis.AxisPara.DoubleHomeMasterPosition = M314.PulseToMM(masterAxis , ltcPositionMaster);
        //                        //masterPara.DoubleHomeSlaveOffset = pulseToMM(ltcPositionMaster) - pulseToMM(ltcPositionSlave);
        //                        masterAxis.AxisPara.DoubleHomeSlaveOffset = M314.PulseToMM(masterAxis , ltcPositionMaster) - M314.PulseToMM(slaveAxis , ltcPositionSlave);
        //                    }
        //                    else
        //                    {
        //                        double ltcPositionMaster = 0;
        //                        double ltcPositionSlave = 0;
        //                        m314.Library.CS_m314_get_ltc_position(0 , 0 , ref ltcPositionMaster);
        //                        m314.Library.CS_m314_get_ltc_position(0 , 1 , ref ltcPositionSlave);
        //                        // double offset = pulseToMM(ltcPositionSlave) + masterAxis.AxisPara.DoubleHomeSlaveOffset - slaveAxis.Encoder;
        //                        double offset = M314.PulseToMM(slaveAxis , ltcPositionSlave) + masterAxis.AxisPara.DoubleHomeSlaveOffset - slaveAxis.Encoder;
        //                        slaveAxis.OffsetM(offset , 1);
        //                    }
        //                    StepChange("8" , "Calculate Offset");
        //                }
        //                break;

        //            case "8":
        //                if (!masterAxis.IsBusy && !slaveAxis.IsBusy)
        //                {
        //                    Timer1.Restart();
        //                    StepChange("9" , "Calculate Offset");
        //                }
        //                break;

        //            case "9":
        //                if (Timer1.ElapsedMilliseconds > 5000)
        //                {
        //                    masterAxis.ResetPos();
        //                    slaveAxis.ResetPos();
        //                    masterAxis.TraceStart(slaveAxis);
        //                    StepChange("10" , "Calculate Offset");
        //                }
        //                break;

        //            case "10":
        //                if (!masterAxis.AxisPara.DoubleHomeIsInit)
        //                {
        //                    masterAxis.Move(masterAxis.AxisPara.OffsetH , SpeedMode.Manual);
        //                    StepChange("11" , "Move to soft ORG");
        //                }
        //                else
        //                {
        //                    masterAxis.AxisPara.DoubleHomeIsInit = false;
        //                    StepChange("801" , "Double home finish");
        //                }

        //                break;

        //            case "11":
        //                if (masterAxis.IsReached)
        //                {
        //                    Timer1.Restart();
        //                    StepChange("12" , "Reset Position");
        //                }
        //                break;

        //            case "12":
        //                if (Timer1.ElapsedMilliseconds > 3000)
        //                {
        //                    masterAxis.ResetPos();
        //                    slaveAxis.ResetPos();
        //                    StepChange("801" , "Double home finish");
        //                }
        //                break;

        //            //flow finish
        //            case "801":
        //                masterAxis.SetSoftLimit(true , masterAxis.softLimitN , masterAxis.softLimitP);
        //                slaveAxis.SetSoftLimit(true , masterAxis.softLimitN , masterAxis.softLimitP);
        //                masterAxis.IsDoubleHomeMoving = false;
        //                masterAxis.IsHome = true;
        //                slaveAxis.IsHome = true;
        //                Complete("Double Home Complete");
        //                break;

        //            //flow trip
        //            case "901":
        //                masterAxis.SetSoftLimit(true , masterAxis.softLimitN , masterAxis.softLimitP);
        //                slaveAxis.SetSoftLimit(true , masterAxis.softLimitN , masterAxis.softLimitP);
        //                masterAxis.IsDoubleHomeMoving = false;
        //                masterAxis.IsHome = false;
        //                slaveAxis.IsHome = false;
        //                Fail("Double Home Fail");
        //                break;
        //        }
        //    }
        //}

        #endregion flow
    }
}