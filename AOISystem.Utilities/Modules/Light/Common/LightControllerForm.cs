using System;
using System.Windows.Forms;
using AOISystem.Utilities.Modules.Light.Common;
using AOISystem.Utilities.Common;

namespace AOISystem.Utilities.Modules.Light.Common
{
    public partial class LightControllerForm : Form
    {
        private ILight _parent;

        public LightControllerForm(ILight parent)
        {
            InitializeComponent();

            _parent = parent;
            InitializeLightChannelControl();
        }

        private void LightControllerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            _parent.ShowMonitor();
        }

        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            _parent.ShowConfiguration();
        }

        private void btnAllON_Click(object sender, EventArgs e)
        {
            if (this.btnAllON.Text == "All ON")
            {
                this.btnAllON.Text = "All OFF";
                _parent.SwitchLight(LightChannel.CHAll, true);
            }
            else
            {
                this.btnAllON.Text = "All ON";
                _parent.SwitchLight(LightChannel.CHAll, false);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            _parent.InitializeConfiguration(_parent.ParameterType);
            InitializeLightChannelControl();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _parent.SaveConfiguration();
        }

        private void InitializeLightChannelControl()
        {
            //this.flowLayoutPanel.Size = new Size(this.flowLayoutPanel.Size.Width, channelNumber * 100);
            FormHelper.DisposeControls(this.flowLayoutPanel);
            foreach (var lightInfo in _parent.LightInfoCollection)
            {
                this.flowLayoutPanel.Controls.Add(new LightChannelControl(_parent, lightInfo.Value.Channel));
            }
        }
    }
}
