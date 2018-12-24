using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AOISystem.Utilities.Component
{
    public class NumericEditor : UITypeEditor
    {
        public NumericEditor()
        {

        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;
            if (provider != null)
            {
                editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            }
            if (editorService != null)
            {
                NumericUpDown picker = new NumericUpDown();
                picker.ImeMode = ImeMode.Disable;
                picker.Maximum = Decimal.MaxValue;
                picker.Minimum = Decimal.MinValue;
                picker.Value = Convert.ToDecimal(value);

                PropertyDescriptor property = context.PropertyDescriptor;
                Type type = property.PropertyType;
                string typeName = property.PropertyType.FullName;
                switch (typeName)
                {
                    case "System.Int16":
                        editorService.DropDownControl(picker);
                        value = (Int16)picker.Value;
                        break;
                    case "System.UInt16":
                        editorService.DropDownControl(picker);
                        value = (UInt16)picker.Value;
                        break;
                    case "System.Int32":
                        editorService.DropDownControl(picker);
                        value = (Int32)picker.Value;
                        break;
                    case "System.UInt32":
                        editorService.DropDownControl(picker);
                        value = (UInt32)picker.Value;
                        break;
                    case "System.Int64":
                        editorService.DropDownControl(picker);
                        value = (Int64)picker.Value;
                        break;
                    case "System.UInt64":
                        editorService.DropDownControl(picker);
                        value = (UInt64)picker.Value;
                        break;
                    case "System.Single":
                        picker.DecimalPlaces = 2;
                        editorService.DropDownControl(picker);
                        value = (Single)picker.Value;
                        break;
                    case "System.Double":
                        picker.DecimalPlaces = 2;
                        editorService.DropDownControl(picker);
                        value = (Double)picker.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(string.Format("NumericEditor don't define {0} convet rule.", typeName));
                }
            }
            return value;
        }
    }
}
