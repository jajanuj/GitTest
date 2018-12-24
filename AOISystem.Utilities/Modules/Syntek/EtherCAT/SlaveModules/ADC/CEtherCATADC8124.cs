using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.Library;
using AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard;
using System;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.ADC
{
    public enum ADCChannel : ushort
    {
        CＨ0 = 0,
        CＨ1 = 0,
        CＨ2 = 0,
        CＨ3 = 0
    }

    public enum ADCRangeMode : ushort
    {
        Positive_Negative_5V = 0,
        Positive_Negative_10V = 1
    }

    public enum ConvstFreqMode : ushort
    {
        Hz_200000 = 0,
        Hz_100000 = 1,
        Hz_50000 = 2,
        Hz_25000 = 3,
        Hz_12500 = 4,
        Hz_6250 = 5,
        Hz_3125 = 6
    }

    public class CEtherCATADC8124 : CEtherCAT
    {
        private bool keyOfIOScan;

        private ParameterCEtherCATADC8124 adcPara;

        private int[] g_nModeValue = new int[2];
        private int[] g_nModeLength = new int[2];

        /// <summary> ADC的電壓值 </summary>
        public double[] Voltage = new double[4];

        /// <summary> ADC的換算過後的數值 </summary>
        public ushort[] Value = new ushort[4];

        /// <summary> ADC 參數 </summary>
        public ParameterCEtherCATADC8124 AdcPara { set { adcPara = value; } get { return adcPara; } }

        public CEtherCATADC8124(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            adcPara = Parameter as ParameterCEtherCATADC8124;

            slaveModuleInitialize();

            ADCInitialize();
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
                if ((adcPara.CardNo == slaveInfo.CardNo) && (adcPara.NodeNo == slaveInfo.NodeID))
                {
                    if (/*slaveInfo.VendorID == 0x1A05 && */slaveInfo.ProductCode == 0x8124) //Analog Input Terminals (R1-EC81xx)
                    {
                        getSlave = true;
                        break;
                    }
                }
            }
            if (!getSlave)
            {
                throw new NotImplementedException(string.Format("EtherCAT can't find slave [{0}] from Card No {1} Node No {2}. There have {3} SlaveInfos.", this.DeviceName, adcPara.CardNo, adcPara.NodeNo, SlaveInfos.Count));
            }
        }

        //scan ADC slave status
        private void systemScan(FlowVar fv)
        {
            if (keyOfIOScan)
            {
                for (int ch = 0; ch < 4; ch++)
                {
                    Value[ch] = GetInputValue((ADCChannel)ch);

                    int nIndexMode = 0;
                    if (ch == 0)
                        nIndexMode = (int)adcPara.ADCRangeModeCH0;
                    else if(ch == 1)
                        nIndexMode = (int)adcPara.ADCRangeModeCH1;
                    else if (ch == 2)
                        nIndexMode = (int)adcPara.ADCRangeModeCH2;
                    else
                        nIndexMode = (int)adcPara.ADCRangeModeCH3;

                    //double dTempData = Value[ch] + 0x8000;
                    //if (dTempData >= 0x10000)
                    //    dTempData -= 0x10000;
                    double dTempData = Value[ch];

                    dTempData = g_nModeValue[nIndexMode] + (dTempData * g_nModeLength[nIndexMode] / 0xFFFF);
                    Voltage[ch] = dTempData;
                }
            }
        }

        #endregion private method

        #region public method

        public void ADCInitialize()
        {
            g_nModeValue[0] = -5;
            g_nModeValue[1] = -10;

            g_nModeLength[0] = 10;
            g_nModeLength[1] = 20;

            SetInputRangeMode(ADCChannel.CＨ0, adcPara.ADCRangeModeCH0);
            SetInputConvstFreqMode(ADCChannel.CＨ0, adcPara.ConvstFreqModeCH0);
            SetInpuAverageMode(ADCChannel.CＨ0, adcPara.AverageTimesCH0);
            SetInputEnable(ADCChannel.CＨ0, adcPara.EnableCH0);
            SetInputRangeMode(ADCChannel.CＨ1, adcPara.ADCRangeModeCH1);
            SetInputConvstFreqMode(ADCChannel.CＨ1, adcPara.ConvstFreqModeCH1);
            SetInpuAverageMode(ADCChannel.CＨ1, adcPara.AverageTimesCH1);
            SetInputEnable(ADCChannel.CＨ1, adcPara.EnableCH1);
            SetInputRangeMode(ADCChannel.CＨ2, adcPara.ADCRangeModeCH2);
            SetInputConvstFreqMode(ADCChannel.CＨ2, adcPara.ConvstFreqModeCH2);
            SetInpuAverageMode(ADCChannel.CＨ2, adcPara.AverageTimesCH2);
            SetInputEnable(ADCChannel.CＨ2, adcPara.EnableCH2);
            SetInputRangeMode(ADCChannel.CＨ3, adcPara.ADCRangeModeCH3);
            SetInputConvstFreqMode(ADCChannel.CＨ3, adcPara.ConvstFreqModeCH3);
            SetInpuAverageMode(ADCChannel.CＨ3, adcPara.AverageTimesCH3);
            SetInputEnable(ADCChannel.CＨ3, adcPara.EnableCH3);
        }

        public void SetInputRangeMode(ADCChannel channelNo, ADCRangeMode rangeMode)
        {
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_R1_EC8124_Set_Input_RangeMode(adcPara.CardNo, adcPara.NodeNo, (ushort)channelNo, (ushort)rangeMode);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_R1_EC8124_Set_Input_RangeMode, " + GetEtherCATErrorCode(g_uRet));
            }
        }

        public ushort GetInputRangeMode(ADCChannel channelNo)
        {
            ushort rangeMode = 0;
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_R1_EC8124_Get_Input_RangeMode(adcPara.CardNo, adcPara.NodeNo, (ushort)channelNo, ref rangeMode);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_R1_EC8124_Set_Input_RangeMode, " + GetEtherCATErrorCode(g_uRet));
            }
            return rangeMode;
        }

        public void SetInputConvstFreqMode(ADCChannel channelNo, ConvstFreqMode rangeMode)
        {
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_ESC8124_Set_Input_ConvstFreq_Mode(adcPara.CardNo, adcPara.NodeNo, (ushort)channelNo, (ushort)rangeMode);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_R1_EC8124_Set_Input_RangeMode, " + GetEtherCATErrorCode(g_uRet));
            }
        }

        public void SetInputEnable(ADCChannel channelNo, bool enable)
        {
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_R1_EC8124_Set_Input_Enable(adcPara.CardNo, adcPara.NodeNo, (ushort)channelNo, (ushort)(enable ? 1 : 0));
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_R1_EC8124_Set_Input_Enable, " + GetEtherCATErrorCode(g_uRet));
            }
        }

        public void SetInpuAverageMode(ADCChannel channelNo, ushort averageTimes)
        {
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_ESC8124_Set_Input_AverageMode(adcPara.CardNo, adcPara.NodeNo, (ushort)channelNo, averageTimes);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_ESC8124_Set_Input_AverageMode, " + GetEtherCATErrorCode(g_uRet));
            }
        }

        public ushort GetInputValue(ADCChannel channelNo)
        {
            ushort value = 0;
            ushort g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_AIO_Get_Input_Value(adcPara.CardNo, adcPara.NodeNo, (ushort)channelNo, ref value);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                throw new Exception("CS_ECAT_Slave_AIO_Get_Input_Value, " + GetEtherCATErrorCode(g_uRet));
            }
            return value;
        }

        public void ShowDialog()
        {
            CEtherCATADC8124Form cEtherCATADC8124Form = new CEtherCATADC8124Form(this);
            cEtherCATADC8124Form.Text = DeviceName;
            cEtherCATADC8124Form.ShowDialog();
        }

        public void Show()
        {
            CEtherCATADC8124Form cEtherCATADC8124Form = new CEtherCATADC8124Form(this);
            cEtherCATADC8124Form.Text = DeviceName;
            cEtherCATADC8124Form.Show();
        }

        #endregion public method
    }
}