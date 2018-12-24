using System.Runtime.InteropServices;

namespace AOISystem.Utilities
{
    public class ComputerInfo
    {
        private static CPU_INFO CpuInfo;
        private static MEMORY_INFO MemoryInfo;

        /// <summary> 
        /// 静态构造函数 
        /// </summary> 
        public static void GetInfo()
        {
            CpuInfo = new CPU_INFO();
            GetSystemInfo(ref CpuInfo);
            MemoryInfo = new MEMORY_INFO();
            GlobalMemoryStatus(ref MemoryInfo);
        }

        #region 服务器相关硬件信息
        #region 定义API引用
        /// <summary> 
        /// CPU信息 
        /// </summary> 
        /// <param name="cpuinfo">CPU_INFO</param> 
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void GetSystemInfo(ref CPU_INFO cpuinfo);

        /// <summary> 
        /// 内存信息 
        /// </summary> 
        /// <param name="meminfo"></param> 
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
        #endregion
        #region CPU相关
        /// <summary> 
        /// 获取CPU数量 
        /// </summary> 
        public static string Count
        {
            get { return CpuInfo.NumberOfProcessors.ToString(); }
        }
        /// <summary> 
        /// 获取CPU类型 
        /// </summary> 
        public static string Type
        {
            get { return CpuInfo.ProcessorType.ToString(); }
        }
        /// <summary> 
        /// 获取CPU等级 
        /// </summary> 
        public static string Level
        {
            get { return CpuInfo.ProcessorLevel.ToString(); }
        }
        /// <summary> 
        /// 获取CPUOemID 
        /// </summary> 
        public static string OemID
        {
            get { return CpuInfo.OemID.ToString(); }
        }
        /// <summary> 
        /// CPU页面大小 
        /// </summary> 
        public static string PageSize
        {
            get { return CpuInfo.PageSize.ToString(); }
        }
        #endregion
        #region 内存相关
        /// <summary> 
        /// 物理内存总大小 
        /// </summary> 
        public static string TotalPhys
        {
            get { return MemoryInfo.TotalPhys.ToString(); }
        }
        /// <summary> 
        /// 可用物理内存 
        /// </summary> 
        public static string AvailPhys
        {
            get { return MemoryInfo.AvailPhys.ToString(); }
        }
        /// <summary> 
        /// 交换页面总大小 
        /// </summary> 
        public static string TotalPageFile
        {
            get { return MemoryInfo.TotalPageFile.ToString(); }
        }
        /// <summary> 
        /// 可交换页面大小 
        /// </summary> 
        public static string AvailPageFile
        {
            get { return MemoryInfo.AvailPageFile.ToString(); }
        }
        /// <summary> 
        /// 虚拟内存总大小 
        /// </summary> 
        public static string TotalVirtual
        {
            get { return MemoryInfo.TotalVirtual.ToString(); }
        }
        /// <summary> 
        /// 可用虚拟内存 
        /// </summary> 
        public static string AvailVirtual
        {
            get { return MemoryInfo.AvailVirtual.ToString(); }
        }
        /// <summary> 
        /// 已经内存 
        /// </summary> 
        public static string Load
        {
            get { return MemoryInfo.MemoryLoad.ToString(); }
        }
        #endregion
        #endregion 
    }

    /// <summary> 
    /// 定义CPU的信息结构 
    /// </summary> 
    public struct CPU_INFO
    {
        public uint OemID;
        public uint PageSize;
        public uint MinimumApplicationAddress;
        public uint MaximumApplicationAddress;
        public uint ActiveProcessorMask;
        public uint NumberOfProcessors;
        public uint ProcessorType;
        public uint AllocationGranularity;
        public uint ProcessorLevel;
        public uint ProcessorRevision;
    }
    /// <summary> 
    /// 定义内存的信息结构   
    /// </summary> 
     [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_INFO
    {
        public uint Length;
        public uint MemoryLoad;
        public uint TotalPhys;
        public uint AvailPhys;
        public uint TotalPageFile;
        public uint AvailPageFile;
        public uint TotalVirtual;
        public uint AvailVirtual;
    }
}
