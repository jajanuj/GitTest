using System.Threading.Tasks;

namespace AOISystem.Utilities.Threading
{
    public class TaskManager
    {
        private static frmTaskMonitor taskMonitor = frmTaskMonitor.CreateInstance();

        public static void AddTask(Task task)
        {
            taskMonitor.AddTask(task);
        }

        public static void ShowTaskMonitor()
        {
            taskMonitor.Show();
        }

        public static void CloseFormTaskMonitor()
        {
            taskMonitor.CloseForm();
        }

        public static void ClearIsCompleted()
        {
            taskMonitor.ClearIsCompleted();
        }

        public static void ClearIsCanceled()
        {
            taskMonitor.ClearIsCanceled();
        }

        public static void ClearIsFaulted()
        {
            taskMonitor.ClearIsFaulted();
        }

        public static void RefreshData()
        {
            taskMonitor.RefreshData();
        }
    }
}
