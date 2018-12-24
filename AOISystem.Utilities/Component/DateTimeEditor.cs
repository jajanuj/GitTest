using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AOISystem.Utilities.Component
{
    public class DateTimeEditor : UITypeEditor
    {
        public DateTimeEditor()
        {
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
                PropertyDescriptor propertyDescriptor = context.PropertyDescriptor;
                PropertyInfo propertyInfo = propertyDescriptor.ComponentType.GetProperty(propertyDescriptor.Name);
                CustomFormatAttribute attribute =
                    (CustomFormatAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(CustomFormatAttribute));

                DateTimePicker picker = new DateTimePicker();
                picker.ShowUpDown = true;
                if (attribute != null)
                {
                    picker.Format = DateTimePickerFormat.Custom;
                    picker.CustomFormat = attribute.CustomFormat;
                    picker.Value = DateTime.ParseExact(value.ToString(), attribute.CustomFormat, CultureInfo.CurrentUICulture, DateTimeStyles.AllowWhiteSpaces);
                    editorService.DropDownControl(picker);
                    value = picker.Value.ToString(attribute.CustomFormat);
                }
                else
                {
                    picker.Value = DateTime.Parse(value.ToString());
                    editorService.DropDownControl(picker);
                    value = picker.Value.ToString();
                }
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
    }

    public class CustomFormatAttribute : Attribute
    {
        public CustomFormatAttribute(string customFormat)
        {
            this.CustomFormat = customFormat;
        }

        public string CustomFormat { get; private set; }
    }
}
