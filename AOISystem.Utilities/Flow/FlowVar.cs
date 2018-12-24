using AOISystem.Utilities.Common;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace AOISystem.Utilities.Flow
{
    public class FlowVar
    {
        #region - Public Constructor -

        public FlowVar(string name = "")
        {
            this.Name = string.IsNullOrEmpty(name) ? this.GetType().Name : name;
            this.DetialLogEnable = true;
            this.IsWaitingPauseFlow = false;
            this.WaitPauseTimeout = 3000;
            this.IsInitializeAfterFailRaised = false;
            Initialize();
        }

        #endregion - Public Constructor -

        #region - Public Fields -

        /// <summary> 同步Key </summary>
        public object SyncKey = new object();

        #endregion - Public Fields -

        #region - Public Properties -

        [Category("General"), Description("Name")]
        public string Name { get; set; }

        [Category("General"), Description("Detail Logging")]
        public bool DetialLogEnable { get; set; }

        /// <summary> 流程是否正在運行 </summary>
        [Category("General"), Description("Is Running")]
        public bool IsRunning { get; set; }

        /// <summary> 流程是否發出暫停要求 </summary>
        [Category("General"), Description("Is Pause Requested")]
        public bool IsPauseRequested { get; set; }

        /// <summary> 流程是否等待暫停流程 </summary>
        [Category("General"), Description("Is Waiting Pause Flow")]
        public bool IsWaitingPauseFlow { get; set; }

        [Category("General"), Description("Wait Pause Timeout")]
        public int WaitPauseTimeout { get; set; }

        /// <summary> 流程失敗時是否初始化 </summary>
        [Category("General"), Description("Is Initialize After Fail Raised")]
        public bool IsInitializeAfterFailRaised { get; set; }

        [Category("Status"), Description("Flow Status")]
        public FlowStatus Status { get; set; }

        [Category("General"), Description("Current step in flow")]
        public string Step1 { get; set; }

        [Category("General"), Description("Current step in flow")]
        public string Step2 { get; set; }

        [Category("General"), Description("Current step in flow")]
        public string Step3 { get; set; }

        [Category("General"), Description("Description of current step")]
        public string Description { get; set; }

        [Category("General"), Description("Flow TimeVar1")]
        public long TimeVar1 { get; set; }

        [Category("General"), Description("Flow TimeVar2")]
        public long TimeVar2 { get; set; }

        [Category("General"), Description("Flow TimeVar3")]
        public long TimeVar3 { get; set; }

        [Browsable(false)]
        public Stopwatch Timer1 { get; set; }

        [Browsable(false)]
        public Stopwatch Timer2 { get; set; }

        [Browsable(false)]
        public Stopwatch Timer3 { get; set; }

        [Browsable(false)]
        public Stopwatch CycleTimer { get; set; }

        [Browsable(false)]
        public SimpleMovingAverage CycleTimes { get; set; }

        [Browsable(false)]
        public long TACT { get; set; }

        [Browsable(false)]
        public bool IsRecordCycleTime { get; set; }

        #endregion - Public Properties -

        #region - Event Methods -

        public delegate void FlowHandler(string description);

        public event FlowHandler StepChanged;

        public event FlowHandler Initialized;

        public event FlowHandler BuildLog;

        public void OnInitialized(string description)
        {
            if (this.Initialized != null)
            {
                this.Initialized(description);
            }
        }

        public void OnBuildLog(string description)
        {
            if (this.BuildLog != null && this.DetialLogEnable)
            {
                this.BuildLog(description);
            }
        }

        public void OnStepChanged(string step, string description)
        {
            if (StepChanged != null && this.DetialLogEnable)
            {
                StepChanged(description);
            }
        }

        #endregion - Event Methods -

        #region - Public Methods -

        public void Initialize()
        {
            this.IsRunning = false;
            this.IsPauseRequested = false;
            this.Status = FlowStatus.Initialize;
            this.Step1 = "0";
            this.Step2 = "0";
            this.Step3 = "0";
            this.Description = "Initialize";
            if (this.Timer1 == null)
            {
                this.Timer1 = new Stopwatch();
            }
            else
            {
                this.Timer1.Reset();
            }
            if (this.Timer2 == null)
            {
                this.Timer2 = new Stopwatch();
            }
            else
            {
                this.Timer2.Reset();
            }
            if (this.Timer3 == null)
            {
                this.Timer3 = new Stopwatch();
            }
            else
            {
                this.Timer3.Reset();
            }
            if (this.CycleTimer == null)
            {
                this.CycleTimer = new Stopwatch();
            }
            else
            {
                this.CycleTimer.Reset();
            }

            this.CycleTimes = new SimpleMovingAverage(50);
            this.IsRecordCycleTime = false;

            OnInitialized(this.Description);
        }

        public bool CheckStepCorrect(string step)
        {
            if (!string.IsNullOrEmpty(step))
            {
                return true;
            }
            else
            {
                if (step == null)
                {
                    throw new ArgumentException("The content of step is null");
                }
                else
                {
                    throw new ArgumentException("The content of step ( " + step + " ) isn't corrent");
                }
            }
        }

        public void ChangeStep1(FlowVar flowVar, string step, string description = "", bool buildLogEnable = true)
        {
            if (CheckStepCorrect(step))
            {
                flowVar.Step1 = step;
                flowVar.Description = string.IsNullOrEmpty(description) ? "Null" : description;

                if (string.IsNullOrEmpty(description))
                {
                    OnStepChanged(step, flowVar.Name + " Step1 = " + step);
                    if (buildLogEnable)
                    {
                        OnBuildLog(flowVar.Name + " Step1 = " + step);
                    }
                }
                else
                {
                    OnStepChanged(step, flowVar.Name + " Step1 = " + step + "  Desc = " + description);
                    if (buildLogEnable)
                    {
                        OnBuildLog(flowVar.Name + " Step1 = " + step + "  Desc = " + description);
                    }
                }
            }
        }

        public void ChangeStep1(string step, string description = "", bool buildLogEnable = true)
        {
            ChangeStep1(this, step, description, buildLogEnable);
        }

        public void ChangeStep2(FlowVar flowVar, string step, string description = "", bool buildLogEnable = true)
        {
            if (CheckStepCorrect(step))
            {
                flowVar.Step2 = step;
                flowVar.Description = string.IsNullOrEmpty(description) ? "Null" : description;

                if (string.IsNullOrEmpty(description))
                {
                    OnStepChanged(step, flowVar.Name + " Step2 = " + step);
                    if (buildLogEnable)
                    {
                        OnBuildLog(flowVar.Name + " Step2 = " + step);
                    }
                }
                else
                {
                    OnStepChanged(step, flowVar.Name + " Step2 = " + step + "  Desc = " + description);
                    if (buildLogEnable)
                    {
                        OnBuildLog(flowVar.Name + " Step2 = " + step + "  Desc = " + description);
                    }
                }
            }
        }

        public void ChangeStep2(string step, string description = "", bool buildLogEnable = true)
        {
            ChangeStep2(this, step, description, buildLogEnable);
        }

        public void ChangeStep3(FlowVar flowVar, string step, string description = "", bool buildLogEnable = true)
        {
            if (CheckStepCorrect(step))
            {
                flowVar.Step3 = step;
                flowVar.Description = string.IsNullOrEmpty(description) ? "Null" : description;

                if (string.IsNullOrEmpty(description))
                {
                    OnStepChanged(step, flowVar.Name + " Step3 = " + step);
                    if (buildLogEnable)
                    {
                        OnBuildLog(flowVar.Name + " Step3 = " + step);
                    }
                }
                else
                {
                    OnStepChanged(step, flowVar.Name + " Step3 = " + step + "  Desc = " + description);
                    if (buildLogEnable)
                    {
                        OnBuildLog(flowVar.Name + " Step3 = " + step + "  Desc = " + description);
                    }
                }
            }
        }

        public void ChangeStep3(string step, string description = "", bool buildLogEnable = true)
        {
            ChangeStep3(this, step, description, buildLogEnable);
        }

        public virtual string GetOtherMessage()
        {
            return string.Empty;
        }

        public virtual string GetFlowInfo()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", this.Name, this.IsRunning, this.Status, this.Step1, this.Step2, this.Step3, this.Description, this.GetOtherMessage());
        }

        #endregion - Public Methods -
    }
}
