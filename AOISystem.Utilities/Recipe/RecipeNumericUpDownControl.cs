using System;
using System.Reflection;
using System.Windows.Forms;
using AOISystem.Utilities.Modules;
using System.ComponentModel;
using System.Drawing;

namespace AOISystem.Utilities.Recipe
{
    public partial class RecipeNumericUpDownControl : UserControl
    {
        [Browsable(true), Category("System Setting"), Description("Value")]
        public double Value
        {
            get { return (double)this.NumValue.Value; }
            set { this.NumValue.Value = (decimal)value; }
        }

        [Browsable(true), Category("System Setting"), Description("顯示名稱"), Localizable(true)]
        public string LabelName
        {
            get { return this.lblName.Text; }
            set { this.lblName.Text = value; }
        }

        [Browsable(true), Category("System Setting"), Description("取得或設定控制項的寬度")]
        public float ValueWidth
        {
            get { return this.tableLayoutPanel.ColumnStyles[1].Width; }
            set { this.tableLayoutPanel.ColumnStyles[1].Width = value; }
        }

        [Browsable(true), Category("System Setting"), Description("最大數值")]
        public double MaxNumber
        {
            get { return (double)this.NumValue.Maximum; }
            set { this.NumValue.Maximum = (decimal)value; }
        }

        [Browsable(true), Category("System Setting"), Description("最小數值")]
        public double MinNumber
        {
            get { return (double)this.NumValue.Minimum; }
            set { this.NumValue.Minimum = (decimal)value; }
        }

        [Browsable(true), Category("System Setting"), Description("表示每按一次按鈕增加/減少的數值")]
        public double Increament
        {
            get { return (double)this.NumValue.Increment; }
            set { this.NumValue.Increment = (decimal)value; }
        }

        [Browsable(true), Category("System Setting"), Description("取得或設定在微調方塊 (也稱為上下按鈕控制項) 中顯示的小數位數")]
        public int DecimalPlaces
        {
            get { return this.NumValue.DecimalPlaces; }
            set { this.NumValue.DecimalPlaces = value; }
        }

        [Browsable(true), Category("System Setting"), Description("工具提示內容")]
        public string ToolTipDescription
        {
            get { return this.toolTip.GetToolTip(this.NumValue); }
            set 
            { 
                this.toolTip.SetToolTip(this.lblName, value);
                this.toolTip.SetToolTip(this.NumValue, value);
            }
        }


        public RecipeNumericUpDownControl()
        {
            InitializeComponent();
        }

        public void SetMaxNum(int Value)
        {
            this.NumValue.Maximum = Value;
        }

        public void SetMinNum(int Value)
        {
            this.NumValue.Minimum = Value;
        }

        public void SetIncreamentNum(int Value)
        {
            this.NumValue.Increment = Value;
        }

    }
}
