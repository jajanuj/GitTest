using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AOISystem.Utilities.Account
{
    //1.0.0.5 新增登出提示訊息視窗
    public partial class LogOutForm : Form
    {
        public LogOutForm()
        {
            InitializeComponent();
        }

        private int _timerCount = 0;
        private const int LOGOUTTIME = 60;

        private void tmrAutoLog_Tick(object sender, EventArgs e)
        {
            _timerCount++;
            lblTime.Text = string.Format("({0})", LOGOUTTIME - _timerCount);

            if (_timerCount >= LOGOUTTIME)
            {
                _timerCount = 0;
                tmrAutoLog.Enabled = false;
                this.DialogResult = DialogResult.Yes;
                Close();
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LogOutForm_Load(object sender, EventArgs e)
        {
            tmrAutoLog.Enabled = true;
            lblTime.Text = string.Format("({0})", LOGOUTTIME - _timerCount);
        }
    }
}
