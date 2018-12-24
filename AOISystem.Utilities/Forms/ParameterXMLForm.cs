using AOISystem.Utilities.Common;
using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public partial class ParameterXMLForm<T> : Form where T : ParameterXML<T>, new() 
    {
        private object _sourceObject;

        public ParameterXMLForm(object sourceObject, string title = "")
        {
            InitializeComponent();
            
            this.Text = string.IsNullOrEmpty(title) ? this.Text : title;
            _sourceObject = sourceObject;
        }

        private void ParameterXMLForm_Load(object sender , EventArgs e)
        {
            propertyGrid.SelectedObject = _sourceObject;
        }

        private void ParameterXMLForm_FormClosing(object sender , FormClosingEventArgs e)
        {
            //if (MessageBox.Show(ResourceHelper.Language.GetString("ApplicationFinish"), ResourceHelper.Language.GetString("SystemHint"), 
            //    MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    Console.WriteLine("ParameterXMLForm says : 888888888888");
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ParameterXML<T> parameterBase = _sourceObject as ParameterXML<T>;
            parameterBase.Load();
            propertyGrid.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ParameterXML<T> parameterBase = _sourceObject as ParameterXML<T>;
            parameterBase.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParameterXML<T> parameterBase = _sourceObject as ParameterXML<T>;
            parameterBase.Load();
            this.Close();
        }
    }
}