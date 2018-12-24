using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules.Light.Common
{
    public partial class LightChannelControl : UserControl
    {
        private ILight _parent;
        private bool _isInitialized = false;

        public LightChannelControl(ILight parent, LightChannel channel)
        {
            InitializeComponent();
            _parent = parent;
            this.Channel = channel;
            this.grpChannel.Text = _parent.LightInfoCollection[this.Channel].Name;
            this.nudLightValue.Value = _parent.GetLightValue(this.Channel);
            this.ledLighSwitch.On = this.nudLightValue.Value > 0 ? true : false;
            _isInitialized = true;
        }

        private LightChannel _channel;
        public LightChannel Channel
        {
            get
            {
                return _channel;
            }
            set
            {
                _channel = value;
            }
        }

        private void ledLighSwitch_Click(object sender, EventArgs e)
        {
            this.ledLighSwitch.On = !this.ledLighSwitch.On;
            _parent.SwitchLight(this.Channel, this.ledLighSwitch.On);
        }

        private void nudLightValue_ValueChanged(object sender, EventArgs e)
        {
            if (!_isInitialized)
            {
                return;
            }
            if (this.nudLightValue.Value == 0)
            {
                this.ledLighSwitch.On = false;
            }
            else
            {
                this.ledLighSwitch.On = true;
            }
            _parent.SetLightValue(this.Channel, (byte)this.nudLightValue.Value);
        }

        private void tsmiRename_Click(object sender, EventArgs e)
        {
            LightChannelRenameForm lightChannelRenameForm = new LightChannelRenameForm();
            lightChannelRenameForm.OldName = _parent.LightInfoCollection[this.Channel].Name;
            if (lightChannelRenameForm.ShowDialog() == DialogResult.OK)
            {
                _parent.LightInfoCollection[this.Channel].Name = lightChannelRenameForm.NewName;
                this.grpChannel.Text = _parent.LightInfoCollection[this.Channel].Name;
            }
        }
    }
}
