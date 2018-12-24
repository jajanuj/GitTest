using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace AOISystem.Utilities.Common
{
    /// <summary>
    /// Windows API方法
    /// </summary>
    public class WinAPI
    {
        [DllImport("Kernel32.dll")]
        private static extern Boolean SetSystemTime([In, Out] SystemTime st);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);

        /// <summary>
        /// 設置系統時間
        /// </summary>
        /// <param name="newdatetime">新時間</param>
        /// <returns></returns>
        public static bool SetSysTime(DateTime newdatetime)
        {
            int UtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(new DateTime(2001, 09, 01)).Hours;
            newdatetime = newdatetime.AddHours(-UtcOffset);

            SystemTime st = new SystemTime();
            st.year = Convert.ToUInt16(newdatetime.Year);
            st.month = Convert.ToUInt16(newdatetime.Month);
            st.day = Convert.ToUInt16(newdatetime.Day);
            st.dayofweek = Convert.ToUInt16(newdatetime.DayOfWeek);
            st.hour = Convert.ToUInt16(newdatetime.Hour);
            st.minute = Convert.ToUInt16(newdatetime.Minute);
            st.second = Convert.ToUInt16(newdatetime.Second);
            st.milliseconds = Convert.ToUInt16(newdatetime.Millisecond);
            return SetSystemTime(st);
        }

        /// <summary>
        /// 得到目前滑鼠焦點位置
        /// </summary>
        /// <returns></returns>
        public static Point GetCursorPos()
        {
            Point cursorPo = new Point();
            GetCursorPos(ref cursorPo);
            return cursorPo;
        }
    } 
}
