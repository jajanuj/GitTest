using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AOISystem.Utilities.BarcodeScanner
{
    class AIDAReaderController
    {
        private Stopwatch _sw = null;
        private StringBuilder _keys = null;

        public AIDAReaderController()
        {
            KeyboardHook.Enabled = true;
            KeyboardHook.GlobalKeyDown += new EventHandler<KeyboardHook.KeyEventArgs>(KeyboardHook_GlobalKeyDown);

            _sw = Stopwatch.StartNew();
            _keys = new StringBuilder();
        }

        public event Action<string> ReaderEventChanged;

        void KeyboardHook_GlobalKeyDown(object sender, KeyboardHook.KeyEventArgs e)
        {
            Console.WriteLine(_sw.ElapsedMilliseconds);
            if (_sw.ElapsedMilliseconds > 30)
            {
                _keys.Clear();
            }
            _sw.Restart();

            string key = e.Keys.ToString();
            if (key.Length == 2 && key.Contains('D') )
            {
                _keys.Append(key[1]);
            }
            else if (key.Length == 1)
            {
                _keys.Append(key);
            }
            else if (key == "Return")
            {
                if (ReaderEventChanged != null)
                {
                    ReaderEventChanged(_keys.ToString());
                }
                _keys.Clear();
            }

            //if (key.Length == 2 && key.Contains('D'))
            //{
            //    _keys.Append(key[1]);
            //}
            //else if (key == "Return")
            //{
            //    if (ReaderEventChanged != null)
            //    {
            //        ReaderEventChanged(_keys.ToString());
            //    }
            //    _keys.Clear();
            //}
        }
    }
}
