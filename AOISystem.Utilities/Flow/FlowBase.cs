using AOISystem.Utilities.Common;
using AOISystem.Utilities.Component;
using AOISystem.Utilities.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace AOISystem.Utilities.Flow
{
    public class FlowBase : FlowVar
    {
        #region - Private Fields -

        private bool _timerKey1;
        private bool _timerKey2;
        private bool _timerKey3;
        private bool _cycleTimerKey;

        private Task _waitPauseTimeoutTask;

        private DateTime _startTime;

        #endregion - Private Fields -

        #region - Public Constructor -

        /// <summary>
        /// 流程項目
        /// </summary>
        /// <param name="name">流程名稱</param>
        /// <param name="action">流程回圈, 以覆寫Flow方法可以保持為Null</param>
        /// <param name="context">UI跨執行緒上下文同步, 保持Null表示使用純執行緒跑流程回圈, 效率會更好</param>
        public FlowBase(string name = "", Action<FlowVar> action = null, SynchronizationContext context = null)
        {
            this.Name = string.IsNullOrEmpty(name) ? this.GetType().Name : name;
            this.Action = action;
            this.CurrentSynchronizationContext = context;
            this.StatusTable = new Dictionary<string, bool>();
        }

        #endregion - Public Constructor -

        #region - Event Methods -

        public event FlowHandler Starting;

        public event FlowHandler Started;

        public event FlowHandler Pausing;

        public event FlowHandler Paused;

        public event FlowHandler WaitPaused;

        public event FlowHandler Stopping;

        public event FlowHandler Stopped;

        public event FlowHandler Failing;

        public event FlowHandler Failed;

        private void OnStarting(string description)
        {
            if (Starting != null)
            {
                Starting(description);
            }
        }

        private void OnStarted(string description)
        {
            if (Started != null)
            {
                Started(description);
            }
        }

        private void OnPausing(string description)
        {
            if (Pausing != null)
            {
                Pausing(description);
            }
        }

        private void OnPaused(string description)
        {
            if (Paused != null)
            {
                Paused(description);
            }
        }

        private void OnWaitPaused(string description)
        {
            if (WaitPaused != null)
            {
                WaitPaused(description);
            }
        }

        private void OnStopping(string description)
        {
            if (Stopping != null)
            {
                Stopping(description);
            }
        }

        private void OnStopped(string description)
        {
            if (Stopped != null)
            {
                Stopped(description);
            }
        }

        public void OnFailing(string description)
        {
            if (Failing != null)
            {
                Failing(description);
            }
        }

        public void OnFailed(string description)
        {
            if (Failed != null)
            {
                Failed(description);
            }
        }

        #endregion - Event Methods -

        #region - Public Properties -

        /// <summary>
        /// 流程控制
        /// </summary>
        [Browsable(false), NonMember]
        public FlowControl FlowControl { get; set; }

        /// <summary>
        /// 流程回圈, 以覆寫Flow方法可以保持為Null
        /// </summary>
        [Browsable(false), NonMember]
        public Action<FlowVar> Action { get; set; }

        /// <summary>
        /// UI跨執行緒使用上下文同步, 保持Null表示使用純執行緒跑流程回圈, 效率會更好
        /// </summary>
        [Browsable(false), NonMember]
        public SynchronizationContext CurrentSynchronizationContext { get; set; }

        /// <summary>
        /// 狀態字典查詢
        /// </summary>
        [Browsable(false), NonMember]
        public Dictionary<string, bool> StatusTable { get; set; }

        #endregion  - Public Properties -

        #region - Public Methods -

        /// <summary>
        /// 流程主體
        /// </summary>
        public void FlowAction()
        {
            try
            {
                lock (this.SyncKey)
                {
                    if (this.IsPauseRequested)
                    {
                        this.WaitPauseTimeoutFlow();
                    }
                    if (this.IsRunning)
                    {
                        _startTime = DateTime.Now;
                        this.CycleTimes.AddSample(this.CycleTimer.ElapsedMilliseconds);
                        this.CycleTimer.Restart();
                        this.Flow();
                        this.TACT = this.CycleTimer.ElapsedMilliseconds;
                        //todo FlowBase TACT
                        if (this.IsRecordCycleTime)
                        //if (this.IsRecordCycleTime || this.TACT > 20)
                        {
                            this.IsRecordCycleTime = false;
                            LogHelper.Debug(this.GetFlowInfo());
                            LogHelper.Debug("Flow : {0} CycleTime AVG : {1} Max : {2} Min : {3} FTT : {4} From : {5:HH:mm:ss.fff}", this.Name, this.CycleTimes.Average, this.CycleTimes.Max, this.CycleTimes.Min, this.TACT, _startTime);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Exception(ex);
                Fail("FlowAction Throw Exception");
            }
        }

        /// <summary>
        /// 流程方法
        /// </summary>
        public virtual void Flow()
        {
            if (Action == null)
            {
                throw new ArgumentNullException(this.Name + " FlowBase Action Loop is Null");
            }
            if (CurrentSynchronizationContext == null)
            {
                Action(this);
            }
            else
            {
                CurrentSynchronizationContext.Send((o) =>
                {
                    Action(this);
                }, null);
            }
        }

        /// <summary>
        /// 等待暫停流程Time out方法
        /// </summary>
        public virtual void WaitPauseTimeoutFlow()
        {
            if (this.IsRunning && _waitPauseTimeoutTask == null)
            {
                _waitPauseTimeoutTask = Task.Factory.StartNew(() =>
                {
                    if (!SpinWait.SpinUntil(() => !this.IsRunning || !this.IsPauseRequested, this.WaitPauseTimeout))
                    {
                        this.Description = "Waiting Pause Timeout, Auto Reset Flow Status.";
                        Fail(this.Name + " FlowControl : " + "Desc = " + this.Description);
                    }
                    else
                    {
                        this.Description = string.Format("Waiting Pause Flow : {0}", this.IsPauseRequested ? "Stop" : "Cancel");
                        OnWaitPaused(this.Name + " FlowControl : " + "Desc = " + this.Description);
                        OnBuildLog(this.Name + " FlowControl : " + "Desc = " + this.Description);
                    }
                }).ContinueWith((_) =>
                {
                    _waitPauseTimeoutTask = null;
                });
            }
        }

        /// <summary>
        /// 開始流程
        /// </summary>
        /// <param name="description"></param>
        public void Start(string description = "")
        {
            lock (this.SyncKey)
            {
                this.IsPauseRequested = false;
                if (!this.IsRunning)
                {
                    this.Status = FlowStatus.Starting;
                    this.IsRunning = true;
                    if (this.FlowControl != null)
                    {
                        this.FlowControl.StartScanFlow();   
                    }
                    OnStarting(this.Name + " Starting : " + "Desc = " + description);
                    if (_timerKey1)
                    {
                        this.Timer1.Start();
                        _timerKey1 = false;
                    }
                    if (_timerKey2)
                    {
                        this.Timer2.Start();
                        _timerKey2 = false;
                    }
                    if (_timerKey3)
                    {
                        this.Timer3.Start();
                        _timerKey3 = false;
                    }
                    if (_cycleTimerKey)
                    {
                        this.CycleTimer.Start();
                        _cycleTimerKey = false;
                    }
                    this.Description = string.IsNullOrEmpty(description) ? "Null" : description;
                    this.Status = FlowStatus.Started;
                    OnStarted(this.Name + " Started : " + "Desc = " + description);
                    OnBuildLog(this.Name + " Start : " + "Desc = " + description);
                }
            }
        }

        /// <summary>
        /// 重啟流程(如流程還在運行會先停止流程後重置)
        /// </summary>
        /// <param name="description"></param>
        public void Restart(string description = "")
        {
            lock (this.SyncKey)
            {
                if (this.IsRunning)
                {
                    Stop(description);
                }
                Initialize();
                Start(description);
            }
        }

        /// <summary>
        /// 暫停流程(週期停止機制, 請在流程加上IsPauseRequested判斷, 最後下IsRunning=false)
        /// </summary>
        /// <param name="description"></param>
        public void Pause(string description = "")
        {
            lock (this.SyncKey)
            {
                if (this.IsRunning)
                {
                    this.Status = FlowStatus.Pausing;
                    OnPausing(this.Name + " Pausing : " + "Desc = " + description);
                    if (this.IsWaitingPauseFlow)
                    {
                        this.IsPauseRequested = true;
                    }
                    else
                    {
                        this.IsRunning = false;
                        StopTimer();
                    }
                    this.Description = string.IsNullOrEmpty(description) ? "Null" : description;
                    this.Status = FlowStatus.Paused;
                    OnPaused(this.Name + " Paused : " + "Desc = " + description);
                    OnBuildLog(this.Name + " Pause : " + "Desc = " + description);
                }
            }
        }

        /// <summary>
        /// 停止流程(會一同重置狀態)
        /// </summary>
        /// <param name="description"></param>
        public void Stop(string description = "")
        {
            lock (this.SyncKey)
            {
                //if (this.IsRunning)
                {
                    this.Status = FlowStatus.Stopping;
                    OnStopping(this.Name + " Stopping : " + "Desc = " + description);
                    Initialize();
                    this.Description = string.IsNullOrEmpty(description) ? "Null" : description;
                    this.Status = FlowStatus.Stopped;
                    OnStopped(this.Name + " Stopped : " + "Desc = " + description);
                    OnBuildLog(this.Name + " Stop : " + "Desc = " + description);
                }
            }
        }

        /// <summary>
        /// 中止流程(會一同重置狀態)
        /// </summary>
        /// <param name="description"></param> 
        public void Fail(string description = "")
        {
            lock (this.SyncKey)
            {
                //if (this.IsRunning)
                {
                    this.Status = FlowStatus.Failing;
                    OnFailing(this.Name + " Failing : " + "Desc = " + description);
                    if (this.IsInitializeAfterFailRaised)
                    {
                        Initialize();
                    }
                    else
                    {
                        this.IsRunning = false;
                        StopTimer();
                    }
                    this.Description = string.IsNullOrEmpty(description) ? "Null" : description;
                    this.Status = FlowStatus.Failed;
                    OnFailed(this.Name + " Failed : " + "Desc = " + description);
                    OnBuildLog(this.Name + " Fail : " + "Desc = " + description);
                }
            }
        }

        /// <summary>
        /// 停止計時器
        /// </summary>
        public void StopTimer()
        {
            if (this.Timer1.IsRunning)
            {
                this.Timer1.Stop();
                _timerKey1 = true;
            }
            if (this.Timer2.IsRunning)
            {
                this.Timer2.Stop();
                _timerKey2 = true;
            }
            if (this.Timer3.IsRunning)
            {
                this.Timer3.Stop();
                _timerKey3 = true;
            }
            if (this.CycleTimer.IsRunning)
            {
                this.CycleTimer.Stop();
                _cycleTimerKey = true;
            }
        }

        public void ShowMonitor(bool hideControlButton)
        {
            FormHelper.OpenUniqueForm(this.Name, () =>
            {
                FlowMonitorForm fmf = new FlowMonitorForm(this, this.Name, hideControlButton);
                fmf.Show();
            });
        }

        public object InvokeMethod(string methodName, object[] parameters)
        {
            return this.GetType().GetMethod(methodName).Invoke(this, parameters);
        }

        #endregion - Public Methods -

        #region - Private Methods -

        #endregion - Private Methods -
    }
}
