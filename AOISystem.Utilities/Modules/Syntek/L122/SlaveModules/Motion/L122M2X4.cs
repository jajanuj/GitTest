using AOISystem.Utilities.Common;
using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Logging;
using AOISystem.Utilities.Modules.Syntek.L122.Library;
using AOISystem.Utilities.Modules.Syntek.L122.MasterCard;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using I16 = System.Int16;
using I32 = System.Int32;
using U16 = System.UInt16;
using U8 = System.Byte;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.Motion
{
    //L122 如果連續改變速度值 可能因為程式寫入速度太快導致數值未正確寫入
    public class L122M2X4 : L122.MasterCard.L122, IMotion
    {
        #region struct

        internal struct M2X4Status
        {
            public bool RDY;
            public bool ALM;
            public bool LimitP;
            public bool LimitN;
            public bool ORG;
            public bool DIR;
            public bool EMG;
            public bool PCS;
            public bool ERC;
            public bool ZPhase;
            public bool CLR;
            public bool Latch;
            public bool SD;
            public bool INP;
            public bool SVON;
            public bool RALM;
            public bool Home;
        }

        #endregion struct

        #region event

        public delegate void HomeHandler(string str);

        public event HomeHandler HomeStarted;

        public event HomeHandler HomeCompleted;

        public event HomeHandler HomeFailure;

        #endregion event

        #region field

        private Thread systemScanThread;
        private bool keyOfIOStatus;
        private int initErr;
        private int nPos;
        private L122M2X4HomeFlow l122M2X4HomeFlow;
        protected internal ParameterL122M2X4 axisPara;
        private M2X4Status status;
        private U16 busy;
        private bool softLimitEnabled;
        private double softLimitN;
        private double softLimitP;
        private double maxSpeed;

        #endregion field

        #region properties

        /// <summary> M2X4模組軸參 </summary>
        internal ParameterL122M2X4 AxisPara { get { return axisPara; } set { axisPara = value; } }

        /// <summary> M2X4模組狀態點資訊 </summary>
        internal M2X4Status Status { get { return status; } }

        /// <summary> 取得目前設定位置 (Command Counter) </summary>
        public double Position { /*set { setPos(value); } */get { return getPos(); } }
        public double Position2 { /*set { setPos(value); } */get { return getPos2(); } }

        ///<summary> 取得編碼器位置 (Position Counter) </summary>
        public double Encoder { /*set { setENC(value); } */get { return getENC(); } }
        public double Encoder2 { /*set { setENC(value); } */get { return getENC2(); } }

        ///<summary> 取得目前速度 (Current Speed) </summary>
        public double Speed { /*set { setENC(value); } */get { return getSpeed(); } }
        public double Speed2 { /*set { setENC(value); } */get { return getSpeed2(); } }

        ///<summary> 取得錯誤命令次數 (Error Count) </summary>
        public double ErrorCount { /*set { setENC(value); } */get { return getErrorCount(); } }
        public double ErrorCount2 { /*set { setENC(value); } */get { return getErrorCount2(); } }

        /// <summary> 取得原點復歸的旗標 </summary>
        public virtual bool IsHome { get { return status.Home; } set { status.Home = value; } }

        /// <summary> 取得原點Sensor訊號 </summary>
        public virtual bool IsORG { get { return status.ORG; } }

        /// <summary> 取得正極限訊號 </summary>
        public virtual bool IsLimitP { get { return status.LimitP; } }

        /// <summary> 取得負極限訊號 </summary>
        public virtual bool IsLimitN { get { return status.LimitN; } }

        /// <summary> 取得警報訊號 </summary>
        public virtual bool IsAlarm { get { return status.ALM; } }

        /// <summary> 取得到位訊號 </summary>
        public bool IsINP { get { return status.INP; } }

        /// <summary> 軸是否啟用(紀錄到xml中) </summary>
        public bool IsActive { get { return axisPara.IsActive; } set { axisPara.IsActive = value; } }

        /// <summary> 軸是否啟用 </summary>
        public bool Enabled { get { return axisPara.Enabled; } set { axisPara.Enabled = value; } }

        /// <summary> M2X4模組是否在移動中 </summary>
        public bool IsBusy
        {
            get
            {
                U16 busyData = new U16();
                if (CCMNet.CS_mnet_m204_motion_done(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref busyData) == 0)
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

        /// <summary> M2X4模組是否到位 </summary>
        public bool IsReached
        {
            get
            {
                //int cmdPos = 0;
                //CCMNet.CS_mnet_m204_get_command(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref cmdPos);
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

        public bool IsSoftLimitN { get { return Encoder <= softLimitN; } }

        public bool IsSoftLimitP { get { return Encoder >= softLimitP; } }

        #endregion properties

        #region construct

        public L122M2X4(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            busy = new U16();

            axisPara = Parameter as ParameterL122M2X4;

            slaveModuleInitialize();

            setHardwareScan();

            taskInitialize();

            setMotion();

            axisPara.ParameterChanged += new ParameterINI.ParameterChangedHandler((paraName) => setMotion());

            softLimitEnabled = axisPara.SoftLimitEnabled;
            softLimitN = axisPara.SoftLimitN;
            softLimitP = axisPara.SoftLimitP;
            maxSpeed = 1000;
            //ServoOn(CmdStatus.ON);
        }

        #endregion construct

        #region private method

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

        private void slaveModuleInitialize()
        {
            uint mask = 0x00000001;
            uint[] deviceTable = DeviceTable;
            //CCMNet.CS_mnet_reset_ring(RingNoOfMNet);
            //Stopwatch sw = Stopwatch.StartNew();
            //I16 retOfGetTable = CCMNet.CS_mnet_get_ring_active_table(RingNoOfMNet, deviceTable);
            //Console.WriteLine("{0}, {1}", deviceTable[0], deviceTable[1]);
            //sw.Stop();
            //Console.WriteLine("{0} CS_mnet_get_ring_active_table {1}", this.DeviceName, sw.ElapsedMilliseconds);
            //if (retOfGetTable != 0)
            //{
            //    throw new Exception("Error occur when get device table !!! \n func = [_mnet_get_ring_active_table]");
            //}
            if ((deviceTable[0] == 0) && (deviceTable[1] == 0))
            {
                throw new Exception("Can't find slave M2X4, there is not any device !!! \n func = [_mnet_get_ring_active_table, deviceTable=0]");
            }

            for (int i = 0; i < 64; i++)
            {
                if (axisPara.SlaveIP > 63)
                {
                    throw new Exception("Wrong SlaveIP, SlaveIP must less than 63!!!");
                }

                if (axisPara.SlaveIP == i)
                {
                    if (i < 32)
                    {
                        if ((deviceTable[0] & mask) == 0)
                        {
                            throw new Exception("Have not found M2X4 !!!\n" + "SlaveIP = " + axisPara.SlaveIP);
                        }
                    }
                    else
                    {
                        if (i == 32)
                            mask = 0x00000001;
                        if ((deviceTable[1] & mask) == 0)
                        {
                            throw new Exception("Have not found M2X4 !!!\n" + "SlaveIP = " + axisPara.SlaveIP);
                        }
                    }
                }
                mask = mask << 1;
            }

            I16 retOfStartRing = CCMNet.CS_mnet_start_ring(RingNoOfMNet);
            if (retOfStartRing != 0)
            {
                throw new Exception("Error occur when start ring !!! \n func = [_mnet_start_ring]");
            }

            U8 slaveType = 0;
            I16 retOfGetSlaveType = CCMNet.CS_mnet_get_slave_type(RingNoOfMNet, axisPara.SlaveIP, ref slaveType);
            if (retOfGetSlaveType == 0)
            {
                if (slaveType != 0xA7)
                {
                    throw new Exception("deviec type is not M2X4!!!\n" + "SlaveIP = " + axisPara.SlaveIP);
                }
            }
            else
            {
                throw new Exception("Error occur when get device type !!! \n func = [_mnet_get_slave_type]");
            }

            I16 retOfInitial = CCMNet.CS_mnet_m204_initial(RingNoOfMNet, axisPara.SlaveIP);
            if (retOfInitial != 0)
            {
                throw new Exception("Error occur when M2X4 module initial !!! \n func = [_mnet_m1_initial]");
            }
        }

        //設定掃描M2X4的Thread
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
            l122M2X4HomeFlow = new L122M2X4HomeFlow(this);
            flowControl.AddFlowBase(l122M2X4HomeFlow);
        }

        //scan M2X4 slave IO _axis
        private void systemScan(FlowVar fv)
            {
            //while (true)
            //{
                if (keyOfIOStatus)
                {
                    U16 status = 0;
                    I16 rt = CCMNet.CS_mnet_m204_get_io_status(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref status);
                    this.status.RDY = BitConverterEx.TestB(status, 0);
                    this.status.ALM = BitConverterEx.TestB(status, 1);
                    this.status.LimitP = BitConverterEx.TestB(status, 2);
                    this.status.LimitN = BitConverterEx.TestB(status, 3);
                    this.status.ORG = BitConverterEx.TestB(status, 4);
                    this.status.DIR = BitConverterEx.TestB(status, 5);
                    this.status.EMG = BitConverterEx.TestB(status, 6);
                    this.status.PCS = BitConverterEx.TestB(status, 7);
                    this.status.ERC = BitConverterEx.TestB(status, 8);
                    this.status.ZPhase = BitConverterEx.TestB(status, 9);
                    this.status.CLR = BitConverterEx.TestB(status, 10);
                    this.status.Latch = BitConverterEx.TestB(status, 11);
                    this.status.SD = BitConverterEx.TestB(status, 12);
                    this.status.INP = BitConverterEx.TestB(status, 13);
                    this.status.SVON = BitConverterEx.TestB(status, 14);
                    this.status.RALM = BitConverterEx.TestB(status, 15);

                    if (this.status.ALM || this.status.EMG)
                        this.IsHome = false;

                    //Thread.Sleep(50);
            }
            //}
        }

        //將軸參寫入M2X4 slave
        private void setMotion()
        {
            U16 hMode;
            //初始化給1 且限制在 1~3，這邊又把home mode變成12 or 9
            if (axisPara.HomeMode == HomeMode.TwoPoint)
                hMode = 12;
            else
                hMode = 9;

            initErr = CCMNet.CS_mnet_m204_set_alm(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, Convert.ToUInt16(axisPara.LogicALM), 0);
            if (axisPara.MotorMode == MotorMode.Servo)
            {
                initErr = CCMNet.CS_mnet_m204_set_inp(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 1, Convert.ToUInt16(axisPara.LogicINP));
            }
            else
            {
                initErr = CCMNet.CS_mnet_m204_set_inp(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 0, Convert.ToUInt16(axisPara.LogicINP));
            }
            initErr = CCMNet.CS_mnet_m204_set_erc_on(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 0);
            initErr = CCMNet.CS_mnet_m204_set_erc(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, Convert.ToUInt16(axisPara.LogicERC), 0, 0);
            initErr = CCMNet.CS_mnet_m204_set_home_config(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, hMode, Convert.ToUInt16(axisPara.LogicORG), Convert.ToUInt16(axisPara.LogicZ), 0, 0);
            initErr = CCMNet.CS_mnet_m204_set_sd(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 0, Convert.ToInt16(axisPara.LogicSD), 0, 0);
            initErr = CCMNet.CS_mnet_m204_set_ltc_logic(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, Convert.ToUInt16(axisPara.LogicLTC));
            initErr = CCMNet.CS_mnet_m204_set_pls_outmode(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, (U16)axisPara.PulseMode);
            initErr = CCMNet.CS_mnet_m204_set_pls_iptmode(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, (U16)axisPara.EncMode, (U16)axisPara.EncDir);
            if (axisPara.MotorMode == MotorMode.Servo)
            {
                //initErr = CCMNet.CS_mnet_m204_set_feedback_src(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 3);
                initErr = CCMNet.CS_mnet_m204_set_feedback_src(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 0);
            }
            else
            {
                initErr = CCMNet.CS_mnet_m204_set_feedback_src(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 2);
            }
            //0:External Feedback, Position Counter
            //1:Command Pulse, Position Counter
            //2:Command Pulse, Command Pulse
            //3:External Feedback, Command Pulse
            initErr = CCMNet.CS_mnet_m204_set_el(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 0, Convert.ToInt16(axisPara.LogicEL));
            initErr = CCMNet.CS_mnet_m204_fix_speed_range(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(axisPara.MaxVel));
        }

        //get command counter
        private double getPos()
        {
            int cmdPos = new int();
            double CmdPosx;
            CCMNet.CS_mnet_m204_get_command(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref cmdPos);
            CmdPosx = cmdPos;
            if (axisPara.PulsePerRole != 0)
            {
                return CmdPosx / axisPara.PulsePerRole * axisPara.DistPerRole;
            }
            else
            {
                return 0;
            }
        }

        private double getPos2()
        {
            int cmdPos = new int();
            CCMNet.CS_mnet_m204_get_command(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref cmdPos);
            return cmdPos;
        }

        private double getENC()
        {
            int feedBackPos = new I32();
            double d_feedBackPos;
            CCMNet.CS_mnet_m204_get_position(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref feedBackPos);
            d_feedBackPos = feedBackPos;

            if (axisPara.PulsePerRole != 0)
            {
                return d_feedBackPos / axisPara.PulsePerRole * axisPara.DistPerRole;
            }
            else
            {
                return 0;
            }
        }

        private double getENC2()
        {
            int feedBackPos = new I32();
            CCMNet.CS_mnet_m204_get_position(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref feedBackPos);
            return feedBackPos;
        }

        private double getSpeed()
        {
            double speed = 0;
            CCMNet.CS_mnet_m204_get_current_speed(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref speed);

            if (axisPara.PulsePerRole != 0)
            {
                return speed / axisPara.PulsePerRole * axisPara.DistPerRole;
            }
            else
            {
                return 0;
            }
        }

        private double getSpeed2()
        {
            double speed = 0;
            CCMNet.CS_mnet_m204_get_current_speed(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref speed);
            return speed;
        }

        private double getErrorCount()
        {
            I16 error = 0;
            CCMNet.CS_mnet_m204_get_error_counter(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref error);
            if (axisPara.PulsePerRole != 0)
            {
                return (double)error / axisPara.PulsePerRole * axisPara.DistPerRole;
            }
            else
            {
                return 0;
            }
        }

        private double getErrorCount2()
        {
            I16 error = 0;
            CCMNet.CS_mnet_m204_get_error_counter(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, ref error);
            return error;
        }

        #endregion private method

        #region public method

        public void SetSoftLimit(bool enabled, double limitN, double limitP)
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
        /// 單位轉換 (Millimeter to pulse)
        /// </summary>
        /// <param name="mm">The Millimeter.</param>
        /// <returns></returns>
        public int MmToPulse(double mm)
        {
            if (axisPara.DistPerRole != 0)
                return (int)(mm / axisPara.DistPerRole * axisPara.PulsePerRole);
            else
                return 0;
        }

        /// <summary>
        /// 清除postion和command的counter
        /// </summary>
        public void ResetPos()
        {
            CCMNet.CS_mnet_m204_reset_position(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo);
            CCMNet.CS_mnet_m204_reset_command(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo);
        }

        /// <summary>
        /// M2X4 裝置啟停
        /// </summary>
        /// <param name="option">The option.</param>
        public void ServoOn(CmdStatus option)
        {
            I16 rc = -1;
            if (axisPara != null)
            {
                if (axisPara.IsActive)
                {
                    setMotion();

                    if (option == CmdStatus.OFF)
                    {
                        this.IsHome = false;
                        axisPara.Enabled = false;
                        rc = CCMNet.CS_mnet_m204_set_svon(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 0);
                    }
                    if (option == CmdStatus.ON)
                    {
                        if (axisPara.IsActive)
                        {
                            axisPara.Enabled = true;
                            rc = CCMNet.CS_mnet_m204_disable_soft_limit(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo);
                            CCMNet.CS_mnet_m204_set_svon(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 1);
                        }
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
                CCMNet.CS_mnet_m204_set_ralm(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 1);
                Task.Factory.StartNew(() => 
                {
                    Thread.Sleep(1000);
                    CCMNet.CS_mnet_m204_set_ralm(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 0);
                });
            }
        }

        /// <summary>
        /// 絕對移動 (需選擇使用軸參的手動速度或是自動速度)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="inAuto">手動或自動速度</param>
        public bool AbsolueMove(double dest, SpeedMode inAuto = SpeedMode.Auto)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                nPos = MmToPulse(dest);
                if (inAuto == SpeedMode.Manual)
                {
                    rc = CCMNet.CS_mnet_m204_start_ta_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, nPos, MmToPulse(axisPara.StrVelM), MmToPulse(axisPara.DevVelM), axisPara.AccVelM, axisPara.DecVelM);
                }
                if (inAuto == SpeedMode.Auto)
                {
                    rc = CCMNet.CS_mnet_m204_start_ta_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, nPos, MmToPulse(axisPara.StrVelA), MmToPulse(axisPara.DevVelA), axisPara.AccVelA, axisPara.DecVelA);
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
                nPos = MmToPulse(dest);
                if (curve)
                {
                rc = CCMNet.CS_mnet_m204_start_ta_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, nPos, MmToPulse(speed) / 2, MmToPulse(speed), axisPara.AccVelM, axisPara.DecVelM);
            }
                else
                {
                    rc = CCMNet.CS_mnet_m204_start_sa_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, nPos, MmToPulse(speed) / 2, MmToPulse(speed), axisPara.AccVelM, axisPara.DecVelM, MmToPulse(speed) / 2, MmToPulse(speed) / 2);
                }
            }
            return rc == 0 ? true : false;
        }

        /// <summary>
        /// 相對移動 (需選擇使用軸參的手動速度或是自動速度)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="inAuto">手動或自動速度</param>
        public bool RelativeMove(double dest, SpeedMode inAuto = SpeedMode.Auto)
        {
            return AbsolueMove(getPos() + dest, inAuto);
        }

        /// <summary>
        /// 相對移動 (自定義速度，初速為恆速1/2，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="speed">速度值</param>
        public bool RelativeMove(double dest, double speed)
        {
            return AbsolueMove(getPos() + dest, speed);
        }


        /// <summary>
        /// 連續移動
        /// </summary>
        /// <param name="speed">速度值</param>
        /// <returns></returns>
        public bool ContinuousMove(RotationDirection dir, double speed)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                if (isAllowMove(dir))
                {
                    sbyte bDir = 1;
                    if (dir == RotationDirection.CW)
                    {
                        bDir = 1;
                    }
                    else
                    {
                        bDir = -1;
                    }
                    CCMNet.CS_mnet_m204_tv_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(checkOverMaxSpeed(speed)) / 2 * bDir, MmToPulse(checkOverMaxSpeed(speed)) * bDir, axisPara.AccVelM);
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
                    sbyte bDir = 1;
                    if (dir == RotationDirection.CW)
                    {
                        bDir = 1;
                    }
                    else
                    {
                        bDir = -1;
                    }
                    if (inAuto == SpeedMode.Auto)
                    {
                        CCMNet.CS_mnet_m204_tv_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(checkOverMaxSpeed(axisPara.StrVelA)) * bDir, MmToPulse(checkOverMaxSpeed(axisPara.DevVelA)) * bDir, axisPara.AccVelA);
                    }
                    else
                    {
                        CCMNet.CS_mnet_m204_tv_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(checkOverMaxSpeed(axisPara.StrVelM)) * bDir, MmToPulse(checkOverMaxSpeed(axisPara.DevVelM)) * bDir, axisPara.AccVelM);
                    }
                }
            }
            return rc == 0 ? true : false;
        }

        /// <summary>
        /// 停止M2X4裝置 (復歸流程中不停止homeTask)
        /// </summary>
        public void Stop(StopType type, bool isStopTask = true)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled)
            {
                if (type == StopType.Emergency)
                {
                    rc = CCMNet.CS_mnet_m204_emg_stop(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo);
                }
                if (type == StopType.SlowDown)
                {
                    rc = CCMNet.CS_mnet_m204_sd_stop(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, axisPara.DecVelM);
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
                if (isStopTask)
                {
                    l122M2X4HomeFlow.Stop();
                }
            }
        }

        /// <summary>
        /// 吋動 (自定義速度，初速為恆速1/2，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="speed">速度值 (millimeter).</param>
        /// <param name="dir">吋動方向</param>
        public bool JogM(double speed, RotationDirection dir)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                if (isAllowMove(dir))
                {
                    sbyte bDir = 1;
                    if (dir == RotationDirection.CW)
                    {
                        bDir = 1;
                    }
                    else
                    {
                        bDir = -1;
                    }
                    if (!this.IsBusy)
                    {
                        CCMNet.CS_mnet_m204_tv_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(checkOverMaxSpeed(speed)) / 2 * bDir, MmToPulse(checkOverMaxSpeed(speed)) * bDir, axisPara.AccVelM);
                    }
                    else
                    {
                        CCMNet.CS_mnet_m204_v_change(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(checkOverMaxSpeed(speed)) * bDir, axisPara.AccVelM);
                    }
                }
            }
            return rc == 0 ? true : false;
        }

        /// <summary>
        /// 吋動 (軸參設定的五個速度值)
        /// </summary>
        /// <param name="speedIndex">軸參之吋動速度</param>
        /// <param name="dir">吋動方向</param>
        public bool Jog(JogSpeed jogSpeed, RotationDirection dir)
        {
            bool success = true;
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
                success = JogM(speed, dir);
            }
            return success;
        }

        public void JogConsiderSoftLimit(JogSpeed jogSpeed, RotationDirection dir)
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
                sbyte bDir;
                if (dir == RotationDirection.CW)
                {
                    bDir = 1;
                }
                else
                {
                    bDir = -1;
                }
                if (softLimitEnabled)
                {
                    if (dir == RotationDirection.CW)
                    {
                        AbsolueMove(softLimitP, speed);
                    }
                    else
                    {
                        AbsolueMove(softLimitN, speed);
                    }
                }
                else
                {
                    if (isAllowMove(dir))
                    {
                        CCMNet.CS_mnet_m204_tv_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(checkOverMaxSpeed(speed)) / 2 * bDir, MmToPulse(checkOverMaxSpeed(speed)) * bDir, axisPara.AccVelM);
                    }
                }
            }
        }

        /// <summary>
        /// 啟動原點復歸 (參數定義在軸參中)
        /// </summary>
        public virtual void Home()
        {
            if (axisPara.IsActive && axisPara.Enabled )
            {
                this.IsHome = false;
                l122M2X4HomeFlow.Restart();
            }
        }

        /// <summary>
        /// Shows the M2X4 Configuration Dialog.
        /// </summary>
        public DialogResult ConfigurationShowDialog()
        {
            L122M2X4Form l122M2X4Form = new L122M2X4Form(this);
            l122M2X4Form.Text = DeviceName;
            return l122M2X4Form.ShowDialog();
        }

        /// <summary>
        /// Shows the M2X4 Configuration.
        /// </summary>
        public void ConfigurationShow(FormClosingEventHandler action = null)
        {
            FormHelper.OpenUniqueForm(DeviceName, () =>
            {
                L122M2X4Form l122M2X4Form = new L122M2X4Form(this);
                l122M2X4Form.Text = DeviceName;
                l122M2X4Form.Name = DeviceName;
                l122M2X4Form.Show();
                if (action != null)
                {
                    l122M2X4Form.FormClosing += action;
                }
            });
        }

        /// <summary>
        /// Shows the M2X4 Slim Configuration.
        /// </summary>
        public void ConfigurationSlimShow(FormClosingEventHandler action = null)
        {
            FormHelper.OpenUniqueForm(DeviceName + "Slim", () =>
            {
                L122M2X4SlimForm l122M2X4SlimForm = new L122M2X4SlimForm(this);
                l122M2X4SlimForm.Text = DeviceName;
                l122M2X4SlimForm.Name = DeviceName + "Slim";
                l122M2X4SlimForm.Show();
                if (action != null)
                {
                    l122M2X4SlimForm.FormClosing += action;
                }
            });
        }

        public bool SetSoftLimit(I32 positiveLimit, I32 negativeLimit, CmdStatus sw, StopType stopType)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                if (sw == CmdStatus.ON)
                {
                    /*
                    0 INT only
                    1 Immediately stop
                    2 Slow down then stop
                    3 Reserved
                     */
                    if (stopType == StopType.Emergency)
                    {
                        rc = CCMNet.CS_mnet_m204_enable_soft_limit(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 1);
                    }
                    else
                    {
                        rc = CCMNet.CS_mnet_m204_enable_soft_limit(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 2);
                    }
                    rc = CCMNet.CS_mnet_m204_set_soft_limit(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, positiveLimit, negativeLimit);
                }
                else
                {
                    rc = CCMNet.CS_mnet_m204_disable_soft_limit(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo);
                }
            }
            return rc == 0 ? true : false;
        }

        public bool EnableSoftLimit(StopType stopType)
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                if (stopType == StopType.Emergency)
                {
                    rc = CCMNet.CS_mnet_m204_enable_soft_limit(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 1);
                }
                else
                {
                    rc = CCMNet.CS_mnet_m204_enable_soft_limit(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 2);
                }
            }
            return rc == 0 ? true : false;
        }

        public bool DisableSoftLimit()
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                rc = CCMNet.CS_mnet_m204_disable_soft_limit(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo);
            }
            return rc == 0 ? true : false;
        }

        //set command counter
        public void SetCommandCounter(double position)
        {
            CCMNet.CS_mnet_m204_set_command(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(position));
        }

        //set encode
        public void SetPositionCounter(double position)
        {
            CCMNet.CS_mnet_m204_set_position(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(position));
        }

        public void DoHomeOrg()
        {
            CCMNet.CS_mnet_m204_set_home_config(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 9, Convert.ToUInt16(axisPara.LogicORG), Convert.ToUInt16(axisPara.LogicZ), 0, 0);
            CCMNet.CS_mnet_m204_start_home_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(axisPara.CreepDevVelH), axisPara.CreepAccVelH, axisPara.CreepAccVelH);
        }

        public void DoHomeLim()
        {
            CCMNet.CS_mnet_m204_set_home_config(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 9, Convert.ToUInt16(axisPara.LogicORG), Convert.ToUInt16(axisPara.LogicZ), 0, 0);
            CCMNet.CS_mnet_m204_start_home_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(axisPara.CreepDevVelH), axisPara.CreepAccVelH, axisPara.CreepAccVelH);
        }

        public void DoHomeLimWithZPhase()
        {
            CCMNet.CS_mnet_m204_set_home_config(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 12, Convert.ToUInt16(axisPara.LogicORG), Convert.ToUInt16(axisPara.LogicZ), 0, 0);
            CCMNet.CS_mnet_m204_start_home_move(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, MmToPulse(axisPara.CreepDevVelH), axisPara.CreepAccVelH, axisPara.CreepAccVelH);
        }

        #endregion public method

        public void SetHomeAutoResetCounter()
        {
            CCMNet.CS_mnet_m204_set_home_auto_reset_counter(RingNoOfMNet, axisPara.SlaveIP, (U16)axisPara.AxisNo, 0);
            //0
            //NULL
            //1
            //RESET COMMAND Count
            //2
            //RESET POSITION Count
            //3
            //RESET ERROR Count
            //4
            //RESET GENENAL Count
        }
    }

    public class L122M2X4HomeFlow : FlowBase
    {
        private L122M2X4 _axis;

        public L122M2X4HomeFlow(L122M2X4 axis)
        {
            this.Name = axis.DeviceName + "HomeFlow";

            _axis = axis;
        }

        public override void Flow()
        {
            switch (this.Step1)
            {
                #region flow init

                case "0":
                    //關掉軟體極限，
                    _axis.DisableSoftLimit();
                    _axis.Stop(StopType.SlowDown, false);
                    _axis.ServoOn(CmdStatus.ON);
                    ChangeStep1("1");

                    this.TimeVar1 = 30000;
                    //if (HomeStarted != null)
                    //{
                    //    HomeStarted(DeviceName);
                    //}
                    break;

                case "1":
                    if (!_axis.IsBusy)
                    {
                        switch (_axis.AxisPara.HomeMode)
                        {
                            case HomeMode.OnePoint:
                                ChangeStep1("1000");
                                break;

                            case HomeMode.TwoPoint:
                                ChangeStep1("2000");
                                break;

                            case HomeMode.ThreePointWithZPhase:
                                ChangeStep1("2000");
                                break;

                            case HomeMode.ThreePoint:
                                ChangeStep1("3000");
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
                    ChangeStep1("1010");
                    break;

                case "1010":
                    if (_axis.IsORG)
                    {
                        _axis.Stop(StopType.Emergency, false);
                        this.Timer1.Restart();
                        ChangeStep1("1020");
                    }
                    break;

                case "1020":
                    if (this.Timer1.ElapsedMilliseconds > 100)
                    {
                        _axis.JogM(5, RotationDirection.CW);
                        this.Timer1.Restart();
                        ChangeStep1("1030");
                    }
                    break;

                case "1030":
                    if (this.Timer1.ElapsedMilliseconds > this.TimeVar1)
                    {
                        //S.Result.Value = -1030;
                        //S.Result.Message = LanguageResourceManager.GetString("TimeoutOutOfORG") + "(" + DeviceName + ")";
                        ChangeStep1("9000");
                    }
                    else
                    {
                        if (!_axis.IsORG)
                        {
                            _axis.Stop(StopType.Emergency, false);
                            _axis.DoHomeOrg();
                            this.Timer1.Restart();
                            ChangeStep1("7000");
                        }
                    }
                    break;

                #endregion OnePoint

                #region TwoPoint

                case "2000":
                    if (_axis.IsLimitN)
                    {
                        _axis.JogM(_axis.AxisPara.OPRDevVelH, RotationDirection.CW);
                        this.Timer1.Restart();
                        ChangeStep1("2100");
                    }
                    else if (_axis.IsLimitP)
                    {
                        _axis.JogM(_axis.AxisPara.OPRDevVelH, RotationDirection.CCW);
                        this.Timer1.Restart();
                        ChangeStep1("2100");
                    }
                    else
                    {
                        this.Timer1.Restart();
                        ChangeStep1("2100");
                    }
                    break;

                case "2100":
                    if (this.Timer1.ElapsedMilliseconds > this.TimeVar1)
                    {
                        //S.Result.Value = -2100;
                        //S.Result.Message = LanguageResourceManager.GetString("TimeoutOutOfLimitN") + "(" + _axis.DeviceName + ")";
                        ChangeStep1("9000");
                    }
                    else
                    {
                        if (!_axis.IsLimitN && !_axis.IsLimitP)
                        {
                            _axis.Stop(StopType.Emergency, false);
                            this.Timer1.Restart();
                            ChangeStep1("2010");
                        }
                    }
                    break;

                case "2010":
                    if (this.Timer1.ElapsedMilliseconds > this.TimeVar1)
                    {
                        //S.Result.Value = -2010;
                        //S.Result.Message = LanguageResourceManager.GetString("TimeoutOutOfLimitN") + "(" + DeviceName + ")";
                        ChangeStep1("9000");
                    }
                    else
                    {
                        if (!_axis.IsLimitN)
                        {
                            _axis.JogM(_axis.AxisPara.OPRDevVelH, RotationDirection.CCW);
                        }
                        ChangeStep1("2020");
                    }
                    break;

                case "2020":
                    if (_axis.IsLimitN)
                    {
                        _axis.Stop(StopType.Emergency, false);
                        this.Timer1.Restart();
                        ChangeStep1("2030");
                    }
                    break;

                case "2030":
                    if (this.Timer1.ElapsedMilliseconds > 100)
                    {
                        if (_axis.AxisPara.HomeMode == HomeMode.ThreePointWithZPhase)
                        {
                            _axis.DoHomeLimWithZPhase();
                        }
                        else
                        {
                            _axis.DoHomeLim();
                        }
                        ChangeStep1("7000");
                    }
                    break;

                case "2040":
                    if (_axis.IsLimitN)
                    {
                        _axis.Stop(StopType.Emergency, false);
                        ChangeStep1("7000");
                    }
                    break;

                #endregion TwoPoint

                #region ThreePoint

                case "3000":
                    //if (!_axis.ORG && !_axis.LimitN)
                    if (!_axis.IsLimitN)
                    {
                        _axis.JogM(_axis.AxisPara.OPRDevVelH, RotationDirection.CCW);
                    }
                    ChangeStep1("3010");
                    break;

                case "3010":
                    //if (_axis.ORG || _axis.LimitN)
                    if (_axis.IsLimitN)
                    {
                        _axis.Stop(StopType.SlowDown, false);
                        this.Timer1.Restart();
                        ChangeStep1("3020");
                    }
                    break;

                case "3020":
                    if (this.Timer1.ElapsedMilliseconds > 100)
                    {
                        _axis.JogM(_axis.AxisPara.CreepDevVelH, RotationDirection.CW);
                        ChangeStep1("3030");
                    }
                    break;

                case "3030":
                    if (this.Timer1.ElapsedMilliseconds > this.TimeVar1)
                    {
                        //S.Result.Value = -3030;
                        //S.Result.Message = LanguageResourceManager.GetString("TimeoutEnterORG") + "(" + DeviceName + ")";
                        ChangeStep1("9000");
                    }
                    else
                    {
                        if (_axis.IsORG)
                        {
                            ChangeStep1("3040");
                        }
                    }
                    break;

                case "3040":
                    if (!_axis.IsORG)
                    {
                        _axis.Stop(StopType.SlowDown, false);
                        this.Timer1.Restart();
                        ChangeStep1("3050");
                    }
                    break;

                case "3050":
                    if (this.Timer1.ElapsedMilliseconds > 100)
                    {
                        _axis.DoHomeOrg();
                        ChangeStep1("7000");
                    }
                    break;

                #endregion ThreePoint

                #region flow continue

                case "7000":
                    if (this.Timer1.ElapsedMilliseconds > 500 && !_axis.IsBusy)
                    {
                        ChangeStep1("7010");
                    }
                    break;

                case "7010":
                    _axis.ResetPos();
                    _axis.AbsolueMove(_axis.AxisPara.OffsetH, SpeedMode.Manual);
                    ChangeStep1("7020");
                    break;

                case "7020":
                    if (_axis.IsReached)
                    {
                        this.Timer1.Restart();
                        ChangeStep1("7030");
                    }
                    break;

                case "7030":
                    if (this.Timer1.ElapsedMilliseconds >= _axis.AxisPara.ClearDelay * 1000)
                    {
                        _axis.ResetPos();
                        ChangeStep1("8000");
                    }
                    break;

                #endregion flow continue

                #region flow end

                //flow finish
                case "8000":
                    //if (HomeCompleted != null)
                    //{
                    //    HomeCompleted(DeviceName);
                    //}
                    _axis.IsHome = true;
                    Stop();
                    break;

                //flow trip
                case "9000":
                    //if (HomeFailure != null)
                    //{
                    //    HomeFailure(DeviceName);
                    //}
                    _axis.IsHome = false;
                    Fail();

                    break;

                #endregion flow end
            }
        }
    }
}