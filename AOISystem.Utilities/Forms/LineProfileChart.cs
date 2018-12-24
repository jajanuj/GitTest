using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartTools
{
    public partial class LineProfileChart : UserControl
    {
        #region - Private Parameters -
        private int _intervalIndex;
        private bool _isInitialize;
        private int _dataLenght;
        private int _dataSize;
        private int _axisXScrollBarSize;
        private int _axisYScrollBarSize;
        private int _axisXIntervalZoom;
        private int _axisYIntervalZoom;
        private int _axisXScaleViewMinSize;
        private int _axisYScaleViewMinSize;
        private int _axisXScaleViewSizeZoom;
        private int _axisYScaleViewSizeZoom;
        private string _axisXTitle;
        private string _axisYTitle;
        # endregion

        #region - Public Parameters -
        //決定每幾點取樣一次
        public int IntervalIndex
        {
            get { return _intervalIndex; }
            set
            {
                if (_intervalIndex != value)
                {
                    _intervalIndex = value;
                    IsInitialize = false;
                }
            }
        }

        //波形圖數據是否初始化
        public bool IsInitialize
        {
            get { return _isInitialize; }
            set
            {
                if (_isInitialize != value)
                {
                    _isInitialize = value;
                }
            }
        }

        //每筆資料長度
        public int DataLenght
        {
            get { return _dataLenght; }
            set
            {
                if (_dataLenght != value)
                {
                    _dataLenght = value;
                    IsInitialize = false;
                }
            }
        }

        //資料大小
        public int DataSize
        {
            get { return _dataSize; }
            set
            {
                if (_dataSize != value)
                {
                    _dataSize = value;
                    IsInitialize = false;
                }
            }
        }

        //X軸Scroll Bar大小
        public int AxisXScrollBarSize
        {
            get { return _axisXScrollBarSize; }
            set
            {
                if (_axisXScrollBarSize != value)
                {
                    _axisXScrollBarSize = value;
                    IsInitialize = false;
                }
            }
        }

        //Y軸Scroll Bar大小
        public int AxisYScrollBarSize
        {
            get { return _axisYScrollBarSize; }
            set
            {
                if (_axisYScrollBarSize != value)
                {
                    _axisYScrollBarSize = value;
                    IsInitialize = false;
                }
            }
        }

        //X軸Scroll Bar格線間隔密度倍率
        public int AxisXIntervalZoom
        {
            get { return _axisXIntervalZoom; }
            set
            {
                if (_axisXIntervalZoom != value)
                {
                    _axisXIntervalZoom = value;
                    IsInitialize = false;
                }
            }
        }

        //Y軸Scroll Bar格線間隔密度倍率
        public int AxisYIntervalZoom
        {
            get { return _axisYIntervalZoom; }
            set
            {
                if (_axisYIntervalZoom != value)
                {
                    _axisYIntervalZoom = value;
                    IsInitialize = false;
                }
            }
        }

        //X軸Scroll Bar放大後格線顯示最小值
        public int AxisXScaleViewMinSize
        {
            get { return _axisXScaleViewMinSize; }
            set
            {
                if (_axisXScaleViewMinSize != value)
                {
                    _axisXScaleViewMinSize = value;
                    IsInitialize = false;
                }
            }
        }

        //Y軸Scroll Bar放大後格線顯示最小值
        public int AxisYScaleViewMinSize
        {
            get { return _axisYScaleViewMinSize; }
            set
            {
                if (_axisYScaleViewMinSize != value)
                {
                    _axisYScaleViewMinSize = value;
                    IsInitialize = false;
                }
            }
        }

        //X軸Scroll Bar放大後格線間隔密度倍率
        public int AxisXScaleViewSizeZoom
        {
            get { return _axisXScaleViewSizeZoom; }
            set
            {
                if (_axisXScaleViewSizeZoom != value)
                {
                    _axisXScaleViewSizeZoom = value;
                    IsInitialize = false;
                }
            }
        }

        //Y軸Scroll Bar放大後格線間隔密度倍率
        public int AxisYScaleViewSizeZoom
        {
            get { return _axisYScaleViewSizeZoom; }
            set
            {
                if (_axisYScaleViewSizeZoom != value)
                {
                    _axisYScaleViewSizeZoom = value;
                    IsInitialize = false;
                }
            }
        }

        //自訂X軸Scroll Bar標題
        public string AxisXTitle
        {
            get { return _axisXTitle; }
            set
            {
                if (_axisXTitle != value)
                {
                    _axisXTitle = value;
                    IsInitialize = false;
                }
            }
        }

        //自訂Y軸Scroll Bar標題
        public string AxisYTitle
        {
            get { return _axisYTitle; }
            set
            {
                if (_axisYTitle != value)
                {
                    _axisYTitle = value;
                    IsInitialize = false;
                }
            }
        }
        # endregion

        # region - Public Methods -
        /// <summary>
        /// 初始化參數設定
        /// </summary>
        public LineProfileChart()
        {
            InitializeComponent();
            IsInitialize = false;
            IntervalIndex = 1;
            DataSize = 255;
            AxisXScrollBarSize = 17;
            AxisYScrollBarSize = 17;
            AxisXIntervalZoom = 10;
            AxisYIntervalZoom = 10;
            AxisXScaleViewMinSize = 10;
            AxisYScaleViewMinSize = 10;
            AxisXScaleViewSizeZoom = 10;
            AxisYScaleViewSizeZoom = 10;
            AxisXTitle = "Pixel Index";
            AxisYTitle = "Pixel Gray";
        }

        /// <summary>
        /// 波形圖數據初始化
        /// </summary>
        /// <param name="dataLenght">每筆資料長度</param>
        private void Initialize(int dataLenght)
        {
            IsInitialize = true;
            this.chart1.ChartAreas["Default"].AxisX.Minimum = 0;
            this.chart1.ChartAreas["Default"].AxisX.Maximum = dataLenght;
            this.chart1.ChartAreas["Default"].AxisY.Minimum = 0;
            this.chart1.ChartAreas["Default"].AxisY.Maximum = DataSize;
            //可放大Chart時，自訂Scroll Bar寬度
            this.chart1.ChartAreas["Default"].AxisX.ScrollBar.Size = AxisXScrollBarSize;
            this.chart1.ChartAreas["Default"].AxisY.ScrollBar.Size = AxisYScrollBarSize;
            //自訂Scroll Bar標題
            this.chart1.ChartAreas["Default"].AxisX.Title = AxisXTitle;
            this.chart1.ChartAreas["Default"].AxisY.Title = AxisYTitle;
            //自訂Scroll Bar格線間隔密度
            this.chart1.ChartAreas["Default"].AxisX.Interval = dataLenght / AxisXIntervalZoom;
            this.chart1.ChartAreas["Default"].AxisY.Interval = DataSize / AxisYIntervalZoom;
            //自訂AxisViewChanged事件
            this.chart1.AxisViewChanged += new EventHandler<ViewEventArgs>(chart1_AxisViewChanged);
        }

        /// <summary>
        /// 產生波形圖(從陣列數據)
        /// </summary>
        /// <param name="data">波形圖數據</param>
        public void AddChartPoints(double[] data)
        {
            if (!IsInitialize || data.Length != DataLenght)
            {
                DataLenght = data.Length;
                Initialize(DataLenght);
            }
            ProcessChartPoints(data);
        }

        /// <summary>
        /// 產產生波形圖(從數據指標與大小)
        /// </summary>
        /// <param name="dataAddress">波形圖數據來源指標</param>
        /// <param name="dataLenght">波形圖數據長度</param>
        public void AddChartPoints(IntPtr dataAddress, int dataLenght)
        {
            if (!IsInitialize || dataLenght != DataLenght)
            {
                DataLenght = dataLenght;
                Initialize(DataLenght);
            }
            double[] data = new double[dataLenght];
            for (int i = 0; i < dataLenght; i++)
            {
                data[i] = (double)Marshal.ReadByte(dataAddress, i);
            }
            ProcessChartPoints(data);
        }
        # endregion

        # region - Private Methods -
        /// <summary>
        /// 產生波形圖
        /// </summary>
        /// <param name="data">波形圖數據</param>
        public void ProcessChartPoints(double[] data)
        {
            double Sum = 0;
            double MaxValue = 0;
            double MinValue = DataSize;

            Series series1 = new Series();
            series1.ChartArea = "Default";
            series1.ChartType = SeriesChartType.FastLine;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Default";
            // Add series.
            for (int i = 0; i < (int)(DataLenght / IntervalIndex); i++)
            {
                series1.Points.AddXY((double)(i * IntervalIndex), (double)data[i * IntervalIndex]);
                //判斷最大最小平均值
                if (MaxValue < data[i * IntervalIndex])
                {
                    MaxValue = data[i * IntervalIndex];
                }
                if (MinValue > data[i * IntervalIndex])
                {
                    MinValue = data[i * IntervalIndex];
                }
                Sum += data[i * IntervalIndex];
            }
            //清除前一組數據並新增
            this.chart1.Series.Clear();
            this.chart1.Series.Add(series1);

            lab_MaximumValue.Text = string.Format("Maximum Value : {0}", MaxValue);
            lab_MinimumValue.Text = string.Format("Minimum Value : {0}", MinValue);
            lab_AverageValue.Text = string.Format("Average Value : {0}", (int)(Sum / (DataLenght / IntervalIndex)));
        }

        //當Scroll Bar發生縮放時
        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (this.chart1.ChartAreas["Default"].AxisX.ScaleView.IsZoomed)
            {
                this.chart1.ChartAreas["Default"].AxisX.Interval = this.chart1.ChartAreas["Default"].AxisX.ScaleView.Size / AxisXScaleViewSizeZoom;
            }
            else
            {
                this.chart1.ChartAreas["Default"].AxisX.Interval = DataLenght / AxisXIntervalZoom;
            }
            if (this.chart1.ChartAreas["Default"].AxisY.ScaleView.IsZoomed)
            {
                this.chart1.ChartAreas["Default"].AxisY.Interval = this.chart1.ChartAreas["Default"].AxisY.ScaleView.Size / AxisYScaleViewSizeZoom;
            }
            else
            {
                this.chart1.ChartAreas["Default"].AxisY.Interval = DataSize / AxisYIntervalZoom;
            }
        }

        //當Scroll Bar縮放功能啟動或取消時
        private void chb_XYAxisZoomEnable_CheckedChanged(object sender, EventArgs e)
        {
            // Enable range selection and zooming end user interface
            if (chb_XAxisZoomEnable.Checked == true)
            {
                this.chart1.ChartAreas["Default"].CursorX.IsUserEnabled = true;
                this.chart1.ChartAreas["Default"].CursorX.IsUserSelectionEnabled = true;
                this.chart1.ChartAreas["Default"].AxisX.ScaleView.MinSize = AxisXScaleViewMinSize;
            }
            else
            {
                this.chart1.ChartAreas["Default"].CursorX.IsUserEnabled = false;
                this.chart1.ChartAreas["Default"].CursorX.IsUserSelectionEnabled = false;
                //取消放大功能行為, 恢復原始View狀態
                this.chart1.ChartAreas["Default"].AxisX.ScaleView.ZoomReset(0);
                this.chart1.ChartAreas["Default"].AxisX.Interval = DataLenght / AxisXIntervalZoom;
            }
            if (chb_YAxisZoomEnable.Checked == true)
            {
                this.chart1.ChartAreas["Default"].CursorY.IsUserEnabled = true;
                this.chart1.ChartAreas["Default"].CursorY.IsUserSelectionEnabled = true;
                this.chart1.ChartAreas["Default"].AxisY.ScaleView.MinSize = AxisYScaleViewMinSize;
            }
            else
            {
                this.chart1.ChartAreas["Default"].CursorY.IsUserEnabled = false;
                this.chart1.ChartAreas["Default"].CursorY.IsUserSelectionEnabled = false;
                //取消放大功能行為, 恢復原始View狀態
                this.chart1.ChartAreas["Default"].AxisY.ScaleView.ZoomReset(0);
                this.chart1.ChartAreas["Default"].AxisY.Interval = DataSize / AxisYIntervalZoom;
            }
        }
        # endregion
    }
}
