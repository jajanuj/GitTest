using AOISystem.Utilities.Common;
using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Logging;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.Library;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.Motion
{
    public class CEtherCATMotion : CEtherCAT
    {
        #region struct

        public struct MotionStatus
        {
            public bool ReadySwitch;
            public bool SwitchOn;
            public bool OperationEnable;
            public bool Fault;
            public bool Voltage;
            public bool QuickStop;
            public bool SwitchOff;
            public bool Warning;
            public bool ManufactureSpecific;
            public bool Remote;
            public bool TargetReached;
            public bool IntLimitActive;
            public bool HomingAttained;
            public bool FollowingError;
            public bool PEL;
            public bool MEL;
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

        private bool keyOfIOStatus;
        private int nPos;
        private MotionHomeFlow motionHomeFlow;
        private ParameterCEtherCATMotion axisPara;
        private MotionStatus status;
        private MotionModel motionModel;

        #endregion field

        #region properties

        /// <summary> 模組軸參 </summary>
        public ParameterCEtherCATMotion AxisPara { get { return axisPara; } set { axisPara = value; } }

        /// <summary> 模組狀態點資訊 </summary>
        public MotionStatus Status { get { return status; } }

        /// <summary> 取得目前設定位置 (Command Counter) </summary>
        public double Position { /*set { setPos(value); } */get { return getPos(); } }
        public double Position2 { /*set { setPos(value); } */get { return getPos2(); } }

        ///<summary> 取得編碼器位置 (Position Counter) </summary>
        public double Encoder { /*set { setENC(value); } */get { return getENC(); } }
        public double Encoder2 { /*set { setENC(value); } */get { return getENC2(); } }

        ///<summary> 取得目前速度 (Current Speed) </summary>
        public double Speed { /*set { setENC(value); } */get { return getSpeed(); } }
        public double Speed2 { /*set { setENC(value); } */get { return getSpeed2(); } }

        ///<summary> 取得目前MotionDone狀態 </summary>
        public ushort MotionDone { get { return getMotionDone(); } }

        ///<summary> 取得錯誤命令次數 (Error Count) </summary>
        public double ErrorCount { get { return getErrorCount(); } }
        public double ErrorCount2 { get { return getErrorCount2(); } }

        /// <summary> 取得原點復歸的旗標 </summary>
        public virtual bool IsHomingFlow { get { return motionHomeFlow.IsRunning; } }

        /// <summary> 取得原點復歸的旗標 </summary>
        public virtual bool IsHome { get; set; }//{ get { return status.Home; } set { status.Home = value; } }

        /// <summary> 取得原點Sensor訊號 </summary>
        public virtual bool IsORG { get { return status.HomingAttained; } }

        /// <summary> 取得極限訊號 </summary>
        public virtual bool IsLimit { get { return status.IntLimitActive; } }

        /// <summary> 取得正極限訊號 </summary>
        public virtual bool IsLimitP
        {
            get { return status.PEL; }
            set
            {
                if (value != status.PEL)
                {
                    status.PEL = value;
                    if (value && this.IsHome && !this.IsReached)
                    {
                        OnErrorRaised(-1, string.Format("已觸發正極限訊號但未到達位置 : {0}, 目前馬達位置 : {1}", PulseToMm(nPos), getENC()));
                    }
                }
            }
        }

        /// <summary> 取得負極限訊號 </summary>
        public virtual bool IsLimitN
        {
            get { return status.MEL; }
            set
            {
                if (value != status.MEL)
                {
                    status.MEL = value;
                    if (value && this.IsHome && !this.IsReached)
                    {
                        OnErrorRaised(-1, string.Format("已觸發負極限訊號但未到達位置 : {0}, 目前馬達位置 : {1}", PulseToMm(nPos), getENC()));
                    }
                }
            }
        }

        private bool _isAlarm = false;
        /// <summary> 取得警報訊號 </summary>
        public virtual bool IsAlarm
        {
            get
            {
                if (status.Fault != _isAlarm)
                {
                    _isAlarm = status.Fault;
                }
                return _isAlarm;
            }
        }

        /// <summary> 取得到位訊號 </summary>
        public bool IsINP { get { return status.TargetReached; } }

        /// <summary> 模組是否在移動中 </summary>
        public bool IsBusy
        {
            get
            {
                ushort uMCDone = getMotionDone();
                return uMCDone != 0;
            }
        }

        /// <summary> 模組是否到位 </summary>
        public bool IsReached
        {
            get
            {
                double nCmd = getPos2();
                double inpPrecise = axisPara.DistPerRole != 0 ? axisPara.InPositionPrecise / axisPara.DistPerRole * axisPara.PulsePerRole : 0;
                return !this.IsBusy && (Math.Abs(nPos - nCmd) <= inpPrecise);
            }
        }

        /// <summary> 模組類型 </summary>
        public MotionModel MotionModel { get; set; }

        private int _moveMode = 0;
        /// <summary> 目前運動模式 </summary>
        public int MoveMode { get { return _moveMode; } }

        #endregion properties

        #region construct

        public CEtherCATMotion(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            axisPara = Parameter as ParameterCEtherCATMotion;

            slaveModuleInitialize();

            setHardwareScan();

            taskInitialize();

            setMotion();

            axisPara.ParameterChanged += new ParameterINI.ParameterChangedHandler((paraName) => setMotion());

            swGetPos2.Start();
            swGetENC2.Start();
        }

        #endregion construct

        #region private method

        private void slaveModuleInitialize()
        {
            bool getSlave = false;
            foreach (var item in SlaveInfos)
            {
                SlaveInfo slaveInfo = item.Value;
                if ((axisPara.CardNo == slaveInfo.CardNo) && (axisPara.NodeNo == slaveInfo.NodeID))
                {
                    if (/*slaveInfo.VendorID == 0x1A05 && */slaveInfo.ProductCode == 0x5621) //R1-EC5621 1-Axis Pulse Output Motion Control Module
                    {
                        MotionModel = MotionModel.SYNTEK_R1_EC5621;
                    }
                    getSlave = true;
                    break;
                }
            }
            //補上NodeNo對應其他型號
            if (!getSlave)
            {
                throw new NotImplementedException(string.Format("EtherCAT can't find slave [{0}] from Card No {1} Node No {2}. There have {3} SlaveInfos.", this.DeviceName, axisPara.CardNo, axisPara.NodeNo, SlaveInfos.Count));
            }
        }

        //設定掃描的Thread
        private void setHardwareScan()
        {
            FlowControl flowControl = ModulesFactory.FlowControlHelper.GetFlowControl("SYNTEKMotion");
            FlowBase flowBase = new FlowBase(this.DeviceName, systemScan);
            flowControl.AddFlowBase(flowBase);
            flowBase.Start();
            keyOfIOStatus = true;
        }

        //流程器初始化
        private void taskInitialize()
        {
            FlowControl flowControl = ModulesFactory.FlowControlHelper.GetFlowControl("SYNTEKMotion");
            motionHomeFlow = new MotionHomeFlow(this);
            motionHomeFlow.Failed += (msg) =>
            {
                OnErrorRaised(-1, string.Format("Motion : {0}, {1}", this.DeviceName, msg));
            };
            flowControl.AddFlowBase(motionHomeFlow);
        }

        //scan  slave IO _axis
        private void systemScan(FlowVar fv)
        {
            //while (true)
            //{
            try
            {
                if (keyOfIOStatus)
                {
                    ushort uStatus = 0;
                    ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Get_StatusWord(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, ref uStatus);
                    if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                    {
                        throw new Exception("CS_ECAT_Slave_Motion_Get_StatusWord Fail, " + GetEtherCATErrorCode(uRet));
                    }
                    //0	Ready to Switch On  All:Ready Switch
                    this.status.ReadySwitch = BitConverterEx.TestB(uStatus, 0);
                    //1 	Switch On   All:Switch On
                    this.status.SwitchOn = BitConverterEx.TestB(uStatus, 1);
                    //2	Operation Enable  All:Operation Enable
                    this.status.OperationEnable = BitConverterEx.TestB(uStatus, 2);
                    //3	Fault  All:Fault
                    this.status.Fault = BitConverterEx.TestB(uStatus, 3);
                    //4	Voltage Disable  All:Voltage
                    this.status.Voltage = BitConverterEx.TestB(uStatus, 4);
                    //5	Quick Stop  All:Quick Stop
                    this.status.QuickStop = BitConverterEx.TestB(uStatus, 5);
                    //6	Switch On Disable  All:Switch Off
                    this.status.SwitchOff = BitConverterEx.TestB(uStatus, 6);
                    //7	Warning  All:Warning
                    this.status.Warning = BitConverterEx.TestB(uStatus, 7);
                    //8	Manufacture Specific
                    this.status.ManufactureSpecific = BitConverterEx.TestB(uStatus, 8);
                    //9	Remote  All:Remote
                    this.status.Remote = BitConverterEx.TestB(uStatus, 9);
                    //10	Target Reached  All:Target Reached
                    this.status.TargetReached = BitConverterEx.TestB(uStatus, 10);
                    //11	Internal Limit Active  All:Int Limit Active
                    this.status.IntLimitActive = BitConverterEx.TestB(uStatus, 11);
                    //12	Operation Mode Specific PP:Set-Point	Homing:Homing Attained
                    this.status.HomingAttained = BitConverterEx.TestB(uStatus, 12);
                    //13	Operation Mode Specific PP:Following Error	Homing:Homing Error
                    this.status.FollowingError = BitConverterEx.TestB(uStatus, 13);

                    if (MotionModel == MotionModel.Panasonic_MINAS_A5B)
                    {
                        //if (this.status.OperationEnable)
                        {
                            byte[] uData = new byte[4];
                            ushort uODIndex = 0x60FD, uODSubIndex = 0x00, uDataSize = 4;
                            uRet = CEtherCAT_DLL.CS_ECAT_Slave_PDO_Get_OD_Data(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, 0, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                            if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                            {
                                throw new Exception("CS_ECAT_Slave_PDO_Get_OD_Data Fail, " + GetEtherCATErrorCode(uRet));
                            }
                            this.status.Home = BitConverterEx.TestB(uData[0], 2);
                            this.IsLimitP = BitConverterEx.TestB(uData[0], 1);
                            this.IsLimitN = BitConverterEx.TestB(uData[0], 0);
                        }
                    }
                    else if (MotionModel == MotionModel.SYNTEK_R1_EC5621)
                    {
                        //14	Manufacture Specific  All:PEL
                        this.status.PEL = BitConverterEx.TestB(uStatus, 14);
                        //15	Manufacture Specific  All:MEL
                        this.status.MEL = BitConverterEx.TestB(uStatus, 15);
                    }

                    if (this.status.Fault || this.status.SwitchOff)
                        this.IsHome = false;

                    //Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CS_ECAT_Slave_Motion_Get_StatusWord {0} {1} Message : {2} \r\n StackTrace : {3}", this.DeviceName, axisPara.NodeNo, ex.Message, ex.StackTrace);
            }
            //}
        }

        //將軸參寫入 slave
        private void setMotion()
        {
            ushort uRet = 0;

            //uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Alm_Reaction(g_uESCCardNo, axisPara.NodeNo, g_uESCSlotID, 2, 1);
            //if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            //{
            //    throw new Exception("CS_ECAT_Slave_Motion_Set_Alm_Reaction Fail, " + GetErrorCode(uRet));
            //}

            if (MotionModel == MotionModel.SYNTEK_R1_EC5621)
            {
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_ESC5621_Set_Output_Mode(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, (ushort)axisPara.PulseMode);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_ESC5621_Set_Output_Mode Fail, " + GetEtherCATErrorCode(uRet));
                }
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_ESC5621_Set_Input_Mode(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, (ushort)axisPara.EncMode);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_ESC5621_Set_Input_Mode Fail, " + GetEtherCATErrorCode(uRet));
                }
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_ESC5621_Set_ORG_Inverse(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, Convert.ToUInt16(axisPara.LogicORG));
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_ESC5621_Set_ORG_Inverse Fail, " + GetEtherCATErrorCode(uRet));
                }
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_ESC5621_Set_QZ_Inverse(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, Convert.ToUInt16(axisPara.LogicZ));
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_ESC5621_Set_QZ_Inverse Fail, " + GetEtherCATErrorCode(uRet));
                }
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_ESC5621_Set_MEL_Inverse(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, Convert.ToUInt16(axisPara.LogicEL));
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_ESC5621_Set_MEL_Inverse Fail, " + GetEtherCATErrorCode(uRet));
                }
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_ESC5621_Set_PEL_Inverse(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, Convert.ToUInt16(axisPara.LogicEL));
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_ESC5621_Set_PEL_Inverse Fail, " + GetEtherCATErrorCode(uRet));
                }
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_ESC5621_Set_Svon_Inverse(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, Convert.ToUInt16(axisPara.LogicSvon));
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_ESC5621_Set_Svon_Inverse Fail, " + GetEtherCATErrorCode(uRet));
                }
            }
        }

        //get command counter
        private double getPos()
        {
            double nCmd = getPos2();
            return axisPara.PulsePerRole != 0 ? Math.Round(nCmd / axisPara.PulsePerRole * axisPara.DistPerRole, 2) : 0;
        }

        Stopwatch swGetPos2 = new Stopwatch();
        int lastPos2 = 0;
        private double getPos2()
        {
            int nCmd = lastPos2;
            if (swGetPos2.IsRunning && swGetPos2.ElapsedMilliseconds > 10)
            {
                swGetPos2.Restart();
                Stopwatch sw = Stopwatch.StartNew();
                ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Get_Command(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, ref nCmd);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_Motion_Get_Command Fail, " + GetEtherCATErrorCode(uRet));
                }
                lastPos2 = nCmd;
                sw.Stop();
                //todo getPos2 TACT
                //if (sw.ElapsedMilliseconds > 10)
                //{
                //    LogHelper.Debug("EtherCAT : {0} getPos2 TACT : {1}", this.DeviceName, sw.ElapsedMilliseconds);
                //}
            }
            return nCmd;
        }

        private double getENC()
        {
            double nPos = getENC2();
            return axisPara.PulsePerRole != 0 ? Math.Round(nPos / axisPara.PulsePerRole * axisPara.DistPerRole, 2) : 0;
        }

        Stopwatch swGetENC2 = new Stopwatch();
        int lastENC2 = 0;
        private double getENC2()
        {
            int nPos = lastENC2;
            if (swGetENC2.IsRunning && swGetENC2.ElapsedMilliseconds > 10)
            {
                Stopwatch sw = Stopwatch.StartNew();
                ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Get_Position(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, ref nPos);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_Motion_Get_Position Fail, " + GetEtherCATErrorCode(uRet));
                }
                lastENC2 = nPos;
                sw.Stop();
                //todo getENC2 TACT
                //if (sw.ElapsedMilliseconds > 10)
                //{
                //    LogHelper.Debug("EtherCAT : {0} getENC2 TACT : {1}", this.DeviceName, sw.ElapsedMilliseconds);
                //}
            }
            return nPos;
        }

        private double getSpeed()
        {
            double nSpeed = getSpeed2();
            return axisPara.PulsePerRole != 0 ? Math.Round(nSpeed / axisPara.PulsePerRole * axisPara.DistPerRole, 2) : 0;
        }

        private double getSpeed2()
        {
            int nSpeed = 0;
            ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Get_Current_Speed(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, ref nSpeed);
            if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Motion_Get_Current_Speed Fail, " + GetEtherCATErrorCode(uRet));
            }
            return nSpeed;
        }

        private ushort getMotionDone()
        {
            ushort uMCDone = 0;
            ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Get_Mdone(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, ref uMCDone);
            if (uMCDone != 0)
            {
                int a = 0;
            }
            if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Motion_Get_Mdone Fail, " + GetEtherCATErrorCode(uRet));
            }
            return uMCDone;
        }

        private double getErrorCount()
        {
            //Int16 error = 0;
            //CCMNet.CS_mnet_m204_get_error_counter(RingNoOfMNet, axisPara.SlaveIP, (UInt16)axisPara.AxisNo, ref error);
            //if (axisPara.PulsePerRole != 0)
            //{
            //    return (double)error / axisPara.PulsePerRole * axisPara.DistPerRole;
            //}
            //else
            //{
            //    return 0;
            //}
            return 0;
        }

        private double getErrorCount2()
        {
            //Int16 error = 0;
            //CCMNet.CS_mnet_m204_get_error_counter(RingNoOfMNet, axisPara.SlaveIP, (UInt16)axisPara.AxisNo, ref error);
            //return error;
            return 0;
        }

        #endregion private method

        #region public method

        /// <summary>
        /// 設定當前運動模式
        /// </summary>
        public void SetMoveMode(ushort opMode)
        {
            //0 : Null
            //1 : PP
            //3 : PV
            //4 : PT
            //6 : Home
            //8 : CSP
            //9 : CSV
            //10 : CST
            _moveMode = opMode;
            ushort g_uRet = 0;
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_MoveMode(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, opMode);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Motion_Set_MoveMode, " + GetEtherCATErrorCode(g_uRet));
            }
        }

        /// <summary>
        /// 得到當前運動模式
        /// </summary>
        public byte GetMoveMode()
        {
            ushort g_uRet = 0;
            byte opMode = 0;
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Get_MoveMode(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, ref opMode);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Motion_Get_MoveMode, " + GetEtherCATErrorCode(g_uRet));
            }
            return opMode;
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
        /// 單位轉換 (pulse to Millimeter)
        /// </summary>
        /// <param name="pulse">The pulse.</param>
        /// <returns></returns>
        public double PulseToMm(int pulse)
        {
            if (axisPara.PulsePerRole != 0)
                return (double)pulse * axisPara.PulsePerRole / axisPara.DistPerRole;
            else
                return 0;
        }

        /// <summary>
        /// 清除postion和command的counter
        /// </summary>
        public void ResetPos()
        {
            SDO_Debug("ResetPos Start");
            SetPosition(0);
            SDO_Debug("ResetPos Ing");
            SetCommand(0);
            SDO_Debug("ResetPos End");
        }

        /// <summary>
        /// 設定postion的counter(mm)
        /// </summary>
        public void SetPosition(double mmPos)
        {
            if (status.OperationEnable)
            {
                ushort g_uRet = 0;
                g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Position(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, MmToPulse(mmPos));
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_Motion_Set_Position(SetPosition), " + GetEtherCATErrorCode(g_uRet));
                }
            }
        }

        /// <summary>
        /// 設定postion的counter(pulse)
        /// </summary>
        public void SetPosition2(int pulsePos)
        {
            if (status.OperationEnable)
            {
                ushort g_uRet = 0;
                g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Position(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, pulsePos);
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_Motion_Set_Position(SetPosition2), " + GetEtherCATErrorCode(g_uRet));
                }
            }
        }

        /// <summary>
        /// 設定command的counter(mm)
        /// </summary>
        public void SetCommand(double mmCmd)
        {
            if (status.OperationEnable)
            {
                ushort g_uRet = 0;
                g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Command(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, MmToPulse(mmCmd));
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_Motion_Set_Command(SetCommand), " + GetEtherCATErrorCode(g_uRet));
                }
            }
        }

        /// <summary>
        /// 設定command的counter(pulse)
        /// </summary>
        public void SetCommand2(int pulseCmd)
        {
            if (status.OperationEnable)
            {
                ushort g_uRet = 0;
                g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Command(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, pulseCmd);
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_Motion_Set_Command(SetCommand2), " + GetEtherCATErrorCode(g_uRet));
                }
            }
        }

        /// <summary>
        ///  裝置啟停
        /// </summary>
        /// <param name="option">The option.</param>
        public void ServoOn(CmdStatus option)
        {
            if (!this.IsAlarm)
            {
                ushort g_uRet = 0;
                if (option == CmdStatus.OFF)
                {
                    this.IsHome = false;
                    g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Svon(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, 0);
                }
                if (option == CmdStatus.ON)
                {
                    g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Svon(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, 1);
                }
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_Motion_Set_Svon, " + GetEtherCATErrorCode(g_uRet));
                }
            }
        }

        /// <summary>
        /// 清除警報
        /// </summary>
        public void ResetAlarm()
        {
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Ralm(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID);

            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Motion_Ralm, " + GetEtherCATErrorCode(g_uRet));
            }

            if (status.SwitchOff)
            {
                ServoOn(CmdStatus.ON);
            }
        }

        /// <summary>
        /// 絕對移動
        /// </summary>
        /// <param name="dest">目標位置 (mm)</param>
        public void AbsolueMove(double dest)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (status.OperationEnable)
            {
                ushort bufferLength = GetBufferLength();
                if (bufferLength > 1)
                {
                    LogHelper.Flow("{0} Buffer Length : {1}", this.DeviceName, bufferLength);
                }
                if (this.MoveMode != 8)
                {
                    SetMoveMode(8);
                }
                nPos = MmToPulse(dest);
                ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_Move(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, nPos, MmToPulse(axisPara.StrVel), MmToPulse(axisPara.ConstVel), MmToPulse(axisPara.EndVel), axisPara.TAcc, axisPara.TDec, 1, 1);
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_CSP_Start_Move, " + GetEtherCATErrorCode(g_uRet));
                }
            }
            sw.Stop();
            //LogHelper.Debug("{0} AbsolueMove TACT {1}", this.DeviceName, sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// 絕對移動 (需要自定義速度，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="dest">目標位置 (mm)</param>
        /// <param name="constVel"> 速度值 </param>
        /// <param name="s_curve"> T-Curve = true, S-Curve = false </param>
        public void AbsolueMove(double dest, double speed, bool curve = true)
        {
            AbsolueMove(dest, speed, 0, 0, curve);
        }

        /// <summary>
        /// 絕對移動 (需要自定義速度，初速與末速為恆速1/2，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="dest">目標位置 (mm)</param>
        /// <param name="constVel"> 常態速度 </param>
        /// <param name="strVel"> 初速度 </param>
        /// <param name="endVel"> 末速度 </param>
        /// <param name="s_curve"> T-Curve = true, S-Curve = false </param>
        public void AbsolueMove(double dest, double constVel, double strVel, double endVel, bool curve = true)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (status.OperationEnable)
            {
                ushort bufferLength = GetBufferLength();
                if (bufferLength > 1)
                {
                    LogHelper.Flow("{0} Buffer Length : {1}", this.DeviceName, bufferLength);
                }
                if (this.MoveMode != 8)
                {
                    SetMoveMode(8);
                }
                nPos = MmToPulse(dest);
                ushort m_curve = (ushort)(curve ? 1 : 2);
                ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_Move(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, nPos, MmToPulse(strVel), MmToPulse(constVel), MmToPulse(endVel), axisPara.TAcc, axisPara.TDec, m_curve, 1);
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_CSP_Start_Move, " + GetEtherCATErrorCode(g_uRet));
                }
            }
            sw.Stop();
            //LogHelper.Debug("{0} AbsolueMove TACT {1}", this.DeviceName, sw.ElapsedMilliseconds);
        }

        public void TargetPosChange(double dest)
        {
            nPos = MmToPulse(dest);
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_TargetPos_Change(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, nPos);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_CSP_TargetPos_Change, " + GetEtherCATErrorCode(g_uRet));
            }
        }

        /// <summary>
        /// 相對移動 (需選擇使用軸參的手動速度或是自動速度)
        /// </summary>
        /// <param name="dest">目標位置 (mm)</param>
        public void RelativeMove(double dest)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (status.OperationEnable)
            {
                ushort bufferLength = GetBufferLength();
                if (bufferLength > 1)
                {
                    LogHelper.Flow("{0} Buffer Length : {1}", this.DeviceName, bufferLength);
                }
                if (this.MoveMode != 8)
                {
                    SetMoveMode(8);
                }
                nPos = MmToPulse(getPos() + dest);
                int dist = MmToPulse(dest);
                ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_Move(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, dist, MmToPulse(axisPara.StrVel), MmToPulse(axisPara.ConstVel), MmToPulse(axisPara.EndVel), axisPara.TAcc, axisPara.TDec, 1, 0);
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_CSP_Start_Move, " + GetEtherCATErrorCode(g_uRet));
                }
            }
            sw.Stop();
            //LogHelper.Debug("{0} RelativeMove TACT {1}", this.DeviceName, sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// 相對移動 (自定義速度，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="dest">目標位置 (mm)</param>
        /// <param name="constVel">速度值</param>
        public void RelativeMove(double dest, double constVel, bool curve = true)
        {
            RelativeMove(dest, constVel, 0, 0, curve);
        }

        /// <summary>
        /// 相對移動 (自定義速度，初速為恆速1/2，加速度減速度為軸參手動設定)
        /// </summary>
        /// <param name="dest">目標位置 (mm)</param>
        /// <param name="constVel"> 常態速度 </param>
        /// <param name="strVel"> 初速度 </param>
        /// <param name="endVel"> 末速度 </param>
        /// <param name="s_curve"> T-Curve = true, S-Curve = false </param>
        public void RelativeMove(double dest, double constVel, double strVel, double endVel, bool curve = true)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (status.OperationEnable)
            {
                ushort bufferLength = GetBufferLength();
                if (bufferLength > 1)
                {
                    LogHelper.Flow("{0} Buffer Length : {1}", this.DeviceName, bufferLength);
                }
                if (this.MoveMode != 8)
                {
                    SetMoveMode(8);
                }
                nPos = MmToPulse(getPos() + dest);
                int dist = MmToPulse(dest);
                ushort m_curve = (ushort)(curve ? 1 : 2);
                ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_Move(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, dist, MmToPulse(strVel), MmToPulse(constVel), MmToPulse(endVel), axisPara.TAcc, axisPara.TDec, m_curve, 0);
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_CSP_Start_Move, " + GetEtherCATErrorCode(g_uRet));
                }
            }
            sw.Stop();
            //LogHelper.Debug("{0} RelativeMove TACT {1}", this.DeviceName, sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// 連續移動
        /// </summary>
        /// <param name="dir">移動方向</param>
        public void ContinuousMove(RotationDirection dir)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (status.OperationEnable)
            {
                if (this.MoveMode != 8)
                {
                    SetMoveMode(8);
                }
                ushort bDir = 0;
                if (dir == RotationDirection.CCW)
                {
                    bDir = 1;
                }
                ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_V_Move(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, bDir, MmToPulse(axisPara.StrVel), MmToPulse(axisPara.ConstVel), axisPara.TAcc, 1);
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_CSP_Start_V_Move, " + GetEtherCATErrorCode(g_uRet));
                }
            }
            sw.Stop();
            //LogHelper.Debug("{0} ContinuousMove TACT {1}", this.DeviceName, sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// 連續移動
        /// </summary>
        /// <param name="dir">移動方向</param>
        /// <param name="speed">速度值</param>
        public void ContinuousMove(RotationDirection dir, double speed)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (status.OperationEnable)
            {
                if (this.MoveMode != 8)
                {
                    SetMoveMode(8);
                }
                ushort bDir = 0;
                if (dir == RotationDirection.CCW)
                {
                    bDir = 1;
                }
                ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_V_Move(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, bDir, MmToPulse(speed) / 2, MmToPulse(speed), axisPara.TAcc, 1);
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_CSP_Start_V_Move, " + GetEtherCATErrorCode(g_uRet));
                }
            }
            sw.Stop();
            //LogHelper.Debug("{0} ContinuousMove TACT {1}", this.DeviceName, sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// 連續移動
        /// </summary>
        /// <param name="dir">移動方向</param>
        /// <param name="jogSpeed">吋動速度</param>
        public void ContinuousMove(RotationDirection dir, JogSpeed jogSpeed)
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
            ContinuousMove(dir, speed);
        }

        /// <summary>
        /// 停止裝置 (復歸流程中不停止homeTask)
        /// </summary>
        public void Stop(StopType type, bool isStopTask = true)
        {
            Stopwatch sw = Stopwatch.StartNew();
            ushort g_uRet = 0;
            if (status.OperationEnable)
            {
                if (type == StopType.Emergency)
                {
                    if (this.MoveMode == 6)
                    {
                        g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Emg_Stop(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID);
                    }
                    else
                    {
                        g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Sd_Stop(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, 0);
                    }
                    if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                    {
                        throw new Exception("CS_ECAT_Slave_Motion_Emg_Stop, " + GetEtherCATErrorCode(g_uRet));
                    }
                }
                if (type == StopType.SlowDown)
                {
                    if (this.MoveMode == 6)
                    {
                        g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Sd_Stop(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, MmToPulse(axisPara.HomeAccVel));
                    }
                    else
                    {
                        g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Sd_Stop(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, axisPara.TDec);
                    }
                    if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                    {
                        throw new Exception("CS_ECAT_Slave_Motion_Sd_Stop, " + GetEtherCATErrorCode(g_uRet));
                    }
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
                    motionHomeFlow.Stop();
                }
            }
            sw.Stop();
            //LogHelper.Debug("{0} Stop TACT {1}", this.DeviceName, sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// 啟動原點復歸 (參數定義在軸參中)
        /// </summary>
        public virtual void Home()
        {
            if (status.OperationEnable)
            {
                this.IsHome = false;
                motionHomeFlow.Restart();
            }
        }

        /// <summary>
        /// Shows the  Configuration Dialog.
        /// </summary>
        public DialogResult ConfigurationShowDialog()
        {
            CEtherCATMotionForm cEtherCATMotionForm = new CEtherCATMotionForm(this);
            cEtherCATMotionForm.Text = DeviceName;
            return cEtherCATMotionForm.ShowDialog();
        }

        /// <summary>
        /// Shows the  Configuration.
        /// </summary>
        public void ConfigurationShow(FormClosingEventHandler action = null)
        {
            FormHelper.OpenUniqueForm(DeviceName, () =>
            {
                CEtherCATMotionForm cEtherCATMotionForm = new CEtherCATMotionForm(this);
                cEtherCATMotionForm.Text = DeviceName;
                cEtherCATMotionForm.Name = DeviceName;
                cEtherCATMotionForm.Show();
                if (action != null)
                {
                    cEtherCATMotionForm.FormClosing += action;
                }
            });
        }

        /// <summary>
        /// Shows the  Slim Configuration.
        /// </summary>
        public void ConfigurationSlimShow(FormClosingEventHandler action = null)
        {
            FormHelper.OpenUniqueForm(DeviceName + "Slim", () =>
            {
                CEtherCATMotionSlimForm cEtherCATMotionSlimForm = new CEtherCATMotionSlimForm(this);
                cEtherCATMotionSlimForm.Text = DeviceName;
                cEtherCATMotionSlimForm.Name = DeviceName + "Slim";
                cEtherCATMotionSlimForm.Show();
                if (action != null)
                {
                    cEtherCATMotionSlimForm.FormClosing += action;
                }
            });
        }

        public void DoHomeAction(ushort homeMode)
        {
            ushort g_uRet = 0;
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Home_Config(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, axisPara.HomeMode
                                                                                                                , MmToPulse(axisPara.HomeOffset), (uint)MmToPulse(axisPara.HomeSwitchVel), (uint)MmToPulse(axisPara.HomeZeroVel), (uint)MmToPulse(axisPara.HomeAccVel));
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Home_Config, " + GetEtherCATErrorCode(g_uRet));
            }
            else
            {
                g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Home_Move(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID);
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_Home_Move, " + GetEtherCATErrorCode(g_uRet) + string.Format(" P : {0}, E : {1}", this.Position, this.Encoder));
                }
            }
        }

        public void DoHomeConfig(ushort homeMode)
        {
            ushort g_uRet = 0;
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Home_Config(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, axisPara.HomeMode
                                                                                                                , MmToPulse(axisPara.HomeOffset), (uint)MmToPulse(axisPara.HomeSwitchVel), (uint)MmToPulse(axisPara.HomeZeroVel), (uint)MmToPulse(axisPara.HomeAccVel));
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Home_Config, " + GetEtherCATErrorCode(g_uRet));
            }
        }

        public void DoHomeMove()
        {
            _moveMode = 6;
            ushort g_uRet = 0;
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Home_Move(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Home_Move, " + GetEtherCATErrorCode(g_uRet) + string.Format(" P : {0}, E : {1}", this.Position, this.Encoder));
            }
        }

        public ushort CheckHomeStatus()
        {
            ushort g_uRet = 0, uStatus = 0;
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Home_Status(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, ref uStatus);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Home_Status, " + GetEtherCATErrorCode(g_uRet));
            }
            //0 : 尚未運動或回Home已經完成
            //1 : 正在執行Home運動中
            //2 : 回Home運動在未完成的狀況下被中止
            //3 : 回Home運動中發生Error
            return uStatus;
        }

        public void SetSlowDown(double Tdec, ushort SourceType, ushort DINodeID, ushort SourceNo, ushort Enable)
        {
            if (!this.IsAlarm)
            {
                ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Set_SLD_Enable(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, Tdec, SourceType, DINodeID, g_uESCSlotID, SourceNo, Enable);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_CSP_Set_SLD_Enable Fail, " + GetEtherCATErrorCode(uRet));
                }
            }
        }

        public void SetTrSeg2(double dest, ushort SourceType, ushort DINodeID, ushort SourceNo, ushort Enable)
        {
            if (!this.IsAlarm)
            {
                nPos = MmToPulse(dest);
                ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Set_TrSeg2_Enable(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, nPos, MmToPulse(axisPara.ConstVel), MmToPulse(axisPara.EndVel), axisPara.TAcc, axisPara.TDec, 2, SourceType, DINodeID, g_uESCSlotID, SourceNo, Enable);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_CSP_Set_TrSeg2_Enable Fail, " + GetEtherCATErrorCode(uRet));
                }
            }
        }

        public void GetDITriggerStatus(ref ushort TriggerStatus, ref uint TriggerCount)
        {
            if (!this.IsAlarm)
            {
                ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Get_DITrigger_Status(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, ref TriggerStatus, ref TriggerCount);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_CSP_Get_DITrigger_Status Fail, " + GetEtherCATErrorCode(uRet));
                }
            }
        }

        public void SetInternalLimitActiveReaction(ushort Internal_Limit_Active_Type)
        {
            ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Internal_Limit_Active_Reaction(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, Internal_Limit_Active_Type);
            if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Motion_Set_Internal_Limit_Active_Reaction Fail, " + GetEtherCATErrorCode(uRet));
            }
        }

        public string GetErrorCode()
        {
            string errorCode = "0.0";
            if (this.MotionModel == MotionModel.Panasonic_MINAS_A5B)
            {
                ushort uODIndex = 0x10F3, uODSubIndex = 0x02, uDataSize = 1;
                byte[] uData = new byte[uDataSize];
                ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                uODSubIndex = uData[0];
                uDataSize = 16;
                uData = new byte[uDataSize];
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                errorCode = string.Format("{0}.{1}", uData[7], uData[6]);
            }
            return errorCode;
        }

        public ushort GetStatusWord()
        {
            ushort StatusWord = 0;
            ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Get_StatusWord(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, ref StatusWord);
            if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Motion_Get_StatusWord Fail, " + GetEtherCATErrorCode(uRet));
            }
            return StatusWord;
        }

        public ushort GetBufferLength()
        {
            ushort BufferLength = 0;
            ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Get_Buffer_Length(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, ref BufferLength);
            if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_Motion_Get_Buffer_Length Fail, " + GetEtherCATErrorCode(uRet));
            }
            return BufferLength;
        }

        public void SDO_60FD()
        {
            byte[] uData = new byte[4];
            ushort uODIndex = 0x60FD, uODSubIndex = 0x00, uDataSize = 4;
            ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
            if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
            }
            this.status.Home = BitConverterEx.TestB(uData[0], 2);
            this.status.PEL = BitConverterEx.TestB(uData[0], 1);
            this.status.MEL = BitConverterEx.TestB(uData[0], 0);
        }

        public void SDO_603F()
        {
            byte[] uData = new byte[2];
            ushort uODIndex = 0x603F, uODSubIndex = 0x00, uDataSize = 2;
            ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
            if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
            }
            Console.WriteLine("{0} {1}", uData[0], uData[1]);
        }

        public void SDO_Debug(string head)
        {
            try
            {
                if (MotionModel == MotionModel.SYNTEK_R1_EC5621)
                {
                    return;
                }
                byte[] uData = new byte[4];
                ushort uODIndex = 0x6000, uODSubIndex = 0x00, uDataSize = 4, uRet = 0;
                uODIndex = 0x6040;
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                int int6040 = BitConverter.ToInt32(uData, 0);
                uODIndex = 0x6041;
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                int int6041 = BitConverter.ToInt32(uData, 0);
                uODIndex = 0x6060;
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                int int6060 = BitConverter.ToInt32(uData, 0);
                uODIndex = 0x6061;
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                int int6061 = BitConverter.ToInt32(uData, 0);
                uODIndex = 0x6062;
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                int int6062 = BitConverter.ToInt32(uData, 0);
                uODIndex = 0x6063;
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                int int6063 = BitConverter.ToInt32(uData, 0);
                uODIndex = 0x6064;
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                int int6064 = BitConverter.ToInt32(uData, 0);
                uODIndex = 0x607C;
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                int int607C = BitConverter.ToInt32(uData, 0);
                uODIndex = 0x60FC;
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                int int60FC = BitConverter.ToInt32(uData, 0);
                uODIndex = 0x607A;
                uRet = CEtherCAT_DLL.CS_ECAT_Slave_SDO_Read_Message(axisPara.CardNo, axisPara.NodeNo, g_uESCSlotID, uODIndex, uODSubIndex, uDataSize, ref uData[0]);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_SDO_Read_Message Fail, " + GetEtherCATErrorCode(uRet));
                }
                int int607A = BitConverter.ToInt32(uData, 0);
                LogHelper.Debug("{0} {1} 0x6040({2}), 0x6041({3}), 0x6060({4}), 0x6061({5}), 0x6062({6}), 0x6063({7}), 0x6064({8}), 0x607C({9}), 0x60FC({10}), 0x607A({11})", head, this.DeviceName, int6040, int6041, int6060, int6061, int6062, int6063, int6064, int607C, int60FC, int607A);
            }
            catch (Exception ex)
            {
                NotifyLogger.Post(ex.Message);
                LogHelper.Exception(ex);
            }
        }

        private uint _loops = 0;
        private int _iterations = 10;

        public void TestMDone()
        {
            ushort MCDone = 0;
            double Speed = 0;

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    ushort uMCDone = getMotionDone();
                    double uSpeed = getSpeed2();
                    if (uMCDone != MCDone)
                    {
                        MCDone = uMCDone;
                        Console.WriteLine("{0} MDone {1} Speed {2}", this.DeviceName, MCDone, Speed);
                    }
                    if (uSpeed != Speed)
                    {
                        Speed = uSpeed;
                        Console.WriteLine("{0} MDone {1} Speed {2}", this.DeviceName, MCDone, Speed);
                    }
                    if (Environment.ProcessorCount == 1 || (++_loops % 100) == 0)
                    {
                        Thread.Sleep(1);
                    }
                    else
                    {
                        Thread.SpinWait(_iterations);
                    }
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        #endregion public method
    }

    public class MotionHomeFlow : FlowBase
    {
        private CEtherCATMotion _axis;

        private bool _homeRetry = false;

        public MotionHomeFlow(CEtherCATMotion axis)
        {
            this.Name = axis.DeviceName + "HomeFlow";

            _axis = axis;

            Failing += (msg) =>
            {
                LogHelper.Exception(msg);
                _axis.SDO_Debug("HomeFail");
                _axis.SetInternalLimitActiveReaction(1);
                axis.Stop(StopType.Emergency, false);
            };
            BuildLog += (msg) => LogHelper.Flow(msg);
        }

        public override void Flow()
        {
            if (this.Timer3.ElapsedMilliseconds > _axis.AxisPara.HomeTimeout * 1000)
            {
                Fail("馬達復歸Timeout");
                return;
            }
            switch (this.Step1)
            {
                case "0":
                    {
                        _homeRetry = false;
                        OnBuildLog(string.Format("{0} Step 0 : M {1} P {2:F2} E {3:F2}", _axis.DeviceName, _axis.GetMoveMode(), _axis.Position, _axis.Encoder));
                        _axis.ServoOn(CmdStatus.ON);
                        ChangeStep1("10");
                        break;
                    }
                case "10":
                    {
                        _axis.Stop(StopType.Emergency, false);
                        this.Timer3.Restart();
                        ChangeStep1("20");
                        break;
                    }
                case "20":
                    {
                        if (!_axis.IsBusy)
                        {
                            if (_axis.Status.PEL)
                            {
                                _axis.AbsolueMove(_axis.Encoder - 5);   //將位置統一
                            }
                            else if (_axis.Status.MEL)
                            {
                                _axis.AbsolueMove(_axis.Encoder + 5);   //將位置統一
                            }
                            else
                            {
                                _axis.AbsolueMove(_axis.Encoder);   //將位置統一
                            }
                            this.Timer1.Restart();
                            ChangeStep1("30");
                        }
                        break;
                    }
                case "30":
                    {
                        if (_axis.IsReached)
                        {
                            _axis.SetInternalLimitActiveReaction(0);
                            if (_axis.MoveMode != 6)
                            {
                                _axis.SetMoveMode(6);
                            }
                            this.Timer1.Restart();
                            ChangeStep1("50");
                        }
                        else if (this.Timer1.ElapsedMilliseconds > 100)
                        {
                            //未到位置，須再次復歸
                            ChangeStep1("10");
                        }
                        break;
                    }
                case "50":
                    {
                        if (this.Timer1.ElapsedMilliseconds > 100)
                        {
                            _axis.DoHomeConfig(_axis.AxisPara.HomeMode);
                            this.Timer1.Restart();
                            ChangeStep1("60");
                        }
                        break;
                    }
                case "60":
                    {
                        if (this.Timer1.ElapsedMilliseconds > 100)
                        {
                            _axis.SDO_Debug("HomeMove Start");
                            _axis.DoHomeMove();
                            _axis.SDO_Debug("HomeMove End");
                            this.Timer1.Restart();
                            this.Timer3.Restart();
                            ChangeStep1("100");
                        }
                        break;
                    }
                case "100":
                    {
                        if (this.Timer1.ElapsedMilliseconds > 100)
                        {
                            ushort homeStatus = _axis.CheckHomeStatus();
                            if (homeStatus == 0)
                            {
                                this.Timer1.Restart();
                                _axis.SDO_Debug("SetMoveMode Start");
                                _axis.SetMoveMode(8);
                                _axis.SDO_Debug("SetMoveMode End");
                                ChangeStep1("110");
                            }
                            else if (homeStatus == 1)
                            {
                            }
                            else if (homeStatus == 2)
                            {
                                Fail("馬達復歸在未完成的狀況下被中止");
                            }
                            else if (homeStatus == 3)
                            {
                                Fail("馬達復歸中發生Error");
                            }
                        }
                        break;
                    }
                case "110":
                    {
                        if (this.Timer1.ElapsedMilliseconds > 100)
                        {
                            //位置匹配
                            OnBuildLog(string.Format("{0} Step 110 : M {1} P {2:F2} E {3:F2}", _axis.DeviceName, _axis.GetMoveMode(), _axis.Position, _axis.Encoder));
                            _axis.AbsolueMove(0);
                            this.Timer1.Restart();
                            ChangeStep1("150");
                        }
                        break;
                    }
                case "150":
                    {
                        if (this.Timer1.ElapsedMilliseconds > 100)
                        {
                            if (_axis.IsReached)
                            {
                                _axis.SetInternalLimitActiveReaction(1);
                                //ChangeStep1("200");
                                ChangeStep1("1000");
                            }
                            else if (!_axis.IsReached && this.Timer1.ElapsedMilliseconds > 1000)
                            {
                                //如果1秒還未到達位置，須再次復歸
                                if (!_homeRetry)
                                {
                                    //容許一次重試
                                    _homeRetry = true;
                                    ChangeStep1("10");
                                }
                                else
                                {
                                    Fail("馬達復歸完成但持續Busy");
                                }
                            }
                        }
                        break;
                    }
                //flow finish
                case "1000":
                    {
                        //if (HomeCompleted != null)
                        //{
                        //    HomeCompleted(DeviceName);
                        //}
                        OnBuildLog(string.Format("{0} Step 1000 : M {1} P {2:F2} E {3:F2}", _axis.DeviceName, _axis.GetMoveMode(), _axis.Position, _axis.Encoder));
                        _axis.IsHome = true;
                        Stop();
                        break;
                    }
            }
        }
    }
}