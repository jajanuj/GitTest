using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules.Light.Common
{
    public partial class LightChannelRenameForm : Form
    {
        public LightChannelRenameForm()
        {
            InitializeComponent();
        }

        public string OldName 
        { 
            get 
            { 
                return this.txtOldName.Text; 
            } 
            set 
            {
                if (value != this.txtOldName.Text)
                {
                    this.txtOldName.Text = value;
                    this.txtNewName.Text = value;
                }
            } 
        }

        public string NewName
        {
            get
            {
                return this.txtNewName.Text;
            }
            set
            {
                if (value != this.txtNewName.Text)
                {
                    this.txtNewName.Text = value;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
