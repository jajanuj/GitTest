using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L132.Library;
using AOISystem.Utilities.Modules.Syntek.L132.MasterCard;
using AOISystem.Utilities.Resources;
using System;
using I16 = System.Int16;
using U16 = System.UInt16;
using U8 = System.Byte;

namespace AOISystem.Utilities.Modules.Syntek.L132.SlaveModules.DAC
{
    public class L132A104DAC : L132.MasterCard.L132
    {
        private ParameterL132A104 dacPara;
        private double[] voltage;

        internal ParameterL132A104 DacPara { get { return dacPara; } set { dacPara = value; } }

        public double[] Voltage { get { return voltage; } set { voltage = value; } }

        public delegate void VoltageHandler(double value);

        public event VoltageHandler VoltageChangedCN0;

        public event VoltageHandler VoltageChangedCN1;

        public event VoltageHandler VoltageChangedCN2;

        public event VoltageHandler VoltageChangedCN3;

        public L132A104DAC(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            dacPara = Parameter as ParameterL132A104;
            slaveModuleInitialize();
            voltage = new double[] { 0 , 0 , 0 , 0 };
            ResetDAC();
        }

        private void slaveModuleInitialize()
        {
            I16 rc;
            uint mask = 0x00000001;
            uint[] deviceTable = new uint[2];
            rc = CMNET_L132.CS_mnet_get_ring_active_table((U16)dacPara.CardSwitchNo , (U16)dacPara.RingNoOfCard , ref deviceTable[0]);
            if (rc != 0)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("DeviceTableError") + "(" + DeviceName + ")"));
            }
            if ((deviceTable[0] == 0) && (deviceTable[1] == 0))
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("DidntFindA104DAC") + "(" + DeviceName + ")"));
            }

            for (int i = 0 ; i < 64 ; i++)
            {
                if (dacPara.SlaveIP > 63)
                {
                    throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongSlaveIP") + "(" + DeviceName + ")"));
                }

                if (dacPara.SlaveIP == i)
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

            U8 slaveType = 0;
            rc = CMNET_L132.CS_mnet_get_slave_type((U16)dacPara.CardSwitchNo , (U16)dacPara.RingNoOfCard , (U16)dacPara.SlaveIP , ref slaveType);
            if (rc == 0)
            {
                if (slaveType != 0xD0)
                {
                    throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("WrongDeviceTypeA104DAC") + "(" + DeviceName + ")"));
                }
            }
            else
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("DeviceTableError") + "(" + DeviceName + ")"));
            }

            rc = CA104_L132.CS_mnet_ao4_initial((U16)dacPara.CardSwitchNo , (U16)dacPara.RingNoOfCard , (U16)dacPara.SlaveIP);
            if (rc != 0)
            {
                throw new Exception(ExceptionHelper.GetFullCurrentMethod(ResourceHelper.Language.GetString("InitializeErrorA104DAC") + "(" + DeviceName + ")"));
            }
        }

        /// <summary>
        /// 設定Channel輸出電壓 -10.0v ~ +10.0v
        /// </summary>
        /// <param name="channel">The Channel No</param>
        /// <param name="voltage">The Voltage</param>
        /// <returns>設定結果</returns>
        public bool SetVoltage(DACChannel channelNo , double setVoltage)
        {
            double lastValue = 0;
            if (setVoltage <= dacPara.MaxVoltage[(int)channelNo] && setVoltage >= dacPara.MinVoltage[(int)channelNo])
            {
                lastValue = setVoltage;
            }
            else
            {
                if (setVoltage > dacPara.MaxVoltage[(int)channelNo])
                {
                    lastValue = dacPara.MaxVoltage[(int)channelNo];
                }
                if (setVoltage < dacPara.MinVoltage[(int)channelNo])
                {
                    lastValue = dacPara.MinVoltage[(int)channelNo];
                }
            }

            I16 rc = CA104_L132.CS_mnet_ao4_set_voltage((U16)dacPara.CardSwitchNo , (U16)dacPara.RingNoOfCard , (U16)dacPara.SlaveIP , (byte)channelNo , lastValue);
            if (rc != 0)
            {
                return false;
            }
            else
            {
                voltage[(int)channelNo] = lastValue;

                switch (channelNo)
                {
                    case DACChannel.CN0:
                        if (VoltageChangedCN0 != null)
                        {
                            VoltageChangedCN0(lastValue);
                        }
                        break;

                    case DACChannel.CN1:
                        if (VoltageChangedCN1 != null)
                        {
                            VoltageChangedCN1(lastValue);
                        }
                        break;

                    case DACChannel.CN2:
                        if (VoltageChangedCN2 != null)
                        {
                            VoltageChangedCN2(lastValue);
                        }
                        break;

                    case DACChannel.CN3:
                        if (VoltageChangedCN3 != null)
                        {
                            VoltageChangedCN3(lastValue);
                        }
                        break;
                }
                return true;
            }
        }

        ///// <summary>
        ///// 設定所有Channel輸出電壓  -10.0v ~ +10.0v
        ///// </summary>
        ///// <param name="voltageCN0">The voltage of channel 0</param>
        ///// <param name="voltageCN1">The voltage of channel 1</param>
        ///// <param name="voltageCN2">The voltage of channel 2</param>
        ///// <param name="voltageCN3">The voltage of channel 3</param>
        ///// <returns>設定結果</returns>
        //public bool SetVoltageAll(double voltageCN0, double voltageCN1, double voltageCN2, double voltageCN3)
        //{
        //    double[] lastValue = new double[4];

        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (voltage[i] > dacPara.MaxVoltage[i])
        //        {
        //            voltage[i] = dacPara.MaxVoltage[i];
        //        }

        //        if (voltage[i] < dacPara.MinVoltage[i])
        //        {
        //            voltage[i] = dacPara.MinVoltage[i];
        //        }
        //    }

        //    if (voltage[(int)channel] > dacPara.MaxVoltage[(int)channel])
        //    {
        //        lastValue = dacPara.MaxVoltage[(int)channel];
        //    }
        //    else
        //    {
        //        lastValue = voltage;
        //    }

        //    if (voltage[(int)channel] < dacPara.MinVoltage[(int)channel])
        //    {
        //        lastValue = dacPara.MinVoltage[(int)channel];
        //    }
        //    else
        //    {
        //        lastValue = voltage;
        //    }

        //    I16 rc = CA104_L132.CS_mnet_ao4_set_voltage_all((U16)dacPara.CardSwitchNo, (U16)dacPara.RingNoOfCard, (U16)dacPara.SlaveIP, voltageCN0, voltageCN1, voltageCN2, voltageCN3);
        //    if (rc != 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        voltage[0] = voltageCN0;
        //        voltage[1] = voltageCN1;
        //        voltage[2] = voltageCN2;
        //        voltage[3] = voltageCN3;

        //        if (VoltageChangedCN0 != null)
        //        {
        //            VoltageChangedCN0(voltageCN0);
        //        }
        //        if (VoltageChangedCN1 != null)
        //        {
        //            VoltageChangedCN1(voltageCN1);
        //        }
        //        if (VoltageChangedCN2 != null)
        //        {
        //            VoltageChangedCN2(voltageCN2);
        //        }
        //        if (VoltageChangedCN3 != null)
        //        {
        //            VoltageChangedCN3(voltageCN3);
        //        }

        //        return true;
        //    }
        //}

        /// <summary>
        /// Reset A104 DAC module
        /// </summary>
        /// <returns>the result of setting</returns>
        public bool ResetDAC()
        {
            I16 rc = CA104_L132.CS_mnet_ao4_reset_DAC((U16)dacPara.CardSwitchNo , (U16)dacPara.RingNoOfCard , (U16)dacPara.SlaveIP);
            if (rc != 0)
            {
                return false;
            }
            else
            {
                voltage[0] = 0;
                voltage[1] = 0;
                voltage[2] = 0;
                voltage[3] = 0;

                if (VoltageChangedCN0 != null)
                {
                    VoltageChangedCN0(0);
                }
                if (VoltageChangedCN1 != null)
                {
                    VoltageChangedCN1(0);
                }
                if (VoltageChangedCN2 != null)
                {
                    VoltageChangedCN2(0);
                }
                if (VoltageChangedCN3 != null)
                {
                    VoltageChangedCN3(0);
                }
                return true;
            }
        }

        ///// <summary>
        ///// 設定Channel輸出值  32767 ~ -32768
        ///// </summary>
        ///// <param name="channel">The Channel No</param>
        ///// <param name="voltage">The Output</param>
        ///// <returns>設定結果</returns>
        //public bool SetOutput(DACChannel channel, I16 setValue)
        //{
        //    I16 rc = CA104_L132.CS_mnet_ao4_set_output((U16)dacPara.CardSwitchNo, (U16)dacPara.RingNoOfCard, (U16)dacPara.SlaveIP, (byte)channel, setValue);
        //    if (rc != 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        ///// <summary>
        ///// 設定所有Channel輸出值  32767 ~ -32768
        ///// </summary>
        ///// <param name="valueCN0">The output of channel 0</param>
        ///// <param name="valueCN1">The output of channel 1</param>
        ///// <param name="valueCN2">The output of channel 2</param>
        ///// <param name="valueCN3">The output of channel 3</param>
        ///// <returns>設定結果</returns>
        //public bool SetOutputAll(I16 valueCN0, I16 valueCN1, I16 valueCN2, I16 valueCN3)
        //{
        //    I16 rc = CA104_L132.CS_mnet_ao4_set_output_all((U16)dacPara.CardSwitchNo, (U16)dacPara.RingNoOfCard, (U16)dacPara.SlaveIP, valueCN0, valueCN1, valueCN2, valueCN3);
        //    if (rc != 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}