using System;
using System.Drawing;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public partial class IOSwitchInput : UserControl
    {
        public IOSwitchInput()
        {
            InitializeComponent();

            OnResize(EventArgs.Empty);
        }

        private void IOSwitchInput_Resize(object sender, EventArgs e)
        {
            this.ledStatus.Size = new Size(this.ledStatus.Size.Height, this.ledStatus.Size.Height);
            this.tableLayoutPanel.ColumnStyles[0].Width = this.ledStatus.Size.Height;
        }

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
            get { return this.ledStatus.On; }
            set { this.ledStatus.On = value; }
        }

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
    }
}
