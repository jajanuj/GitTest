using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AOISystem.Utilities.MultiLanguage;

namespace AOISystem.Utilities.Threading
{
    public enum ActionResultType
    {
        Started, 
        Completed, 
        Failed
    }

    public class ActionItemResult
    {
        public int Value;
        public string Message;
        public ActionResultType Status;
    }

    /// <summary>流程使用，當ActionTask.TurnOn時候，Step、Result會清為0</summary>
    public class TProcVar
    {
        /// <summary>流程步序1 初始為0</summary>
        public int Step1 = 0;

        /// <summary>異常流程步序1 初始為0</summary>
        public int ErrorStep1 = 0;

        /// <summary>字串流程步序1 初始為"0"</summary>
        public string StrStep1 = "0";

        /// <summary>字串異常流程步序1 初始為0</summary>
        public string StrErrorStep1 = "0";

        /// <summary>流程中使用的bool</summary>
        public bool key1;

        /// <summary>流程中使用的bool</summary>
        public bool key2;

        /// <summary>計時器1</summary>
        public Stopwatch TM1 = new Stopwatch();

        /// <summary>計時器2</summary>
        public Stopwatch TM2 = new Stopwatch();

        /// <summary>計時器2</summary>
        public Stopwatch TM3 = new Stopwatch();

        /// <summary> item的執行結果，在流程中手動寫入，預設寫法==> value: 0=未執行(ActionTask.TurnOn會清0)、1=正常、小於0代表trip的case值，Data代表對應值之字串</summary>
        public ActionItemResult Result = new ActionItemResult();

        /// <summary>流程中使用的retry</summary>
        public byte Retry;

        /// <summary>流程中使用的timeout</summary>
        public long Timeout;

        /// <summary>流程中使用的object</summary>
        public object obj;

        /// <summary> 當前步序描述 </summary>
        public string Description = "";
    }

    /// <summary>任務運轉模式-連續或是單次或是順序</summary>
    public enum TaskRunType
    {
        //None,
        //Once,
        //Sequence,
        Cycle
    }

    /// <summary>
    /// 要新增的任務項目類別
    /// </summary>
    public class ActionItem : IEquatable<ActionItem>
    {
        /// <summary> 任務名稱(在同一個ActionTask下，名字必須是唯一的。) </summary>
        public string Name { get; set; }

        /// <summary> 方法啟停 </summary>
        public bool Switch { get; set; }

        /// <summary>  若該方法有操作到表單控制項，必須連同表單傳入，可為null。 </summary>
        public Form Form { get; set; }

        /// <summary>  要傳入的方法 </summary>
        public Action<TProcVar> Action { get; set; }

        /// <summary> 呼叫Switch Case步序時要傳入的TProcVar </summary>
        public TProcVar TProcVar { get; set; }

        /// <summary> 任務標題 </summary>
        public string Title { get; set; }

        /// <summary> 檢查Name是否相等，使用indexOf時會用到該方法。 </summary>
        public bool Equals(ActionItem act)
        {
            if (act == null) return base.Equals(act);

            if (!(act is ActionItem))
                throw new InvalidCastException("The 'act' argument is not a ActionItem object.");
            else
                if (act.Name == this.Name)
                    return true;
                else
                    return false;
        }

        public ActionItem(string name, bool sw, Form form, Action<TProcVar> action)
        {
            this.Name = name;
            this.Switch = sw;
            this.Form = form;
            this.Action = action;
            this.TProcVar = new TProcVar();
        }

        public ActionItem()
        {
            TProcVar = new TProcVar();
        }
    }

    /// <summary>
    /// <see cref="ActionTask"/>類別定義一個任務清單，可以傳入多個方法，並單次或是連續或順序執行。
    /// </summary>
    public class ActionTask
    {
        // 任務清單
        private List<ActionItem> totalTask;
        private List<ActionItem> taskInThread;
        private Thread cycleTaskThread;
        //private Thread onceTaskThread;
        //private Thread sequenceTaskThread;
        private int interval;
        //private TaskRunType runType;
        private bool isRunning;
        private string runningAction;
        //使用在事件
        private bool actionTaskRunKey = true;
        private bool actionTaskWaitKey = true;

        ///// <summary> Task中的RunType </summary>
        //public TaskRunType RunType { get { return runType; } }
        /// <summary> Task中是否有任務在運行 </summary>
        public bool IsRunning { get { return isRunning; } }
        /// <summary> 取得當前運行的方法名稱 </summary>
        public string RunningAction { get { return runningAction; } }

        public delegate void ActionTaskHandler();
        public event ActionTaskHandler ActionTaskRun;
        public event ActionTaskHandler ActionTaskWait;

        ///// <summary>
        ///// 建構式，必須手動執行Run()方法，流程才會開始跑，另一個建構式ActionTask(TaskRunType type, int interval)會執行Run()。
        ///// </summary>
        //public ActionTask()
        //{
        //    isRunning = false;
        //    totalTask = new List<ActionItem>();
        //    taskInThread = new List<ActionItem>();
        //}

        /// <summary>
        /// 建構式，會執行Run()。
        /// </summary>
        public ActionTask(TaskRunType type, int interval)
        {
            isRunning = false;
            totalTask = new List<ActionItem>();
            taskInThread = new List<ActionItem>();
            this.Run(type, interval);
        }

        /// <summary>
        /// 建構式，自訂interval，會執行Run()。
        /// </summary>
        public ActionTask(int interval)
        {
            isRunning = false;
            totalTask = new List<ActionItem>();
            taskInThread = new List<ActionItem>();
            this.Run(TaskRunType.Cycle, interval);
        }

        /// <summary>
        /// 建構式，預設interval=15。
        /// </summary>
        public ActionTask()
        {
            isRunning = false;
            totalTask = new List<ActionItem>();
            taskInThread = new List<ActionItem>();
            this.Run(TaskRunType.Cycle, 15);
        }

        /// <summary>
        /// 新增ActionItem 到ActionTask中，ActionItem名稱不能相同。
        /// </summary>
        public void Add(ActionItem item)
        {
            if (item != null)
            {
                if (totalTask.Count > 0)
                {
                    foreach (ActionItem eachItem in totalTask)
                    {
                        if (item.Name == eachItem.Name)
                        {
                            throw new ArgumentNullException("ActionItem.Name is repeated");
                        }
                    }
                    totalTask.Add(item);
                }
                else
                {
                    totalTask.Add(item);
                }
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 從ActionTask中刪除ActionItem
        /// </summary>
        public void Remove(ActionItem item)
        {
            if (item != null)
            {
                totalTask.Remove(item);
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 插入ActionItem到ActionTask中，配合Run(TaskRunType.Sequence,interval)使用。
        /// </summary>
        public void Insert(int index, ActionItem item)
        {
            if (item != null)
            {
                totalTask.Insert(index, item);
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 將指定ActionItem.Switch改為true，步序重新開始。
        /// </summary>
        public void TurnOn(ActionItem item)
        {
            if (item != null)
            {
                int idx = totalTask.IndexOf(item);
                if (idx != -1)
                {
                    ActionItem[] ai = totalTask.ToArray();
                    ai[idx].Switch = true;
                    initProcVar(ai[idx]);
                    totalTask = ai.ToList();
                }
                else
                {
                    throw new ArgumentNullException("ActionItem Is Not Found");
                }
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 將指定ActionItem.Switch改為truem，並指定Step(int)
        /// </summary>
        public void TurnOnStep(ActionItem item, int step)
        {
            if (item != null)
            {
                int idx = totalTask.IndexOf(item);
                if (idx != -1)
                {
                    ActionItem[] ai = totalTask.ToArray();
                    ai[idx].Switch = true;
                    initProcVar(ai[idx], step);
                    totalTask = ai.ToList();
                }
                else
                {
                    throw new ArgumentNullException("ActionItem Is Not Found");
                }
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 將指定ActionItem.Switch改為truem，並指定Step(string)
        /// </summary>
        public void TurnOnStep(ActionItem item, string step)
        {
            if (item != null)
            {
                int idx = totalTask.IndexOf(item);
                if (idx != -1)
                {
                    ActionItem[] ai = totalTask.ToArray();
                    ai[idx].Switch = true;
                    initProcVar(ai[idx], step);
                    totalTask = ai.ToList();
                }
                else
                {
                    throw new ArgumentNullException("ActionItem Is Not Found");
                }
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 將指定ActionItem.Switch改為true，不指定、初始化步序，從原本停下的步序往下Run。
        /// </summary>
        public void TurnOnWithoutInit(ActionItem item)
        {
            if (item != null)
            {
                int idx = totalTask.IndexOf(item);
                if (idx != -1)
                {
                    ActionItem[] ai = totalTask.ToArray();
                    ai[idx].Switch = true;
                    totalTask = ai.ToList();
                }
                else
                {
                    throw new ArgumentNullException("ActionItem Is Not Found");
                }
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 將ActionTask中的所有ActionItem.Switch改為true
        /// </summary>
        public void TurnOnAll()
        {
            if (totalTask.Count > 0)
            {
                ActionItem[] ai = totalTask.ToArray();
                for (int i = 0; i < ai.Length; i++)
                {
                    ai[i].Switch = true;
                    initProcVar(ai[i]);
                }
                totalTask = ai.ToList();
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 將List中第一個ActionItem.Switch改為true，配合Run(TaskRunType.Sequence,interval)使用。
        /// </summary>
        public void TurnOnFirst()
        {
            if (totalTask.Count > 0)
            {
                TurnOffAll();
                ActionItem[] ai = totalTask.ToArray();
                ai[0].Switch = true;
                initProcVar(ai[0]);
                totalTask = ai.ToList();
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 將指定ActionItem.Switch改為false
        /// </summary>
        /// <param name="item">ActionItem.</param>
        public void TurnOff(ActionItem item)
        {
            if (item != null)
            {
                int idx = totalTask.IndexOf(item);
                if (idx != -1)
                {
                    ActionItem[] ai = totalTask.ToArray();
                    ai[idx].Switch = false;
                    totalTask = ai.ToList();
                }
                else
                {
                    throw new ArgumentNullException("ActionItem Is Not Found");
                }
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 將所有ActionItem.Switch改為false
        /// </summary>
        public void TurnOffAll()
        {
            if (totalTask.Count > 0)
            {
                ActionItem[] ai = totalTask.ToArray();
                for (int i = 0; i < ai.Length; i++)
                {
                    ai[i].Switch = false;
                }
                totalTask = ai.ToList();
            }
            else
            {
                throw new ArgumentNullException("ActionItem Is Null");
            }
        }

        /// <summary>
        /// 清除ActionTask中的所有項目
        /// </summary>
        public void Clear()
        {
            totalTask.Clear();
        }

        /// <summary>
        /// 取得ActionTask中的所有ActionItem
        /// </summary>
        public List<ActionItem> GetTotalContents()
        {
            return totalTask;
        }

        /// <summary>
        /// 取得ActionTask中的所有Switch=true的ActionItem
        /// </summary>
        public List<ActionItem> GetRunningContents()
        {
            List<ActionItem> totalRunning = new List<ActionItem>();
            foreach (ActionItem eachItem in totalTask)
            {
                if (eachItem.Switch == true)
                {
                    totalRunning.Add(eachItem);
                }
            }
            return totalRunning;
        }

        public bool CheckItemRunning(string itemName)
        {
            int result = (from item in GetRunningContents()
                          where item.Name == itemName
                          select item).Count();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <param name="type">
        /// <para>TaskRunType.Single: Thread只輪詢一次TaskList就停止，必須自己TurnOn(item)。[用法待驗證]</para>
        /// <para>TaskRunType.Cycle:Thread會一直輪詢TaskList，輪詢期間必須自己手動TurnOn(item)或TurnOff(item)。</para>
        /// <para>TaskRunType.Sequence:Thread只輪詢一次TaskList就停止，程式會依照Add的時間自動TurnOn(item)。[用法待驗證]</para>
        /// </param>
        /// <param name="interval">延遲執行時間.</param>
        public void Run(TaskRunType type, int interval)
        {
            this.interval = interval;
            switch (type)
            {
                //case TaskRunType.Once:
                //    onceTaskThread = new Thread(new ParameterizedThreadStart(delegate { singleRun(interval); }));
                //    onceTaskThread.IsBackground = true;
                //    onceTaskThread.Start();
                //    break;

                case TaskRunType.Cycle:
                    if (cycleTaskThread != null)
                    {
                        if (cycleTaskThread.IsAlive)
                        {
                            return;
                        }
                    }
                    cycleTaskThread = new Thread(new ParameterizedThreadStart(delegate { cycleRun(interval); }));
                    cycleTaskThread.IsBackground = true;
                    cycleTaskThread.Start();
                    break;

                //case TaskRunType.Sequence:
                //    sequenceTaskThread = new Thread(new ParameterizedThreadStart(delegate { sequenceRun(interval); }));
                //    sequenceTaskThread.IsBackground = true;
                //    sequenceTaskThread.Start();
                //    break;
            }
        }

        /// <summary>
        /// <para>1. Stops the task(建議以ChangeSwitch將Switch改為false，即可再下一次運行停止該項目)。由於本方法是直接停止thread，
        /// 若欲再次執行task則必須再度建立thread，若是經常性的操作，將會損耗系統資源。</para>
        /// <para>2. 若正在執行Run的過程中，欲離開程式時，最好執行Stop來將流程停止與釋放Thread。</para>
        /// </summary>
        public void Stop()
        {
            if (cycleTaskThread != null)
            {
                try
                {
                    cycleTaskThread.Abort();
                }
                catch (ThreadAbortException)
                {
                    Thread.ResetAbort();
                }
                isRunning = false;
                //runType = TaskRunType.None;
            }
        }

        public void SetLanguage(Language language)
        {
            CultureInfo ci;
            if (language == Language.English)
            {
                ci = new CultureInfo("en");
            }
            else
            {
                ci = new CultureInfo("zh-TW");
            }
            cycleTaskThread.CurrentCulture = ci;
            cycleTaskThread.CurrentUICulture = ci;
        }

        private void initProcVar(ActionItem ai)
        {
            ai.TProcVar.Step1 = 0;
            ai.TProcVar.ErrorStep1 = 0;
            ai.TProcVar.StrStep1 = "0";
            ai.TProcVar.StrErrorStep1 = "ErrInit";
            ai.TProcVar.key1 = false;
            ai.TProcVar.key2 = false;
            ai.TProcVar.Retry = 0;
            ai.TProcVar.Timeout = 0;
            ai.TProcVar.obj = new object();
            ai.TProcVar.TM1.Reset();
            ai.TProcVar.TM2.Reset();
            ai.TProcVar.Result.Value = 0;
            ai.TProcVar.Result.Message = "";
            ai.TProcVar.Description = "";
            ai.TProcVar.TM1.Reset();
            ai.TProcVar.TM2.Reset();
            ai.TProcVar.TM3.Reset();
        }

        private void initProcVar(ActionItem ai, int step)
        {
            ai.TProcVar.Step1 = step;
            ai.TProcVar.ErrorStep1 = step;
        }

        private void initProcVar(ActionItem ai, string step)
        {
            ai.TProcVar.StrStep1 = step;
            ai.TProcVar.StrErrorStep1 = step;
        }

        /// <summary>
        /// Cycle run task.
        /// </summary>
        /// <param name="interval">The interval.</param>
        private void cycleRun(int interval)
        {
            Stopwatch sw = new Stopwatch();
            sw = Stopwatch.StartNew();
            long ms = 0;
            while (true)
            {
                ms = sw.ElapsedMilliseconds;
                //if (ms > interval)
                //{
                //把totalTask 複製到 taskInThread
                if (totalTask.Count != taskInThread.Count)
                {
                    taskInThread.Clear();
                    totalTask.ForEach((i) => { taskInThread.Add(i); });
                }
                //Console.WriteLine("threading...");
                int swCnt = 0;
                foreach (ActionItem eachTask in taskInThread)
                {
                    if (eachTask.Switch == true)
                    {
                        swCnt++;
                        runningAction = eachTask.Name;
                        if (eachTask.Form != null)
                        {
                            //Console.WriteLine("ActionName: " + eachTask.Name);
                            try
                            {
                                if (!eachTask.Form.IsDisposed && eachTask.Form != null)
                                {
                                    eachTask.Form.Invoke(eachTask.Action, eachTask.TProcVar);
                                }
                            }
                            catch (ObjectDisposedException)
                            {
                                Stop();
                                //Console.WriteLine("Application Exit");
                            }
                            catch (InvalidOperationException)
                            {
                                Stop();
                            }
                        }
                        else
                        {
                            //Console.WriteLine("ActionName: " + eachTask.Name);
                            try
                            {
                                eachTask.Action(eachTask.TProcVar);
                            }
                            catch (ObjectDisposedException)
                            {
                                Stop();
                                //Console.WriteLine("Application Exit");
                            }
                        }
                    }
                }
                if (swCnt > 0)
                {
                    isRunning = true;
                    if (ActionTaskRun != null)
                    {
                        if (actionTaskRunKey)
                        {
                            ActionTaskRun();
                            actionTaskRunKey = false;
                            actionTaskWaitKey = true;
                        }
                    }
                }
                if (swCnt == 0)
                {
                    isRunning = false;
                    runningAction = "Null";
                    if (ActionTaskWait != null)
                    {
                        if (actionTaskWaitKey)
                        {
                            ActionTaskWait();
                            actionTaskRunKey = true;
                            actionTaskWaitKey = false;
                        }
                    }
                }
                runningAction = null;
                sw.Restart();
                //}
                Thread.Sleep(interval);
            }
        }

        /// <summary>
        /// Single run task. 待驗證
        /// </summary>
        /// <param name="interval">The interval.</param>
        private void singleRun(int interval)
        {
            Stopwatch sw = new Stopwatch();
            sw = Stopwatch.StartNew();
            long ms = 0;
            bool key = true;
            while (key)
            {
                ms = sw.ElapsedMilliseconds;
                if (ms > interval)
                {
                    int swCnt = 0;
                    foreach (ActionItem eachTask in totalTask)
                    {
                        if (eachTask.Switch == true)
                        {
                            swCnt++;
                            if (eachTask.Form != null)
                            {
                                runningAction = eachTask.Name;
                                Console.WriteLine("ActionName: " + eachTask.Name);
                                try
                                {
                                    eachTask.Form.Invoke(eachTask.Action);
                                }
                                catch (ObjectDisposedException)
                                {
                                    Stop();
                                    Console.WriteLine("Application Exit");
                                }
                            }
                            else
                            {
                                runningAction = eachTask.Name;
                                Console.WriteLine("ActionName: " + eachTask.Name);
                                try
                                {
                                    eachTask.Action(eachTask.TProcVar);
                                }
                                catch (ObjectDisposedException)
                                {
                                    Stop();
                                    Console.WriteLine("Application Exit");
                                }
                            }
                        }
                    }
                    if (swCnt > 0)
                    {
                        isRunning = true;
                    }
                    if (swCnt == 0)
                    {
                        isRunning = false;
                    }
                    key = false;
                }
            }
            isRunning = false;
            runningAction = null;
            Stop();
        }

        /// <summary>
        /// Sequence run task. 待驗證
        /// </summary>
        /// <remarks>假設method()為定義在ActionItem中的方法，必須在該方法尾端執行ActionTask.TurnOff(actionItem)
        /// 把自己停止，ActionTask會以此為判斷，自動啟動下一個存在Task中的方法。</remarks>
        /// <param name="interval">The interval.</param>
        private void sequenceRun(int interval)
        {
            Stopwatch sw = new Stopwatch();
            sw = Stopwatch.StartNew();
            long ms = 0;
            int currentIdx = 0;
            TurnOffAll();
            TurnOnFirst();
            ActionItem[] ai = totalTask.ToArray();
            ai[currentIdx].Switch = true;
            while (currentIdx < ai.Length)
            {
                if (ai[currentIdx].Switch == false)
                {
                    if (currentIdx == ai.Length - 1)
                        break;
                    ai[currentIdx + 1].Switch = true;
                    currentIdx++;
                }
                totalTask = ai.ToList();
                ms = sw.ElapsedMilliseconds;
                if (ms > interval)
                {
                    Console.WriteLine("sequence...");
                    Console.WriteLine("state:" + cycleTaskThread.ThreadState);
                    int swCnt = 0;
                    foreach (ActionItem eachTask in totalTask)
                    {
                        if (eachTask.Switch == true)
                        {
                            swCnt++;
                            if (eachTask.Form != null)
                            {
                                runningAction = eachTask.Name;
                                Console.WriteLine("ActionName: " + eachTask.Name);
                                try
                                {
                                    eachTask.Form.Invoke(eachTask.Action);
                                }
                                catch (ObjectDisposedException)
                                {
                                    Stop();
                                    Console.WriteLine("Application Exit");
                                }
                            }
                            else
                            {
                                runningAction = eachTask.Name;
                                Console.WriteLine("ActionName: " + eachTask.Name);
                                try
                                {
                                    eachTask.Action(eachTask.TProcVar);
                                }
                                catch (ObjectDisposedException)
                                {
                                    Stop();
                                    Console.WriteLine("Application Exit");
                                }
                            }
                        }
                    }
                    if (swCnt > 0)
                    {
                        isRunning = true;
                    }
                    if (swCnt == 0)
                    {
                        isRunning = false;
                    }
                    runningAction = null;
                    sw.Restart();
                }
                Thread.Sleep(15);
            }
            isRunning = false;
            runningAction = null;
            Stop();
        }
    }
}
