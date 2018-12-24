using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public partial class NumpadForm : Form
    {
        public NumpadForm()
        {
            InitializeComponent();
            this.TypeString = string.Empty;
        }

        public string TypeString { get; set; }

        public event EventHandler TypeStringChanged;

        private void OnTypeStringChanged()
        {
            TypeStringChanged(this, EventArgs.Empty);
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            this.TypeString += ((Button)sender).Text;
            OnTypeStringChanged();
        }

        private void btnBS_Click(object sender, EventArgs e)
        {
            if (this.TypeString.Length > 0)
            {
                this.TypeString = this.TypeString.Substring(0, this.TypeString.Length - 1);   
            }
            OnTypeStringChanged();
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            this.TypeString = string.Empty;
            OnTypeStringChanged();
        }
    }
}
