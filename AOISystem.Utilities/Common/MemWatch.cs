using System;

namespace AOISystem.Utilities
{
    public class MemWatch
    {
        //比較記憶體使用量變化的基準值
        private long _lastTotalMemory = 0;
        //記憶體使用量變化
        public long MemorySizeChange = 0;
        //是否強制GC再測量記憶體用量
        private bool _forceGC = false;
        //可指定測量前是否要先做GC
        //(可排除己不用但尚未回收的記憶體)
        public MemWatch(bool forceGC)
        {
            _forceGC = forceGC;
        }
        public MemWatch() : this(false) { }
        //保留測量開始之基準
        public void Start()
        {
            _lastTotalMemory =
                GC.GetTotalMemory(_forceGC);
        }
        //測量從Start()至今的記憶體變化
        public void Stop()
        {
            MemorySizeChange =
                 GC.GetTotalMemory(_forceGC) - _lastTotalMemory;
        }
        //記憶體使用量變化(以KB計)
        public string MemorySizeChangeInKB
        {
            get
            {
                return string.Format("{0:N0}KB",
                    MemorySizeChange / 1024);
            }
        }
        //記憶體使用量變化(以MB計)
        public string MemorySizeChangeInMB
        {
            get
            {
                //return string.Format("{0:N0}MB",
                //    MemorySizeChange*1.0 / 1024.0 / 1024.0);
                return string.Format("{0:N3}MB",
                        MemorySizeChange * 1.0 / 1024.0 / 1024.0);
            }
        }
    }
}
