using System.Windows.Forms;
using AOISystem.Utilities.Common;
using AOISystem.Utilities.Forms;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.SlaveModules.DAC
{
    public partial class CEtherCATDAC9144Form : Form
    {
        private bool initFinished = false;
        private CEtherCATDAC9144 dac;

        public CEtherCATDAC9144Form()
        {
            InitializeComponent();
        }

        public CEtherCATDAC9144Form(CEtherCATDAC9144 dac)
        {
            InitializeComponent();
            this.Text = dac.DeviceName;
            this.dac = dac;
        }

        private void CEtherCATDAC9144Form_Load(object sender, System.EventArgs e)
        {
            this.cboDAModeCh0.SelectedIndex = (int)this.dac.DacPara.RangeModeCH0;
            this.cboDAModeCh1.SelectedIndex = (int)this.dac.DacPara.RangeModeCH1;
            this.cboDAModeCh2.SelectedIndex = (int)this.dac.DacPara.RangeModeCH2;
            this.cboDAModeCh3.SelectedIndex = (int)this.dac.DacPara.RangeModeCH3;

            initFinished = true;
        }

        private void btnSetCh0_Click(object sender, System.EventArgs e)
        {
            this.dac.SetOutputValue(DACChannel.CＨ0, (ushort)((double)this.nudDACh0.Value *0.01 * 65535));
        }

        private void btnSetCh1_Click(object sender, System.EventArgs e)
        {
            this.dac.SetOutputValue(DACChannel.CＨ1, (ushort)((double)this.nudDACh1.Value * 0.01 * 65535));
        }

        private void btnSetCh2_Click(object sender, System.EventArgs e)
        {
            this.dac.SetOutputValue(DACChannel.CＨ2, (ushort)((double)this.nudDACh2.Value * 0.01 * 65535));
        }

        private void btnSetCh3_Click(object sender, System.EventArgs e)
        {
            this.dac.SetOutputValue(DACChannel.CＨ3, (ushort)((double)this.nudDACh3.Value * 0.01 * 65535));
        }

        private void cboDAModeCh0_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!initFinished)
            {
                return;
            }
            this.dac.SetOutputRangeMode(DACChannel.CＨ0, (RangeMode)this.cboDAModeCh0.SelectedIndex);
            this.dac.SetOutputEnable(DACChannel.CＨ0, true);
        }

        private void cboDAModeCh1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!initFinished)
            {
                return;
            }
            this.dac.SetOutputRangeMode(DACChannel.CＨ1, (RangeMode)this.cboDAModeCh1.SelectedIndex);
        }

        private void cboDAModeCh2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!initFinished)
            {
                return;
            }
            this.dac.SetOutputRangeMode(DACChannel.CＨ2, (RangeMode)this.cboDAModeCh2.SelectedIndex);
        }

        private void cboDAModeCh3_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (!initFinished)
            {
                return;
            }
            this.dac.SetOutputRangeMode(DACChannel.CＨ3, (RangeMode)this.cboDAModeCh3.SelectedIndex);
        }

        private void btnSystemSetting_Click(object sender, System.EventArgs e)
        {
            ParameterINIForm propertyGridForm = new ParameterINIForm(dac.DacPara, dac.DeviceName);
            propertyGridForm.ShowDialog();
        }
    }
}