using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public class BlendProgressBar : ProgressBar
    {
        [Browsable(true), Description("是否顯示數值")]
        public bool ShowText { get; set; }

        private bool _doMarqueeOverlay = false;
        [Browsable(true), Description("是否顯示特效")]
        public bool ShowOverlay { get; set; }

        private Timer timer1 = new Timer();
        private float _pos = 0;
        private float _rWidth = 71;
        public float OverlayWidth
        {
            get { return _rWidth; }
            set { _rWidth = value; }
        }
        private int _c = 0;
        private float _posAdd = 1.5F;
        public float OverlayAddAmount
        {
            get { return _posAdd; }
            set { _posAdd = value; }
        }
        private float _posDelay = 50;
        public float OverlayReshowDelay
        {
            get { return _posDelay; }
            set { _posDelay = value; }
        }


        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
        public Color Color3 { get; set; }
        public Color Color4 { get; set; }

        [Description("顯示第二段漸層位置(數值為0F-1.0F)")]
        public float PositionColor2 { get; set; }
        [Description("顯示第三段漸層位置(數值為0F-1.0F)")]
        public float PositionColor3 { get; set; }

        public new int Value
        {
            get { return base.Value; }
            set
            {
                if (value != base.Value)
                {
                    base.Value = value;
                    //maybe dont invalidata always...
                    if (!_doMarqueeOverlay)
                        this.Invalidate();                   
                }
            }
        }

        public BlendProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            timer1.Tick += new EventHandler(timer1_Tick);

            Color1 = Color.Red;
            Color2 = Color.OrangeRed;
            Color3 = Color.Lime;
            Color4 = Color.Green;

            ForeColor = Color.Black;
            PositionColor2 = 0.12F;
            PositionColor3 = 0.39F;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;

            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            else
                e.Graphics.DrawRectangle(Pens.Gray, 0, 0, this.Width, this.Height);

            rec.Height = rec.Height - 4;

            using (System.Drawing.Drawing2D.LinearGradientBrush l =
                new System.Drawing.Drawing2D.LinearGradientBrush(e.ClipRectangle, Color.Red, Color.Green, 0F))
            {
                System.Drawing.Drawing2D.ColorBlend lb = new System.Drawing.Drawing2D.ColorBlend();     //建立顏色漸層
                lb.Colors = new Color[] { Color1, Color2, Color3, Color4 };                                                                  //設定漸層顏色
                lb.Positions = new float[] { 0, 0.25F, 0.45F, 1.0F };                                                                                   //設定漸層位置
                l.InterpolationColors = lb;

                e.Graphics.FillRectangle(l, 2, 2, rec.Width, rec.Height);                                                                           //將漸層顏色填滿Process Bar
            }

            using (System.Drawing.Drawing2D.LinearGradientBrush l2 =
                new System.Drawing.Drawing2D.LinearGradientBrush(e.ClipRectangle, Color.FromArgb(147, 255, 255, 255), Color.FromArgb(0, 255, 255, 255), System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                System.Drawing.Drawing2D.ColorBlend lb = new System.Drawing.Drawing2D.ColorBlend();
                lb.Colors = new Color[] { Color.FromArgb(40, 255, 255, 255), Color.FromArgb(147, 255, 255, 255), 
				Color.FromArgb(40, 255, 255, 255), Color.FromArgb(0, 255, 255, 255) };
                lb.Positions = new float[] { 0, PositionColor2, PositionColor3, 1.0F };
                l2.InterpolationColors = lb;

                l2.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                e.Graphics.FillRectangle(l2, 2, 2, rec.Width, rec.Height);
            }

            if (this.ShowText)
            {
                using (SolidBrush sb = new SolidBrush(this.ForeColor))
                {
                    SizeF sz = e.Graphics.MeasureString(this.Value.ToString("N0") + " %", this.Font);
                    e.Graphics.DrawString(this.Value.ToString("N0") + " %", this.Font, sb,
                     new PointF((this.Width - sz.Width) / 2F, (this.Height - sz.Height) / 2F));
                }
            }

            #region 掃描特效
            if (this.ShowOverlay)
            {
                if (Value > 0 && _doMarqueeOverlay == false)
                    StartMarquee();
                if (Value == Maximum - 1)
                    StopMarquee();

                if (_doMarqueeOverlay)
                {
                    float rWidth = _rWidth;

                    if (rec.Width < rWidth)
                        rWidth = rec.Width;

                    if (rWidth + _pos > rec.Width)
                        rWidth = rec.Width - _pos;

                    using (System.Drawing.Drawing2D.LinearGradientBrush l =
                        new System.Drawing.Drawing2D.LinearGradientBrush(new RectangleF(_pos + 2, 2, _rWidth, rec.Height),
                            Color.FromArgb(127, 255, 255, 255), Color.FromArgb(0, 255, 255, 255), System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                    {
                        System.Drawing.Drawing2D.Blend lb = new System.Drawing.Drawing2D.Blend();
                        lb.Factors = new float[] { 1, 0, 1 };
                        lb.Positions = new float[] { 0, 0.5F, 1.0F };
                        l.Blend = lb;

                        //l.TranslateTransform(_pos - rWidth, 0);
                        l.WrapMode = System.Drawing.Drawing2D.WrapMode.TileFlipXY;

                        e.Graphics.FillRectangle(l, _pos + 2, 2, rWidth, rec.Height);

                        _pos += _posAdd;
                        if (_pos >= rec.Width)
                        {
                            if (_c < _posDelay)
                            {
                                _pos -= _posAdd;
                                _c++;
                            }
                            else
                            {
                                _pos = -_rWidth - _posDelay;
                                _c = 0;
                            }
                        }
                    }
                }
            }
            #endregion 掃描特效
        }

        private void StopMarquee()
        {
            timer1.Stop();
            _doMarqueeOverlay = false;
        }

        private void StartMarquee()
        {
            _doMarqueeOverlay = true;
            timer1.Interval = 10;
            timer1.Start();
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Invalidate();
            timer1.Start();
        }


    }
}
