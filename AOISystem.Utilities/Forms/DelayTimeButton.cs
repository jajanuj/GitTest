using System;
using System.Windows.Forms;
using AOISystem.Utilities.Account;

namespace AOISystem.Utilities.Forms
{
    public partial class DelayTimeButton : Button
    {
        private Timer _timer;
        private bool _isMousePress;
        private DateTime _startDateTime;
        protected bool _isTrigger;
        private string _btnText;
        private bool _isLogin;

        public DelayTimeButton()
        {
            InitializeComponent();

            DelayTime = 1000;
            TestPermission = false;
            AccountLevel = AccountLevel.Engineer;

            _isMousePress = false;
            _startDateTime = new DateTime();
            _isTrigger = false;

            _timer = new Timer();
            _timer.Interval = 150;
            _timer.Tick += new EventHandler(timer_Tick);
            _timer.Enabled = false;
        }

        public double DelayTime { get; set; }

        public bool TestPermission { get; set; }

        public AccountLevel AccountLevel { get; set; }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (!CheckPermission())
            {
                return;
            }
            base.OnMouseDown(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                _startDateTime = DateTime.Now;
                _isMousePress = true;
                _btnText = this.Text;
                //_timer.Enabled = true;
                _timer.Start();
                _isTrigger = false;   
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (mevent.Button == MouseButtons.Left)
            {
                CancelTimer();
            }
        }

        private void CancelTimer()
        {
            if (_isMousePress)
            {
                int num;
                if (int.TryParse(this.Text, out num))
                {
                    this.Text = _btnText;
                }
                _isMousePress = false;
                //_timer.Enabled = false;
                _timer.Stop();   
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (!CheckPermission())
            {
                return;
            }
            if (this.DelayTime == 0)
            {
                _isTrigger = false;
                base.OnClick(e);
                return;
            }
            if (!_isTrigger)
            {
                return;
            }
            _isTrigger = false;
            base.OnClick(e);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_isMousePress == true)
            {
                if (this.DelayTime > 0)
                {
                    double totalMilliseconds = (DateTime.Now - _startDateTime).TotalMilliseconds;
                    this.Text = (this.DelayTime - totalMilliseconds < 0 ? 0 : (int)(this.DelayTime - totalMilliseconds)).ToString();
                    if (totalMilliseconds > this.DelayTime)
                    {
                        _isMousePress = false;
                        this.Text = _btnText;
                        //_timer.Enabled = false;
                        _timer.Stop();
                        _isTrigger = true;
                        OnClick(EventArgs.Empty);
                    }
                }
            }
        }

        private bool CheckPermission()
        {
            bool success = false;
            if (this.TestPermission)
            {
                if (AccountInfoManager.TestPermission(this.AccountLevel))
                {
                    //第一次確認不進行Click事件
                    //if (_isLogin)
                    {
                        success = true;   
                    }
                    _isLogin = true;
                }
            }
            else
            {
                success = true;
            }
            return success;
        }
    }
}
