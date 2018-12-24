using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AOISystem.Utilities.Resources;

namespace AOISystem.Utilities.Recipe
{
    public enum SafeDoorStation
    {
        Loader = 0,
        AoiWindow,
        AoiDoor,
        Bin,
        Unloader
    }

    public enum SafeDoorSide
    {
        Right = 1,
        Left,
        Center,
        None
    }

    

    public partial class SafeDoorCtl : UserControl
    {
        private SafeDoorStation _safeDoorStation;
        private int _errorCodeNum;
        private bool _enable;

        public SafeDoorCtl()
        {
            InitializeComponent();

            this.SafeDoorNumber = Convert.ToInt16(lblNumValue.Text);
            this.Station = SafeDoorStation.Loader;
            this.ErrorCodeNum = 1;
            this.Side = SafeDoorSide.Right;
            lblTitleText.BackColor = Color.DimGray;
        }

        private void SafeDoorCtl_Load(object sender, EventArgs e)
        {
            //LanguageManager.Apply(this);
        }

        [Category("Setting")]
        [Description("安全門站別")]
        public SafeDoorStation Station
        {
            get { return _safeDoorStation; }
            set
            {
                _safeDoorStation = value;
                lblStationValue.Text = ResourceHelper.Language.GetString("SafeDoorStation" + _safeDoorStation);
                //lblStationValue.Text =  _safeDoorStation.ToString();
            }
        }

        [Category("Setting")]
        [Description("安全門編號")]
        public int SafeDoorNumber
        {
            get { return Convert.ToInt16(lblNumValue.Text); }
            set { lblNumValue.Text = value.ToString(); }
        }

        [Browsable(false)]
        [Category("Setting")]
        [Description("安全門啟用")]
        public bool Enable 
        {
            get { return _enable; }
            set
            {
                _enable = value;
                chkEnable.Checked = _enable;
                lblTitleText.BackColor = Enable ? Color.Green : Color.DimGray;       
            }
        }

        [Category("Setting")]
        [Description("安全門錯誤代碼")]
        public int ErrorCodeNum
        {
            get { return _errorCodeNum; }
            set { _errorCodeNum = Convert.ToInt16(string.Format("{0:D4}", value)); }
        }

        [Category("Setting")]
        [Description("左/右側")]
        public SafeDoorSide Side { get; set; }

        private void chkEnable_CheckedChanged(object sender, System.EventArgs e)
        {
            Enable = chkEnable.Checked;
            lblTitleText.BackColor = Enable ? Color.Green : Color.DimGray;
        }
    }
}
