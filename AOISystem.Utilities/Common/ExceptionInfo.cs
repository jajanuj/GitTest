using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AOISystem.Utilities
{
    public class ExceptionInfo
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public const int WM_CLOSE = 0x10;
        /// <summary>
        /// Shows the MSG.
        /// </summary>
        /// <param name="ClassName">Name of the class.</param>
        /// <param name="FuncName">Name of the func.</param>
        /// <param name="e">The e.</param>
        public static void ShowMsg(string ClassName, string FuncName, Exception e)
        {
            string[] sep = new string[] { ":", "。" };
            string[] ErrorPool = e.ToString().Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string Message = ErrorPool[0] + "(" + ErrorPool[1] + ")";
            //StartKiller(3);//三秒後關閉
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("錯誤物件：" + ClassName.ToString());
            sb.AppendLine("程式函數：" + FuncName.ToString());
            sb.AppendLine("例外處理類型：" + e.GetType().ToString());
            sb.AppendLine("錯誤訊息：" + e.Message);
            sb.AppendLine("程式或物件名稱：" + e.Source);
            sb.AppendLine("產生錯誤程序：" + e.TargetSite.Name);
            sb.AppendLine("錯誤之處：" + e.StackTrace);
            MessageBox.Show(sb.ToString());

        }

        public static void StartKiller(int Second)
        {
            Timer timer = new Timer();
            timer.Interval = Second * 1000; //3秒動依次
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        public static void Timer_Tick(object sender, EventArgs e)
        {
            KillMessageBox();
            //停止Timer
            ((Timer)sender).Stop();
        }

        public static void KillMessageBox()
        {
            //依MessageBox的標題,找出MessageBox的視窗
            IntPtr ptr = FindWindow(null, "錯誤訊息");
            if (ptr != IntPtr.Zero)
            {
                //找到則關閉MessageBox視窗
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
