using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L132.Library;
using AOISystem.Utilities.Modules.Syntek.L132.MasterCard;
using AOISystem.Utilities.Resources;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using I16 = System.Int16;
using U16 = System.UInt16;
using U8 = System.Byte;

namespace AOISystem.Utilities.Modules.Syntek.L132.SlaveModules.ADC
{
    public class L132A180ADC : L132.MasterCard.L132
    {
        #region private field

        private Thread systemScanThread;
        private bool keyOfAdcScan;
        private ParameterL132A180 adcPara;

        #endregion private field

        #region public properties

        /// <summary> ADC的電壓值 </summary>
        public double[] Voltage = new double[8];

        /// <summary> ADC的換算過後的數值 </summary>
        public short[] Value = new short[8];

        /// <summary> ADC 參數 </summary>
        internal ParameterL132A180 AdcPara { get { return adcPara; } set { adcPara = value; } }

        #endregion public properties

        #region construct

        public L132A180ADC(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            adcPara = Parameter as ParameterL132A180;
            adcPara.ParameterChanged += new ParameterINI.ParameterChangedHandler((paraName) => { setADC(paraName); });
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
            I16 rc;
            uint mask = 0x00000001;
            uint[] deviceTable = new uint[2];
            rc = CMNET_L132.CS_mnet_get_ring_active_table((U16)adcPara.CardSwitchNo , (U16)adcPara.RingNoOfCard , ref deviceTable[0]);
            if (rc != 0)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("DeviceTableError") + "(" + DeviceName + ")"));
            }
            if ((deviceTable[0] == 0) && (deviceTable[1] == 0))
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("DidntFindA180ADC") + "(" + DeviceName + ")"));
            }

            for (int i = 0 ; i < 64 ; i++)
            {
                if (adcPara.SlaveIP > 63)
                {
                    throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongSlaveIP") + "(" + DeviceName + ")"));
                }

                if (adcPara.SlaveIP == i)
                {
                    if (i < 32)
                    {
                        if ((deviceTable[0] & mask) == 0)
                        {
                            throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongSlaveIP") + "(" + DeviceName + ")"));
                        }
                    }
                    else
                    {
                        if (i == 32)
                            mask = 0x00000001;
                        if ((deviceTable[1] & mask) == 0)
                        {
                            throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongSlaveIP") + "(" + DeviceName + ")"));
                        }
                    }
                }
                mask = mask << 1;
            }

            //CMNET_L132.CS_mnet_start_ring((U16)adcPara.CardSwitchNo, (U16)adcPara.RingNoOfCard);

            U8 slaveType = 0;
            rc = CMNET_L132.CS_mnet_get_slave_type((U16)adcPara.CardSwitchNo , (U16)adcPara.RingNoOfCard , (U16)adcPara.SlaveIP , ref slaveType);
            if (rc == 0)
            {
                if (slaveType != 0xD1)
                {
                    throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongDeviceTypeA180ADC") + "(" + DeviceName + ")"));
                }
            }
            else
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("DeviceTableError") + "(" + DeviceName + ")"));
            }

            rc = CA180_L132.CS_mnet_ai8_initial((U16)adcPara.CardSwitchNo , (U16)adcPara.RingNoOfCard , (U16)adcPara.SlaveIP);
            if (rc != 0)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("InitializeErrorA180ADC") + "(" + DeviceName + ")"));
            }

            rc = CA180_L132.CS_mnet_ai8_set_cycle_time((U16)adcPara.CardSwitchNo , (U16)adcPara.RingNoOfCard , (U16)adcPara.SlaveIP , 0);
            if (rc != 0)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("SetCycleErrorA180ADC") + "(" + DeviceName + ")"));
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
                    if (p.PropertyType.Name == "AdcChannelEnable")
                    {
                        CA180_L132.CS_mnet_ai8_enable_channel((U16)adcPara.CardSwitchNo , (U16)adcPara.RingNoOfCard , (U16)adcPara.SlaveIP , (U16)totalEnbchannel.IndexOf(paraName) , (U8)val);
                    }
                    if (p.PropertyType.Name == "AdcGain")
                    {
                        CA180_L132.CS_mnet_ai8_set_channel_gain((U16)adcPara.CardSwitchNo , (U16)adcPara.RingNoOfCard , (U16)adcPara.SlaveIP , (U16)totalAdcGain.IndexOf(paraName) , (U8)val);
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
                        CA180_L132.CS_mnet_ai8_get_voltage((U16)adcPara.CardSwitchNo , (U16)adcPara.RingNoOfCard , (U16)adcPara.SlaveIP , (U16)i , ref Voltage[i]);
                        CA180_L132.CS_mnet_ai8_get_value((U16)adcPara.CardSwitchNo , (U16)adcPara.RingNoOfCard , (U16)adcPara.SlaveIP , (U16)i , ref Value[i]);
                    }
                    Thread.Sleep(30);
                }
            }
        }

        #endregion private method

        #region public method

        public bool EnableDevice(CmdStatus status)
        {
            I16 rc = CA180_L132.CS_mnet_ai8_enable_device((U16)adcPara.CardSwitchNo , (U16)adcPara.RingNoOfCard , (U16)adcPara.SlaveIP , (byte)status);
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
            I16 rc = CA180_L132.CS_mnet_ai8_set_channel_gain((U16)adcPara.CardSwitchNo , (U16)adcPara.RingNoOfCard , (U16)adcPara.SlaveIP , (U16)ChannelNo , (U8)Gain);
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
            L132A180ADCForm frmA180ADC = new L132A180ADCForm(this);
            frmA180ADC.Text = DeviceName;
            frmA180ADC.ShowDialog();
        }

        #endregion public method
    }
}