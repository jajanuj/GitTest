using AOISystem.Utilities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AOISystem.Utilities.Flow
{
    public class FlowControl
    {
        #region - Private Fields -

        private Thread _flowScanner;//掃描流程的執行序緒
        //private MMTimer mmTimer;
        //private Multimedia.Timer mmTimer;
        private object _key = new object();
        private Dictionary<string, FlowBase> _totalFlow;
        private uint _loops = 0;
        private int _iterations = 10;

        #endregion - Private Fields -

        #region - Public Constructor -

        public FlowControl(string name)
        {
            this.Name = name;
            _totalFlow = new Dictionary<string, FlowBase>();
        }

        #endregion - Public Constructor -

        #region - Public Properties -

        [Category("General"), Description("Name")]
        public string Name { get; private set; }

        #endregion - Public Properties -

        #region - Public Methods -
        /// <summary>
        /// 新增流程項目
        /// </summary>
        /// <param name="flowBase"></param>
        /// <returns></returns>
        public bool AddFlowBase(FlowBase flowBase, bool autoReplaceInstance = true)
        {
            lock (_key)
            {
                if (_totalFlow.ContainsKey(flowBase.Name))
                {
                    if (autoReplaceInstance)
                    {
                        if (_totalFlow[flowBase.Name].IsRunning)
                        {
                            _totalFlow[flowBase.Name].Stop();
                        }
                        flowBase.FlowControl = this;
                        _totalFlow[flowBase.Name] = flowBase;
                    }
                    //throw new ArgumentException(string.Format("FlowControl exists same FlowBase {0}.", flowBase.Name));
                    return false;
                }
                else
                {
                    flowBase.FlowControl = this;
                    _totalFlow.Add(flowBase.Name, flowBase);
                    return true;
                }
            }
        }

        /// <summary>
        /// 刪除流程項目
        /// </summary>
        /// <param name="flowBase"></param>
        /// <returns></returns>
        public bool DeleteFlowBase(FlowBase flowBase)
        {
            lock (_key)
            {
                if (_totalFlow.ContainsKey(flowBase.Name))
                {
                    if (_totalFlow[flowBase.Name].IsRunning)
                    {
                        _totalFlow[flowBase.Name].Stop();
                    }
                    _totalFlow.Remove(flowBase.Name);
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 清空流程項目
        /// </summary>
        /// <param name="flowBase"></param>
        /// <returns></returns>
        public void ClearFlowBase()
        {
            lock (_key)
            {
                foreach (var flow in _totalFlow)
                {
                    if (flow.Value.IsRunning)
                    {
                        flow.Value.Stop();
                    }
                }
            }
            _totalFlow.Clear();
        }

        /// <summary>
        /// 得到流程項目
        /// </summary>
        /// <param name="name">FlowBase Name</param>
        /// <param name="autoAddNewInstance">當找不到FlowBase是否要自動產生</param>
        /// <returns>FlowBase</returns>
        public FlowBase GetFlowBase(string name, bool autoAddNewInstance = true)
        {
            FlowBase flowBase = null;
            if (!_totalFlow.ContainsKey(name))
            {
                if (autoAddNewInstance)
                {
                    flowBase = new FlowBase(name);
                    flowBase.FlowControl = this;
                    lock (_key)
                    {
                        _totalFlow.Add(name, flowBase);
                    }
                }
                //throw new ArgumentException(string.Format("The FlowBase {0} doesn't exist.", name));
            }
            else
            {
                flowBase = _totalFlow[name];
            }
            return flowBase;
        }

        /// <summary>
        /// 流程項目是否在執行
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsRunning(string name)
        {
            return GetFlowBase(name).IsRunning;
        }

        /// <summary>
        /// 所有流程項目是否有在執行
        /// </summary>
        /// <returns>有項目正在執行</returns>
        public bool IsRunningAll()
        {
            string isRunningItems = string.Empty;
            return IsRunningAll(out isRunningItems);
        }

        /// <summary>
        /// 所有流程項目是否有在執行
        /// </summary>
        /// <param name="isRunningItems">輸出目前還在執行項目</param>
        /// <returns>有項目正在執行</returns>
        public bool IsRunningAll(out string isRunningItems)
        {
            bool isRunning = false;
            StringBuilder itesms = new StringBuilder();
            lock (_key)
            {
                foreach (var flow in _totalFlow)
                {
                    if (flow.Value.IsRunning)
                    {
                        isRunning = true;
                        itesms.AppendLine(flow.Value.Name);
                    }
                }   
            }
            isRunningItems = itesms.ToString();
            return isRunning;
        }

        /// <summary>
        /// 初始化項目
        /// </summary>
        public void Initialize(string name)
        {
            FlowBase instance = GetFlowBase(name);
            instance.Initialize();
        }

        /// <summary>
        /// 初始化所有項目
        /// </summary>
        public void InitializeAll()
        {
            lock (_key)
            {
                foreach (var item in _totalFlow)
                {
                    FlowBase instance = item.Value;
                    instance.Initialize();
                }
            }
        }

        /// <summary>
        /// 啟動流程項目
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void Start(string name, string description = "")
        {
            FlowBase instance = GetFlowBase(name);
            instance.Start(description);
        }

        /// <summary>
        /// 啟動所有流程項目
        /// </summary>
        /// <param name="description"></param>
        public void StartAll(string description = "")
        {
            lock (_key)
            {
                foreach (var item in _totalFlow)
                {
                    FlowBase instance = item.Value;
                    instance.Start(description);
                }
            }
        }

        /// <summary>
        /// 重新啟動流程項目
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void Restart(string name, string description = "")
        {
            FlowBase instance = GetFlowBase(name);
            instance.Restart(description);
        }

        /// <summary>
        /// 重新啟動所有流程項目
        /// </summary>
        /// <param name="description"></param>
        public void RestartAll(string description = "")
        {
            lock (_key)
            {
                foreach (var item in _totalFlow)
                {
                    FlowBase instance = item.Value;
                    instance.Restart(description);
                }
            }
        }

        /// <summary>
        /// 暫停流程項目
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void Pause(string name, string description = "")
        {
            FlowBase instance = GetFlowBase(name);
            instance.Pause(description);
        }

        /// <summary>
        /// 暫停所有流程項目
        /// </summary>
        /// <param name="description"></param>
        public void PauseAll(string description = "")
        {
            lock (_key)
            {
                foreach (var item in _totalFlow)
                {
                    FlowBase instance = item.Value;
                    instance.Pause(description);
                }
            }
        }

        /// <summary>
        /// 停止流程項目
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void Stop(string name, string description = "")
        {
            FlowBase instance = GetFlowBase(name);
            instance.Stop(description);
        }

        /// <summary>
        /// 停止所有流程項目
        /// </summary>
        /// <param name="description"></param>
        public void StopAll(string description = "")
        {
            lock (_key)
            {
                foreach (var item in _totalFlow)
                {
                    FlowBase instance = item.Value;
                    instance.Stop(description);
                }
            }
        }

        /// <summary>
        /// 中斷流程項目
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void Fail(string name, string description = "")
        {
            FlowBase instance = GetFlowBase(name);
            instance.Fail(description);
        }

        /// <summary>
        /// 中斷所有流程項目
        /// </summary>
        /// <param name="description"></param>
        public void FailAll(string description = "")
        {
            lock (_key)
            {
                foreach (var item in _totalFlow)
                {
                    FlowBase instance = item.Value;
                    instance.Fail(description);
                }
            }
        }

        /// <summary>
        /// 顯示流程項目監控
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hideControlButton"></param>
        public void ShowMonitor(string name, bool hideControlButton)
        {
            FlowBase flowBase = GetFlowBase(name);
            flowBase.ShowMonitor(hideControlButton);
        }

        /// <summary>
        /// 得到所有流程項目資訊
        /// </summary>
        public List<string> GetAllFlowBaseInfo()
        {
            string time = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            List<string> flowBaseInfos = new List<string>();
            lock (_key)
            {
                foreach (var item in _totalFlow)
                {
                    FlowBase fbVar = item.Value;
                    flowBaseInfos.Add(string.Format("{0},{1}", time, fbVar.GetFlowInfo()));
                }
            }
            return flowBaseInfos;
        }

        /// <summary>
        /// 釋放Media Timer資源
        /// </summary>
        public void Dispose()
        {
            //if (mmTimer != null)
            //{
            //    mmTimer.Stop();
            //    mmTimer.Dispose();
            //}
        }

        /// <summary>
        /// 啟動流程掃描
        /// </summary>
        public void StartScanFlow()
        {
            if (_flowScanner == null || !_flowScanner.IsAlive)
            {
                _flowScanner = new Thread(() => { ScanTotalFlow(); });
                _flowScanner.IsBackground = true;
                _flowScanner.Start();
            }
            //mmTimer = new MMTimer();
            //mmTimer.Timer += (senser, ea) =>
            //{
            //    FlowAction();
            //};
            //mmTimer.Start(1, true);
            //if (ModulesFactory.Components == null)
            //{
            //    throw new NullReferenceException("ModulesFactory.Components is null, check the MainForm.Design IContainer instance.");
            //}
            //mmTimer = new Multimedia.Timer(ModulesFactory.Components);
            //mmTimer.Mode = Multimedia.TimerMode.Periodic;
            //mmTimer.Period = 1;
            //mmTimer.Resolution = 1;
            //mmTimer.Tick += (senser, ea) =>
            //{
            //    FlowAction();
            //};
            //mmTimer.Start();
        }

        #endregion - Public Methods -

        #region - Private Methods -

        private void ScannerStop(string message)
        {
            if (_flowScanner != null)
            {
                try
                {
                    _flowScanner.Abort();
                }
                catch (ThreadAbortException)
                {
                    Thread.ResetAbort();
                }
                ExceptionHelper.CommonMessageShow("The Flow Scanner has aborted, you must close and restart the system, please. : \r\n Error Message : \r\n" + message, "SystemHint", MessageBoxIcon.Error);
            }
            //if (mmTimer != null)
            //{
            //    mmTimer.Stop();
            //    ExceptionHelper.CommonMessageShow("The Flow Scanner has aborted, you must close and restart the system, please. : \r\n Error Message : \r\n" + message, "SystemHint", MessageBoxIcon.Error);
            //}
        }
        Stopwatch sleepSW = new Stopwatch();
        // scan total flow.
        private void ScanTotalFlow()
        {
            Interlocked.Increment(ref FlowControlHelper.IsAliveFlowControlCount);
            while (IsRunningAll())
            {
                FlowAction();

                long spinTimes = 0;
                sleepSW.Restart();
                _loops = (_loops + 1) % 100;
                if (Environment.ProcessorCount == 1 || _loops == 0)
                {
                    Thread.Sleep(1);
                    //Wait(1, out spinTimes);
                }
                else
                {
                    //Thread.SpinWait(_iterations);
                    Wait(1, out spinTimes);
                }
                sleepSW.Stop();
                //todo FlowControl TACT
                //if (sleepSW.ElapsedMilliseconds > 20)
                //{
                //    LogHelper.Debug("Sleep FTT : {0}, {1}", sleepSW.ElapsedMilliseconds, spinTimes);
                //}
            }
            Interlocked.Decrement(ref FlowControlHelper.IsAliveFlowControlCount);
        }

        private void FlowAction()
        {
            lock (_key)
            {
                foreach (var item in _totalFlow)
                {
                    FlowBase eachFlow = item.Value;
                    try
                    {
                        eachFlow.FlowAction();
                    }
                    catch (Exception ex)
                    {
                        ScannerStop(ex.Message + "\r\n" + ex.StackTrace);
                    }
                }
            }
        }

        SpinWait spin = new SpinWait();
        private void Wait(long timeout, out long spinTimes)
        {
            spinTimes = 0;
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeout)
            {
                spin.SpinOnce();
                spinTimes++;
            }
            sw.Stop();
        }

        #endregion - Private Methods -
    }
}
