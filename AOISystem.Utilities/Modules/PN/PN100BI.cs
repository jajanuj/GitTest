using AOISystem.Utilities.Logging;
using AOISystem.Utilities.Modules.Ports;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace AOISystem.Utilities.Modules.PN
{

    /// <summary>PN模組通訊命令 </summary>
    public enum PnModuleCmd
    {
        EQINT,
        CVSPS,
        CVSPG,
        SMPTS,
        SMPTG,
        PNMPS,
        PNMPG,
        PNMDS,
        PNMDG,
        PNDFS,
        PNDFG,
        CNTRS,
        CMESS,
        CMESE,
        STATR,
        CMPND
    }

    public class PN100BI : SocketModulesBase
    {
        #region field
        
        #endregion field

        #region properties

        public bool ReplyStatus {get; private set;}

        public bool ModuleReady { get; private set; }

        public bool Connected { get; private set; }

        public ConcurrentQueue<string> PnQueue = new ConcurrentQueue<string>();

        #endregion properties

        #region construct

        public PN100BI(ModulesType modulesType, string xmlFilePath, string deviceName)
            : base(modulesType, xmlFilePath, deviceName)
        {

        }

        #endregion construct

        #region - Public Event Methods -

        public event Action SocketConnectEvent;
        private void OnSocketConnectEvent()
        {
            if (SocketConnectEvent != null)
                SocketConnectEvent();
        }

        public event Action SocketDisconnectEvent;
        private void OnSocketDisconnectEvent()
        {
            if (SocketDisconnectEvent != null)
                SocketDisconnectEvent();
        }

        public event Action StartMeasureReadyEvent;
        private void OnStartMeasureReadyEvent()
        {
            if (StartMeasureReadyEvent != null)
                StartMeasureReadyEvent();
        }

        #endregion - Public Event Methods -

        #region public method

        /// <summary>連結服務及發送初始化命令(EQINT)</summary>
        /// <returns>回傳是否成功回應</returns>
        public bool StartService()
        {
            if (SendCmd(PnModuleCmd.EQINT))
            {
                Connected = true;
                OnSocketConnectEvent();
                LogHelper.PN("PN Start Service");
                return true;
            }
            else
                return false;
        }

        public void StopService()
        {
            try
            {
                
                this.CloseConnection();
                ModuleReady = false;
                Connected = false;
                OnSocketDisconnectEvent();
                LogHelper.Handshake("PN Stop Service");
            }
            catch  (SocketException se)
            {
                throw new Exception(se.Message, se.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public bool SendCmd(PnModuleCmd cmd)
        {
            string CmdString = string.Format("{0}{1}", cmd.ToString(), GetCmdEndSymbol());

            //switch (cmd)
            //{
            //    case PnModuleCmd.EQINT:
            //        CmdString = string.Format("EQINT{0}", GetCmdEndSymbol());
            //        break;
            //    case PnModuleCmd.CVSPS:
            //        CmdString = "EQINT,\n";
            //        break;
            //    case PnModuleCmd.CVSPG:
            //        CmdString = "EQINT,\r";
            //        break;
            //    case PnModuleCmd.SMPTS:
            //        CmdString = "EQINT,\r\n";
            //        break;
            //    case PnModuleCmd.SMPTG:
            //        CmdString = "EQINT,";
            //        break;
            //    case PnModuleCmd.PNMPS:
            //        break;
            //    case PnModuleCmd.PNMPG:
            //        break;
            //    case PnModuleCmd.PNMDS:
            //        break;
            //    case PnModuleCmd.PNMDG:
            //        break;
            //    case PnModuleCmd.PNDFS:
            //        break;
            //    case PnModuleCmd.PNDFG:
            //        break;
            //    case PnModuleCmd.CNTRS:
            //        break;
            //    case PnModuleCmd.CMESS:
            //        CmdString = string.Format("CMESS{0}", GetCmdEndSymbol());
            //        break;
            //    case PnModuleCmd.CMESE:
            //        break;
            //    case PnModuleCmd.STATR:
            //        CmdString = string.Format("STATR{0}", GetCmdEndSymbol());
            //        break;
            //    default:
            //        break;

            //}

            if (this.SendReceive(CmdString, true))
            {
                return AnalyzeReceiveData(this.ResponseString);
            }
            else
                return false;
        }

        public bool GetResult()
        {
            if (AnalyzeReceiveData(this.ResponseString))
                return true;
            else
                return false;
        }

        public void ClearPnQueue()
        {
            while (PnQueue.Count > 0)
            {
                string PnResult = string.Empty;
                PnQueue.TryDequeue(out PnResult);
            }
        }

        #endregion public method

        #region private method

        private string GetCmdEndSymbol()
        {
            return ",";
        }

        private bool AnalyzeReceiveData(string receiveString)
        {
            LogHelper.Handshake("Receive Data :{0}", receiveString);
            ReplyStatus = false;
            PnModuleCmd cmd;

            try
            {
                string[] SplitArray = receiveString.Split(',');
                string SendCmd = SplitArray[0];
                string Data = string.Empty;

                cmd = (PnModuleCmd)Enum.Parse(typeof(PnModuleCmd), SendCmd);

                switch (cmd)
                {
                    case PnModuleCmd.EQINT:
                        ModuleReady = false;
                        ReplyStatus = ( SplitArray[1] == "00" ) ? true : false;

                        if (ReplyStatus)
                        {
                            LogHelper.PN("PN Module Init OK");
                        }
                        break;
                    case PnModuleCmd.CVSPS:
                        break;
                    case PnModuleCmd.CVSPG:
                        break;
                    case PnModuleCmd.SMPTS:
                        break;
                    case PnModuleCmd.SMPTG:
                        break;
                    case PnModuleCmd.PNMPS:
                        break;
                    case PnModuleCmd.PNMPG:
                        break;
                    case PnModuleCmd.PNMDS:
                        break;
                    case PnModuleCmd.PNMDG:
                        break;
                    case PnModuleCmd.PNDFS:
                        break;
                    case PnModuleCmd.PNDFG:
                        break;
                    case PnModuleCmd.CNTRS:
                        break;
                    case PnModuleCmd.CMESS:
                        {
                            ReplyStatus = ( SplitArray[1] == "00" ) ? true : false;

                            if (ReplyStatus)
                            {
                                ModuleReady = ReplyStatus;
                                OnStartMeasureReadyEvent();
                            }
                        }
                        break;
                    case PnModuleCmd.CMESE:
                        break;
                    case PnModuleCmd.STATR:
                        ReplyStatus = ( SplitArray[1] == "00" ) ? true : false;
                        break;
                    case PnModuleCmd.CMPND:
                        ReplyStatus = ( SplitArray[3] == "00" ) ? true : false;

                        if (ReplyStatus)
                        {
                            LogHelper.PN("Recieve Data:{0}", receiveString);

                            if (CheckMeasureData(receiveString, out Data))
                                PnQueue.Enqueue(Data);
                        }
                        break;
                    default:
                        break;
                }

                return ReplyStatus;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(string.Format("Receive String Error\n Msg:{0}\nStackTrace:{1}", ex.Message, ex.StackTrace));
            }
        }

        private bool CheckMeasureData(string data, out string MeasureData)
        {
            MeasureData = string.Empty;
            string[] SplitArray = data.Split(',');

            if (SplitArray.Length > 5)
            {
                MeasureData = ( Int16.Parse(SplitArray[4]) > Int16.Parse(SplitArray[5]) ) ? "P" : "N";
                LogHelper.PN("PN :{0}", MeasureData);
            }
            else
            {
                MeasureData = "NA";
                LogHelper.PN("PN :{0}", "PN Error");
            }
            return true;
        }

        #endregion private method
    }
}
