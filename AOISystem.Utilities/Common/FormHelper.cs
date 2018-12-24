using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Common
{
    public class FormHelper
    {
        /// <summary>打開唯一視窗</summary>
        public static void OpenUniqueForm(string name, Action action)
        {
            bool exitUniqueForm = false;
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == name)
                {
                    openForm.Visible = true;
                    openForm.Activate();
                    exitUniqueForm = true;
                    break;
                }
            }
            if (!exitUniqueForm)
            {
                action();
            }
        }

        /// <summary>清除所有子控制項</summary>
        public static void DisposeControls(Control targetControl)
        {
            for (int i = targetControl.Controls.Count - 1; i >= 0; i--)
            {
                Control control = targetControl.Controls[i];
                targetControl.Controls.RemoveAt(i);
                control.Dispose();
            }
        }
    }
}
