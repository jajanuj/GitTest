using System;
using System.Windows.Forms;
using AOISystem.Utilities.Common;
using AOISystem.Utilities.Resources;

namespace AOISystem.Utilities.Forms
{
    public partial class ParameterINIForm : Form
    {
        private object _sourceObject;

        public ParameterINIForm(object sourceObject, string title = "")
        {
            InitializeComponent();

            this.Text = string.IsNullOrEmpty(title) ? this.Text : title;
            _sourceObject = sourceObject;
        }

        private void ParameterINIForm_Load(object sender , EventArgs e)
        {
            propertyGrid.SelectedObject = _sourceObject;
        }

        private void ParameterINIForm_FormClosing(object sender , FormClosingEventArgs e)
        {
            if (MessageBox.Show(ResourceHelper.Language.GetString("ApplicationFinish"), ResourceHelper.Language.GetString("SystemHint"), 
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Console.WriteLine("ParameterINIForm says : 888888888888");
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ParameterINI parameterBase = _sourceObject as ParameterINI;
            parameterBase.Load();
            propertyGrid.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ParameterINI parameterBase = _sourceObject as ParameterINI;
            parameterBase.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParameterINI parameterBase = _sourceObject as ParameterINI;
            parameterBase.Load();
            this.Close();
        }
    }
}