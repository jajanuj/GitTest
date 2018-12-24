using AOISystem.Utilities.Common;
using AOISystem.Utilities.Modules.Syntek.L122.Library;
using AOISystem.Utilities.Modules.Syntek.L122.MasterCard;
using AOISystem.Utilities.Resources;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using I16 = System.Int16;
using U16 = System.UInt16;
using U8 = System.Byte;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.DAC
{
    public class L122A104 : L122.MasterCard.L122
    {
        private ParameterL122A104 dacPara;

        internal ParameterL122A104 DacPara { get { return dacPara; } set { dacPara = value; } }

        private double _voltageCN0 = 0;
        /// <summary> CN0 電壓值 default value=0v </summary>
        [Browsable(true), Description("CN0 電壓值")]
        public double VoltageCN0
        {
            get { return _voltageCN0; }
            set
            {
                _voltageCN0 = Math.Min(value, this.DacPara.MaxVoltageCN0);
                _voltageCN0 = Math.Max(value, this.DacPara.MinVoltageCN0);
            }
        }

        private double _voltageCN1 = 0;
        /// <summary> CN1 電壓值 default value=0v </summary>
        [Browsable(true), Description("CN1 電壓值")]
        public double VoltageCN1
        {
            get { return _voltageCN1; }
            set
            {
                _voltageCN1 = Math.Min(value, this.DacPara.MaxVoltageCN1);
                _voltageCN1 = Math.Max(value, this.DacPara.MinVoltageCN1);
            }
        }

        private double _voltageCN2 = 0;
        /// <summary> CN2 電壓值 default value=0v </summary>
        [Browsable(true), Description("CN2 電壓值")]
        public double VoltageCN2
        {
            get { return _voltageCN2; }
            set
            {
                _voltageCN2 = Math.Min(value, this.DacPara.MaxVoltageCN2);
                _voltageCN2 = Math.Max(value, this.DacPara.MinVoltageCN2);
            }
        }

        private double _voltageCN3 = 0;
        /// <summary> CN3 電壓值 default value=0v </summary>
        [Browsable(true), Description("CN3 電壓值")]
        public double VoltageCN3
        {
            get { return _voltageCN3; }
            set
            {
                _voltageCN3 = Math.Min(value, this.DacPara.MaxVoltageCN3);
                _voltageCN3 = Math.Max(value, this.DacPara.MinVoltageCN3);
            }
        }

        public L122A104(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            dacPara = Parameter as ParameterL122A104;
            slaveModuleInitialize();
            ResetDAC();
        }

        private void slaveModuleInitialize()
        {
            uint mask = 0x00000001;
            uint[] deviceTable = DeviceTable;
            if ((deviceTable[0] == 0) && (deviceTable[1] == 0))
            {
                throw new Exception("Can't find slave A104, there is not any device !!! \n func = [_mnet_get_ring_active_table, deviceTable=0]");
            }

            for (int i = 0; i < 64; i++)
            {
                if (dacPara.SlaveIP > 63)
                {
                    throw new Exception("Wrong SlaveIP, SlaveIP must less than 63!!!");
                }

                if (dacPara.SlaveIP == i)
                {
                    if (i < 32)
                    {
                        if ((deviceTable[0] & mask) == 0)
                        {
                            throw new Exception("Have not found A104 !!!\n" + "SlaveIP = " + dacPara.SlaveIP);
                        }
                    }
                    else
                    {
                        if (i == 32)
                            mask = 0x00000001;
                        if ((deviceTable[1] & mask) == 0)
                        {
                            throw new Exception("Have not found A104 !!!\n" + "SlaveIP = " + dacPara.SlaveIP);
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
            I16 retOfGetSlaveType = CCMNet.CS_mnet_get_slave_type(RingNoOfMNet, dacPara.SlaveIP, ref slaveType);
            if (retOfGetSlaveType == 0)
            {
                if (slaveType != 0xD0)
                {
                    throw new Exception("deviec type is not A104!!!\n" + "SlaveIP = " + dacPara.SlaveIP);
                }
            }
            else
            {
                throw new Exception("Error occur when get device type !!! \n func = [_mnet_get_slave_type]");
            }

            I16 retOfInitial = CDAC_A104.CS_mnet_ao4_initial((U16)dacPara.RingNoOfCard, (U16)dacPara.SlaveIP);
            if (retOfInitial != 0)
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
        public bool SetVoltage(DACChannel channel, double voltage)
        {
            SetVoltageProperty(channel, voltage);
            double lastValue = GetVoltageProperty(channel);

            I16 rc = CDAC_A104.CS_mnet_ao4_set_voltage((U16)dacPara.RingNoOfCard, (U16)dacPara.SlaveIP, (byte)channel, lastValue);
            return rc == 0;
        }

        /// <summary>
        /// 設定所有Channel輸出電壓  -10.0v ~ +10.0v
        /// </summary>
        /// <param name="voltageCN0">The voltage of channel 0</param>
        /// <param name="voltageCN1">The voltage of channel 1</param>
        /// <param name="voltageCN2">The voltage of channel 2</param>
        /// <param name="voltageCN3">The voltage of channel 3</param>
        /// <returns>設定結果</returns>
        public bool SetVoltageAll(double voltageCN0, double voltageCN1, double voltageCN2, double voltageCN3)
        {
            this.VoltageCN0 = voltageCN0;
            this.VoltageCN1 = voltageCN1;
            this.VoltageCN2 = voltageCN2;
            this.VoltageCN3 = voltageCN3;

            I16 rc = CDAC_A104.CS_mnet_ao4_set_voltage_all((U16)dacPara.RingNoOfCard, (U16)dacPara.SlaveIP, this.VoltageCN0, this.VoltageCN1, this.VoltageCN2, this.VoltageCN3);
            return rc == 0;
        }

        /// <summary>
        /// Reset A104 DAC module
        /// </summary>
        /// <returns>the result of setting</returns>
        public bool ResetDAC()
        {
            I16 rc = CDAC_A104.CS_mnet_ao4_reset_DAC((U16)dacPara.RingNoOfCard, (U16)dacPara.SlaveIP);
            if (rc != 0)
            {
                return false;
            }
            else
            {
                this.VoltageCN0 = 0;
                this.VoltageCN1 = 0;
                this.VoltageCN2 = 0;
                this.VoltageCN3 = 0;

                return true;
            }
        }

        /// <summary>
        /// Shows the A104 Configuration Dialog.
        /// </summary>
        public DialogResult ConfigurationShowDialog()
        {
            L122A104Form l122A104DACForm = new L122A104Form(this);
            l122A104DACForm.Text = DeviceName;
            return l122A104DACForm.ShowDialog();
        }

        /// <summary>
        /// Shows the A104 Configuration.
        /// </summary>
        public void ConfigurationShow(FormClosingEventHandler action = null)
        {
            L122A104Form l122A104DACForm = new L122A104Form(this);
            l122A104DACForm.Text = DeviceName;
            l122A104DACForm.Show();
            if (action != null)
            {
                l122A104DACForm.FormClosing += action;
            }
        }

        private void SetVoltageProperty(DACChannel channel, double voltage)
        {
            switch (channel)
            {
                case DACChannel.CN0:
                    this.VoltageCN0 = voltage;
                    break;
                case DACChannel.CN1:
                    this.VoltageCN1 = voltage;
                    break;
                case DACChannel.CN2:
                    this.VoltageCN2 = voltage;
                    break;
                case DACChannel.CN3:
                    this.VoltageCN3 = voltage;
                    break;
            }
        }

        private double GetVoltageProperty(DACChannel channel)
        {
            double voltage = 0;
            switch (channel)
            {
                case DACChannel.CN0:
                    voltage = this.VoltageCN0;
                    break;
                case DACChannel.CN1:
                    voltage = this.VoltageCN1;
                    break;
                case DACChannel.CN2:
                    voltage = this.VoltageCN2;
                    break;
                case DACChannel.CN3:
                    voltage = this.VoltageCN3;
                    break;
            }
            return voltage;
        }
    }
}
