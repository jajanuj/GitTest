using AOISystem.Utilities.Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AOISystem.Utilities.Modules.Ports
{
    public abstract class SocketModulesBase : ModulesBase
    {
        internal ParameterSocket socketPara;

        protected const char CR = '\r';

        protected const string CRLF = "\r\n";

        public string ResponseString = "";

        public string ErrorMessage = "";

        private IPAddress Module_IP;

        private int Module_port;

        private byte[] Sendbuf;

        private byte[] Rcvbuf;

        private TcpClient client;

        private NetworkStream stream;

        private bool TCPopen;

        private bool IsConnectionSuccessful = false;
        private Exception socketexception;
        private ManualResetEvent timeoutMRE = new ManualResetEvent(false);

        private bool isInitialized;
        public bool IsInitialized { get { return isInitialized; } set { isInitialized = value; } }

        protected SocketModulesBase(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            socketPara = Parameter as ParameterSocket;
            SocketlInitialize();
        }

        private void SocketlInitialize()
        {
            this.Module_IP = IPAddress.Parse(socketPara.Address);
            this.Module_port = socketPara.Port;
            this.Rcvbuf = new byte[800];
            this.Sendbuf = new byte[256];
            isInitialized = true;
        }

        #region Socket Method

        public void CloseConnection()
        {
            try
            {
                this.stream.Close();
            }
            catch
            {
            }
            try
            {
                this.client.Close();
            }
            catch
            {
            }
            this.TCPopen = false;
        }

        public void SetReceiveTimeout(int TimeOutVal)
        {
            socketPara.ReceiveTimeout = TimeOutVal;
        }

        public bool SendReceive(string cmd, bool KeepOpen)
        {
            bool result = true;
            try
            {
                byte b = 0;
                if (!this.TCPopen)
                {
                    //this.client = new TcpClient(this.Module_IP.ToString(), this.Module_port);
                    //this.stream = this.client.GetStream();
                    //this.TCPopen = true;

                    //將執行緒通知事件ManualResetEvent設定為未收到訊號,此時進入的事件會持續執行
                    timeoutMRE.Reset();
                    socketexception = null;//清除例外承載物件
                    this.client = new TcpClient();
                    //使用非同步連線方式,當連線完成後會通知CallBackMethod事件
                    this.client.BeginConnect(this.Module_IP.ToString(), this.Module_port, new AsyncCallback(CallBackMethod), this.client);

                    if (timeoutMRE.WaitOne(socketPara.ConnectTimeout, false))
                    {
                        if (IsConnectionSuccessful)
                        {
                            this.stream = this.client.GetStream();
                            this.TCPopen = true;
                        }
                        else
                        {
                            //沒有連線成功丟出Exception
                            throw socketexception;
                        }
                    }
                    else
                    {
                        //當等候時間逾時
                        this.client.Close();
                        throw new TimeoutException("Time Out Exception !");
                    }
                }
                this.Sendbuf = Encoding.ASCII.GetBytes(cmd + '\r');
                while (this.stream.DataAvailable)
                {
                    b = (byte)this.stream.ReadByte();
                }
                this.stream.Write(this.Sendbuf, 0, this.Sendbuf.Length);
                this.ResponseString = "";
                this.client.ReceiveTimeout = socketPara.ReceiveTimeout;
                do
                {
                    try
                    {
                        b = (byte)this.stream.ReadByte();
                    }
                    catch (Exception ex)
                    {
                        this.ResponseString = "";
                        this.ErrorMessage = ex.Message;
                        b = 13;
                    }
                    if (b != 13)
                    {
                        this.ResponseString += (char)b;
                    }
                }
                while (b != 13);
                if (!KeepOpen)
                {
                    this.client.Close();
                    this.stream.Close();
                    this.TCPopen = false;
                }
            }
            catch (Exception ex2)
            {
                result = false;
                this.ResponseString = "";
                this.ErrorMessage = ex2.Message;
                OnErrorRaised(1000, ExceptionHelper.GetFullCurrentMethod(this.ErrorMessage + "(" + DeviceName + ")"));
            }
            return result;
        }

        public void Recieve()
        { 
            this.ResponseString = "";
            byte b = 0;
            this.client.ReceiveTimeout = socketPara.ReceiveTimeout;

            do
             {
                    try
                    {
                        b = (byte)this.stream.ReadByte();
                    }
                    catch (Exception ex)
                    {
                        this.ResponseString = "";
                        this.ErrorMessage = ex.Message;
                        b = 13;
                    }

                    if (b != 13)
                    {
                        this.ResponseString += (char)b;
                    }
            }while (b != 13);
        }


        public bool SendReceive(string cmd)
        {
            return this.SendReceive(cmd, false);
        }

        //當連線完成後，進入此事件處理
        private void CallBackMethod(IAsyncResult asyncresult)
        {
            try
            {
                IsConnectionSuccessful = false;
                //檢查回傳物件是否存在(存在代表連線成功)
                TcpClient tcpclient = asyncresult.AsyncState as TcpClient;
                if (tcpclient.Client != null)
                {
                    tcpclient.EndConnect(asyncresult);
                    IsConnectionSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                //發生連線意外
                IsConnectionSuccessful = false;
                socketexception = ex;
            }
            finally
            {
                //通知等候結束
                timeoutMRE.Set();
            }
        }

        #endregion Socket Method

        public void ShowConfiguration()
        {
            ParameterSocketSettingForm frmParameterSocketSetting = new ParameterSocketSettingForm(this.Parameter, this.DeviceName);
            frmParameterSocketSetting.ShowDialog();
            ((ParameterINI)this.Parameter).Save();
        }

        public void ShowMonitor()
        {
        }
    }
}
