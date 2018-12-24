using AOISystem.Utilities.Modules.Syntek.L122.Library;
using AOISystem.Utilities.Modules.Syntek.L122.MasterCard;
using System;
using System.Threading;
using I16 = System.Int16;
using U8 = System.Byte;
using AOISystem.Utilities.Flow;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.IO
{
    public class L122DIO : L122.MasterCard.L122
    {
        private Thread systemScanThread;
        private bool keyOfIOScan;
        private L122DIOForm dialogOfDIO;
        private ParameterL122DIO dIOPara;
        private uint _loops = 0;
        private int _iterations = 10;

        /// <summary> 32點IO狀態，一維陣列 index:[0~31] </summary>
        private bool[] status1 = new bool[32];

        /// <summary> 32點IO狀態，二維陣列 index:[0~3 , 0~7] </summary>
        private bool[,] status2 = new bool[4 , 8];

        internal ParameterL122DIO DIOPara { set { dIOPara = value; } get { return dIOPara; } }

        public bool[] Status1 { get { return status1; } set { status1 = value; } }

        public bool[,] Status2 { get { return status2; } set { status2 = value; } }

        public L122DIO(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            dIOPara = Parameter as ParameterL122DIO;

            //system scan
            FlowControl flowControl = ModulesFactory.FlowControlHelper.GetFlowControl("SYNTEKMotion");
            FlowBase flowBase = new FlowBase(this.DeviceName, systemScan);
            flowControl.AddFlowBase(flowBase);
            flowBase.Start();
            //systemScanThread = new Thread(systemScan);
            //systemScanThread.IsBackground = true;
            //systemScanThread.Start();
            keyOfIOScan = true;
        }

        #region private method

        //scan DIO slave status
        private void systemScan(FlowVar fv)
            {
            //while (true)
            //{
                if (keyOfIOScan)
                {
                    I16 status;
                    for (byte portNo = 0 ; portNo < 4 ; portNo++)
                    {
                        status = CCMNet.CS_mnet_io_input(RingNoOfMNet , dIOPara.SlaveIP , portNo);
                        for (int i = 0 ; i < 8 ; i++)
                        {
                            Status1[portNo * 8 + i] = BitConverterEx.TestB(status , (byte)i);
                            Status2[portNo, i] = BitConverterEx.TestB(status, (byte)i);
                        }
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
        /// 設定Output數值
        /// </summary>
        /// <param name="portNo">Port Number</param>
        /// <param name="bitNo">Bit Number</param>
        /// <param name="status">寫入成功回傳true，反之false(DI點一定false)</param>
        /// <returns></returns>
        public bool SetOutput2(PortNo portNo , BitNo bitNo , bool status)
        {
            if (dIOPara.IsActive)
            {
                I16 retOfIO = CCMNet.CS_mnet_io_input(RingNoOfMNet , dIOPara.SlaveIP , (U8)portNo);
                BitConverterEx.SetBit(ref retOfIO, (byte)bitNo, status);
                I16 retOfSetOutput = CCMNet.CS_mnet_io_output(RingNoOfMNet , dIOPara.SlaveIP , (U8)portNo , (byte)retOfIO);
                if (retOfSetOutput >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        public void SetOutput1(byte idx , bool status)
        {
            byte portNo = (byte)(idx / 8);
            byte bitNo = (byte)(idx % 8);
            SetOutput2((PortNo)portNo , (BitNo)bitNo , status);
        }

        /// <summary>
        /// 清除所有DO點狀態
        /// </summary>
        public void ClearAllOutput()
        {
            CCMNet.CS_mnet_io_output(RingNoOfMNet , dIOPara.SlaveIP , 0 , 0);
            CCMNet.CS_mnet_io_output(RingNoOfMNet , dIOPara.SlaveIP , 1 , 0);
            CCMNet.CS_mnet_io_output(RingNoOfMNet , dIOPara.SlaveIP , 2 , 0);
            CCMNet.CS_mnet_io_output(RingNoOfMNet , dIOPara.SlaveIP , 3 , 0);
        }

        public void ShowDialog()
        {
            dialogOfDIO = new L122DIOForm(this);
            dialogOfDIO.Text = DeviceName;
            dialogOfDIO.ShowDialog();
        }

        #endregion public method
    }
}