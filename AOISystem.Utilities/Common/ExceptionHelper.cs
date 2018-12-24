using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using AOISystem.Utilities.Logging;
using AOISystem.Utilities.Resources;

namespace AOISystem.Utilities.Common
{
    public class ExceptionHelper
    {
        public static event Action PrintScreenEvent;

        private static void OnPrintScreenEvent()
        {
            if (PrintScreenEvent != null)
            {
                PrintScreenEvent();
            }
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        public static void ExceptionHandler_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                Log.Exception(t.Exception);
                OnPrintScreenEvent();
                result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error",
                        "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only 
        // log the event, and inform the user about it. 
        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                if (e.ExceptionObject is AggregateException)
                {
                    AggregateException aggEx = (AggregateException)e.ExceptionObject;
                    foreach (Exception ex in aggEx.InnerExceptions)
                    {
                        RecordException(ex);
                    }
                }
                else
                {
                    RecordException((Exception)e.ExceptionObject);
                }
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error",
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        // Creates the error message and displays it.
        public static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred. Please contact the adminstrator " +
                "with the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }

        public static void RecordException(Exception ex)
        {
            Log.Exception(ex);
            string errorMsg = "An application error occurred. Please contact the adminstrator " +
                "with the following information:";

            //// Since we can't prevent the app from terminating, log this to the event log.
            //if (!NotifyLog.SourceExists("AOISystemException"))
            //{
            //    NotifyLog.CreateEventSource("AOISystemException", "Application");
            //}

            //// Create an NotifyLog instance and assign its source.
            //NotifyLog myLog = new NotifyLog();
            //myLog.Source = "AOISystemException";
            //myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);

            //Log檔案路徑
            string LogFile = string.Format(@"{0}\{1}", Application.StartupPath, "ErrorLog.log");
            //檔案不存在的話，建立新檔案
            if (!File.Exists(LogFile))
            {
                using (FileStream fs = File.Create(LogFile)) { }
            }
            FileInfo info = new FileInfo(LogFile);
            using (StreamWriter sw = info.AppendText())
            {
                sw.WriteLine(string.Format("{0} | {1}", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"), errorMsg));
                sw.WriteLine(string.Format("{0}\n{1}", "例外處理類型：", ex.GetType()));
                sw.WriteLine(string.Format("{0}\n{1}", "錯誤訊息：", ex.Message));
                sw.WriteLine(string.Format("{0}\n{1}", "錯誤之處：", ex.StackTrace));
            }
        }

        public static string GetFullCurrentMethod(string message, int skipFrames = 1)
        {
            StackTrace stackTrace = new StackTrace(new StackFrame(skipFrames, true));
            StackFrame stackFrame = stackTrace.GetFrame(0);
            MethodBase methodBase = stackFrame.GetMethod();
            string totalMessage = string.Format("Message:\n{0}\n\nFrom:\n{1}.{2}()", message, methodBase.DeclaringType.FullName, methodBase.Name);
            int line = stackFrame.GetFileLineNumber();
            if (line != 0)
            {
                totalMessage = string.Format("{0} Line:{1}", totalMessage, line);
            }
            return totalMessage;
        }

        public static void CommonMessageShow(string text, string caption, MessageBoxIcon icon = MessageBoxIcon.Warning)
        {
            try
            {
                MessageBox.Show(GetFullCurrentMethod(text, 3), ResourceHelper.Language.GetString(caption),
                    MessageBoxButtons.OK, icon);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static void MessageBoxShow(Exception ex, string caption, MessageBoxIcon icon = MessageBoxIcon.Warning)
        {
            CommonMessageShow(ex.Message + "\r\n" + ex.StackTrace, caption, icon);
        }
    }
}