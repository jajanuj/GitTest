using AOISystem.MMFHandshake;
using AOISystem.Utilities.Logging;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ServerApp
{
    public partial class MainForm : Form
    {
        private MMFServer _mmfServer;
        private Mutex _mutex;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LogHelper.Initialize(@"D:\ServerApp_Data", 30);

            _mmfServer = new MMFServer();
            _mmfServer.ErrorRaised += _mmfServer_ErrorRaised;
            _mutex = new Mutex(false, MMFDefine.MUTEX_NAME);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mmfServer.ErrorRaised -= _mmfServer_ErrorRaised;
            _mmfServer.Stop();
            this.timer.Stop();
        }

        private void _mmfServer_ErrorRaised(Exception ex)
        {
            LogHelper.Exception(ex);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _mmfServer.Start();
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
                    _mmfServer.ServerBlock.Message = this.txtMessage.Text;
                    byte[] buffer = Encoding.ASCII.GetBytes(this.txtBuffer.Text);
                    if (buffer.Length > MMFDefine.BLOCK_FIELD_ARRAY_SIZE)
                    {
                        throw new Exception("陣列超過長度" + MMFDefine.BLOCK_FIELD_ARRAY_SIZE);
                    }
                    _mmfServer.ServerBlock.Buffer = new byte[MMFDefine.BLOCK_FIELD_ARRAY_SIZE];
                    Array.Copy(buffer, _mmfServer.ServerBlock.Buffer, buffer.Length);
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

        private void slbServerToClient_Click(object sender, EventArgs e)
        {
            this.slbServerToClient.Checked = !this.slbServerToClient.Checked;
            _mmfServer.ServerBlock.ServerToClientRequest = this.slbServerToClient.Checked;
        }

        private void slbClientToServer_Click(object sender, EventArgs e)
        {
            this.slbClientToServer.Checked = !this.slbClientToServer.Checked;
            _mmfServer.ServerBlock.ClientToServerReply = this.slbClientToServer.Checked;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_mutex.WaitOne())
            {
                try
                {
                    this.ledServerToClient.On = _mmfServer.ClientBlock.ServerToClientReply;
                    this.ledClientToServer.On = _mmfServer.ClientBlock.ClientToServerRequest;
                    if (_mmfServer.ClientBlock.Message != null)
                    {
                        if (_mmfServer.ClientBlock.Message != this.lblMessage.Text)
                        {
                            this.lblMessage.Text = _mmfServer.ClientBlock.Message;
                        }
                    }
                    if (_mmfServer.ClientBlock.Buffer != null)
                    {
                        string buffer = Encoding.ASCII.GetString(_mmfServer.ClientBlock.Buffer).TrimEnd('\0');
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
