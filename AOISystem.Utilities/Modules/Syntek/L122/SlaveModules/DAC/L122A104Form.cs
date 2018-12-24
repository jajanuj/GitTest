using System.Windows.Forms;
using AOISystem.Utilities.Common;
using AOISystem.Utilities.Forms;
using AOISystem.Utilities.Modules.Syntek.L122.MasterCard;

namespace AOISystem.Utilities.Modules.Syntek.L122.SlaveModules.DAC
{
    public partial class L122A104Form : Form
    {
        private L122A104 dac;

        public L122A104Form()
        {
            InitializeComponent();
        }

        public L122A104Form(L122A104 dac)
        {
            InitializeComponent();
            this.Text = dac.DeviceName;
            this.dac = dac;
        }

        private void btnSetCh0_Click(object sender, System.EventArgs e)
        {
            this.dac.SetVoltage(DACChannel.CN0, double.Parse(this.txtA104Ch0.Text));
        }

        private void btnSetCh1_Click(object sender, System.EventArgs e)
        {
            this.dac.SetVoltage(DACChannel.CN1, double.Parse(this.txtA104Ch1.Text));
        }

        private void btnSetCh2_Click(object sender, System.EventArgs e)
        {
            this.dac.SetVoltage(DACChannel.CN2, double.Parse(this.txtA104Ch2.Text));
        }

        private void btnSetCh3_Click(object sender, System.EventArgs e)
        {
            this.dac.SetVoltage(DACChannel.CN3, double.Parse(this.txtA104Ch3.Text));
        }

        private void btnResetAll_Click(object sender, System.EventArgs e)
        {
            this.dac.ResetDAC();
        }

        private void btnSetAll_Click(object sender, System.EventArgs e)
        {
            this.dac.SetVoltageAll(double.Parse(this.txtA104Ch0.Text), double.Parse(this.txtA104Ch1.Text), double.Parse(this.txtA104Ch2.Text), double.Parse(this.txtA104Ch3.Text));
        }

        private void btnSystemSetting_Click(object sender, System.EventArgs e)
        {
            ParameterINIForm propertyGridForm = new ParameterINIForm(dac.DacPara, dac.DeviceName);
            propertyGridForm.ShowDialog();
            ((ParameterINI)dac.DacPara).Save();
        }
    }
}