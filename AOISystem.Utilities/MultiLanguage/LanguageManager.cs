using AOISystem.Utilities.Resources;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace AOISystem.Utilities.MultiLanguage
{
    public static class LanguageManager
    {
        public static void Apply(Control control, Language language)
        {
            switch (language)
            {
                case Language.English:
                    Apply(control, "en");
                    break;
                case Language.TraditionalChinese:
                    Apply(control, "zh-TW");
                    break;
            }
        }

        public static void Apply(Control control, string language)
        {
            if (string.IsNullOrEmpty(language))
                return;

            if (control == null)
                throw new ArgumentNullException("control");

            System.Globalization.CultureInfo info;
            try
            {
                info = new System.Globalization.CultureInfo(language);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // UI 的語系(MessageBox 屬於Form的UI)
            Thread.CurrentThread.CurrentUICulture = info;
            // 非 UI 的語系(Exception 不屬於Form的UI)，因為有些錯誤資訊是擷取Exception的data，所以exception的語系也要改掉。
            Thread.CurrentThread.CurrentCulture = info;
            //針對視窗主體多國語言處理
            Apply(control);
            //註冊MessageBox多國語言訊息
            ApplyMessageBox();
        }

        public static void Apply(Control control)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            ComponentResourceManager Manager = null;
            try
            {
                Manager = new ComponentResourceManager(control.GetType());
                Manager.ApplyResources(control, "$this");

                foreach (Control Ctrl in control.Controls)
                {
                    ApplyLanguage(Ctrl, Manager);
                }
            }
            finally
            {
                Manager = null;
            }
        }

        private static void ApplyLanguage(Control control, ComponentResourceManager manager)
        {
            if (control is MenuStrip)
            {
                MenuStrip menu = control as MenuStrip;
                manager.ApplyResources(control, control.Name);

                foreach (ToolStripItem item in menu.Items)
                {
                    ApplyLanguage(item, manager);
                }
            }
            else
            {
                manager.ApplyResources(control, control.Name);
                foreach (Control item in control.Controls)
                {
                    if (item is TextBox)
                    {
                    }
                    else
                    {
                        ApplyLanguage(item, manager);
                    }
                }
            }
        }

        private static void ApplyLanguage(ToolStripItem control, ComponentResourceManager Manager)
        {
            if (!(control is System.Windows.Forms.ToolStripSeparator))
            {
                Manager.ApplyResources(control, control.Name);
                ToolStripMenuItem menu = control as ToolStripMenuItem;
                foreach (ToolStripItem item in menu.DropDownItems)
                {
                    ApplyLanguage(item, Manager);
                }
            }
        }

        private static void ApplyMessageBox()
        {
            if (MessageBoxManager.IsRegisted)
            {
                MessageBoxManager.Unregister();
            }

            //Set button text from resources
            MessageBoxManager.OK = ResourceHelper.Language.GetString("MessageBoxOK");
            MessageBoxManager.Cancel = ResourceHelper.Language.GetString("MessageBoxCancel");
            MessageBoxManager.Retry = ResourceHelper.Language.GetString("MessageBoxRetry");
            MessageBoxManager.Ignore = ResourceHelper.Language.GetString("MessageBoxIgnore");
            MessageBoxManager.Abort = ResourceHelper.Language.GetString("MessageBoxAbort");
            MessageBoxManager.Yes = ResourceHelper.Language.GetString("MessageBoxYes");
            MessageBoxManager.No = ResourceHelper.Language.GetString("MessageBoxNo");

            //Register manager
            MessageBoxManager.Register();
        }
    }
}
