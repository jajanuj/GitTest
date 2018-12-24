using System;
using System.Reflection;
using System.Windows.Forms;
using AOISystem.Utilities.Modules;
using System.ComponentModel;
using System.Drawing;

namespace AOISystem.Utilities.Recipe
{
    public partial class RecipeTextControl : UserControl
    {
        public RecipeTextControl()
        {
            InitializeComponent();
            LabelFont = this.lblName.Font;
            this.Font = this.lblName.Font;
        }

        [Browsable(true), Category("System Setting"), Description("Value")]
        public double Value
        {
            get { return double.Parse(this.txtValue.Text); }
            set { this.txtValue.Text = value.ToString(); }
        }

        [Browsable(true), Category("System Setting"), Description("顯示名稱")]
        public string LabelName
        {
            get { return this.lblName.Text; }
            set 
            { 
                this.lblName.Text = value;
            }
        }

        private Font labelFont;
        [Browsable(true), Category("System Setting"), Description("字體")]
        public Font LabelFont
        {
            get { return labelFont; }
            set
            {
                if (value != labelFont)
                {
                    labelFont = value;
                    this.lblName.Font = value;
                }
            }
        }

        //public override Font Font
        //{
        //    get
        //    {
        //        return base.Font;
        //    }
        //    set
        //    {
        //        base.Font = value;
        //        this.lblName.Font = value;
        //    }
        //}

        [Browsable(true), Category("System Setting"), Description("工具提示內容")]
        public string ToolTipDescription
        {
            get { return this.toolTip.GetToolTip(this.txtValue); }
            set
            {
                this.toolTip.SetToolTip(this.lblName, value);
                this.toolTip.SetToolTip(this.txtValue, value);
            }
        }

        public void Save()
        {
            double Value = double.Parse(this.txtValue.Text);
//            PropertyInfo propertyInfo = _type.GetProperty(_valuePropertyName);
            //PropertyInfo propertyInfo = _type.GetProperty(this.Tag.ToString());
            //propertyInfo.SetValue(_instance, Value, null);
        }

        public void Restore()
        {
            //PropertyInfo propertyInfo = _type.GetProperty(_valuePropertyName);
            //this.txtValue.Text = propertyInfo.GetValue(_instance, null).ToString();
        }

        private void lblName_FontChanged(object sender, EventArgs e)
        {
            //lblName.Font = new Font(LabelFont.FontFamily, LabelFont.Size);
        }

    }
}
