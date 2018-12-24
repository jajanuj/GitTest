using System;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace AOISystem.Utilities.Logging
{

    /// <summary>
    /// 歷史紀錄類別
    /// </summary>
    public class SimpleLogger
    {
        public delegate void LogMessageRecordedEventHandler(DateTime date, string logCategory, string logText);
        public event LogMessageRecordedEventHandler LogMessageRecorded;

        private string _logFilePath = Application.StartupPath;
        private string _logFileName = "Logging";
        private int _logReserverDay = 30;
        private DateTime _todayDateTime = DateTime.Today;
        private object _key = new object();
        private FileStream _loggerFS = null;
        private StreamWriter _loggerSW = null;
        private int _fileCount = 0;

        private const int MAX_FILE_SIZE = 50000000;

        /// <summary>
        /// Log 類別建立
        /// </summary>
        /// <param name="filename">Log儲存檔案名稱</param>
        public SimpleLogger(string filePath, string fileName, int reserverDay)
        {
            _logFilePath = filePath;
            _logFileName = fileName;
            _logReserverDay = reserverDay;
            _todayDateTime = DateTime.Today;
            Initialize();
        }

        /// <summary>
        /// Log 類別釋放
        /// </summary>
        public void Dispose()
        {
            if (_loggerSW != null)
            {
                _loggerSW.Close();
            }
            _loggerSW = null;
        }

        /// <summary>
        /// 訊息輸出
        /// </summary>
        public void Message(DateTime dateTime, string message)
        {
            try
            {
                lock (_key)
                {
                    if ((DateTime.Today > _todayDateTime) || (_loggerFS.Length > MAX_FILE_SIZE))
                    {
                        _todayDateTime = DateTime.Today;
                        Initialize();
                    }

                    OnLogMessageRecorded(dateTime, _logFileName, message);

                    _loggerSW.WriteLine(string.Format("{0} | {1}", dateTime.ToString("yyyy.MM.dd HH:mm:ss.fff"), message));
                    _loggerSW.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        /// <summary>
        /// Log輸出
        /// </summary>
        public string LogMessage
        {
            set
            {
                Message(DateTime.Now, value);
            }
        }

        private void Initialize()
        {
            lock (_key)
            {
                string filePath = _logFilePath + @"\LOG\";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                JudgeDirectorySaveDate(filePath);

                if (_loggerSW != null)
                {
                    _loggerSW.Close();
                }
 
                _fileCount = 1;
                //文字檔輸出
                FileInfo logFilePath = GenerateFilePath();

                _loggerFS = new FileStream(logFilePath.FullName, FileMode.Append);
                _loggerSW = new StreamWriter(_loggerFS);
            }
        }

        /// <summary>
        /// 產生路徑名稱
        /// </summary>
        /// <returns></returns>
        private FileInfo GenerateFilePath()
        {
            FileInfo path = new FileInfo(string.Format(@"{0}\LOG\{1}_{2}_{3}.log", _logFilePath, _logFileName, DateTime.Now.ToString("yyyyMMdd"), _fileCount++));
            if (path.Exists && (path.Length > MAX_FILE_SIZE))
            {
                path = GenerateFilePath();
            }
            return path;
        }

        /// <summary>判斷Log檔案是否超過保存日期</summary>
        /// <param name="path">指定要檢查資料夾檔案的完整路徑</param>
        private void JudgeDirectorySaveDate(string path)
        {
            try
            {
                DirectoryInfo pathInfo = new DirectoryInfo(path);
                FileInfo[] logFilePaths = pathInfo.GetFiles("*.log");

                // 判斷存放Log檔案是否超過保存日期
                for (int i = 0; i < logFilePaths.Length; i++)
                {
                    string compareString = string.Format("LOG\\{0}_", _logFileName);
                    if (logFilePaths[i].FullName.Contains(compareString))
                    {
                        int index = logFilePaths[i].FullName.LastIndexOf(compareString) + compareString.Length;
                        string FileStr = logFilePaths[i].FullName.Substring(index, logFilePaths[i].FullName.Length - index);
                        // 這邊可以再修改======================================================================================
                        int yy = 2010, mm = 1, dd = 1;
                        yy = int.Parse(FileStr.Substring(0, 4));
                        mm = int.Parse(FileStr.Substring(4, 2));
                        dd = int.Parse(FileStr.Substring(6, 2));
                        DateTime tempDateTime = DateTime.Parse(mm.ToString() + "/" + dd.ToString() + "/" + yy.ToString() + " 00:00:00");
                        // ====================================================================================================

                        // 判斷Log檔案是否超過保存日期
                        if (tempDateTime.AddDays(_logReserverDay) < DateTime.Now)
                        {
                            string filePath = string.Format(@"{0}\{1}_{2}", path, _logFileName, FileStr);

                            if (logFilePaths[i].Exists)
                            {
                                logFilePaths[i].Delete();
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

        void OnLogMessageRecorded(DateTime date, string logCategory, string logText)
        {
            if (LogMessageRecorded != null)
            {
                LogMessageRecorded(date, logCategory, logText);
            }
        }
    }
}
