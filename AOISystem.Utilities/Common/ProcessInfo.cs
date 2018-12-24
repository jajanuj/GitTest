using System;
using System.Diagnostics;

namespace AOISystem.Utilities
{
    public class ProcessInfo
    {
        /// <summary>
        /// 目前Process是否為IDE編輯模式
        /// </summary>
        /// <returns></returns>
        public static bool IsDesignMode()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            return processName == "devenv" || processName == "VCSExpress" || processName == "WDExpress";
        }

        public static void GetCurrentProcessMemoryState()
        {
            Process process = Process.GetCurrentProcess();
            Console.WriteLine(string.Format("關聯處理序的實體記憶體量: {0}", process.WorkingSet64));
            Console.WriteLine(string.Format("關聯處理序的可分頁系統記憶體量: {0}", process.PagedSystemMemorySize64));
            Console.WriteLine(string.Format("關聯處理序的分頁記憶體量: {0}", process.PagedMemorySize64));
            Console.WriteLine(string.Format("關聯處理序的未分頁系統記憶體量: {0}", process.NonpagedSystemMemorySize64));
            Console.WriteLine(string.Format("關聯處理序所使用之虛擬記憶體分頁檔的最大記憶體量: {0}", process.PeakPagedMemorySize64));
            Console.WriteLine(string.Format("關聯處理序所使用的最大虛擬記憶體量: {0}", process.PeakVirtualMemorySize64));
            Console.WriteLine(string.Format("關聯處理序所使用最大實體記憶體: {0}", process.PeakWorkingSet64));
            Console.WriteLine(string.Format("關聯處理序的私用記憶體量: {0}", process.PrivateMemorySize64));
            Console.WriteLine(string.Format("關聯處理序的虛擬記憶體量: {0}", process.VirtualMemorySize64));
        }
    }
}
