using System;
using System.Reflection;
using System.Windows.Forms;
using AOISystem.Utilities.Modules;
using System.ComponentModel;
using System.IO;

namespace AOISystem.Utilities.Recipe
{
    public partial class RecipeEnumControl : UserControl
    {
        public RecipeEnumControl()
        {
            InitializeComponent();
        }

        private void LoadListData()
        {
            if (!string.IsNullOrEmpty(this.AssemblyName) && !string.IsNullOrEmpty(this.EnumTypeName))
            {
                this.EnumType = Assembly.Load(this.AssemblyName).GetType(this.EnumTypeName);
                if (this.EnumType != null && this.EnumType.IsEnum)
                {
                    string[] enumNames = Enum.GetNames(this.EnumType);
                    cboFileList.Items.AddRange(enumNames);   
                }
            }
        }

        private string assemblyName;
        [Browsable(true), Category("System Setting"), Description("AssemblyName")]
        public string AssemblyName
        {
            get { return assemblyName; }
            set
            {
                if (!string.IsNullOrEmpty(value) && (value != assemblyName))
                {
                    assemblyName = value;
                    LoadListData();
                }
            }
        }

        private string enumTypeName;
        [Browsable(true), Category("System Setting"), Description("EnumTypeName")]
        public string EnumTypeName
        {
            get { return enumTypeName; }
            set 
            {
                if (!string.IsNullOrEmpty(value) && (value != enumTypeName))
                {
                    enumTypeName = value;
                    LoadListData();
                }
            }
        }

        [Browsable(true), Category("System Setting"), Description("EnumType")]
        public Type EnumType { get; private set; }

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
    }
}
