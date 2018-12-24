using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L122.Library;
using AOISystem.Utilities.Modules.Syntek.L122.MasterCard;
using AOISystem.Utilities.Threading;
using System;
using System.Threading;
using I16 = System.Int16;
using I32 = System.Int32;
using U16 = System.UInt16;
using U32 = System.UInt32;
using U8 = System.Byte;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.Motion
{
    //L122 如果連續改變速度值 可能因為程式寫入速度太快導致數值未正確寫入
    public class L122M1X1 : L122.MasterCard.L122
    {
        #region struct

        internal struct M1X1Status
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

        private L122M1X1Form dialogOfM1X1;
        private Thread systemScanThread;
        private bool keyOfIOStatus;
        private int initErr;
        private int nPos;
        private ActionTask homeTask;
        private ActionItem itemHome;
        private ParameterL122M1X1 axisPara;
        private M1X1Status status;
        private U16 busy;
        private bool softLimitEnabled;
        private double softLimitN;
        private double softLimitP;
        private double maxSpeed;

        #endregion field

        #region properties

        /// <summary> M1X1模組軸參 </summary>
        internal ParameterL122M1X1 AxisPara { get { return axisPara; } set { axisPara = value; } }

        /// <summary> M1X1模組狀態點資訊 </summary>
        internal M1X1Status Status { get { return status; } }

        /// <summary> 取得目前設定位置 (Command Counter) </summary>
        public double Position { /*set { setPos(value); } */get { return getPos(); } }

        ///<summary> 取得編碼器位置 (Position Counter) </summary>
        public double Encoder { /*set { setENC(value); } */get { return getENC(); } }

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
                if (CCMNet.CS_mnet_m1_motion_done(RingNoOfMNet , axisPara.SlaveIP , ref busyData) == 0)
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

        /// <summary> M1X1模組是否到位 </summary>
        public bool IsReached
        {
            get
            {
                int cmdPos = 0;
                CCMNet.CS_mnet_m1_get_command(RingNoOfMNet , axisPara.SlaveIP , ref cmdPos);
                return (!IsBusy) && (nPos == cmdPos);
            }
        }

        public bool IsSoftLimitN { get { return Encoder <= softLimitN; } }

        public bool IsSoftLimitP { get { return Encoder >= softLimitP; } }

        #endregion properties

        #region construct

        public L122M1X1(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            busy = new U16();

            axisPara = Parameter as ParameterL122M1X1;

            slaveModuleInitialize();

            setHardwareScan();

            taskInitialize();

            setMotion();

            axisPara.ParameterChanged += new ParameterINI.ParameterChangedHandler((paraName) => setMotion());

            softLimitEnabled = axisPara.SoftLimitEnabled;
            softLimitN = axisPara.SoftLimitN;
            softLimitP = axisPara.SoftLimitP;
            maxSpeed = 1000;
            ServoOn(CmdStatus.ON);
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
            uint[] deviceTable = new uint[2];
            CCMNet.CS_mnet_reset_ring(RingNoOfMNet);
            I16 retOfGetTable = CCMNet.CS_mnet_get_ring_active_table(RingNoOfMNet , deviceTable);
            if (retOfGetTable != 0)
            {
                throw new Exception("Error occur when get device table !!! \n func = [_mnet_get_ring_active_table]");
            }
            if ((deviceTable[0] == 0) && (deviceTable[1] == 0))
            {
                throw new Exception("Can't find slave M1X1, there is not any device !!! \n func = [_mnet_get_ring_active_table, deviceTable=0]");
            }

            for (int i = 0 ; i < 64 ; i++)
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
                            throw new Exception("Have not found M1X1 !!!\n" + "SlaveIP = " + axisPara.SlaveIP);
                        }
                    }
                    else
                    {
                        if (i == 32)
                            mask = 0x00000001;
                        if ((deviceTable[1] & mask) == 0)
                        {
                            throw new Exception("Have not found M1X1 !!!\n" + "SlaveIP = " + axisPara.SlaveIP);
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
            I16 retOfGetSlaveType = CCMNet.CS_mnet_get_slave_type(RingNoOfMNet , axisPara.SlaveIP , ref slaveType);
            if (retOfGetSlaveType == 0)
            {
                if (slaveType != 0xA3)
                {
                    throw new Exception("deviec type is not M1X1!!!\n" + "SlaveIP = " + axisPara.SlaveIP);
                }
            }
            else
            {
                throw new Exception("Error occur when get device type !!! \n func = [_mnet_get_slave_type]");
            }

            I16 retOfInitial = CCMNet.CS_mnet_m1_initial(RingNoOfMNet , axisPara.SlaveIP);
            if (retOfInitial != 0)
            {
                throw new Exception("Error occur when M1X1 module initial !!! \n func = [_mnet_m1_initial]");
            }
        }

        //設定掃描M1X1的Thread
        private void setHardwareScan()
        {
            systemScanThread = new Thread(systemScan);
            systemScanThread.IsBackground = true;
            systemScanThread.Start();
            keyOfIOStatus = true;
        }

        //流程器初始化
        private void taskInitialize()
        {
            homeTask = new ActionTask();
            itemHome = new ActionItem("Home", false, null, flowHome);
            homeTask.Add(itemHome);
            homeTask.Run(TaskRunType.Cycle , 15);
        }

        //scan m1x1 slave IO status
        private void systemScan()
        {
            while (true)
            {
                if (keyOfIOStatus)
                {
                    U32 status = 0;
                    I16 rt = CCMNet.CS_mnet_m1_get_io_status(RingNoOfMNet , axisPara.SlaveIP , ref status);
                    this.status.RDY = BitConverterEx.TestB(status , 0);
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
                        this.status.Home = false;

                    Thread.Sleep(50);
                }
            }
        }

        //將軸參寫入m1x1 slave
        private void setMotion()
        {
            U16 hMode;
            //初始化給1 且限制在 1~3，這邊又把home mode變成12 or 9
            if (axisPara.HomeMode == HomeMode.TwoPoint)
                hMode = 12;
            else
                hMode = 9;

            initErr = CCMNet.CS_mnet_m1_set_alm(RingNoOfMNet , axisPara.SlaveIP , Convert.ToUInt16(axisPara.LogicALM) , 0);
            initErr = CCMNet.CS_mnet_m1_set_inp(RingNoOfMNet , axisPara.SlaveIP , 0 , Convert.ToUInt16(axisPara.LogicINP));
            initErr = CCMNet.CS_mnet_m1_set_erc_on(RingNoOfMNet , axisPara.SlaveIP , 0);
            initErr = CCMNet.CS_mnet_m1_set_erc(RingNoOfMNet , axisPara.SlaveIP , Convert.ToUInt16(axisPara.LogicERC) , 0 , 0);
            initErr = CCMNet.CS_mnet_m1_set_home_config(RingNoOfMNet , axisPara.SlaveIP , hMode , Convert.ToUInt16(axisPara.LogicORG) , Convert.ToUInt16(axisPara.LogicZ) , 0 , 0);
            initErr = CCMNet.CS_mnet_m1_set_sd(RingNoOfMNet , axisPara.SlaveIP , 0 , Convert.ToInt16(axisPara.LogicSD) , 0 , 0);
            initErr = CCMNet.CS_mnet_m1_set_ltc_logic(RingNoOfMNet , axisPara.SlaveIP , Convert.ToUInt16(axisPara.LogicLTC));
            initErr = CCMNet.CS_mnet_m1_set_feedback_src(RingNoOfMNet , axisPara.SlaveIP , 3);
            initErr = CCMNet.CS_mnet_m1_set_pls_outmode(RingNoOfMNet , axisPara.SlaveIP , (U16)axisPara.PulseMode);
            initErr = CCMNet.CS_mnet_m1_set_pls_iptmode(RingNoOfMNet , axisPara.SlaveIP , (U16)axisPara.EncMode , (U16)axisPara.EncDir);
            initErr = CCMNet.CS_mnet_m1_fix_speed_range(RingNoOfMNet , axisPara.SlaveIP , MmToPulse(axisPara.MaxVel));
        }

        private void flowHome(TProcVar S)
        {
            switch (S.Step1)
            {
                #region flow init

                case 0:
                    //關掉軟體極限，
                    DisableSoftLimit();
                    Stop(StopType.SlowDown , false);
                    ServoOn(CmdStatus.ON);
                    status.Home = false;
                    S.Step1 = 1;
                    S.Timeout = 30000;
                    if (HomeStarted != null)
                    {
                        HomeStarted(DeviceName);
                    }
                    break;

                case 1:
                    if (!IsBusy)
                    {
                        switch (axisPara.HomeMode)
                        {
                            case HomeMode.OnePoint:
                                S.Step1 = 1000;
                                break;

                            case HomeMode.TwoPoint:
                                S.Step1 = 2000;
                                break;

                            case HomeMode.ThreePointWithZPhase:
                                S.Step1 = 2000;
                                break;

                            case HomeMode.ThreePoint:
                                S.Step1 = 3000;
                                break;
                        }
                    }
                    break;

                #endregion flow init

                #region OnePoint

                case 1000:
                    //不在原點上
                    if (!status.ORG)
                    {
                        JogM(axisPara.OPRDevVelH , RotationDirection.CCW);
                    }
                    S.Step1 = 1010;
                    break;

                case 1010:
                    if (status.ORG)
                    {
                        Stop(StopType.Emergency , false);
                        S.TM1.Restart();
                        S.Step1 = 1020;
                    }
                    break;

                case 1020:
                    if (S.TM1.ElapsedMilliseconds > 100)
                    {
                        JogM(5 , RotationDirection.CW);
                        S.TM1.Restart();
                        S.Step1 = 1030;
                    }
                    break;

                case 1030:
                    if (S.TM1.ElapsedMilliseconds > S.Timeout)
                    {
                        S.Result.Value = -1030;
                        S.Result.Message = LanguageResourceManager.GetString("TimeoutOutOfORG") + "(" + DeviceName + ")";
                        S.Step1 = 9000;
                    }
                    else
                    {
                        if (!status.ORG)
                        {
                            Stop(StopType.Emergency , false);
                            doHomeOrg();
                            S.TM1.Restart();
                            S.Step1 = 7000;
                        }
                    }
                    break;

                #endregion OnePoint

                #region TwoPoint

                case 2000:
                    if (status.LimitN)
                    {
                        JogM(axisPara.OPRDevVelH , RotationDirection.CW);
                        S.TM1.Restart();
                        S.Step1 = 2100;
                    }
                    else if (status.LimitP)
                    {
                        JogM(axisPara.OPRDevVelH , RotationDirection.CCW);
                        S.TM1.Restart();
                        S.Step1 = 2100;
                    }
                    else
                    {
                        S.TM1.Restart();
                        S.Step1 = 2100;
                    }
                    break;

                case 2100:
                    if (S.TM1.ElapsedMilliseconds > S.Timeout)
                    {
                        S.Result.Value = -2100;
                        S.Result.Message = LanguageResourceManager.GetString("TimeoutOutOfLimitN") + "(" + DeviceName + ")";
                        S.Step1 = 9000;
                    }
                    else
                    {
                        if (!status.LimitN && !status.LimitP)
                        {
                            Stop(StopType.Emergency , false);
                            S.TM1.Restart();
                            S.Step1 = 2010;
                        }
                    }
                    break;

                case 2010:
                    if (S.TM1.ElapsedMilliseconds > S.Timeout)
                    {
                        S.Result.Value = -2010;
                        S.Result.Message = LanguageResourceManager.GetString("TimeoutOutOfLimitN") + "(" + DeviceName + ")";
                        S.Step1 = 9000;
                    }
                    else
                    {
                        if (!status.LimitN)
                        {
                            JogM(axisPara.OPRDevVelH , RotationDirection.CCW);
                        }
                        S.Step1 = 2020;
                    }
                    break;

                case 2020:
                    if (status.LimitN)
                    {
                        Stop(StopType.Emergency , false);
                        S.TM1.Restart();
                        S.Step1 = 2030;
                    }
                    break;

                case 2030:
                    if (S.TM1.ElapsedMilliseconds > 100)
                    {
                        if (axisPara.HomeMode == HomeMode.ThreePointWithZPhase)
                        {
                            doHomeLimWithZPhase();
                        }
                        else
                        {
                            doHomeLim();
                        }
                        S.Step1 = 7000;
                    }
                    break;

                case 2040:
                    if (status.LimitN)
                    {
                        Stop(StopType.Emergency , false);
                        S.Step1 = 7000;
                    }
                    break;

                #endregion TwoPoint

                #region ThreePoint

                case 3000:
                    if (!status.ORG && !status.LimitN)
                    {
                        JogM(axisPara.OPRDevVelH , RotationDirection.CCW);
                    }
                    S.Step1 = 3010;
                    break;

                case 3010:
                    if (status.ORG || status.LimitN)
                    {
                        Stop(StopType.Emergency , false);
                        S.TM1.Restart();
                        S.Step1 = 3020;
                    }
                    break;

                case 3020:
                    if (S.TM1.ElapsedMilliseconds > 100)
                    {
                        JogM(axisPara.CreepDevVelH , RotationDirection.CW);
                        S.Step1 = 3030;
                    }
                    break;

                case 3030:
                    if (S.TM1.ElapsedMilliseconds > S.Timeout)
                    {
                        S.Result.Value = -3030;
                        S.Result.Message = LanguageResourceManager.GetString("TimeoutEnterORG") + "(" + DeviceName + ")";
                        S.Step1 = 9000;
                    }
                    else
                    {
                        if (status.ORG)
                        {
                            S.Step1 = 3040;
                        }
                    }
                    break;

                case 3040:
                    if (!status.ORG)
                    {
                        Stop(StopType.Emergency , false);
                        S.TM1.Restart();
                        S.Step1 = 3050;
                    }
                    break;

                case 3050:
                    if (S.TM1.ElapsedMilliseconds > 100)
                    {
                        doHomeOrg();
                        S.Step1 = 7000;
                    }
                    break;

                #endregion ThreePoint

                #region flow continue

                case 7000:
                    if (S.TM1.ElapsedMilliseconds > 500 && !IsBusy)
                    {
                        S.Step1 = 7010;
                    }
                    break;

                case 7010:
                    ResetPos();
                    Move(axisPara.OffsetH , SpeedMode.Manual);
                    S.Step1 = 7020;
                    break;

                case 7020:
                    if (IsReached)
                    {
                        S.TM1.Restart();
                        S.Step1 = 7030;
                    }
                    break;

                case 7030:
                    if (S.TM1.ElapsedMilliseconds >= axisPara.ClearDelay * 1000)
                    {
                        ResetPos();
                        S.Step1 = 8000;
                    }
                    break;

                #endregion flow continue

                #region flow end

                //flow finish
                case 8000:
                    if (HomeCompleted != null)
                    {
                        HomeCompleted(DeviceName);
                    }
                    status.Home = true;
                    homeTask.TurnOff(itemHome);
                    break;

                //flow trip
                case 9000:
                    if (HomeFailure != null)
                    {
                        HomeFailure(DeviceName);
                    }
                    status.Home = false;
                    homeTask.TurnOff(itemHome);

                    break;

                #endregion flow end
            }
        }

        private void doHomeOrg()
        {
            CCMNet.CS_mnet_m1_set_home_config(RingNoOfMNet , axisPara.SlaveIP , 9 , Convert.ToUInt16(axisPara.LogicORG) , Convert.ToUInt16(axisPara.LogicZ) , 0 , 0);
            CCMNet.CS_mnet_m1_set_tmove_speed(RingNoOfMNet , axisPara.SlaveIP , 0 , MmToPulse(axisPara.CreepDevVelH) , axisPara.CreepAccVelH , axisPara.CreepDecVelH);
            CCMNet.CS_mnet_m1_start_home_move(RingNoOfMNet , axisPara.SlaveIP , 0);
        }

        private void doHomeLim()
        {
            CCMNet.CS_mnet_m1_set_home_config(RingNoOfMNet , axisPara.SlaveIP , 6 , Convert.ToUInt16(axisPara.LogicORG) , Convert.ToUInt16(axisPara.LogicZ) , 0 , 0);
            CCMNet.CS_mnet_m1_set_tmove_speed(RingNoOfMNet , axisPara.SlaveIP , 0 , MmToPulse(axisPara.CreepDevVelH) , axisPara.CreepAccVelH , axisPara.CreepDecVelH);
            CCMNet.CS_mnet_m1_start_home_move(RingNoOfMNet , axisPara.SlaveIP , 0);
        }

        private void doHomeLimWithZPhase()
        {
            CCMNet.CS_mnet_m1_set_home_config(RingNoOfMNet , axisPara.SlaveIP , 12 , Convert.ToUInt16(axisPara.LogicORG) , Convert.ToUInt16(axisPara.LogicZ) , 0 , 0);
            CCMNet.CS_mnet_m1_set_tmove_speed(RingNoOfMNet , axisPara.SlaveIP , 0 , MmToPulse(axisPara.CreepDevVelH) , axisPara.CreepAccVelH , axisPara.CreepDecVelH);
            CCMNet.CS_mnet_m1_start_home_move(RingNoOfMNet , axisPara.SlaveIP , 0);
        }

        //get command counter
        private double getPos()
        {
            int cmdPos = new int();
            double CmdPosx;
            CCMNet.CS_mnet_m1_get_command(RingNoOfMNet , axisPara.SlaveIP , ref cmdPos);
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

        //set command counter
        private void setPos(double s)
        {
            int cmdPos = new int();
            cmdPos = Convert.ToInt32(Math.Round(s / axisPara.DistPerRole * axisPara.PulsePerRole));
            CCMNet.CS_mnet_m1_set_command(RingNoOfMNet , axisPara.SlaveIP , cmdPos);
        }

        private double getENC()
        {
            int feedBackPos = new I32();
            double d_feedBackPos;
            CCMNet.CS_mnet_m1_get_position(RingNoOfMNet , axisPara.SlaveIP , ref feedBackPos);
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

        //set encode
        private void setENC(int s)
        {
            CCMNet.CS_mnet_m1_set_position(RingNoOfMNet , axisPara.SlaveIP , s);
        }

        //get encode，取得feedback的位置(光學尺)
        //private double getENC()
        //{
        //    int feedBackPos = new I32();
        //    double CmdPosx;
        //    //if (axisPara.IsActive && axisPara.Enabled )
        //    //{
        //    //    CCMNet.CS_mnet_m1_get_position((U16)axisPara.CardSwitchNo, (U16)axisPara.RingNoOfCard, axisPara.SlaveIP, ref feedBackPos);
        //    //    return feedBackPos;
        //    //}
        //    //else
        //    //{
        //    //    return 0;
        //    //}
        //    CCMNet.CS_mnet_m1_get_position((U16)axisPara.CardSwitchNo, (U16)axisPara.RingNoOfCard, axisPara.SlaveIP, ref feedBackPos);
        //    CmdPosx = feedBackPos;
        //    if (axisPara.PulsePerRole != 0)
        //    {
        //        return CmdPosx / axisPara.PulsePerRole * axisPara.DistPerRole;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //set encode
        //private void setENC(double s)
        //{
        //    //if (axisPara.IsActive && axisPara.Enabled )
        //    //{
        //    //    CCMNet.CS_mnet_m1_set_position((U16)axisPara.CardSwitchNo, (U16)axisPara.RingNoOfCard, axisPara.SlaveIP, s);
        //    //}
        //    int pos = 0;
        //    if (axisPara.PulsePerRole != 0)
        //    {
        //        pos = (int)Math.Round(s * axisPara.PulsePerRole / axisPara.DistPerRole);
        //    }
        //    CCMNet.CS_mnet_m1_set_position((U16)axisPara.CardSwitchNo, (U16)axisPara.RingNoOfCard, axisPara.SlaveIP, pos);
        //}

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
            CCMNet.CS_mnet_m1_reset_position(RingNoOfMNet , axisPara.SlaveIP);
            CCMNet.CS_mnet_m1_reset_command(RingNoOfMNet , axisPara.SlaveIP);
        }

        /// <summary>
        /// M1X1 裝置啟停
        /// </summary>
        /// <param name="option">The option.</param>
        public void ServoOn(CmdStatus option)
        {
            if (axisPara != null)
            {
                if (axisPara.IsActive)
                {
                    if (option == CmdStatus.OFF)
                    {
                        status.Home = false;
                        axisPara.Enabled = false;
                        CCMNet.CS_mnet_m1_set_svon(RingNoOfMNet , axisPara.SlaveIP , 0);
                    }
                    if (option == CmdStatus.ON)
                    {
                        if (axisPara.IsActive)
                        {
                            axisPara.Enabled = true;
                            CCMNet.CS_mnet_m1_disable_soft_limit(RingNoOfMNet , axisPara.SlaveIP);
                            CCMNet.CS_mnet_m1_set_svon(RingNoOfMNet , axisPara.SlaveIP , 1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 清除警報
        /// </summary>
        /// <param name="status">OFF=Don't Output  ON=Output</param>
        public void ResetAlarm(CmdStatus status)
        {
            if (axisPara.IsActive && axisPara.Enabled )
            {
                CCMNet.CS_mnet_m1_set_ralm(RingNoOfMNet , axisPara.SlaveIP , (U16)status);
            }
        }

        /// <summary>
        /// 絕對移動 (需選擇使用軸參的手動速度或是自動速度)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="inAuto">手動或自動速度</param>
        public void Move(double dest , SpeedMode inAuto)
        {
            if (axisPara.IsActive && axisPara.Enabled )
            {
                if (inAuto == SpeedMode.Manual)
                {
                    CCMNet.CS_mnet_m1_set_tmove_speed(RingNoOfMNet , axisPara.SlaveIP , MmToPulse(axisPara.StrVelM) , MmToPulse(axisPara.DevVelM) , axisPara.AccVelM , axisPara.DecVelM);
                }
                if (inAuto == SpeedMode.Auto)
                {
                    CCMNet.CS_mnet_m1_set_tmove_speed(RingNoOfMNet , axisPara.SlaveIP , MmToPulse(axisPara.StrVelA) , MmToPulse(axisPara.DevVelA) , axisPara.AccVelA , axisPara.DecVelA);
                }
                nPos = MmToPulse(dest);
                CCMNet.CS_mnet_m1_start_a_move(RingNoOfMNet , axisPara.SlaveIP , nPos);
            }
        }

        /// <summary>
        /// 絕對移動 (需要自定義速度，初速為恆速1/2，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="speed"> 速度值 </param>
        public void MoveM(double dest , double speed)
        {
            if (axisPara.IsActive && axisPara.Enabled )
            {
                CCMNet.CS_mnet_m1_set_tmove_speed(RingNoOfMNet , axisPara.SlaveIP , MmToPulse(speed) / 2 , MmToPulse(speed) , axisPara.AccVelM , axisPara.DecVelM);
                nPos = MmToPulse(dest);
                CCMNet.CS_mnet_m1_start_a_move(RingNoOfMNet , axisPara.SlaveIP , nPos);
            }
        }

        /// <summary>
        /// 相對移動 (需選擇使用軸參的手動速度或是自動速度)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="inAuto">手動或自動速度</param>
        public void Offset(double dest , SpeedMode inAuto)
        {
            Move(getPos() + dest , inAuto);
        }

        /// <summary>
        /// 相對移動 (自定義速度，初速為恆速1/2，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="dest">目標位置 (millimeter).</param>
        /// <param name="inAuto">速度值</param>
        public void OffsetM(double dest , double speed)
        {
            MoveM(getPos() + dest , speed);
        }

        ///// <summary>
        ///// 停止M1X1裝置 (復歸流程中使用，不停止homeTask)
        ///// </summary>
        //public void Stop(StopType type)
        //{
        //    if (type == StopType.Emergency)
        //    {
        //        CCMNet.CS_mnet_m1_emg_stop((U16)axisPara.CardSwitchNo, (U16)axisPara.RingNoOfCard, axisPara.SlaveIP);
        //    }
        //    if (type == StopType.SlowDown)
        //    {
        //        CCMNet.CS_mnet_m1_sd_stop((U16)axisPara.CardSwitchNo, (U16)axisPara.RingNoOfCard, axisPara.SlaveIP);
        //    }

        //}

        /// <summary>
        /// 停止M1X1裝置 (復歸流程中不停止homeTask)
        /// </summary>
        public void Stop(StopType type , bool isStopTask)
        {
            if (axisPara.IsActive && axisPara.Enabled)
            {
                if (type == StopType.Emergency)
                {
                    CCMNet.CS_mnet_m1_emg_stop(RingNoOfMNet , axisPara.SlaveIP);
                }
                if (type == StopType.SlowDown)
                {
                    CCMNet.CS_mnet_m1_sd_stop(RingNoOfMNet , axisPara.SlaveIP);
                }
                if (isStopTask)
                {
                    itemHome.TProcVar.Step1 = 9000;
                    homeTask.TurnOffAll();
                }
            }
        }

        /// <summary>
        /// 吋動 (自定義速度，初速為恆速1/2，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="speed">速度值 (millimeter).</param>
        /// <param name="dir">吋動方向</param>
        public void JogM(double speed , RotationDirection dir)
        {
            if (axisPara.IsActive && axisPara.Enabled )
            {
                int i = MmToPulse(speed);
                if (!this.IsBusy)
                {
                    CCMNet.CS_mnet_m1_set_tmove_speed(RingNoOfMNet , axisPara.SlaveIP , i / 2 , i , axisPara.AccVelM , axisPara.DecVelM);
                }
                CCMNet.CS_mnet_m1_v_change(RingNoOfMNet , axisPara.SlaveIP , (double)i , 0.1);
                if (!this.IsBusy)
                {
                    CCMNet.CS_mnet_m1_v_move(RingNoOfMNet , axisPara.SlaveIP , Convert.ToByte(dir));
                }
            }
        }

        /// <summary>
        /// 吋動 (軸參設定的五個速度值)
        /// </summary>
        /// <param name="speedIndex">軸參之吋動速度</param>
        /// <param name="dir">吋動方向</param>
        public void Jog(JogSpeed jogSpeed , RotationDirection dir)
        {
            if (axisPara.IsActive && axisPara.Enabled )
            {
                double speed = 0;
                int i = 0;
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
                i = MmToPulse(speed);
                CCMNet.CS_mnet_m1_set_tmove_speed(RingNoOfMNet , axisPara.SlaveIP , i / 2 , i , axisPara.AccVelM , axisPara.DecVelM);
                CCMNet.CS_mnet_m1_v_move(RingNoOfMNet , axisPara.SlaveIP , Convert.ToByte(dir));
            }
        }

        public void JogConsiderSoftLimit(JogSpeed jogSpeed , RotationDirection dir)
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
                if (softLimitEnabled)
                {
                    if (dir == RotationDirection.CW)
                    {
                        MoveM(softLimitP , speed);
                    }
                    else
                    {
                        MoveM(softLimitN , speed);
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
                        CCMNet.CS_mnet_m1_set_tmove_speed(RingNoOfMNet , axisPara.SlaveIP , MmToPulse(checkOverMaxSpeed(speed)) / 2 , MmToPulse(checkOverMaxSpeed(speed)) , axisPara.AccVelM , axisPara.DecVelM);
                        CCMNet.CS_mnet_m1_v_move(RingNoOfMNet , axisPara.SlaveIP , Convert.ToByte(dir));
                    }
                }
            }
        }

        /// <summary>
        /// 啟動原點復歸 (參數定義在軸參中)
        /// </summary>
        public void Home()
        {
            if (axisPara.IsActive && axisPara.Enabled )
            {
                homeTask.TurnOn(itemHome);
            }
        }

        /// <summary>
        /// Shows the M1X1 dialog.
        /// </summary>
        public void ShowDialog(Language language)
        {
            dialogOfM1X1 = new L122M1X1Form(this , language);
            dialogOfM1X1.Text = DeviceName;
            dialogOfM1X1.ShowDialog();
        }

        public bool SetSoftLimit(I32 positiveLimit , I32 negativeLimit , CmdStatus sw , StopType stopType)
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
                        rc = CCMNet.CS_mnet_m1_enable_soft_limit(RingNoOfMNet , axisPara.SlaveIP , 1);
                    }
                    else
                    {
                        rc = CCMNet.CS_mnet_m1_enable_soft_limit(RingNoOfMNet , axisPara.SlaveIP , 2);
                    }
                    rc = CCMNet.CS_mnet_m1_set_soft_limit(RingNoOfMNet , axisPara.SlaveIP , positiveLimit , negativeLimit);
                }
                else
                {
                    rc = CCMNet.CS_mnet_m1_disable_soft_limit(RingNoOfMNet , axisPara.SlaveIP);
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
                    rc = CCMNet.CS_mnet_m1_enable_soft_limit(RingNoOfMNet , axisPara.SlaveIP , 1);
                }
                else
                {
                    rc = CCMNet.CS_mnet_m1_enable_soft_limit(RingNoOfMNet , axisPara.SlaveIP , 2);
                }
            }
            return rc == 0 ? true : false;
        }

        public bool DisableSoftLimit()
        {
            I16 rc = -1;
            if (axisPara.IsActive && axisPara.Enabled )
            {
                rc = CCMNet.CS_mnet_m1_disable_soft_limit(RingNoOfMNet , axisPara.SlaveIP);
            }
            return rc == 0 ? true : false;
        }

        #endregion public method
    }
}