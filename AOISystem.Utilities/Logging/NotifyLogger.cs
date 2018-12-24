using System;
using System.Threading;
using System.Windows.Forms;

namespace AOISystem.Utilities.Logging
{
    public static class NotifyLogger
    {
        public static SynchronizationContext _synchronizationContext;

        public static void InitializeConfiguration(SynchronizationContext synchronizationContext)
        {
            _synchronizationContext = synchronizationContext;
        }

        public static void Post(string format, params object[] arg)
        {
            Post(string.Format(format, arg));
        }

        public static void Post(string message)
        {
            LogHelper.Notify(message);

            string writeString = string.Format("{0:yyyy.MM.dd HH:mm:ss} | {1}", DateTime.Now, message);
            if (_synchronizationContext != null)
            {
                _synchronizationContext.Send((o) =>
                {
                    GetMessageNotificationWindow();
                    MessageNotificationForm.GetInstance().Post(writeString);
                }, null);
            }
            else
            {
                GetMessageNotificationWindow();
                MessageNotificationForm.GetInstance().Post(writeString);
            }
        }

        public static void Post(Exception e)
        {
            if (e.InnerException != null)
            {
                if (e.InnerException is AggregateException)
                {
                    AggregateException aggEx = (AggregateException)e.InnerException;
                    foreach (Exception ex in aggEx.InnerExceptions)
                    {
                        Post(ex);
                    }
                }
                else
                {
                    Post(e.InnerException);
                }
            }
            else
            {
                Post(e.Message);
                Post(e.StackTrace);
            }
        }

        //判斷窗口是否已經打開
        private static bool CheckFormIsOpen(string Forms)
        {
            bool bResult = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == Forms)
                {
                    bResult = true;
                    break;
                }
            }
            return bResult;
        }

        private static void GetMessageNotificationWindow()
        {
            MessageNotificationForm msgForm = null;
            bool wExist = CheckFormIsOpen("MessageNotificationForm");

            if (!wExist)
            {
                msgForm = MessageNotificationForm.GetInstance();
                msgForm.Name = "MessageNotificationForm";
                msgForm.Show();
            }
            else
            {
                msgForm = (MessageNotificationForm)Application.OpenForms["MessageNotificationForm"];
                if (msgForm.WindowState == FormWindowState.Minimized)
                {
                    msgForm.WindowState = FormWindowState.Normal;
                }
                msgForm.Activate();
            }
        }
    }
}
