using AOISystem.Utilities.Modules.Syntek.L132.Library;
using AOISystem.Utilities.Modules.Syntek.L132.MasterCard;
using System.Threading;
using I16 = System.Int16;
using U16 = System.UInt16;
using U8 = System.Byte;

namespace AOISystem.Utilities.Modules.Syntek.L132.SlaveModules.IO
{
    public class L132DIO : L132.MasterCard.L132
    {
        private Thread systemScanThread;
        private bool keyOfIOScan;
        private L132DIOForm dialogOfDIO;
        private ParameterL132DIO dIOPara;

        /// <summary> 32點IO狀態，一維陣列 index:[0~31] </summary>
        private bool[] status1 = new bool[32];

        /// <summary> 32點IO狀態，二維陣列 index:[0~3 , 0~7] </summary>
        private bool[,] status2 = new bool[4 , 8];

        internal ParameterL132DIO DIOPara { set { dIOPara = value; } get { return dIOPara; } }

        public bool[] Status1 { get { return status1; } set { status1 = value; } }

        public bool[,] Status2 { get { return status2; } set { status2 = value; } }

        public L132DIO(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            dIOPara = Parameter as ParameterL132DIO;

            //system scan
            systemScanThread = new Thread(systemScan);
            systemScanThread.IsBackground = true;
            systemScanThread.Start();
            keyOfIOScan = true;
        }

        #region private method

        //scan DIO slave status
        private void systemScan()
        {
            while (true)
            {
                if (keyOfIOScan)
                {
                    I16 status;
                    U8 val = 0;
                    for (byte portNo = 0 ; portNo < 4 ; portNo++)
                    {
                        status = CMNET_L132.CS_mnet_io_input((U16)dIOPara.CardSwitchNo , (U16)dIOPara.RingNoOfCard , dIOPara.SlaveIP , portNo , ref val);
                        for (int i = 0 ; i < 8 ; i++)
                        {
                            Status1[portNo * 8 + i] = BitConverterEx.TestB(val , (byte)i);
                            Status2[portNo, i] = BitConverterEx.TestB(val, (byte)i);
                        }
                    }
                    Thread.Sleep(15);
                }
            }
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
                U8 val = 0;
                CMNET_L132.CS_mnet_io_input((U16)dIOPara.CardSwitchNo , (U16)dIOPara.RingNoOfCard , dIOPara.SlaveIP , (U8)portNo , ref val);
                BitConverterEx.SetBit(ref val, (byte)bitNo, status);
                I16 retOfSetOutput = CMNET_L132.CS_mnet_io_output((U16)dIOPara.CardSwitchNo , (U16)dIOPara.RingNoOfCard , dIOPara.SlaveIP , (U8)portNo , val);
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
            CMNET_L132.CS_mnet_io_output((U16)dIOPara.CardSwitchNo , (U16)dIOPara.RingNoOfCard , dIOPara.SlaveIP , 0 , 0);
            CMNET_L132.CS_mnet_io_output((U16)dIOPara.CardSwitchNo , (U16)dIOPara.RingNoOfCard , dIOPara.SlaveIP , 1 , 0);
            CMNET_L132.CS_mnet_io_output((U16)dIOPara.CardSwitchNo , (U16)dIOPara.RingNoOfCard , dIOPara.SlaveIP , 2 , 0);
            CMNET_L132.CS_mnet_io_output((U16)dIOPara.CardSwitchNo , (U16)dIOPara.RingNoOfCard , dIOPara.SlaveIP , 3 , 0);
        }

        public void ShowDialog()
        {
            dialogOfDIO = new L132DIOForm(this);
            dialogOfDIO.Text = DeviceName;
            dialogOfDIO.ShowDialog();
        }

        #endregion public method
    }
}