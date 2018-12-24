using AOISystem.Utilities.Forms;
using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.ADC
{
    public partial class CEtherCATADC8124Form : Form
    {
        private bool initFinished = false;
        private CEtherCATADC8124 adc;

        private CEtherCATADC8124Form()
        {
            InitializeComponent();
        }

        public CEtherCATADC8124Form(CEtherCATADC8124 adc)
        {
            InitializeComponent();
            this.Text = adc.DeviceName;
            this.adc = adc;
        }

        private void CEtherCATADC8124Form_Load(object sender , EventArgs e)
        {
            lbtnCH0.Active = adc.AdcPara.EnableCH0;
            lbtnCH1.Active = adc.AdcPara.EnableCH1;
            lbtnCH2.Active = adc.AdcPara.EnableCH2;
            lbtnCH3.Active = adc.AdcPara.EnableCH3;
            initFinished = true;
        }

        private void timer1_Tick(object sender , EventArgs e)
        {
            txbValueCH0.Text = adc.Value[0].ToString();
            txbValueCH1.Text = adc.Value[1].ToString();
            txbValueCH2.Text = adc.Value[2].ToString();
            txbValueCH3.Text = adc.Value[3].ToString();

            txbVoltageCH0.Text = adc.Voltage[0].ToString();
            txbVoltageCH1.Text = adc.Voltage[1].ToString();
            txbVoltageCH2.Text = adc.Voltage[2].ToString();
            txbVoltageCH3.Text = adc.Voltage[3].ToString();
        }

        private void lbtnCH0_Click(object sender, EventArgs e)
        {
            this.lbtnCH0.Active = !this.lbtnCH0.Active;
            this.adc.SetInputEnable(ADCChannel.CＨ0, this.lbtnCH0.Active);
        }

        private void lbtnCH1_Click(object sender, EventArgs e)
        {
            this.lbtnCH1.Active = !this.lbtnCH1.Active;
            this.adc.SetInputEnable(ADCChannel.CＨ1, this.lbtnCH1.Active);
        }

        private void lbtnCH2_Click(object sender, EventArgs e)
        {
            this.lbtnCH2.Active = !this.lbtnCH2.Active;
            this.adc.SetInputEnable(ADCChannel.CＨ2, this.lbtnCH2.Active);
        }

        private void lbtnCH3_Click(object sender, EventArgs e)
        {
            this.lbtnCH3.Active = !this.lbtnCH3.Active;
            this.adc.SetInputEnable(ADCChannel.CＨ3, this.lbtnCH3.Active);
        }

        private void btnSystemSetting_Click(object sender , EventArgs e)
        {
            ParameterINIForm dialog = new ParameterINIForm(adc.AdcPara, adc.DeviceName);
            dialog.ShowDialog();
        }
    }
}