using System;

namespace AOISystem.Utilities.Logging
{
    public class LogMessageInfo
    {
        public LogMessageInfo()
            : this(DateTime.Now, LogFilter.Debug, string.Empty)
        {
        }

        public LogMessageInfo(string message)
            : this(DateTime.Now, LogFilter.Debug, message)
        {
        }

        public LogMessageInfo(LogFilter logFilter, string message)
            : this(DateTime.Now, logFilter, message)
        {
        }

        public LogMessageInfo(DateTime dateTime, LogFilter logFilter, string message)
        {
            this.DateTime = dateTime;
            this.LogFilter = logFilter;
            this.Message = message;
        }

        public DateTime DateTime { get; set; }

        public LogFilter LogFilter { get; set; }

        public string Message { get; set; }
    }
}
