using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public partial class LedRound : Control
    {
        #region Public and Private Members

        private Color _color;
        private bool _textVisible;
        private Color _textColor;
        private Font _textFont;
        private bool _on;

        /// <summary>
        /// Gets or Sets the color of the LED light
        /// </summary>
        [DefaultValue(typeof(Color), "153, 255, 54")]
        public Color Color
        {
            get { return _color; }
            set
            {
                if (value != _color)
                {
                    _color = value;
                    this.DarkColor = ControlPaint.Dark(_color);
                    this.DarkDarkColor = ControlPaint.DarkDark(_color);
                    this.Invalidate();	// Redraw the control
                }
            }
        }

        /// <summary>
        /// Dark shade of the LED color used for gradient
        /// </summary>
        public Color DarkColor { get; protected set; }

        /// <summary>
        /// Very dark shade of the LED color used for gradient
        /// </summary>
        public Color DarkDarkColor { get; protected set; }

        /// <summary>
        /// 文字是否顯示
        /// </summary>
        [DefaultValue(typeof(bool), "false")]
        public bool TextVisible
        {
            get { return _textVisible; }
            set
            {
                if (value != _textVisible)
                {
                    _textVisible = value;
                    this.Invalidate();	// Redraw the control
                }
            }
        }

        /// <summary>
        /// 顯示文字顏色
        /// </summary>
        [DefaultValue(typeof(Color), "255, 255, 255")]
        public Color TextColor
        {
            get { return _textColor; }
            set
            {
                if (value != _textColor)
                {
                    _textColor = value;
                    this.Invalidate();	// Redraw the control
                }
            }
        }

        /// <summary>
        /// 顯示文字
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (value != base.Text)
                {
                    base.Text = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// 顯示文字字型
        /// </summary>
        public Font TextFont
        {
            get { return _textFont; }
            set
            {
                if (value != _textFont)
                {
                    _textFont = value;
                    this.Invalidate();	// Redraw the control
                }
            }
        }

        /// <summary>
        /// Gets or Sets whether the light is turned on
        /// </summary>
        public bool On
        {
            get { return _on; }
            set
            {
                if (value != _on)
                {
                    _on = value;
                    this.Invalidate();
                }
            }
        }

        [DefaultValue(typeof(TextImageRelation), "Overlay")]
        public TextImageRelation TextImageRelation { get; set; }

        #endregion

        #region Constructor

        public LedRound()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Color = Color.FromArgb(255, 153, 255, 54);
            this.TextVisible = false;
            this.TextColor = Color.FromArgb(255, 255, 255);
            this.Padding = new Padding { All = 1 };
            this.Size = new Size(23, 23);
            this.TextFont = new Font("Lucida Sans Unicode", 14, FontStyle.Bold);
            this.On = false;
        }

        #endregion

        #region Transpanency Methods

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        protected override void OnMove(EventArgs e)
        {
            RecreateHandle();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }

        #endregion

        #region Drawing Methods

        /// <summary>
        /// Handles the Paint event for this UserControl
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.Control);
            // Create an offscreen graphics object for double buffering
            using (Bitmap offScreenBmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height))
            {
                using (System.Drawing.Graphics g = Graphics.FromImage(offScreenBmp))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    // Draw the control
                    drawControl(g);
                    // Draw the image to the screen
                    e.Graphics.DrawImageUnscaled(offScreenBmp, 0, 0);
                }
            }
        }

        /// <summary>
        /// Renders the control to an image
        /// </summary>
        /// <param name="g"></param>
        private void drawControl(Graphics g)
        {
            Color lightColor = (this.On) ? this.Color : this.DarkColor;
            Color darkColor = (this.On) ? this.DarkColor : this.DarkDarkColor;
            Color textColor = (this.On) ? this.TextColor : Color.FromArgb(255 - this.TextColor.R, 255 - this.TextColor.G, 255 - this.TextColor.B);

            Rectangle paddedRectangle = new Rectangle(this.Padding.Left, this.Padding.Top, this.Width - (this.Padding.Left + this.Padding.Right), this.Height - (this.Padding.Top + this.Padding.Bottom));
            int width = (paddedRectangle.Width < paddedRectangle.Height) ? paddedRectangle.Width : paddedRectangle.Height;
            Rectangle drawRectangle = new Rectangle(paddedRectangle.X, paddedRectangle.Y, width, width);

            // Draw the background ellipse
            if (drawRectangle.Width < 1) drawRectangle.Width = 1;
            if (drawRectangle.Height < 1) drawRectangle.Height = 1;

            using (SolidBrush colorBrush = new SolidBrush(darkColor))
            {
                g.FillEllipse(colorBrush, drawRectangle);
            }

            // Draw the glow gradient
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(drawRectangle);
                using (PathGradientBrush pathBrush = new PathGradientBrush(path))
                {
                    pathBrush.CenterColor = lightColor;
                    pathBrush.SurroundColors = new Color[] { Color.FromArgb(100, lightColor) };
                    g.FillEllipse(pathBrush, drawRectangle);
                }

                // Set the clip boundary  to the edge of the ellipse
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddEllipse(drawRectangle);
                    g.SetClip(gp);
                }

                // Draw the white reflection gradient
                using (GraphicsPath path1 = new GraphicsPath())
                {
                    Rectangle whiteRect = new Rectangle(drawRectangle.X - Convert.ToInt32(drawRectangle.Width * .15F), drawRectangle.Y - Convert.ToInt32(drawRectangle.Width * .15F), Convert.ToInt32(drawRectangle.Width * .8F), Convert.ToInt32(drawRectangle.Height * .8F));
                    path1.AddEllipse(whiteRect);
                    using (PathGradientBrush pathBrush1 = new PathGradientBrush(path))
                    {
                        pathBrush1.CenterColor = Color.FromArgb(180, 255, 255, 255);
                        pathBrush1.SurroundColors = new Color[] { Color.FromArgb(0, 255, 255, 255) };
                        g.FillEllipse(pathBrush1, whiteRect);
                    }
                }
            }

            // Draw the border
            float w = drawRectangle.Width;
            g.SetClip(this.ClientRectangle);
            if (this.On)
            {
                using (Pen colorPen = new Pen(Color.FromArgb(85, Color.Black), 1F))
                {
                    g.DrawEllipse(colorPen, drawRectangle);
                }
            }

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            //Draw the Text
            if (this.Text != null && this.TextVisible == true)
            {
                if (this.TextImageRelation == TextImageRelation.ImageBeforeText)
                {
                    SizeF sizeF = g.MeasureString(this.Text, this.TextFont);
                    textColor = Color.FromArgb(255 - this.TextColor.R, 255 - this.TextColor.G, 255 - this.TextColor.B);
                    drawRectangle = new Rectangle(drawRectangle.X + drawRectangle.Width, drawRectangle.Y, (int)(drawRectangle.X + drawRectangle.Width + sizeF.Width), drawRectangle.Y + drawRectangle.Height);
                }
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                using (SolidBrush colorBrush = new SolidBrush(textColor))
                {
                    //g.DrawRectangle(new Pen(SystemColors.Control), drawRectangle);
                    g.DrawString(this.Text, this.TextFont, colorBrush, drawRectangle, drawFormat);
                }
            }
        }

        #endregion
    }
}
