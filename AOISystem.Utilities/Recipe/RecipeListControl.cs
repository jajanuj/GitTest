using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace AOISystem.Utilities.Recipe
{
    public partial class RecipeListControl : UserControl
    {
        public RecipeListControl()
        {
            InitializeComponent();
            LoadListData();
        }

        [Browsable(true), Category("System Setting"), Description("Value")]
        private string listValue;
        public string ListValue
        {
            get 
            {
                if (cboFileList.SelectedItem != null)
                    listValue = cboFileList.SelectedItem.ToString();
                else
                    listValue = cboFileList.Items.Count > 0 ? cboFileList.Items[0].ToString() : string.Empty;

                return listValue;
            }
            set
            {
                int index = cboFileList.Items.IndexOf(value);
                cboFileList.SelectedIndex = index;
            }
        }

        [Browsable(true), Category("System Setting"), Description("顯示名稱")]
        public string LabelName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

        //v1.0.5.0 分類法則清單元件新增指定路徑功能
        private string folderPath = @"D:\Sorting Rule File";
        [Browsable(true), Category("System Setting"), Description("資料夾路徑")]
        public string FolderPath
        {
            get { return folderPath; }
            set { folderPath = value; }
        }

        private void LoadListData()
        {
            cboFileList.Items.Clear();

            if (!Directory.Exists(FolderPath))
                return;

            foreach (string FileName in Directory.GetFiles(FolderPath, "*.csv"))
            {
                cboFileList.Items.Add(Path.GetFileNameWithoutExtension(FileName));
            }
        }

        private void cboFileList_Click(object sender, System.EventArgs e)
        {
            LoadListData();
        }
    }
}
