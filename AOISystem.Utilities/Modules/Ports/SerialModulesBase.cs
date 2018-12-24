using AOISystem.Utilities.Common;
using AOISystem.Utilities.IO;
using AOISystem.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace AOISystem.Utilities.Modules.Ports
{
    public abstract class SerialModulesBase : ModulesBase
    {
        private bool isInitialized;
        private bool comportWriting;
        private bool comportReading;
        private SerialPort comport;
        internal ParameterSerial serialPara;

        private List<List<byte>> sendCmdCollection = new List<List<byte>>();
        private List<List<byte>> receiveCmdCollection = new List<List<byte>>();
        private object key1 = new object();
        private object key2= new object();
        private int step1 = 0;
        private int step2 = 0;

        public bool IsInitialized { get { return isInitialized; } set { isInitialized = value; } }

        public bool IsProcessing { get { return comportWriting | comportReading; } }

        public event EventHandler OnReceiveCommand;

        public event EventHandler OnSendCommand;

        protected SerialModulesBase(ModulesType modulesType, string parameterFolderPath, string deviceName)
            : base(modulesType, parameterFolderPath, deviceName)
        {
            serialPara = Parameter as ParameterSerial;
            serialInitialize();
            comportWriting = false;
            comportReading = false;
        }

        private void serialInitialize()
        {
            if (comport == null)
            {
                try
                {
                    comport = new SerialPort(serialPara.PortNumber, serialPara.BaudRate, serialPara.Parity, serialPara.DataBits, serialPara.StopBits);
                    comport.ReadTimeout = serialPara.ReadTimeout;
                    comport.WriteTimeout = serialPara.WriteTimeout;
                    if (openComport())
                    {
                        new Thread(doSending) { IsBackground = true }.Start();
                        //new Thread(doReceiving) { IsBackground = true }.Start();
                        isInitialized = true;
                    }
                }
                catch (ArgumentException)
                {
                    string path = IniFile.GetIniFilePath(this.ParameterFolderPath, this.DeviceName);
                    if (File.Exists(path))
                    {
                        File.Delete(path);   
                    }
                }
            }
        }

        #region serial method

        /// <summary>
        /// Opens the comport.
        /// </summary>
        private bool openComport()
        {
            bool result = false;
            try
            {
                comport.Open();
                comport.DiscardInBuffer();
                comport.DiscardOutBuffer();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                OnErrorRaised(-1, ExceptionHelper.GetFullCurrentMethod(ex.Message + "(" + DeviceName + ")"));
            }
            return result;
        }

        /// <summary>
        /// Closes the comport.
        /// </summary>
        private void closeComport()
        {
            try
            {
                comport.Close();
                comportWriting = false;
                comportReading = false;
            }
            catch (Exception ex)
            {
                OnErrorRaised(-1, ExceptionHelper.GetFullCurrentMethod(ex.Message + "(" + DeviceName + ")"));
            }
        }

        /// <summary>
        /// 將cmdContent加上起始字元和結束字元，用thread作 comport.Write。
        /// </summary>
        /// <param name="cmdContent">Content of the CMD.</param>
        protected internal void sendCommand(List<byte> cmdCombined)
        {
            if (isInitialized)
            {
                if (!comport.IsOpen)
                {
                    openComport();
                }
                lock (key1)
                {
                    sendCmdCollection.Add(cmdCombined);
                }
            }
            else
            {
                OnErrorRaised(-1, ExceptionHelper.GetFullCurrentMethod("Comport initialize not completed" + "(" + DeviceName + ")"));
            }
        }

        protected virtual void doSending()
        {
            while (true)
            {
                if (comport.IsOpen)
                {
                    try
                    {
                        if (sendCmdCollection.Count > 0)
                        {
                            List<byte> sendCmd = null;
                            lock (key1)
                            {
                                sendCmd = sendCmdCollection[0];
                                sendCmdCollection.RemoveAt(0);
                            }
                            comportWriting = true;
                            comport.Write(sendCmd.ToArray(), 0, sendCmd.Count);
                            if (OnSendCommand != null)
                            {
                                OnSendCommand(Encoding.ASCII.GetString(sendCmd.ToArray()), new EventArgs());
                            }
                            Thread.Sleep(serialPara.ReadDelayTime);
                            comportReading = true;
                            if (!doReceiving())
                            {
                                Thread.Sleep(serialPara.WriteIntervalTime);
                                comport.Write(sendCmd.ToArray(), 0, sendCmd.Count);
                                if (OnSendCommand != null)
                                {
                                    OnSendCommand(Encoding.ASCII.GetString(sendCmd.ToArray()), new EventArgs());
                                }
                                Thread.Sleep(serialPara.ReadDelayTime);
                                comportReading = true;
                                if (doReceiving())
                                {
                                    LogHelper.Debug("{0} first rerty sendCmd {1}", this.DeviceName, Encoding.ASCII.GetString(sendCmd.ToArray()));
                                }
                                else
                                {
                                    LogHelper.Debug("{0} sencod rerty sendCmd {1}", this.DeviceName, Encoding.ASCII.GetString(sendCmd.ToArray()));
                                    OnErrorRaised(1000, string.Format("{0} sencod rerty sendCmd {1}", this.DeviceName, Encoding.ASCII.GetString(sendCmd.ToArray())));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        comport.DiscardOutBuffer();
                        //closeComport();
                        OnErrorRaised(-1, ExceptionHelper.GetFullCurrentMethod(ex.Message + "(" + DeviceName + ")"));
                        comportWriting = false;
                    }
                    finally
                    {
                        comportWriting = false;
                    }
                }
                Thread.Sleep(serialPara.WriteIntervalTime);
            }
        }

        protected virtual bool doReceiving()
        {
            bool success = false;
            List<Byte> recevedDataList = new List<Byte>();
            Stopwatch sw = new Stopwatch();
            while (comportReading)
            {
                if (comport.IsOpen)
                {
                    try
                    {
                        while (comport.BytesToRead > 0)
                        {
                            int receivedValue = comport.ReadByte();
                            recevedDataList.Add((Byte)receivedValue);
                            if (analyzeReceiveData(recevedDataList))
                            {
                                if (OnReceiveCommand != null)
                                {
                                    OnReceiveCommand(Encoding.ASCII.GetString(recevedDataList.ToArray()), new EventArgs());
                                }
                                recevedDataList.Clear();
                                success = true;
                            }
                            else
                            {
                                if (!sw.IsRunning)
                                {
                                    sw.Restart();
                                }
                            }
                            if (sw.IsRunning && sw.ElapsedMilliseconds > 500)
                            {
                                comport.DiscardInBuffer();
                                OnErrorRaised(-1, string.Format("{0} Receiving Timeout. Exist Cmd {1}", this.DeviceName, Encoding.ASCII.GetString(recevedDataList.ToArray())));
                                recevedDataList.Clear();
                                sw.Stop();
                                success = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        comport.DiscardInBuffer();
                        //closeComport();
                        LogHelper.Exception(ex);
                        OnErrorRaised(-1, ExceptionHelper.GetFullCurrentMethod(ex.Message + "(" + DeviceName + ")"));
                    }
                    finally
                    {
                        comportReading = false;
                    }
                }
                //Thread.Sleep(this.ReceveDataIntervalTime);
            }
            return success;
        }

        /// <summary>
        /// 分析接收到訊息
        /// </summary>
        protected virtual bool analyzeReceiveData(List<Byte> recevedDataList)
        {
            return true;
        }

        #endregion serial method

        public void ShowConfiguration()
        {
            ParameterSerialSettingForm frmParameterSerialSetting = new ParameterSerialSettingForm(this.Parameter, this.DeviceName);
            frmParameterSerialSetting.ShowDialog();
            ((ParameterINI)this.Parameter).Save();
        }

        public void ShowMonitor()
        {
            FormHelper.OpenUniqueForm("SerialModulesMonitorForm" + this.DeviceName, () =>
            {
                SerialModulesMonitorForm serialModulesMonitorForm = new SerialModulesMonitorForm(this);
                serialModulesMonitorForm.Name = "SerialModulesMonitorForm" + this.DeviceName;
                serialModulesMonitorForm.Show();
            });
        }
    }
}
