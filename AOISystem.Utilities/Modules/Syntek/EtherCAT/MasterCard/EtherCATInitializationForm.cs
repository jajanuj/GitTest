using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules.Syntek.EtherCAT.MasterCard
{
    public partial class EtherCATInitializationForm : Form
    {
        private static EtherCATInitializationForm _splashScreenForm = new EtherCATInitializationForm();

        private Thread _thread;

        public EtherCATInitializationForm()
        {
            InitializeComponent();
        }

        private void SplashScreenForm_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        public static EtherCATInitializationForm GetInstance()
        {
            if (_splashScreenForm.IsDisposed)
            {
                _splashScreenForm = new EtherCATInitializationForm();
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

        public void SetCardList(Dictionary<ushort, CardInfo> dicCardInfos)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<Dictionary<ushort, CardInfo>>(SetCardList), new object[] { dicCardInfos });
                return;
            }
            this.cboCardNo.Items.Clear();
            foreach (var item in dicCardInfos)
            {
                this.cboCardNo.Items.Add(item.Value.CardNo); 
            }
            this.cboCardNo.SelectedIndex = 0;
        }

        public void SetStatus( string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetStatus), new object[] { msg });
                return;
            }
            this.lblStatusMsg.Text = msg;
        }

        public void SetStatus(ushort cardNo, string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<ushort, string>(SetStatus), new object[] { cardNo, msg });
                return;
            }
            if (this.cboCardNo.Text == cardNo.ToString())
            {
                this.lblStatusMsg.Text = msg;   
            }
        }
    }
}
