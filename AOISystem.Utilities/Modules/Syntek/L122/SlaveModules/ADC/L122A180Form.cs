using System;
using System.Reflection;
using System.Windows.Forms;
using AOISystem.Utilities.Forms;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.ADC
{
    public partial class L122A180Form : Form
    {
        private L122A180 adc;
        private bool initFinished;

        private L122A180Form()
        {
            InitializeComponent();
        }

        public L122A180Form(L122A180 adc)
        {
            InitializeComponent();
            this.adc = adc;
        }

        private void controlsInit()
        {
            lbtnCH0.Tag = "EnableCH0";
            lbtnCH1.Tag = "EnableCH1";
            lbtnCH2.Tag = "EnableCH2";
            lbtnCH3.Tag = "EnableCH3";
            lbtnCH4.Tag = "EnableCH4";
            lbtnCH5.Tag = "EnableCH5";
            lbtnCH6.Tag = "EnableCH6";
            lbtnCH7.Tag = "EnableCH7";

            cmbGainCH0.Tag = "GainCH0";
            cmbGainCH1.Tag = "GainCH1";
            cmbGainCH2.Tag = "GainCH2";
            cmbGainCH3.Tag = "GainCH3";
            cmbGainCH4.Tag = "GainCH4";
            cmbGainCH5.Tag = "GainCH5";
            cmbGainCH6.Tag = "GainCH6";
            cmbGainCH7.Tag = "GainCH7";

            cmbGainCH0.SelectedIndex = Convert.ToInt32(adc.AdcPara.GainCH0);
            cmbGainCH1.SelectedIndex = Convert.ToInt32(adc.AdcPara.GainCH1);
            cmbGainCH2.SelectedIndex = Convert.ToInt32(adc.AdcPara.GainCH2);
            cmbGainCH3.SelectedIndex = Convert.ToInt32(adc.AdcPara.GainCH3);
            cmbGainCH4.SelectedIndex = Convert.ToInt32(adc.AdcPara.GainCH4);
            cmbGainCH5.SelectedIndex = Convert.ToInt32(adc.AdcPara.GainCH5);
            cmbGainCH6.SelectedIndex = Convert.ToInt32(adc.AdcPara.GainCH6);
            cmbGainCH7.SelectedIndex = Convert.ToInt32(adc.AdcPara.GainCH7);
        }

        private void FrmA180ADC_Load(object sender , EventArgs e)
        {
            controlsInit();
            adc.EnableDevice(CmdStatus.OFF);
            lbtnDevEnable.Active = false;
            initFinished = true;
        }

        private void FrmA180ADC_FormClosed(object sender , FormClosedEventArgs e)
        {
            adc.EnableDevice(CmdStatus.ON);
        }

        private void lbtnDevEnable_Click(object sender , EventArgs e)
        {
            if (lbtnDevEnable.Active == false)
            {
                lbtnDevEnable.Active = true;
                adc.EnableDevice(CmdStatus.ON);
                panel1.Enabled = false;
                panel2.Enabled = false;
            }
            else
            {
                lbtnDevEnable.Active = false;
                adc.EnableDevice(CmdStatus.OFF);
                panel1.Enabled = true;
                panel2.Enabled = true;
            }
        }

        private void lbtnEnable_MouseClick(object sender , MouseEventArgs e)
        {
            PropertyInfo[] pi = adc.AdcPara.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo p in pi)
            {
                if (((LedButton)sender).Tag.ToString() == p.Name)
                {
                    //byte val = Convert.ToByte();
                    //if (p.PropertyType.BaseType.Name == "Enum")
                    //    p.SetValue(adc.AdcPara , Enum.Parse(p.PropertyType , val.ToString()) , null);
                    p.SetValue(adc.AdcPara, !((LedButton)sender).Active, null);
                }
            }
        }

        private void cmbGain_SelectedIndexChanged(object sender , EventArgs e)
        {
            if (initFinished == true)
            {
                PropertyInfo[] pi = adc.AdcPara.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo p in pi)
                {
                    if (((ComboBox)sender).Tag.ToString() == p.Name)
                    {
                        int val = ((ComboBox)sender).SelectedIndex;
                        if (p.PropertyType.BaseType.Name == "Enum")
                            p.SetValue(adc.AdcPara , Enum.Parse(p.PropertyType , val.ToString()) , null);
                    }
                }
            }
        }

        private void timer1_Tick(object sender , EventArgs e)
        {
            txbValueCH0.Text = adc.Value[0].ToString();
            txbValueCH1.Text = adc.Value[1].ToString();
            txbValueCH2.Text = adc.Value[2].ToString();
            txbValueCH3.Text = adc.Value[3].ToString();
            txbValueCH4.Text = adc.Value[4].ToString();
            txbValueCH5.Text = adc.Value[5].ToString();
            txbValueCH6.Text = adc.Value[6].ToString();
            txbValueCH7.Text = adc.Value[7].ToString();

            txbVoltageCH0.Text = adc.Voltage[0].ToString();
            txbVoltageCH1.Text = adc.Voltage[1].ToString();
            txbVoltageCH2.Text = adc.Voltage[2].ToString();
            txbVoltageCH3.Text = adc.Voltage[3].ToString();
            txbVoltageCH4.Text = adc.Voltage[4].ToString();
            txbVoltageCH5.Text = adc.Voltage[5].ToString();
            txbVoltageCH6.Text = adc.Voltage[6].ToString();
            txbVoltageCH7.Text = adc.Voltage[7].ToString();

            lbtnCH0.Active = adc.AdcPara.EnableCH0;
            lbtnCH1.Active = adc.AdcPara.EnableCH1;
            lbtnCH2.Active = adc.AdcPara.EnableCH2;
            lbtnCH3.Active = adc.AdcPara.EnableCH3;
            lbtnCH4.Active = adc.AdcPara.EnableCH4;
            lbtnCH5.Active = adc.AdcPara.EnableCH5;
            lbtnCH6.Active = adc.AdcPara.EnableCH6;
            lbtnCH7.Active = adc.AdcPara.EnableCH7;
        }

        private void btnSystemSetting_Click(object sender , EventArgs e)
        {
            ParameterINIForm dialog = new ParameterINIForm(adc.AdcPara, adc.DeviceName);
            dialog.ShowDialog();
        }
    }
}