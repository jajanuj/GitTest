using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public partial class SplashScreenForm : Form
    {
        private static SplashScreenForm _splashScreenForm = new SplashScreenForm();

        private Thread _thread;

        public SplashScreenForm()
        {
            InitializeComponent();
        }

        private void SplashScreenForm_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        public static SplashScreenForm GetInstance()
        {
            if (_splashScreenForm.IsDisposed)
            {
                _splashScreenForm = new SplashScreenForm();
            }
            return _splashScreenForm;
        }

        public void ShowForm()
        {
            if (!_splashScreenForm.Visible)
            {
                _thread = new Thread(() => Application.Run(_splashScreenForm));
                _thread.IsBackground = true;
                _thread.SetApartmentState(ApartmentState.STA);
                _thread.Start();   
            }
        }

        public void CloseForm()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(CloseForm));
                return;
            }
            if (!_splashScreenForm.IsDisposed)
            {
                _splashScreenForm.Close();
            }
        }

        public void SetStatus(string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetStatus), msg);
                return;
            }
            this.lblStatusMsg.Text = msg;
        }
    }
}
