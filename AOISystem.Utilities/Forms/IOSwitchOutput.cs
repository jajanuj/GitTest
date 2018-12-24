using System;
using System.Drawing;
using System.Windows.Forms;
using AOISystem.Utilities.Account;

namespace AOISystem.Utilities.Forms
{
    public partial class IOSwitchOutput : UserControl
    {
        public IOSwitchOutput()
        {
            InitializeComponent();

            OnResize(EventArgs.Empty);
        }

        private void IOSwitchOutput_Resize(object sender, EventArgs e)
        {
            this.tableLayoutPanel.ColumnStyles[0].Width = this.btnStatus.Size.Width;
        }

        public event EventHandler SwitchStatusChenged;

        public new Font Font
        {
            get { return this.lblName.Font; }
            set { this.lblName.Font = value; }
        }

        public new string Text
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

        public bool On
        {
            get { return this.btnStatus.Checked; }
            set { this.btnStatus.Checked = value; }
        }

        public bool TestPermission { get; set; }

        public AccountLevel AccountLevel { get; set; }

        public float IONumberSize
        {
            get { return this.tableLayoutPanel.ColumnStyles[1].Width; }
            set { this.tableLayoutPanel.ColumnStyles[1].Width = value; }
        }

        public string IONumber
        {
            get { return this.lblIONumber.Text; }
            set { this.lblIONumber.Text = value; }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            bool success = false;
            if (this.TestPermission)
            {
                if (AccountInfoManager.TestPermission(this.AccountLevel))
                {
                    success = true;
                }
            }
            else
            {
                success = true;
            }
            if (success)
            {
                this.On = !this.On;
                if (SwitchStatusChenged != null)
                {
                    SwitchStatusChenged(this, e);
                }          
            }
        }
    }
}
