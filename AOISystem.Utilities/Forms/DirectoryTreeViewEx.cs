using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public partial class DirectoryTreeViewEx : UserControl
    {
        public DirectoryTreeViewEx()
        {
            InitializeComponent();
        }

        public string SelectedPath 
        {
            get
            {
                return this.txtPath.Text;
            }
            set
            {
                if (value != this.txtPath.Text)
                {
                    this.txtPath.Text = value;
                    OnDirectiorySelected(value);
                }
            }
        }

        public event Action<string> DirectiorySelected;

        private void OnDirectiorySelected(string path)
        {
            if (DirectiorySelected != null)
            {
                DirectiorySelected(path);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            OnDirectiorySelected(this.txtPath.Text);
        }

        private void directoryTreeView_DirectiorySelected(string path)
        {
            this.txtPath.Text = path;
            OnDirectiorySelected(path);
        }
    }
}
