using AOISystem.Utilities.Modules.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AOISystem.Utilities.Modules
{
    public partial class SerialModulesMonitorForm : Form
    {
        private SerialModulesBase serialModulesBase;

        private delegate void SetRTBHanlder(RTBItem item , string content);

        public SerialModulesMonitorForm(SerialModulesBase serialModulesBase)
        {
            InitializeComponent();
            this.serialModulesBase = serialModulesBase;
        }

        private void SerialModulesMonitorForm_Load(object sender , EventArgs e)
        {
            this.serialModulesBase.OnReceiveCommand += serialModulesBase_OnReceiveCommand;
            this.serialModulesBase.OnSendCommand += serialModulesBase_OnSendCommand;
        }

        private void SerialModulesMonitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.serialModulesBase.OnReceiveCommand -= serialModulesBase_OnReceiveCommand;
            this.serialModulesBase.OnSendCommand -= serialModulesBase_OnSendCommand;
        }

        private void serialModulesBase_OnSendCommand(object sender , EventArgs e)
        {
            SetRTBContent(RTBItem.Send , sender.ToString());
        }

        private void serialModulesBase_OnReceiveCommand(object sender , EventArgs e)
        {
            SetRTBContent(RTBItem.Receive , sender.ToString());
        }

        private void btnSend_Click(object sender , EventArgs e)
        {
            //serialModulesBase.IsProcessing = false;
            List<byte> content = new List<byte>();
            content.AddRange(Encoding.ASCII.GetBytes(rtbSendCommand.Text));
            serialModulesBase.sendCommand(content);
        }

        private enum RTBItem { Send , Receive }

        private void SetRTBContent(RTBItem item , string content)
        {
            if (this.InvokeRequired)
            {
                SetRTBHanlder callback = new SetRTBHanlder(SetRTBContent);
                this.Invoke(callback , item , content);
            }
            else
            {
                string contentAppendTime = string.Format("{0}.{1} --> {2}\n" , DateTime.Now , DateTime.Now.Millisecond.ToString() , content);
                switch (item)
                {
                    case RTBItem.Send:
                        rtbOnSendCommand.Text += contentAppendTime;
                        rtbOnSendCommand.SelectionStart = rtbOnSendCommand.Text.Length + 1;
                        rtbOnSendCommand.ScrollToCaret();
                        break;

                    case RTBItem.Receive:
                        rtbOnReceiveCommand.Text += contentAppendTime;
                        rtbOnReceiveCommand.SelectionStart = rtbOnReceiveCommand.Text.Length + 1;
                        rtbOnReceiveCommand.ScrollToCaret();
                        break;
                }
            }
        }

        private void btnReceiveClear_Click(object sender , EventArgs e)
        {
            rtbOnReceiveCommand.Text = "";
        }

        private void btnSendClear_Click(object sender , EventArgs e)
        {
            rtbOnSendCommand.Text = "";
        }

        private void btnResetCmd_Click(object sender , EventArgs e)
        {
            //serialModulesBase.IsProcessing = false;
        }
    }
}