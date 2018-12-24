using AOISystem.Utilities.Common;
using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.Library;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard;
using System;
using System.Diagnostics;
using System.Threading;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.IO
{
    public class CEtherCATDI6022 : CEtherCAT
    {
        private Thread systemScanThread;
        private bool keyOfIOScan;
        private CEtherCATDI6022Form dialogOfDIO;
        private ParameterCEtherCATDI6022 dIOPara;
        private uint _loops = 0;
        private int _iterations = 10;

        /// <summary> 16點IO狀態，一維陣列 index:[0~15] </summary>
        private bool[] status1 = new bool[16];

        /// <summary> 16點IO狀態，二維陣列 index:[0~1 , 0~7] </summary>
        private bool[,] status2 = new bool[2 , 8];

        public ParameterCEtherCATDI6022 DIOPara { set { dIOPara = value; } get { return dIOPara; } }

        public bool[] Status1 { get { return status1; } set { status1 = value; } }

        public bool[,] Status2 { get { return status2; } set { status2 = value; } }

        public CEtherCATDI6022(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            dIOPara = Parameter as ParameterCEtherCATDI6022;

            slaveModuleInitialize();

            if (ParameterDictionary.GetValue("DI6022BlockScan") == "true")
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
                    if (/*slaveInfo.VendorID == 0x1A05 && */slaveInfo.ProductCode == 0x6002) //R1-EC6002 16-ch Sink/Source Type Digital Input Module with 2ms Filter Function
                    {
                        getSlave = true;
                        break;
                    }
                    if (/*slaveInfo.VendorID == 0x1A05 && */slaveInfo.ProductCode == 0x6022) //R1-EC6022 16-ch Sink/Source Type Digital Input Module with 2ms Filter Function
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
                    ushort uValue = 0, uRet = 0;
                    uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Get_Input_Value(dIOPara.CardNo, dIOPara.NodeNo, g_uESCSlotID, ref uValue);

                    if (uRet == CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                    {
                        for (int nBit = 0; nBit < 16; nBit++)
                        {
                            Status1[nBit] = BitConverterEx.TestB(uValue, (byte)nBit);
                            Status2[nBit / 8, nBit % 8] = BitConverterEx.TestB(uValue, (byte)nBit);
                        }
                    }
                    else
                    {
                        throw new Exception("CS_ECAT_Slave_DIO_Get_Input_Value, " + GetEtherCATErrorCode(uRet));
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
            //}
        }

        #endregion private method

        #region public method

        /// <summary>
        /// 取得Intput數值
        /// </summary>
        /// <param name="bitNum">Bit Number</param>
        /// <returns></returns>
        public bool GetInput(ushort bitNum)
        {
            if (ParameterDictionary.GetValue("DI6022BlockScan") == "true")
            {
                return Status1[bitNum];
            }
            else
            {
                Stopwatch sw = Stopwatch.StartNew();
                ushort uValue = 0;
                ushort uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Get_Single_Input_Value(dIOPara.CardNo, dIOPara.NodeNo, g_uESCSlotID, bitNum, ref uValue);
                if (uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    throw new Exception("CS_ECAT_Slave_DIO_Get_Single_Input_Value, " + GetEtherCATErrorCode(uRet));
                }
                sw.Stop();
                //LogHelper.Debug("{0} GetInput TACT {1}", this.DeviceName, sw.ElapsedMilliseconds);
                return uValue == 1;
            }
        }

        public void ShowDialog()
        {
            dialogOfDIO = new CEtherCATDI6022Form(this);
            dialogOfDIO.Text = DeviceName;
            dialogOfDIO.ShowDialog();
        }

        #endregion public method
    }
}