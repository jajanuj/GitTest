using System;
using System.Reflection;
using System.Windows.Forms;
using AOISystem.Utilities.Modules;
using System.ComponentModel;
using System.Drawing;

namespace AOISystem.Utilities.Recipe
{
    public enum DataTypeEnum
    {
        SystemData,
        RecipeData
    }

    public enum FieldTypeEnum
    {
        DoubleType,
        StringType,
        NumericType
    }

    public partial class DataRecordControl : UserControl
    {
        public DataRecordControl()
        {
            InitializeComponent();
            //LabelFont = this.lblName.Font;
            FieldType = FieldTypeEnum.DoubleType;
            this.Font = this.lblName.Font;


        }

        [Browsable(true), Category("System Setting"), Description("Data Type")]
        public DataTypeEnum DataType { get; set; }

        private object _dataValue;
        [Browsable(true), Category("System Setting"), Description("Data Value")]
        public object DataValue
        { 
            get 
            {
                switch (this.FieldType)
                {
                    case FieldTypeEnum.DoubleType:
                        return tb.Text;
                    case FieldTypeEnum.StringType:
                        return tb.Text;
                    case FieldTypeEnum.NumericType:
                        return tb.Text;
                    default:
                        return tb.Text;
                }
                
            }
            set
            {
                _dataValue = value;


                if (_dataValue != null)
                {
                    switch (this.FieldType)
                    {
                        case FieldTypeEnum.DoubleType:
                            tb.Text = _dataValue.ToString();
                            break;
                        case FieldTypeEnum.StringType:
                            tb.Text = _dataValue.ToString();
                            break;
                        case FieldTypeEnum.NumericType:
                        default:
                            tb.Text = _dataValue.ToString();

                            break;
                    }
                }
            } 
        }

        private TextBox tb = new TextBox();
        private ListBox lb = new ListBox();
        private FieldTypeEnum _fieldType;
        private object _instance;
        private Type _type;

        [Browsable(true), Category("System Setting"), Description("Field")]
        public FieldTypeEnum FieldType
        {
            get { return _fieldType; }
            set
            {
                _fieldType = value;

                switch (_fieldType)
                {
                    default:
                    case FieldTypeEnum.DoubleType:
                        this.tableLayoutPanel.Controls.Add(tb, 2, 0);
                        break;
                    case FieldTypeEnum.StringType:
                        this.tableLayoutPanel.Controls.Add(tb, 2, 0);
                        break;
                    case FieldTypeEnum.NumericType:
                        this.tableLayoutPanel.Controls.Add(lb, 2, 0);
                        break;
                }
               
            }
        }

        [Browsable(true), Category("System Setting"), Description("Value")]
        public double Value
        {
            get { return double.Parse(this.txtValue.Text); }
            set 
            { this.txtValue.Text = value.ToString(); }
        }

        [Browsable(true), Category("System Setting"), Description("顯示名稱"), Localizable(true)]
        public string LabelName
        {
            get { return this.lblName.Text; }
            set 
            { 
                this.lblName.Text = value;
            }
        }

        private Font labelFont;
        [Browsable(false), Category("System Setting"), Description("字體")]
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

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                this.lblName.Font = value;
            }
        }

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
            //double position = double.Parse(this.DataValue.ToString());
            //PropertyInfo propertyInfo = _type.GetProperty(this.Tag.ToString());
            //propertyInfo.SetValue(_instance, position, null);
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
