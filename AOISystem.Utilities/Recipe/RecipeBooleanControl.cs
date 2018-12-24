using System.ComponentModel;
using System.Windows.Forms;

namespace AOISystem.Utilities.Recipe
{
    public partial class RecipeBooleanControl : UserControl
    {
        [Browsable(true), Category("System Setting"), Description("Value")]
        public bool Value
        {
            get 
            { 
                return bool.Parse(this.cboValue.SelectedItem.ToString()); 
            }
            set
            {
                this.cboValue.SelectedIndex = value ? 1 : 0;
            }
        }

        [Browsable(true), Category("System Setting"), Description("顯示名稱")]
        public string LabelName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

        public RecipeBooleanControl()
        {
            InitializeComponent();

            this.cboValue.SelectedIndex = 0;
        }
    }
}
