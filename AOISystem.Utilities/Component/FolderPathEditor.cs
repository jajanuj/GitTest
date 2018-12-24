using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace AOISystem.Utilities.Component
{
    public class FolderPathEditor : UITypeEditor
    {
        public FolderPathEditor()
        {
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    value = dialog.SelectedPath;
            }
            return value;
        }
    }
}
