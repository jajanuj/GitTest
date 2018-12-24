using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace AOISystem.Utilities.Component
{
    public class FilePathEditor : UITypeEditor
    {
        public FilePathEditor()
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    value = dialog.FileName;
            }
            return value;
        }
    }
}
