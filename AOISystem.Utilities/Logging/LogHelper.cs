using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace AOISystem.Utilities.Logging
{
    public sealed class LogHelper
    {
        private static bool _isInitialized = false;
        private static object _key = new object();
        private static bool _isWritting = false;
        private static SimpleLogger _alarmLogger;
        private static SimpleLogger _warningLogger;
        private static SimpleLogger _flowLogger;
        private static SimpleLogger _operationLogger;
        private static SimpleLogger _algorithmLogger;
        private static SimpleLogger _exceptionLogger;
        private static SimpleLogger _handshakeLogger;
        private static SimpleLogger _debugLogger;
        private static SimpleLogger _notifyLogger;
        private static SimpleLogger _monitorLogger;
        private static SimpleLogger _parameterLogger;           //v1.0.6.20 新增參數變動紀錄    
        private static SimpleLogger _pnLogger;           //v1.0.6.20 新增參數變動紀錄    

        private static ConcurrentQueue<LogMessageInfo> _messageInfoQueue;
        private static Thread _flowScanner;
        private static uint _loops = 0;

        public static bool AlarmLogShowEnable { get; set; }
        public static bool WarningLogShowEnable { get; set; }
        public static bool FlowLogShowEnable { get; set; }
        public static bool OperationLogShowEnable { get; set; }
        public static bool AlgorithmLogShowEnable { get; set; }
        public static bool ExceptionLogShowEnable { get; set; }
        public static bool HandshakeLogShowEnable { get; set; }
        public static bool DebugLogShowEnable { get; set; }
        public static bool NotifyLogShowEnable { get; set; }
        public static bool MonitorLogShowEnable { get; set; }
        public static bool ParameterLogShowEnable { get; set; }
        public static bool PNLogShowEnable { get; set; }

        public static bool LogMessageInfoQueueIsEmpty
        {
            get
            {
                if (!_isInitialized || _messageInfoQueue == null)
                {
                    return true;
                }
                return _messageInfoQueue.IsEmpty && !_isWritting;   
            }
        }

        public static void Initialize(string filePath, int reserverDay)
        {
            _alarmLogger = new SimpleLogger(filePath, LogFilter.Alarm.ToString(), reserverDay);
            _alarmLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_alarmLogger_LogMessageRecorded);
            _warningLogger = new SimpleLogger(filePath, LogFilter.Warning.ToString(), reserverDay);
            _warningLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_warningLogger_LogMessageRecorded);
            _flowLogger = new SimpleLogger(filePath, LogFilter.Flow.ToString(), reserverDay);
            _flowLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_flowLogger_LogMessageRecorded);
            _operationLogger = new SimpleLogger(filePath, LogFilter.Operation.ToString(), reserverDay);
            _operationLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_operationLogger_LogMessageRecorded);
            _algorithmLogger = new SimpleLogger(filePath, LogFilter.Algorithm.ToString(), reserverDay);
            _algorithmLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_algorithmLogger_LogMessageRecorded);
            _exceptionLogger = new SimpleLogger(filePath, LogFilter.Exception.ToString(), reserverDay);
            _exceptionLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_exceptionLogger_LogMessageRecorded);
            _handshakeLogger = new SimpleLogger(filePath, LogFilter.Handshake.ToString(), reserverDay);
            _handshakeLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_handshakeLogger_LogMessageRecorded);
            _debugLogger = new SimpleLogger(filePath, LogFilter.Debug.ToString(), reserverDay);
            _debugLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_debugLogger_LogMessageRecorded);
            _notifyLogger = new SimpleLogger(filePath, LogFilter.Notify.ToString(), reserverDay);
            _notifyLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_notifyLogger_LogMessageRecorded);
            _monitorLogger = new SimpleLogger(filePath, LogFilter.Monitor.ToString(), reserverDay);
            _monitorLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_monitorLogger_LogMessageRecorded);
            _parameterLogger = new SimpleLogger(filePath, LogFilter.Parameter.ToString(), reserverDay);
            _parameterLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_parameterLogger_LogMessageRecorded);
            _pnLogger = new SimpleLogger(filePath, LogFilter.PN.ToString(), reserverDay);
            _pnLogger.LogMessageRecorded += new SimpleLogger.LogMessageRecordedEventHandler(_pnLogger_LogMessageRecorded);


            AlarmLogShowEnable = true;
            WarningLogShowEnable = true;
            FlowLogShowEnable = true;
            OperationLogShowEnable = true;
            AlgorithmLogShowEnable = true;
            ExceptionLogShowEnable = true;
            HandshakeLogShowEnable = true;
            DebugLogShowEnable = true;
            NotifyLogShowEnable = true;
            MonitorLogShowEnable = true;
            ParameterLogShowEnable = true;
            PNLogShowEnable = true;
            _isInitialized = true;

            _messageInfoQueue = new ConcurrentQueue<LogMessageInfo>();
            _flowScanner = new Thread(() => { ScanTotalFlow(); });
            _flowScanner.IsBackground = true;
            _flowScanner.Start();
        }

        public static void Dispose()
        {
            SpinWait.SpinUntil(() => LogHelper.LogMessageInfoQueueIsEmpty, 1000);
            if (_flowScanner != null)
            {
                _flowScanner.Abort();
            }
            if (_alarmLogger != null)
            {
                _alarmLogger.Dispose();   
            }
            if (_warningLogger != null)
            {
                _warningLogger.Dispose();
            }
            if (_flowLogger != null)
            {
                _flowLogger.Dispose();
            }
            if (_operationLogger != null)
            {
                _operationLogger.Dispose();
            }
            if (_algorithmLogger != null)
            {
                _algorithmLogger.Dispose();
            }
            if (_exceptionLogger != null)
            {
                _exceptionLogger.Dispose();
            }
            if (_handshakeLogger != null)
            {
                _handshakeLogger.Dispose();
            }
            if (_debugLogger != null)
            {
                _debugLogger.Dispose();
            }
            if (_notifyLogger != null)
            {
                _notifyLogger.Dispose();
            }
            if (_monitorLogger != null)
            {
                _monitorLogger.Dispose();
            }
            if (_parameterLogger != null)
            {
                _parameterLogger.Dispose();
            }
            if (_pnLogger != null)
            {
                _pnLogger.Dispose();
            }
        }

        public static void BuildLog(LogFilter category, string content)
        {
            if (!_isInitialized)
            {
                return;
                //throw new NullReferenceException("Flow Data Logger doesn't initialized.");
            }
            SimpleLogger logger = null;
            switch (category)
            {
                case LogFilter.Alarm:
                    logger = _alarmLogger;
                    break;
                case LogFilter.Warning:
                    logger = _warningLogger;
                    break;
                case LogFilter.Flow:
                    logger = _flowLogger;
                    break;
                case LogFilter.Operation:
                    logger = _operationLogger;
                    break;
                case LogFilter.Algorithm:
                    logger = _algorithmLogger;
                    break;
                case LogFilter.Exception:
                    logger = _exceptionLogger;
                    break;
                case LogFilter.Handshake:
                    logger = _handshakeLogger;
                    break;
                case LogFilter.Debug:
                    logger = _debugLogger;
                    break;
                case LogFilter.Notify:
                    logger = _notifyLogger;
                    break;
                case LogFilter.Monitor:
                    logger = _monitorLogger;
                    break;
                case LogFilter.Parameter:
                    logger = _parameterLogger;
                    break;
                case LogFilter.PN:
                    logger = _pnLogger;
                    break;
            }
            //logger.LogMessage = content;
            _messageInfoQueue.Enqueue(new LogMessageInfo(category, content));
        }

        public static void Alarm(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_alarmLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Alarm, message));
        }

        public static void Warning(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_warningLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Warning, message));
        }

        public static void Flow(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_flowLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Flow, message));
        }

        public static void Operation(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_operationLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Operation, message));
        }

        public static void Algorithm(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_algorithmLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Algorithm, message));
        }

        public static void Exception(Exception e)
        {
            if (!_isInitialized)
            {
                return;
            }
            if (e.InnerException != null)
            {
                if (e.InnerException is AggregateException)
                {
                    AggregateException aggEx = (AggregateException)e.InnerException;
                    foreach (Exception ex in aggEx.InnerExceptions)
                    {
                        Exception(ex);
                    }
                }
                else
                {
                    Exception(e.InnerException);
                }
            }
            else
            {
                string msg = string.Format("ExceptionHelper\n{0}\n{1}\n{2}\n{3}\n{4}\n{5}", "例外處理類型：", e.GetType(), "錯誤訊息：", e.Message, "錯誤之處：", e.StackTrace);
                Exception(msg);
            }
        }

        public static void Exception(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_exceptionLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Exception, message));
        }

        public static void Handshake(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_handshakeLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Handshake, message));
        }

        public static void Debug(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_debugLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Debug, message));
        }

        public static void Notify(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_notifyLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Notify, message));
        }

        public static void Monitor(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_monitorLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Monitor, message));
        }

        public static void Parameter(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_monitorLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.Parameter, message));
        }

        public static void PN(string format, params object[] args)
        {
            if (!_isInitialized)
            {
                return;
            }
            string message = args.Length == 0 ? format : string.Format(format, args);
            //_monitorLogger.LogMessage = message;
            _messageInfoQueue.Enqueue(new LogMessageInfo(LogFilter.PN, message));
        }

        static void _alarmLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (AlarmLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _warningLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (WarningLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _flowLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (FlowLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _operationLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (OperationLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _algorithmLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (AlgorithmLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _exceptionLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (ExceptionLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _handshakeLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (HandshakeLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _debugLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (DebugLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _notifyLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (NotifyLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _monitorLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (MonitorLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _parameterLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (ParameterLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void _pnLogger_LogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (PNLogShowEnable)
            {
                string message = string.Format("{0} | {1} | {2}", date.ToString("yyyy.MM.dd HH:mm:ss.fff"), logCategory, logText);
                OnShowLog(message);
            }
        }

        static void OnShowLog(string message)
        {
            lock (_key)
            {
                if (Log.ShowLog != null)
                {
                    Log.ShowLog(message);
                }
            }
        }

        static void ScanTotalFlow()
        {
            while (true)
            {
                if (!_isInitialized)
                {
                    return;
                }
                if (!_messageInfoQueue.IsEmpty)
                {
                    _isWritting = true;
                    LogMessageInfo message;
                    if (_messageInfoQueue.TryDequeue(out message))
                    {
                        SimpleLogger logger = null;
                        switch (message.LogFilter)
                        {
                            case LogFilter.Alarm:
                                logger = _alarmLogger;
                                break;
                            case LogFilter.Warning:
                                logger = _warningLogger;
                                break;
                            case LogFilter.Flow:
                                logger = _flowLogger;
                                break;
                            case LogFilter.Operation:
                                logger = _operationLogger;
                                break;
                            case LogFilter.Algorithm:
                                logger = _algorithmLogger;
                                break;
                            case LogFilter.Exception:
                                logger = _exceptionLogger;
                                break;
                            case LogFilter.Handshake:
                                logger = _handshakeLogger;
                                break;
                            case LogFilter.Debug:
                                logger = _debugLogger;
                                break;
                            case LogFilter.Notify:
                                logger = _notifyLogger;
                                break;
                            case LogFilter.Monitor:
                                logger = _monitorLogger;
                                break;
                            case LogFilter.Parameter:
                                logger = _parameterLogger;
                                break;
                            case LogFilter.PN:
                                logger = _pnLogger;
                                break;
                        }
                        logger.Message(message.DateTime, message.Message);
                    }
                    _isWritting = false;
                }
                else
                {
                    Thread.Sleep(15);

                    //long spinTimes = 0;
                    //_loops = (_loops + 1) % 100;
                    //if (Environment.ProcessorCount == 1 || _loops == 0)
                    //{
                    //    Thread.Sleep(1);
                    //}
                    //else
                    //{
                    //    Wait(1, out spinTimes);
                    //}
                }
            }
        }

        static SpinWait spin = new SpinWait();
        static void Wait(long timeout, out long spinTimes)
        {
            spinTimes = 0;
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeout)
            {
                spin.SpinOnce();
                spinTimes++;
            }
            sw.Stop();
        }
    }
}
