using System.Collections.Generic;
using System.Text;

namespace AOISystem.Utilities.Flow
{
    public class FlowControlHelper
    {
        public Dictionary<string, FlowControl> FlowControlCollection { get; set; }

        public FlowControlHelper()
        {
            this.FlowControlCollection = new Dictionary<string, FlowControl>();
        }

        /// <summary>
        /// 目前存在多少掃描流程
        /// </summary>
        public static int IsAliveFlowControlCount;

        /// <summary>
        /// 新增流程控制
        /// </summary>
        /// <param name="flowControl"></param>
        /// <returns></returns>
        public bool AddFlowControl(FlowControl flowControl)
        {
            if (FlowControlCollection.ContainsKey(flowControl.Name))
            {
                //throw new ArgumentException(string.Format("FlowManager exists same FlowControl {0}.", flowControl.Name));
                return false;
            }
            else
            {
                FlowControlCollection.Add(flowControl.Name, flowControl);
                return true;
            }
        }

        /// <summary>
        /// 得到流程控制
        /// </summary>
        /// <param name="name">FlowControl Name</param>
        /// <param name="autoAddNewInstance">當找不到FlowControl是否要自動產生</param>
        /// <param name="autoAddInFlowControlCollection">當沒有在FlowControl集合內是否要自動加入集合</param>
        /// <returns>FlowControl</returns>
        public FlowControl GetFlowControl(string name, bool autoAddNewInstance = true, bool autoAddInFlowControlCollection = true)
        {
            FlowControl flowControl = null;
            if (!FlowControlCollection.ContainsKey(name))
            {
                if (autoAddNewInstance)
                {
                    flowControl = new FlowControl(name);
                }
                if (autoAddInFlowControlCollection)
                {
                    AddFlowControl(flowControl);
                }
                //throw new ArgumentException(string.Format("The FlowControl {0} doesn't exist.", name));
            }
            else
            {
                flowControl = FlowControlCollection[name];
            }
            return flowControl;
        }

        /// <summary>
        /// 流程項目是否在執行
        /// </summary>
        /// <param name="flowControlName">流程控制名稱</param>
        /// <param name="flowBaseName">流程項目名稱</param>
        /// <returns></returns>
        public bool IsRunning(string flowControlName, string flowBaseName)
        {
            bool isRunning = false;
            FlowControl flowControl = GetFlowControl(flowControlName);
            if (flowControl != null)
            {
                FlowBase flowBase = flowControl.GetFlowBase(flowBaseName);
                isRunning = flowBase.IsRunning;
            }
            return isRunning;
        }

        /// <summary>
        /// 流程控制內所有流程項目是否有在執行
        /// </summary>
        /// <param name="flowControlName">流程控制名稱</param>
        /// <returns>有項目正在執行</returns>
        public bool IsRunningAllFlowBase(string flowControlName)
        {
            string isRunningItems = string.Empty;
            return IsRunningAllFlowBase(flowControlName, out isRunningItems);
        }

        /// <summary>
        /// 流程控制內所有流程項目是否有在執行
        /// </summary>
        /// <param name="flowControlName">流程控制名稱</param>
        /// <param name="isRunningItems">輸出目前還在執行項目</param>
        /// <returns>有項目正在執行</returns>
        public bool IsRunningAllFlowBase(string flowControlName, out string isRunningItems)
        {
            bool isRunning = false;
            isRunningItems = string.Empty;
            FlowControl flowControl = GetFlowControl(flowControlName);
            isRunning = flowControl.IsRunningAll(out isRunningItems);
            return isRunning;
        }

        /// <summary>
        /// 所有流程控制內所有流程項目是否有在執行
        /// </summary>
        /// <returns>有項目正在執行</returns>
        public bool IsRunningAllFlowControl()
        {
            string isRunningItems = string.Empty;
            return IsRunningAllFlowControl(out isRunningItems);
        }

        /// <summary>
        /// 所有流程控制內所有流程項目是否有在執行
        /// </summary>
        /// <param name="isRunningItems">輸出目前還在執行項目</param>
        /// <returns>有項目正在執行</returns>
        public bool IsRunningAllFlowControl(out string isRunningItems)
        {
            bool isRunning = false;
            StringBuilder itesms = new StringBuilder();
            foreach (var flowControl in FlowControlCollection)
            {
                string items = string.Empty;
                if (flowControl.Value.IsRunningAll(out items))
                {
                    isRunning = true;
                    itesms.AppendLine(string.Format("FlowControl {0} IsRunning", flowControl.Value.Name));
                    itesms.AppendLine(items);
                }
            }
            isRunningItems = itesms.ToString();
            return isRunning;
        }

        /// <summary>
        /// 所有流程控制內啟動所有流程項目
        /// </summary>
        public void StartAll(string description = "")
        {
            foreach (var flowControl in FlowControlCollection)
            {
                flowControl.Value.StartAll(description);
            }
        }

        /// <summary>
        /// 所有流程控制內重新啟動所有流程項目
        /// </summary>
        public void RestartAll(string description = "")
        {
            foreach (var flowControl in FlowControlCollection)
            {
                flowControl.Value.RestartAll(description);
            }
        }

        /// <summary>
        /// 所有流程控制內暫停所有流程項目
        /// </summary>
        public void PauseAll(string description = "")
        {
            foreach (var flowControl in FlowControlCollection)
            {
                flowControl.Value.PauseAll(description);
            }
        }

        /// <summary>
        /// 所有流程控制內停止所有流程項目
        /// </summary>
        public void StopAll(string description = "")
        {
            foreach (var flowControl in FlowControlCollection)
            {
                flowControl.Value.StopAll(description);
            }
        }

        /// <summary>
        /// 所有流程控制內中斷所有流程項目
        /// </summary>
        public void FailAll(string description = "")
        {
            foreach (var flowControl in FlowControlCollection)
            {
                flowControl.Value.FailAll(description);
            }
        }

        /// <summary>
        /// 初始化所有項目
        /// </summary>
        public void InitializeAll()
        {
            foreach (var flowControl in FlowControlCollection)
            {
                flowControl.Value.InitializeAll();
            }
        }

        /// <summary>
        /// 得到所有流程項目資訊
        /// </summary>
        public List<string> GetAllFlowBaseInfo()
        {
            List<string> flowBaseInfos = new List<string>();
            foreach (var flowControl in FlowControlCollection)
            {
                flowBaseInfos.AddRange(flowControl.Value.GetAllFlowBaseInfo());
            }
            return flowBaseInfos;
        }

        /// <summary>
        /// 釋放Media Timer資源
        /// </summary>
        public void Dispose()
        {
            foreach (var flowControl in FlowControlCollection)
            {
                flowControl.Value.Dispose();
            }
        }
    }
}
