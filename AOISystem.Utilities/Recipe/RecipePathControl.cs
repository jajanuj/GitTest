using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AOISystem.Utilities.Recipe
{
    public partial class RecipePathControl : UserControl
    {

        [Browsable(true), Category("System Setting"), Description("Value")]
        public string Value
        {
            get { return this.txtValue.Text; }
            set { this.txtValue.Text = value.ToString(); }
        }

        [Browsable(true), Category("System Setting"), Description("顯示名稱")]
        public string LabelName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

        [Browsable(true), Category("System Setting"), Description("路徑類型")]
        public PathMode PathMode { get; set; }

        public RecipePathControl()
        {
            InitializeComponent();
        }

        public void Save()
        {
            string Value = this.txtValue.Text;

        }

        public void Restore()
        {
            //PropertyInfo propertyInfo = _type.GetProperty(_valuePropertyName);
            //this.txtValue.Text = propertyInfo.GetValue(_instance, null).ToString();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.PathMode == PathMode.Folder)
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtValue.Text = folderBrowserDialog.SelectedPath;
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtValue.Text = openFileDialog.FileName;
                }
            }
        }
    }
}
