using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    [ToolboxBitmap(typeof(SplitContainer))]
    public partial class SplitContainerEx : SplitContainer
    {
        enum MouseState
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal,
            /// <summary>
            /// 鼠標移入
            /// </summary>
            Hover
        }

        public SplitContainerEx()
        {
            InitializeComponent();

            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            this.SplitterWidth = 9;
            this.Panel1MinSize = 0;
            this.Panel2MinSize = 0;
            this.BackColor = System.Drawing.SystemColors.Control;
        }

        public SplitContainerEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            this.SplitterWidth = 9;
            this.Panel1MinSize = 0;
            this.Panel2MinSize = 0;
            this.BackColor = System.Drawing.SystemColors.Control;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int SplitterWidth
        {
            get
            {
                return base.SplitterWidth;
            }
            set
            {
                base.SplitterWidth = 9;
            }
        }

        private int mCollpaseOrExpandSize = 435;
        public int CollpaseOrExpandSize 
        {
            get { return mCollpaseOrExpandSize; }
            set
            {
                if (value != mCollpaseOrExpandSize)
                {
                    mCollpaseOrExpandSize = value;

                    if (!mCollpased)
                    {
                        if (CollpasePanel == SplitterPanelEnum.Panel1)
                        {
                            this.SplitterDistance = mCollpaseOrExpandSize;
                        }
                        else
                        {
                            if (this.Orientation == Orientation.Horizontal)
                            {
                                if (this.Height - 9 - mCollpaseOrExpandSize > 0)
                                {
                                    this.SplitterDistance = this.Height - 9 - mCollpaseOrExpandSize;   
                                }
                            }
                            else
                            {
                                if (this.Width - 9 - mCollpaseOrExpandSize > 0)
                                {
                                    this.SplitterDistance = this.Width - 9 - mCollpaseOrExpandSize;   
                                }
                            }
                        }
                        this.Invalidate(this.ControlRect);
                    }
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int Panel1MinSize
        {
            get
            {
                return base.Panel1MinSize;
            }
            set
            {
                base.Panel1MinSize = 0;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new int Panel2MinSize
        {
            get
            {
                return base.Panel2MinSize;
            }
            set
            {
                base.Panel2MinSize = 0;
            }
        }

        public enum SplitterPanelEnum
        {
            Panel1,
            Panel2
        }

        SplitterPanelEnum mCollpasePanel = SplitterPanelEnum.Panel2;
        /// <summary>
        /// 進行摺疊或展開的SplitterPanel
        /// </summary>
        [DefaultValue(SplitterPanelEnum.Panel2)]
        public SplitterPanelEnum CollpasePanel
        {
            get
            {
                return mCollpasePanel;
            }
            set
            {
                if (value != mCollpasePanel)
                {
                    mCollpasePanel = value;
                    this.Invalidate(this.ControlRect);
                }
            }
        }

        bool mCollpased = false;
        /// <summary>
        /// 是否為摺疊狀態
        /// </summary>
        public bool IsCollpased
        {
            get { return mCollpased; }
            set 
            {
                if (value != mCollpased)
                {
                    //mCollpased = value;
                    CollpaseOrExpand();
                }
            }
        }

        bool mIsCollpaseOrExpandFixed = false;
        /// <summary>
        /// 是否鎖定控制項摺疊或展開功能
        /// </summary>
        public bool IsCollpaseOrExpandFixed
        {
            get
            {
                return mIsCollpaseOrExpandFixed;
            }
            set
            {
                if (value != mIsCollpaseOrExpandFixed)
                {
                    mIsCollpaseOrExpandFixed = value;
                    this.Invalidate(this.ControlRect);
                }
            }
        }

        Rectangle mRect = new Rectangle();
        /// <summary>
        /// 控制器繪製區域
        /// </summary>
        private Rectangle ControlRect
        {
            get
            {
                if (this.Orientation == Orientation.Horizontal)
                {
                    mRect.X = this.Width <= 80 ? 0 : this.Width / 2 - 40;
                    mRect.Y = this.SplitterDistance;
                    mRect.Width = 80;
                    mRect.Height = 9;
                }
                else
                {
                    mRect.X = this.SplitterDistance;
                    mRect.Y = this.Height <= 80 ? 0 : this.Height / 2 - 40;
                    mRect.Width = 9;
                    mRect.Height = 80;
                }
                return mRect;
            }
        }

        /// <summary>
        /// 鼠標狀態
        /// </summary>
        MouseState mMouseState = MouseState.Normal;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!this.IsCollpaseOrExpandFixed)
            {
                //繪製參數
                bool collpase = false;
                if ((this.CollpasePanel == SplitterPanelEnum.Panel1 && mCollpased == false)
                    || this.CollpasePanel == SplitterPanelEnum.Panel2 && mCollpased)
                {
                    collpase = true;
                }
                Color color = mMouseState == MouseState.Normal ? SystemColors.ButtonShadow : SystemColors.ControlDarkDark;
                //需要繪製的圖片
                Bitmap bmp = CreateControlImage(collpase, color);
                //繪製區域
                if (this.Orientation == Orientation.Vertical)
                {
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipX);
                }
                //清除繪製區域
                e.Graphics.SetClip(this.SplitterRectangle);   //這裡需要注意一點就是需要清除拆分器整個區域，如果僅清除控制按鈕區域，則會出現虛線狀態
                e.Graphics.Clear(this.BackColor);

                //繪製
                e.Graphics.DrawImage(bmp, this.ControlRect);
            }
        }

        public new bool IsSplitterFixed
        {
            get
            {
                return base.IsSplitterFixed;
            }
            set
            {
                base.IsSplitterFixed = value;
                //此處設計防止運行時更改base.IsSplitterFixed屬性時導致mIsSplitterFixed變量判斷失效
                if (value && mIsSplitterFixed == false)
                {
                    mIsSplitterFixed = true;
                }
            }
        }

        bool mIsSplitterFixed = true;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //鼠標在控制按鈕區域
            if (this.SplitterRectangle.Contains(e.Location))
            {
                if (!this.IsCollpaseOrExpandFixed && this.ControlRect.Contains(e.Location))
                {
                    //如果拆分器可移動，則鼠標在控制按鈕範圍內時臨時關閉拆分器
                    if (this.IsSplitterFixed == false)
                    {
                        this.IsSplitterFixed = true;
                        mIsSplitterFixed = false;
                    }
                    this.Cursor = Cursors.Hand;
                    mMouseState = MouseState.Hover;
                    this.Invalidate(this.ControlRect);
                }
                else
                {
                    //如果拆分器為臨時關閉，則開啟拆分器
                    if (mIsSplitterFixed == false)
                    {
                        this.IsSplitterFixed = false;
                        if (this.Orientation == Orientation.Horizontal)
                        {
                            this.Cursor = Cursors.HSplit;
                        }
                        else
                        {
                            this.Cursor = Cursors.VSplit;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                    mMouseState = MouseState.Normal;
                    this.Invalidate(this.ControlRect);
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.Cursor = Cursors.Default;
            mMouseState = MouseState.Normal;
            this.Invalidate(this.ControlRect);
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!IsCollpaseOrExpandFixed && this.ControlRect.Contains(e.Location))
            {
                CollpaseOrExpand();
            }
            base.OnMouseClick(e);
        }

        /// <summary>
        /// 摺疊或展開
        /// </summary>
        public void CollpaseOrExpand()
        {
            if (mCollpased)
            {
                mCollpased = false;
                if (CollpasePanel == SplitterPanelEnum.Panel1)
                {
                    this.SplitterDistance = this.CollpaseOrExpandSize;
                }
                else
                {
                    if (this.Orientation == Orientation.Horizontal)
                    {
                        this.SplitterDistance = this.Height - 9 - this.CollpaseOrExpandSize;
                    }
                    else
                    {
                        this.SplitterDistance = this.Width - 9 - this.CollpaseOrExpandSize;
                    }
                }
            }
            else
            {
                mCollpased = true;

                if (CollpasePanel == SplitterPanelEnum.Panel1)
                {
                    this.SplitterDistance = 0;
                }
                else
                {
                    if (this.Orientation == Orientation.Horizontal)
                    {
                        this.SplitterDistance = this.Height - 9;
                    }
                    else
                    {
                        this.SplitterDistance = this.Width - 9;
                    }
                }
            }
            this.Invalidate(this.ControlRect); //局部刷新繪製
        }

        /// <summary>
        /// 摺疊
        /// </summary>
        public void Collpase()
        {
            if (!mCollpased)
            {
                mCollpased = true;
                if (CollpasePanel == SplitterPanelEnum.Panel1)
                {
                    this.SplitterDistance = 0;
                }
                else
                {
                    if (this.Orientation == Orientation.Horizontal)
                    {
                        this.SplitterDistance = this.Height - 9;
                    }
                    else
                    {
                        this.SplitterDistance = this.Width - 9;
                    }
                }
                this.Invalidate(this.ControlRect); //局部刷新繪製
            }
        }

        /// <summary>
        /// 展開
        /// </summary>
        public void Expand()
        {
            if (mCollpased)
            {
                mCollpased = false;
                if (CollpasePanel == SplitterPanelEnum.Panel1)
                {
                    this.SplitterDistance = this.CollpaseOrExpandSize;
                }
                else
                {
                    if (this.Orientation == Orientation.Horizontal)
                    {
                        this.SplitterDistance = this.Height - 9 - this.CollpaseOrExpandSize;
                    }
                    else
                    {
                        this.SplitterDistance = this.Width - 9 - this.CollpaseOrExpandSize;
                    }
                }
                this.Invalidate(this.ControlRect); //局部刷新繪製
            }
        }

        /// <summary>
        /// 需要繪製的用於摺疊窗口的按鈕樣式
        /// </summary>
        /// <param name="collapse"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private Bitmap CreateControlImage(bool collapse, Color color)
        {
            Bitmap bmp = new Bitmap(80, 9);
            for (int x = 5; x <= 30; x += 5)
            {
                for (int y = 1; y <= 7; y += 3)
                {
                    bmp.SetPixel(x, y, color);
                }
            }
            for (int x = 50; x <= 75; x += 5)
            {
                for (int y = 1; y <= 7; y += 3)
                {
                    bmp.SetPixel(x, y, color);
                }
            }
            //控制小三角底邊向上或者向下
            if (collapse)
            {
                int k = 0;
                for (int y = 7; y >= 1; y--)
                {
                    for (int x = 35 + k; x <= 45 - k; x++)
                    {
                        bmp.SetPixel(x, y, color);
                    }
                    k++;
                }
            }
            else
            {
                int k = 0;
                for (int y = 1; y <= 7; y++)
                {
                    for (int x = 35 + k; x <= 45 - k; x++)
                    {
                        bmp.SetPixel(x, y, color);
                    }
                    k++;
                }
            }
            return bmp;
        }
    }
}
