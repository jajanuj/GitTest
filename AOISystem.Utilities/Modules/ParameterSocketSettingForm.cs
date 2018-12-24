using AOISystem.Utilities.Modules.Ports;
using AOISystem.Utilities.Resources;
using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules
{
    public partial class ParameterSocketSettingForm : Form
    {
        private ParameterSocket socketPara;
        private bool isInitFinished;

        private ParameterSocketSettingForm()
        {
            InitializeComponent();
        }

        internal ParameterSocketSettingForm(ParameterSocket serialPara, string deviceName)
        {
            InitializeComponent();
            this.socketPara = serialPara;
            this.Text = deviceName;
        }

        private void ParameterSerialSettingForm_Load(object sender , EventArgs e)
        {
            InitializeControlValues();
            isInitFinished = true;
        }

        private void InitializeControlValues()
        {
            this.txtAddress.Text = socketPara.Address;
            this.txtPort.Text = socketPara.Port.ToString();
            this.txtReceiveTimeout.Text = socketPara.ReceiveTimeout.ToString();
        }

        private void FrmParameterSerialSetting_FormClosing(object sender , FormClosingEventArgs e)
        {
            if (MessageBox.Show(ResourceHelper.Language.GetString("ApplicationFinish"), ResourceHelper.Language.GetString("SystemHint"), MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Console.WriteLine("FrmPropertyGrid says : 888888888888");
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            if (isInitFinished)
            {
                socketPara.Address = this.txtAddress.Text;
            }
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            if (isInitFinished)
            {
                socketPara.Port = Convert.ToInt32(this.txtPort.Text);
            }
        }

        private void txtTimeout_TextChanged(object sender, EventArgs e)
        {
            if (isInitFinished)
            {
                socketPara.ReceiveTimeout = Convert.ToInt32(this.txtReceiveTimeout.Text);
            }
        }
    }
}