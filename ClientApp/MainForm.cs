﻿using AOISystem.MMFHandshake;
using AOISystem.Utilities.Logging;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class MainForm : Form
    {
        private MMFClient _mmfClient;
        private Mutex _mutex;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LogHelper.Initialize(@"D:\ClientApp_Data", 30);

            _mmfClient = new MMFClient();
            _mmfClient.ErrorRaised += _mmfClient_ErrorRaised;
            _mutex = new Mutex(false, MMFDefine.MUTEX_NAME);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mmfClient.ErrorRaised -= _mmfClient_ErrorRaised;
            _mmfClient.Stop();
            this.timer.Stop();
        }

        private void _mmfClient_ErrorRaised(Exception ex)
        {
            LogHelper.Exception(ex);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _mmfClient.Start();
            this.timer.Start();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_mutex.WaitOne())
            {
                try
                {
                    if (this.txtMessage.Text.Length > MMFDefine.BLOCK_FIELD_STRING_SIZE)
                    {
                        throw new Exception("字串超過長度" + MMFDefine.BLOCK_FIELD_STRING_SIZE);
                    }
                    _mmfClient.ClientBlock.Message = this.txtMessage.Text;
                    byte[] buffer = Encoding.ASCII.GetBytes(this.txtBuffer.Text);
                    if (buffer.Length > MMFDefine.BLOCK_FIELD_ARRAY_SIZE)
                    {
                        throw new Exception("陣列超過長度" + MMFDefine.BLOCK_FIELD_ARRAY_SIZE);
                    }
                    _mmfClient.ClientBlock.Buffer = new byte[MMFDefine.BLOCK_FIELD_ARRAY_SIZE];
                    Array.Copy(buffer, _mmfClient.ClientBlock.Buffer, buffer.Length);
                }
                catch (Exception ex)
                {
                    LogHelper.Exception(ex);
                }
                finally
                {
                    _mutex.ReleaseMutex();
                }
            }
        }

        private void slbClientToServer_Click(object sender, EventArgs e)
        {
            this.slbClientToServer.Checked = !this.slbClientToServer.Checked;
            _mmfClient.ClientBlock.ClientToServerRequest = this.slbClientToServer.Checked;
        }

        private void slbServerToClient_Click(object sender, EventArgs e)
        {
            this.slbServerToClient.Checked = !this.slbServerToClient.Checked;
            _mmfClient.ClientBlock.ServerToClientReply = this.slbServerToClient.Checked;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_mutex.WaitOne())
            {
                try
                {
                    this.ledClientToServer.On = _mmfClient.ServerBlock.ClientToServerReply;
                    this.ledServerToClient.On = _mmfClient.ServerBlock.ServerToClientRequest;
                    if (_mmfClient.ServerBlock.Message != null)
                    {
                        if (_mmfClient.ServerBlock.Message != this.lblMessage.Text)
                        {
                            this.lblMessage.Text = _mmfClient.ServerBlock.Message;
                        }
                    }
                    if (_mmfClient.ServerBlock.Buffer != null)
                    {
                        string buffer = Encoding.ASCII.GetString(_mmfClient.ServerBlock.Buffer).TrimEnd('\0');
                        if (buffer != this.lblBuffer.Text)
                        {
                            this.lblBuffer.Text = buffer;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Exception(ex);
                }
                finally
                {
                    _mutex.ReleaseMutex();
                }
            }
        }
    }
}
