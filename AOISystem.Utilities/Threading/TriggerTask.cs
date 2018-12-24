using System;
using System.Diagnostics;
using System.Threading;
using AOISystem.Utilities.Modules;

namespace AOISystem.Utilities.Threading
{
    public class TriggerTask
    {
        private Action _callback;
        private long _delayMilliseconds;
        private int _iterations = 10;
        private Stopwatch _sw = null;
        //private Thread _thread = null;
        //private MMTimer mmTimer;
        private Multimedia.Timer mmTimer;
        private bool _scanTask = false;

        public TriggerTask(Action callback, long delayMilliseconds)
        {
            _callback = callback;
            _delayMilliseconds = delayMilliseconds;
            _sw = new Stopwatch();
            _scanTask = true;
            //_mmTimer = new MMTimer();
            //_mmTimer.Timer += (senser, ea) =>
            //{
            //    if (_scanTask)
            //    {
            //        if (this.IsRunning && _callback != null)
            //        {
            //            if (_sw.ElapsedMilliseconds > _delayMilliseconds)
            //            {
            //                _callback.Invoke();
            //                Stop();
            //            }
            //        }
            //    }
            //};
            //_mmTimer.Start(1, true);
            mmTimer = new Multimedia.Timer(ModulesFactory.Components);
            mmTimer.Mode = Multimedia.TimerMode.Periodic;
            mmTimer.Period = 1;
            mmTimer.Resolution = 1;
            mmTimer.Tick += (senser, ea) =>
            {
                if (_scanTask)
                {
                    if (this.IsRunning && _callback != null)
                    {
                        if (_sw.ElapsedMilliseconds > _delayMilliseconds)
                        {
                            _callback.Invoke();
                            Stop();
                        }
                    }
                }
            };
            mmTimer.Start();
            //_thread = new Thread(() =>
            //{
            //    uint loops = 0;
            //    while (_scanTask)
            //    {
            //        if (this.IsRunning && _callback != null)
            //        {
            //            if (_sw.ElapsedMilliseconds > _delayMilliseconds)
            //            {
            //                _callback.Invoke();
            //                Stop();
            //            }   
            //        }
            //        loops = (loops + 1) % 100;
            //        if (Environment.ProcessorCount == 1 || loops == 0)
            //        {
            //            Thread.Sleep(1);
            //        }
            //        else
            //        {
            //            //Thread.SpinWait(_iterations);
            //            Wait(1);
            //        }
            //    }
            //});
            //_thread.IsBackground = true;
            //_thread.Start();
        }

        public long DelayMilliseconds
        {
            get { return _delayMilliseconds; }
            set { _delayMilliseconds = value; }
        }

        public int Iterations
        {
            get { return _iterations; }
            set { _iterations = value; }
        }

        public bool IsRunning { get; internal set; }

        public void Dispose()
        {
            _scanTask = false;
        }

        public void Update(Action callback, long delayMilliseconds)
        {
            _callback = callback;
            _delayMilliseconds = delayMilliseconds;
        }

        public void Start()
        {
            if (this.IsRunning)
            {
                return;
            }

            this.IsRunning = true;
            _sw.Restart();
        }

        public void Stop()
        {
            if (!this.IsRunning)
            {
                return;
            }
            this.IsRunning = false;
        }

        public static TriggerTask StartNew(Action callback, long delayMilliseconds)
        {
            TriggerTask triggerTask = new TriggerTask(callback, delayMilliseconds);
            triggerTask.Start();
            return triggerTask;
        }

        private void Wait(long timeout)
        {
            SpinWait spin = new SpinWait();
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeout)
            {
                spin.SpinOnce();
            }
            sw.Stop();
        }
    }
}
