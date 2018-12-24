using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public partial class PropertyGridForm : Form
    {
        private object _sourceObject;

        public PropertyGridForm(object sourceObject, string title = "")
        {
            InitializeComponent();

            this.Text = string.IsNullOrEmpty(title) ? this.Text : title;
            _sourceObject = sourceObject;
        }

        private void PropertyGridForm_Load(object sender , EventArgs e)
        {
            propertyGrid.SelectedObject = _sourceObject;
        }

        private void PropertyGridForm_FormClosing(object sender , FormClosingEventArgs e)
        {
        }
    }
}