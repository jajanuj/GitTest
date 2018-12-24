using AOISystem.Utilities.Common;
using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.Library;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard;
using System;
using System.Diagnostics;
using System.Threading;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.IO
{
    public class CEtherCATDO7062 : CEtherCAT
    {
        private Thread systemScanThread;
        private bool keyOfIOScan;
        private CEtherCATDO7062Form dialogOfDIO;
        private ParameterCEtherCATDO7062 dIOPara;
        private uint _loops = 0;
        private int _iterations = 10;
        private ushort _lastOutputValue = 0;

        /// <summary> 16點IO狀態，一維陣列 index:[0~15] </summary>
        private bool[] status1 = new bool[16];

        /// <summary> 16點IO狀態，二維陣列 index:[0~1 , 0~7] </summary>
        private bool[,] status2 = new bool[2 , 8];

        public ParameterCEtherCATDO7062 DIOPara { set { dIOPara = value; } get { return dIOPara; } }

        public bool[] Status1 { get { return status1; } set { status1 = value; } }

        public bool[,] Status2 { get { return status2; } set { status2 = value; } }

        public CEtherCATDO7062(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            dIOPara = Parameter as ParameterCEtherCATDO7062;

            slaveModuleInitialize();

            if (ParameterDictionary.GetValue("DO7062BlockScan") == "true")
            {
                //system scan
                FlowControl flowControl = ModulesFactory.FlowControlHelper.GetFlowControl("SYNTEKMotion");
                FlowBase flowBase = new FlowBase(this.DeviceName, systemScan);
                flowControl.AddFlowBase(flowBase);
                flowBase.Start();
            }
            keyOfIOScan = true;
        }

        #region private method

        private void slaveModuleInitialize()
        {
            bool getSlave = false;
            foreach (var item in SlaveInfos)
            {
                SlaveInfo slaveInfo = item.Value;
                if ((dIOPara.CardNo == slaveInfo.CardNo) && (dIOPara.NodeNo == slaveInfo.NodeID))
                {
                    if (/*slaveInfo.VendorID == 0x1A05 && */slaveInfo.ProductCode == 0x7062) //R1-EC7062 16-ch 24VDC/0.5A/Sink Type Digital Output Module
                    {
                        getSlave = true;
                        break;
                    }
                }
            }
            if (!getSlave)
            {
                throw new NotImplementedException(string.Format("EtherCAT can't find slave [{0}] from Card No {1} Node No {2}. There have {3} SlaveInfos.", this.DeviceName, dIOPara.CardNo, dIOPara.NodeNo, SlaveInfos.Count));
            }
        }

        //scan DIO slave status
        private void systemScan(FlowVar fv)
        {
            //while (true)
            //{
            if (keyOfIOScan)
            {
                ushort uValue = BitConverterEx.ConvertToUInt16(Status1);
                if (uValue != _lastOutputValue)
                {
                    _lastOutputValue = uValue;
                    ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Set_Output_Value(dIOPara.CardNo, dIOPara.NodeNo, g_uESCSlotID, uValue);

                    if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                    {
                        throw new Exception("CS_ECAT_Slave_DIO_Set_Output_Value, " + GetEtherCATErrorCode(uRet));
                    }

                    //Thread.Sleep(15);
                    //if (Environment.ProcessorCount == 1 || (++_loops % 100) == 0)
                    //{
                    //    Thread.Sleep(1);
                    //}
                    //else
                    //{
                    //    Thread.SpinWait(_iterations);
                    //}
                }
            }
            //}
        }

        #endregion private method

        #region public method

        /// <summary>
        /// 設定Output數值
        /// </summary>
        /// <param name="bitNum">Bit Number</param>
        /// <param name="status">寫入成功回傳true，反之false(DI點一定false)</param>
        /// <returns></returns>
        public bool SetOutput(ushort bitNum, bool status)
        {
            if (dIOPara.IsActive)
            {
                if (ParameterDictionary.GetValue("DO7062BlockScan") == "true")
                {
                    Status1[bitNum] = status;
                    Status2[bitNum / 8, bitNum % 8] = status;
                }
                else
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    ushort uValue = (ushort)(status ? 1 : 0);
                    ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Set_Single_Output_Value(dIOPara.CardNo, dIOPara.NodeNo, g_uESCSlotID, bitNum, uValue);
                    if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                    {
                        throw new Exception("CS_ECAT_Slave_DIO_Set_Single_Output_Value, " + GetEtherCATErrorCode(uRet));
                    }
                    sw.Stop();
                    //LogHelper.Debug("{0} SetOutput TACT {1}", this.DeviceName, sw.ElapsedMilliseconds);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 清除所有DO點狀態
        /// </summary>
        public void ClearAllOutput()
        {
            ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Set_Output_Value(dIOPara.CardNo, dIOPara.NodeNo, g_uESCSlotID, 0);
            if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_DIO_Set_Output_Value, " + GetEtherCATErrorCode(uRet));
            }
        }

        public void ShowDialog()
        {
            dialogOfDIO = new CEtherCATDO7062Form(this);
            dialogOfDIO.Text = DeviceName;
            dialogOfDIO.ShowDialog();
        }

        #endregion public method
    }
}