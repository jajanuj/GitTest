using System.Runtime.InteropServices;

namespace AOISystem.Utilities.Common
{
    /// <summary>
    ///系統時間類
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class SystemTime
    {
        public ushort year;
        public ushort month;
        public ushort dayofweek;
        public ushort day;
        public ushort hour;
        public ushort minute;
        public ushort second;
        public ushort milliseconds;
    }
}
