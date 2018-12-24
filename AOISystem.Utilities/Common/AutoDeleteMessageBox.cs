using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AOISystem.Utilities.Common
{
    public class AutoDeleteMessageBox
    {
        public AutoDeleteMessageBox()
        {
            TimerInterval = 3000;
        }

        public int TimerInterval;

        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public const int WM_CLOSE = 0x10;

        public void doStartKiller()
        {
            StartKiller();
            MessageBox.Show("3秒後自動關閉MessageBox視窗", "MessageBox");
        }

        public void doStartKiller(string Msg)
        {
            StartKiller();
            MessageBox.Show(Msg, "Message",
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);
        }

        public void doStartKiller(string Msg,MessageBoxIcon MessageType,int DisplayTime)
        {
            TimerInterval = DisplayTime;
            StartKiller();
            MessageBox.Show(Msg, "Message",
                MessageBoxButtons.OK,
                MessageType);
        }

        private void StartKiller()
        {
            Timer timer = new Timer();
            timer.Interval = TimerInterval; //3秒啓動
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            KillMessageBox();
            //停止Timer
            ((Timer)sender).Stop();
        }

        private void KillMessageBox()
        {
            //依MessageBox的標題,找出MessageBox的視窗
            IntPtr ptr = FindWindow(null, "Message");
            if (ptr != IntPtr.Zero)
            {
                //找到則關閉MessageBox視窗
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
