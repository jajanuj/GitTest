using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using AOISystem.Utilities.IO;

namespace AOISystem.Utilities.Logging
{
    public class TackTimeLogger
    {
        private static List<TackTimeItem> _tackTimeItemCollection = new List<TackTimeItem>();
        private static Stopwatch _stopwatch = new Stopwatch();
        private static object _objKey = new object();

        public static bool IsRecordTackTimeLogger = false;

        public static void AddItem(string name)
        {
            if (!IsRecordTackTimeLogger)
            {
                return;
            }
            if (_tackTimeItemCollection.Find( x => { return x.Name == name; } ) == null)
            {
                _tackTimeItemCollection.Add(new TackTimeItem(name));
            }
            else
            {
                MessageBox.Show(string.Format("已存在Key : {0}", name));
            }
            _stopwatch.Restart();
        }

        public static void RecordTime(string name, string format, params object[] args)
        {
            RecordTime(name, string.Format(format, args));
        }

        public static void RecordTime(string name, string taskName)
        {
            if (!IsRecordTackTimeLogger)
            {
                return;
            }
            if (_tackTimeItemCollection.Find(x => { return x.Name == name; }) == null)
            {
                MessageBox.Show(string.Format("查無此Key : {0}", name));
                return;
            }
            lock (_objKey)
            {
                TimeSpan timeSpan = _stopwatch.Elapsed;
                foreach (TackTimeItem item in _tackTimeItemCollection)
                {
                    if (item.Name == name)
                    {
                        item.RecordTime(taskName, timeSpan);
                    }
                    else
                    {
                        item.RecordTime(string.Empty, timeSpan);
                    }
                }
            }
        }

        public static void WriteCSV(string path)
        {
            if (!IsRecordTackTimeLogger)
            {
                return;
            }
            object[] headObjs = new object[_tackTimeItemCollection.Count + 2];
            headObjs[0] = "DateTime";
            headObjs[1] = "Millisecond";
            for (int i = 0; i < _tackTimeItemCollection.Count; i++)
            {
                headObjs[i + 2] = string.Format("{0}({1})", _tackTimeItemCollection[i].Name, _tackTimeItemCollection[i].TackTimeInfoCollection.Count);
            }
            string dateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            SimpleCsvHelper.AddData(path, string.Format("TackTime_{0}.csv", dateTime), headObjs);
            for (int j = 0; j < _tackTimeItemCollection[0].TackTimeInfoCollection.Count; j++)
            {
                object[] objs = new object[_tackTimeItemCollection.Count + 2];
                objs[0] = _tackTimeItemCollection[0].TackTimeInfoCollection[j].TimeSpan.ToString();
                objs[1] = _tackTimeItemCollection[0].TackTimeInfoCollection[j].TimeSpan.TotalMilliseconds;
                for (int i = 0; i < _tackTimeItemCollection.Count; i++ )
                {
                    objs[i + 2] = _tackTimeItemCollection[i].TackTimeInfoCollection[j].TaskName;
                }
                SimpleCsvHelper.AddData(path, string.Format("TackTime_{0}.csv", dateTime), objs);
            }
        }
    }

    public class TackTimeItem
    {
        public TackTimeItem(string name)
        {
            this.Name = name;
            this.TackTimeInfoCollection = new List<TackTimeInfo>();
        }

        public string Name { get; set; }

        public List<TackTimeInfo> TackTimeInfoCollection { get; set; }

        public void RecordTime(string taskName, TimeSpan timeSpan)
        {
            this.TackTimeInfoCollection.Add(new TackTimeInfo() 
            { 
                TimeSpan = timeSpan,
                TaskName = taskName
            });
        }
    }

    public class TackTimeInfo
    {
        public TackTimeInfo()
        { 
        }
        public TimeSpan TimeSpan { get; set; }

        public string TaskName { get; set; }
    }
}
