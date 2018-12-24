using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L122.Library;
using AOISystem.Utilities.Modules.Syntek.L122.MasterCard;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using I16 = System.Int16;
using U16 = System.UInt16;
using U8 = System.Byte;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.ADC
{
    public class L122A180 : L122.MasterCard.L122
    {
        #region private field

        private Thread systemScanThread;
        private bool keyOfAdcScan;
        private ParameterL122A180 adcPara;

        #endregion private field

        #region public properties

        /// <summary> ADC的電壓值 </summary>
        public double[] Voltage = new double[8];

        /// <summary> ADC的換算過後的數值 </summary>
        public short[] Value = new short[8];

        /// <summary> ADC 參數 </summary>
        internal ParameterL122A180 AdcPara { get { return adcPara; } set { adcPara = value; } }

        #endregion public properties

        #region construct

        public L122A180(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            adcPara = Parameter as ParameterL122A180;
            adcPara.ParameterChanged += new Common.ParameterINI.ParameterChangedHandler((paraName) => { setADC(paraName); });
            slaveModuleInitialize();
            setADC();
            //system scan
            systemScanThread = new Thread(systemScan);
            systemScanThread.IsBackground = true;
            systemScanThread.Start();
            keyOfAdcScan = true;

            EnableDevice(CmdStatus.ON);
        }

        #endregion construct

        #region private method

        private void slaveModuleInitialize()
        {
            uint mask = 0x00000001;
            uint[] deviceTable = DeviceTable;
            if ((deviceTable[0] == 0) && (deviceTable[1] == 0))
            {
                throw new Exception("Can't find slave A180, there is not any device !!! \n func = [_mnet_get_ring_active_table, deviceTable=0]");
            }

            for (int i = 0; i < 64; i++)
            {
                if (adcPara.SlaveIP > 63)
                {
                    throw new Exception("Wrong SlaveIP, SlaveIP must less than 63!!!");
                }

                if (adcPara.SlaveIP == i)
                {
                    if (i < 32)
                    {
                        if ((deviceTable[0] & mask) == 0)
                        {
                            throw new Exception("Have not found A180 !!!\n" + "SlaveIP = " + adcPara.SlaveIP);
                        }
                    }
                    else
                    {
                        if (i == 32)
                            mask = 0x00000001;
                        if ((deviceTable[1] & mask) == 0)
                        {
                            throw new Exception("Have not found A180 !!!\n" + "SlaveIP = " + adcPara.SlaveIP);
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
            I16 retOfGetSlaveType = CCMNet.CS_mnet_get_slave_type(RingNoOfMNet, adcPara.SlaveIP, ref slaveType);
            if (retOfGetSlaveType == 0)
            {
                if (slaveType != 0xD1)
                {
                    throw new Exception("deviec type is not A180!!!\n" + "SlaveIP = " + adcPara.SlaveIP);
                }
            }
            else
            {
                throw new Exception("Error occur when get device type !!! \n func = [_mnet_get_slave_type]");
            }

            I16 rc = CADC_A180.CS_mnet_ai8_initial((U16)adcPara.CardSwitchNo, (U16)adcPara.SlaveIP);
            if (rc != 0)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(LanguageResourceManager.GetString("InitializeErrorA180ADC") + "(" + DeviceName + ")"));
            }

            rc = CADC_A180.CS_mnet_ai8_set_cycle_time((U16)adcPara.CardSwitchNo, (U16)adcPara.SlaveIP, 0);
            if (rc != 0)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(LanguageResourceManager.GetString("SetCycleErrorA180ADC") + "(" + DeviceName + ")"));
            }
        }

        private void setADC()
        {
            List<string> totalEnbchannel = new List<string>();
            totalEnbchannel.Add("EnableCH0");
            totalEnbchannel.Add("EnableCH1");
            totalEnbchannel.Add("EnableCH2");
            totalEnbchannel.Add("EnableCH3");
            totalEnbchannel.Add("EnableCH4");
            totalEnbchannel.Add("EnableCH5");
            totalEnbchannel.Add("EnableCH6");
            totalEnbchannel.Add("EnableCH7");
            foreach (string eachVar in totalEnbchannel)
            {
                setADC(eachVar);
            }

            List<string> totalAdcGain = new List<string>();
            totalAdcGain.Add("GainCH0");
            totalAdcGain.Add("GainCH1");
            totalAdcGain.Add("GainCH2");
            totalAdcGain.Add("GainCH3");
            totalAdcGain.Add("GainCH4");
            totalAdcGain.Add("GainCH5");
            totalAdcGain.Add("GainCH6");
            totalAdcGain.Add("GainCH7");
            foreach (string eachVar in totalAdcGain)
            {
                setADC(eachVar);
            }
        }

        private void setADC(string paraName)
        {
            List<string> totalEnbchannel = new List<string>();
            totalEnbchannel.Add("EnableCH0");
            totalEnbchannel.Add("EnableCH1");
            totalEnbchannel.Add("EnableCH2");
            totalEnbchannel.Add("EnableCH3");
            totalEnbchannel.Add("EnableCH4");
            totalEnbchannel.Add("EnableCH5");
            totalEnbchannel.Add("EnableCH6");
            totalEnbchannel.Add("EnableCH7");

            List<string> totalAdcGain = new List<string>();
            totalAdcGain.Add("GainCH0");
            totalAdcGain.Add("GainCH1");
            totalAdcGain.Add("GainCH2");
            totalAdcGain.Add("GainCH3");
            totalAdcGain.Add("GainCH4");
            totalAdcGain.Add("GainCH5");
            totalAdcGain.Add("GainCH6");
            totalAdcGain.Add("GainCH7");

            PropertyInfo[] pi = adcPara.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo p in pi)
            {
                if (paraName == p.Name)
                {
                    object val = p.GetValue(adcPara , null);
                    if (p.PropertyType.Name == "Boolean")
                    {
                        CADC_A180.CS_mnet_ai8_enable_channel((U16)adcPara.CardSwitchNo, (U16)adcPara.SlaveIP, (U16)totalEnbchannel.IndexOf(paraName), Convert.ToByte(val));
                    }
                    if (p.PropertyType.Name == "AdcGain")
                    {
                        CADC_A180.CS_mnet_ai8_set_channel_gain((U16)adcPara.CardSwitchNo, (U16)adcPara.SlaveIP, (U16)totalAdcGain.IndexOf(paraName), (U8)val);
                    }
                }
            }
        }

        private void systemScan()
        {
            while (true)
            {
                if (keyOfAdcScan)
                {
                    for (int i = 0 ; i < 8 ; i++)
                    {
                        CADC_A180.CS_mnet_ai8_get_voltage((U16)adcPara.CardSwitchNo, (U16)adcPara.SlaveIP, (U16)i, ref Voltage[i]);
                        CADC_A180.CS_mnet_ai8_get_value((U16)adcPara.CardSwitchNo, (U16)adcPara.SlaveIP, (U16)i, ref Value[i]);
                    }
                    Thread.Sleep(30);
                }
            }
        }

        #endregion private method

        #region public method

        public bool EnableDevice(CmdStatus status)
        {
            I16 rc = CADC_A180.CS_mnet_ai8_enable_device((U16)adcPara.CardSwitchNo, (U16)adcPara.SlaveIP, (byte)status);
            if (rc != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool SetChannelEnable(ADCChannel channelNo, bool enable)
        {
            I16 rc = CADC_A180.CS_mnet_ai8_enable_channel((U16)adcPara.CardSwitchNo, (U16)adcPara.SlaveIP, (U16)channelNo, Convert.ToByte(enable));
            if (rc != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool SetChannelGain(ADCChannel ChannelNo , ADCGain Gain)
        {
            I16 rc = CADC_A180.CS_mnet_ai8_set_channel_gain((U16)adcPara.CardSwitchNo, (U16)adcPara.SlaveIP, (U16)ChannelNo, (U8)Gain);
            if (rc != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ShowDialog()
        {
            L122A180Form frmA180ADC = new L122A180Form(this);
            frmA180ADC.Text = DeviceName;
            frmA180ADC.ShowDialog();
        }

        public void Show()
        {
            L122A180Form frmA180ADC = new L122A180Form(this);
            frmA180ADC.Text = DeviceName;
            frmA180ADC.Show();
        }

        #endregion public method
    }
}