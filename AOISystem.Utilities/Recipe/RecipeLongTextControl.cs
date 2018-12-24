using System.ComponentModel;
using System.Windows.Forms;

namespace AOISystem.Utilities.Recipe
{
    public partial class RecipeLongTextControl : UserControl
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
        public RecipeLongTextControl()
        {
            InitializeComponent();
        }

        public void Save()
        {
            string Value = this.txtValue.Text;

        }

        public void Restore()
        {

        }

    }
}
