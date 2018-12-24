using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.Library;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard;
using System;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.DAC
{
    public enum DACChannel : ushort
    {
        CＨ0 = 0,
        CＨ1 = 1,
        CＨ2 = 2,
        CＨ3 = 3
    }

    public enum RangeMode : ushort
    {
        Positive_0_5V = 0,
        Positive_0_10V = 1,
        Positive_Negative_5V = 2,
        Positive_Negative_10V = 3,
        Positive_4_20mA = 4,
        Positive_0_20mA = 5,
        Positive_0_24mA = 6
    }

    public class CEtherCATDAC9144 : CEtherCAT
    {
        private bool keyOfIOScan;

        private ParameterCEtherCATDAC9144 dacPara;

        private int[] g_nModeValue = new int[7];
        private int[] g_nModeLength = new int[7];

        /// <summary> DAC的電壓值 </summary>
        public double[] Voltage = new double[4];

        /// <summary> DAC的換算過後的數值 </summary>
        public ushort[] Value = new ushort[4];

        /// <summary> DAC 參數 </summary>
        public ParameterCEtherCATDAC9144 DacPara { set { dacPara = value; } get { return dacPara; } }

        public CEtherCATDAC9144(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            dacPara = Parameter as ParameterCEtherCATDAC9144;

            slaveModuleInitialize();

            setDACSetting();
            //system scan
            FlowControl flowControl = ModulesFactory.FlowControlHelper.GetFlowControl("SYNTEKMotion");
            FlowBase flowBase = new FlowBase(this.DeviceName, systemScan);
            flowControl.AddFlowBase(flowBase);
            flowBase.Start();
            keyOfIOScan = true;
        }

        #region private method

        private void slaveModuleInitialize()
        {
            bool getSlave = false;
            foreach (var item in SlaveInfos)
            {
                SlaveInfo slaveInfo = item.Value;
                if ((dacPara.CardNo == slaveInfo.CardNo) && (dacPara.NodeNo == slaveInfo.NodeID))
                {
                    if (/*slaveInfo.VendorID == 0x1A05 && */slaveInfo.ProductCode == 0x9144) //Analog Output Terminals (R1-EC91xx)
                    {
                        getSlave = true;
                        break;
                    }
                }
            }
            if (!getSlave)
            {
                throw new NotImplementedException(string.Format("EtherCAT can't find slave [{0}] from Card No {1} Node No {2}. There have {3} SlaveInfos.", this.DeviceName, dacPara.CardNo, dacPara.NodeNo, SlaveInfos.Count));
            }
        }

        private void setDACSetting()
        {
            g_nModeValue[0] = 0;
            g_nModeValue[1] = 0;
            g_nModeValue[2] = -5;
            g_nModeValue[3] = -10;
            g_nModeValue[4] = 4;
            g_nModeValue[5] = 0;
            g_nModeValue[6] = 0;

            g_nModeLength[0] = 5;
            g_nModeLength[1] = 10;
            g_nModeLength[2] = 10;
            g_nModeLength[3] = 20;
            g_nModeLength[4] = 16;
            g_nModeLength[5] = 20;
            g_nModeLength[6] = 24;

            SetOutputRangeMode(DACChannel.CＨ0, dacPara.RangeModeCH0);
            SetOutputEnable(DACChannel.CＨ0, dacPara.EnableCH0);
            SetOutputRangeMode(DACChannel.CＨ1, dacPara.RangeModeCH1);
            SetOutputEnable(DACChannel.CＨ1, dacPara.EnableCH1);
            SetOutputRangeMode(DACChannel.CＨ2, dacPara.RangeModeCH2);
            SetOutputEnable(DACChannel.CＨ2, dacPara.EnableCH2);
            SetOutputRangeMode(DACChannel.CＨ3, dacPara.RangeModeCH3);
            SetOutputEnable(DACChannel.CＨ3, dacPara.EnableCH3);
        }

        //scan DAC slave status
        private void systemScan(FlowVar fv)
        {
            if (keyOfIOScan)
            {
                for (int ch = 0; ch < 4; ch++)
                {
                    Value[ch] = GetOutputValue((DACChannel)ch);

                    int nIndexMode = 0;
                    if (ch == 0)
                        nIndexMode = (int)dacPara.RangeModeCH0;
                    else if(ch == 1)
                        nIndexMode = (int)dacPara.RangeModeCH1;
                    else if (ch == 2)
                        nIndexMode = (int)dacPara.RangeModeCH2;
                    else
                        nIndexMode = (int)dacPara.RangeModeCH3;

                    double dTempData = Value[ch];
                    dTempData = g_nModeValue[nIndexMode] + (dTempData * g_nModeLength[nIndexMode] / 0xFFFF);
                    Voltage[ch] = dTempData;
                }
            }
        }
        #endregion private method

        #region public method

        public void SetOutputRangeMode(DACChannel channelNo, RangeMode rangeMode)
        {
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_R1_EC9144_Set_Output_RangeMode(dacPara.CardNo, dacPara.NodeNo, (ushort)channelNo, (ushort)rangeMode);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_R1_EC9144_Set_Output_RangeMode, " + GetEtherCATErrorCode(g_uRet));
            }
        }

        public void SetOutputEnable(DACChannel channelNo, bool enable)
        {
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_R1_EC9144_Set_Output_Enable(dacPara.CardNo, dacPara.NodeNo, (ushort)channelNo, (ushort)(enable ? 1 : 0));
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_R1_EC9144_Set_Output_Enable, " + GetEtherCATErrorCode(g_uRet));
            }
        }

        public ushort GetOutputReturnCode(DACChannel channelNo)
        {
            ushort returnCode = 0;
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_R1_EC9144_Get_Output_ReturnCode(dacPara.CardNo, dacPara.NodeNo, (ushort)channelNo, ref returnCode);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_R1_EC9144_Get_Output_ReturnCode, " + GetEtherCATErrorCode(g_uRet));
            }
            return returnCode;
        }

        public void SetOutputValue(DACChannel channelNo, ushort value)
        {
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_AIO_Set_Output_Value(dacPara.CardNo, dacPara.NodeNo, (ushort)channelNo, value);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_AIO_Set_Output_Value, " + GetEtherCATErrorCode(g_uRet));
            }
        }

        public ushort GetOutputValue(DACChannel channelNo)
        {
            ushort value = 0;
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_AIO_Get_Output_Value(dacPara.CardNo, dacPara.NodeNo, (ushort)channelNo, ref value);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_AIO_Get_Output_Value, " + GetEtherCATErrorCode(g_uRet));
            }
            return value;
        }

        public void ShowDialog()
        {
            CEtherCATDAC9144Form cEtherCATDAC9144Form = new CEtherCATDAC9144Form(this);
            cEtherCATDAC9144Form.Text = DeviceName;
            cEtherCATDAC9144Form.ShowDialog();
        }

        public void Show()
        {
            CEtherCATDAC9144Form cEtherCATDAC9144Form = new CEtherCATDAC9144Form(this);
            cEtherCATDAC9144Form.Text = DeviceName;
            cEtherCATDAC9144Form.Show();
        }

        #endregion public method
    }
}