using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AOISystem.Utilities.Common;

namespace AOISystem.Utilities.Logging
{
    /// <summary>
    /// The class looging exceptions and traces 
    /// </summary>
    public static class Log
    {
        #region Log - Attribute
        private static System.IO.FileStream myTraceLog;

        private static System.Diagnostics.TextWriterTraceListener myListener;

        private static string m_LogFilePath;

        private static string m_FileName;

        private static string m_FilePath;

        private static string m_FileExp;

        private static string m_LogPath;

        private static int m_ReserveDay;

        public static bool IsWithUserName { get; set; }

        public static string UserName { get; set; }

        private static object objKey1 = new object();

        private static object objKey2 = new object();

        private static DateTime _todayDateTime = DateTime.Now;
        #endregion

        #region Log - Private

        /// <summary>
        /// 設定Log路徑
        /// </summary>
        /// <param name="path"></param>
        private static void SetLogFilePath(string path)
        {
            m_LogFilePath = path;

            if (myTraceLog != null)
            {
                myTraceLog.Close();
            }
            myTraceLog = new System.IO.FileStream(path, System.IO.FileMode.Append);
            // Creates the new trace listener.  
            if (myListener != null)
            {
                myListener.Close();
            }
            myListener = new System.Diagnostics.TextWriterTraceListener(myTraceLog);
            System.Diagnostics.Trace.Listeners.Add(myListener);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="msgTime"></param>
        /// <param key="msglvl"></param>
        /// <param key="threadid"></param>
        /// <param key="threadName"></param>
        /// <param key="msg"></param>
        private static void WriteLine(DateTime msgTime, CategoryFilterMode msglvl, int threadid, string threadName, string msg)
        {
            string message;

            //DateTime msgtime = DateTime.Now;

            //if (threadName == null)
            //{
            //    string messageId = string.Format("<{0}.{1}>{2}", m_ProcessId, threadid, Enum.Format(typeof(CategoryFilterMode), msglvl, "G"));
            //    message = string.Format("{0}-{1}-{2}-{3}", messageId, msglvl, msgtime.ToString(), msg);

            //}
            //else
            //{
            //    string messageId = string.Format("<{0}.{1}.{2}>{3}", m_ProcessId, threadid, threadName, Enum.Format(typeof(CategoryFilterMode), msglvl, "G"));
            //    message = string.Format("{0}-{1}-{2}", messageId, msgtime.ToString(), msg);
            //}
            string msgtime = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff");
            message = string.Format("{0} | {1} | {2} | {3} | {4}", msgtime, msglvl, GetMemoryLoad(), GetThreadPoolTaskNumber(), msg);
            //message = string.Format("{0} | {1} | {2} | {3}", msgtime, msglvl, GetThreadPoolTaskNumber(), msg);

            if (ShowLog != null)
            {
                lock (objKey1)
                {
                    ShowLog(message);
                }

                //myListener.WriteLine(message);

                //myListener.Flush();
            }
        }

        //寫入檔案
        private static void WriteLineToFile(DateTime msgTime, CategoryFilterMode msglvl, int threadid, string threadName, string msg)
        {
            try
            {
                //換天時執行
                if (DateTime.Today > _todayDateTime)
                {
                    SetLogFile(m_FilePath, m_FileName, m_FileExp, m_ReserveDay);
                }

                string message;

                //DateTime msgtime = DateTime.Now;
                //if (threadName == null)
                //{
                //    string messageId = string.Format("<{0}.{1}>{2}", m_ProcessId, threadid, Enum.Format(typeof(CategoryFilterMode), msglvl, "G"));
                //    message = string.Format("{0}-{1}-{2}-{3}", messageId, msglvl, msgtime.ToString(), msg);

                //}
                //else
                //{
                //    string messageId = string.Format("<{0}.{1}.{2}>{3}", m_ProcessId, threadid, threadName, Enum.Format(typeof(CategoryFilterMode), msglvl, "G"));
                //    message = string.Format("{0}-{1}-{2}", messageId, msgtime.ToString(), msg);
                //}

                string msgtime = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff");
                message = string.Format("{0} | {1} | {2} | {3} | {4}", msgtime, msglvl, GetMemoryLoad(), GetThreadPoolTaskNumber(), msg);
                //message = string.Format("{0} | {1} | {2} | {3}", msgtime, msglvl, GetThreadPoolTaskNumber(), msg);

                lock (objKey2)
                {
                    if (myListener ==null)
                    {
                        SetLogFilePath(string.Format(@"{0}\{1}", Application.StartupPath, "DefaultLog.log"));
                    }
                    myListener.WriteLine(message);
                    myListener.Flush();
                }
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
        }

        /// <summary>判斷Log檔案是否超過保存日期</summary>
        /// <param name="path">指定要檢查資料夾檔案的完整路徑</param>
        private static void JudgeDirectorySaveDate(string path)
        {
            string FileStr;
            DateTime tempDateTime;
            try
            {
                string[] tempPaths = Directory.GetFiles(path, "*." + m_FileExp);

                // 判斷存放Log檔案是否超過保存日期
                for (int i = 0; i < tempPaths.Length; i++)
                {
                    string compareString = string.Format("LOG\\{0}_", m_FileName);
                    if (tempPaths[i].Contains(compareString))
                    {
                        int index = tempPaths[i].LastIndexOf(compareString) + compareString.Length;
                        FileStr = tempPaths[i].Substring(index, tempPaths[i].Length - index);
                        // 這邊可以再修改======================================================================================
                        int yy = 2010, mm = 1, dd = 1;
                        yy = int.Parse(FileStr.Substring(0, 4));
                        mm = int.Parse(FileStr.Substring(4, 2));
                        dd = int.Parse(FileStr.Substring(6, 2));
                        tempDateTime = DateTime.Parse(mm.ToString() + "/" + dd.ToString() + "/" + yy.ToString() + " 00:00:00");
                        // ====================================================================================================

                        // 判斷Log檔案是否超過保存日期
                        if (tempDateTime.AddDays(m_ReserveDay) < DateTime.Now)
                        {
                            string filePath = string.Format(@"{0}\{1}_{2}", path, m_FileName, FileStr);
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private static uint GetMemoryLoad()
        {
            //MEMORY_INFO memoryInfo = new MEMORY_INFO();
            //ComputerInfo.GlobalMemoryStatus(ref memoryInfo);
            //return memoryInfo.MemoryLoad;
            //return (uint)ComputerUtility.GetInstance().GetUsedPhysicalMemoryPercentage();
            return (uint)PerformanceInfo.GetPhysicalOccupiedMemoryInPercent();
        }

        private static int GetThreadPoolTaskNumber()
        {
            int workerMaxThreads, workerAvailableThreads;
            int portMaxThreads, portAvailableThreads;
            ThreadPool.GetMaxThreads(out workerMaxThreads, out portMaxThreads);
            ThreadPool.GetAvailableThreads(out workerAvailableThreads, out portAvailableThreads);
            return workerMaxThreads - workerAvailableThreads;
        }
        #endregion

        #region Log - delegate :
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam key="T"></typeparam>
        /// <param key="t"></param>
        public delegate void ShowLogDelegate<T>(T t);
        /// <summary>
        /// 
        /// </summary>
        public static ShowLogDelegate<string> ShowLog;
        #endregion

        #region Log - Public :
        /// <summary>Logger初始化</summary>
        /// <param name="AFileName">Log文件存檔的檔案名稱</param>
        /// <param name="AFileExp">Log文件存檔的副檔名</param>
        /// <param name="AReserveDay">Log文件的保存期限</param>
        public static void SetLogFile(string AFileName, string AFileExp, int AReserveDay)
        {
            SetLogFile(Application.StartupPath, AFileName, AFileExp, AReserveDay);
        }

        /// <summary>Logger初始化</summary>
        /// <param name="AFileName">Log文件存檔的路徑名稱</param>
        /// <param name="AFileName">Log文件存檔的檔案名稱</param>
        /// <param name="AFileExp">Log文件存檔的副檔名</param>
        /// <param name="AReserveDay">Log文件的保存期限</param>
        public static void SetLogFile(string AFilePath, string AFileName, string AFileExp, int AReserveDay)
        {
            _todayDateTime = DateTime.Today;
            // 設定Log文件存檔的檔名
            m_FileName = AFileName;
            // 設定Log文件的存檔路徑名稱
            m_FilePath = AFilePath;
            string filePath = string.Format(@"{0}\LOG", AFilePath);
            // 設定Log文件存檔的副檔名
            m_FileExp = AFileExp;
            // 建立Log文件存檔的資料夾
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // 設定Log文件的保存期限
            m_ReserveDay = AReserveDay;

            // 設定文字檔檔名
            m_LogPath = string.Format(@"{0}\{1}_{2}.{3}", filePath, m_FileName, DateTime.Now.ToString("yyyyMMdd"), AFileExp);
            lock (objKey2)
            {
                // 檢查過期文件
                JudgeDirectorySaveDate(filePath);
            }

            SetLogFilePath(m_LogPath);
        }

        /// <summary>Logger釋放</summary>
        public static void Dispose()
        {
            if (myTraceLog != null)
            {
                myTraceLog.Close();
            }
            myTraceLog = null;
            if (myListener != null)
            {
                myListener.Close();
            }
            myListener = null;
        }

        /// <summary>
        /// 系統訊息記錄
        /// </summary>
        /// <param key="message"></param>
        public static void Info(string format, params object[] args)
        {
            string message;
            if (IsWithUserName == false)
            {
                message = string.Format(format, args);
            }
            else
            {
                message = string.Format("{0} | {1}", UserName, string.Format(format, args));
            }

            Log.WriteLine(DateTime.Now, CategoryFilterMode.INF, Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, message);
            Log.WriteLineToFile(DateTime.Now, CategoryFilterMode.INF, Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, message);
        }

        /// <summary>
        /// 系統訊息記錄
        /// </summary>
        /// <param key="message"></param>
        public static void Info(object param)
        {
            string message;
            if (IsWithUserName == false)
            {
                message = Convert.ToString(param);
            }
            else
            {
                message = string.Format("{0} | {1}", UserName, Convert.ToString(param));
            }

            Log.WriteLine(DateTime.Now, CategoryFilterMode.INF, Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, message);
            Log.WriteLineToFile(DateTime.Now, CategoryFilterMode.INF, Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, message);
        }

        /// <summary>
        /// 歷程訊息記錄
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Trace(string format, params object[] args)
        {
            string message;
            if (IsWithUserName == false)
            {
                message = string.Format(format, args);
            }
            else
            {
                message = string.Format("{0} | {1}", UserName, string.Format(format, args));
            }
            Log.WriteLine(DateTime.Now, CategoryFilterMode.TRC, Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, message);
            Log.WriteLineToFile(DateTime.Now, CategoryFilterMode.TRC, Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, message);
        }

        /// <summary>
        /// 歷程訊息記錄
        /// </summary>
        /// <param key="message"></param>
        public static void Trace(object param)
        {
            string message;
            if (IsWithUserName == false)
            {
                message = Convert.ToString(param);
            }
            else
            {
                message = string.Format("{0} | {1}", UserName, Convert.ToString(param));
            }
            Log.WriteLine(DateTime.Now, CategoryFilterMode.TRC, Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, message);
            Log.WriteLineToFile(DateTime.Now, CategoryFilterMode.TRC, Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, message);
        }

        /// <summary>
        /// 歷程訊息記錄不顯示
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Record(string format, params object[] args)
        {
            string message;
            if (IsWithUserName == false)
            {
                message = string.Format(format, args);
            }
            else
            {
                message = string.Format("{0} | {1}", UserName, string.Format(format, args));
            }
            Log.WriteLineToFile(DateTime.Now, CategoryFilterMode.TRC, Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, message);
        }

        /// <summary>
        /// 歷程訊息記錄不顯示
        /// </summary>
        /// <param key="message"></param>
        public static void Record(object param)
        {
            string message;
            if (IsWithUserName == false)
            {
                message = Convert.ToString(param);
            }
            else
            {
                message = string.Format("{0} | {1}", UserName, Convert.ToString(param));
            }
            Log.WriteLineToFile(DateTime.Now, CategoryFilterMode.TRC, Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.Name, message);
        }

        /// <summary>
        /// 錯誤訊息記錄
        /// </summary>
        /// <param key="e"></param>
        public static void Exception(Exception e)
        {
            //Exception("例外處理類型：" + e.GetType().ToString());
            //Exception("錯誤訊息：" + e.Message);
            //Exception("錯誤之處：" + e.StackTrace);
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
                    string msg = string.Format("ExceptionHelper\n{0}\n{1}\n{2}\n{3}\n{4}\n{5}", "例外處理類型：", e.InnerException.GetType(), "錯誤訊息：", e.InnerException.Message, "錯誤之處：", e.InnerException.StackTrace);
                    Exception(msg);
                }
            }
            else
            {
                string msg = string.Format("ExceptionHelper\n{0}\n{1}\n{2}\n{3}\n{4}\n{5}", "例外處理類型：", e.GetType(), "錯誤訊息：", e.Message, "錯誤之處：", e.StackTrace);
                Exception(msg);
            }
            //sw.WriteLine(string.Format("{0} | {1}", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff"), errorMsg));
            //sw.WriteLine(string.Format("{0}\n{1}", "例外處理類型：", ex.GetType()));
            //sw.WriteLine(string.Format("{0}\n{1}", "錯誤訊息：", ex.Message));
            //sw.WriteLine(string.Format("{0}\n{1}", "錯誤之處：", ex.StackTrace));
            //string message;
            //if (IsWithUserName == false)
            //{
            //    message = e.Message;
            //}
            //else
            //{
            //    message = string.Format("{0} | {1}", UserName, e.Message);
            //}
            //Log.WriteLine(DateTime.Now, CategoryFilterMode.ERR, Thread.CurrentContext.GetHashCode(), Thread.CurrentThread.Name, message);
            //Log.WriteLineToFile(DateTime.Now, CategoryFilterMode.ERR, Thread.CurrentContext.GetHashCode(), Thread.CurrentThread.Name, message);
        }

        /// <summary>
        /// 錯誤訊息記錄
        /// </summary>
        /// <param key="e"></param>
        public static void Exception(object param)
        {
            string message;
            if (IsWithUserName == false)
            {
                message = Convert.ToString(param);
            }
            else
            {
                message = string.Format("{0} | {1}", UserName, Convert.ToString(param));
            }
            Log.WriteLine(DateTime.Now, CategoryFilterMode.ERR, Thread.CurrentContext.GetHashCode(), Thread.CurrentThread.Name, message);
            Log.WriteLineToFile(DateTime.Now, CategoryFilterMode.ERR, Thread.CurrentContext.GetHashCode(), Thread.CurrentThread.Name, message);
        }

        public static void Exception(string format, params object[] args)
        {
            string message;
            if (IsWithUserName == false)
            {
                message = string.Format(format, args);
            }
            else
            {
                message = string.Format("{0} | {1}", UserName, string.Format(format, args));
            }
            Log.WriteLine(DateTime.Now, CategoryFilterMode.ERR, Thread.CurrentContext.GetHashCode(), Thread.CurrentThread.Name, message);
            Log.WriteLineToFile(DateTime.Now, CategoryFilterMode.ERR, Thread.CurrentContext.GetHashCode(), Thread.CurrentThread.Name, message);
        }

        /// <summary>
        /// 結束此處理序,將記錄項目寫入到 Windows 應用程式事件記錄檔中
        /// </summary>
        /// <param key="messge"></param>
        public static void FailFast(string source, string messge)
        {
            //System.Environment.FailFast(messge);

            EventLogTraceListener myTraceListener = new EventLogTraceListener(source);

            // Add the event log trace listener to the collection.
            System.Diagnostics.Trace.Listeners.Add(myTraceListener);

            // Write output to the event log.
            System.Diagnostics.Trace.WriteLine(messge);
        }

        public static void ReadLogAlllLines()
        {
            string tempPath = m_LogPath + ".tmp";
            File.Copy(m_LogPath, tempPath, true);
            string[] lines = File.ReadAllLines(tempPath);
            File.Delete(tempPath);
            if (lines.Length > 500)
            {
                string[] newLines = new string[500];
                Array.Copy(lines, lines.Length - 500, newLines, 0, 500);
                lines = newLines;
            }
            foreach (string line in lines)
            {
                if (ShowLog != null)
                {
                    lock (objKey1)
                    {
                        ShowLog(line);
                    }
                }
            }
        }

        public static void RegisterPresenter(ILoggerTarget logTargetView)
        {
            Log.ShowLog += delegate(string obj)
            {
                logTargetView.SetCommandLine(obj);
            };
        }

        public static void DeregisterPresenter(ILoggerTarget logTargetView)
        {
            Log.ShowLog -= delegate(string obj)
            {
                logTargetView.SetCommandLine(obj);
            };
        }
        #endregion
    }
}
