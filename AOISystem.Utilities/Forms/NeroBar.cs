using AOISystem.Utilities.Flow;
using AOISystem.Utilities.Modules;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
	[Serializable()]
	[DefaultProperty("Value")]
	public partial class NeroBar : UserControl
	{

		#region "Credits"

		//The codeproject article for this control can be found at:
		//http://www.codeproject.com/KB/progress/NeroBar.aspx

		//This control is a vastly enhanced version based on the VistaProgressBar found at:
		//http://www.openwinforms.com/vista_style_progress_bar.html

		//Glow animation based on code found at:
		//http://www.codeproject.com/KB/cpp/VistaProgressBar.aspx
		//Thanks to Xasthom

		//Color correction code (original author unknown) provided by Four13 Designs.
		//Thanks a lot! And thanks to the original author as well!

		#endregion

		#region "Version History"

		//VERSION HISTORY:
		//VERSION 1.3 (December 10th, 2008)
		//Added:
		//    This version history
		//    Enhancement: 100% user defined colors
		//    Enhancement: When setting "Value" to a value higher than "MaxValue", the percentagetext will show "> 100%" (can be more than 100% as well depending on the PercentageBasedOn setting)
		//    Enhancement: Added alpha channel transparency to user selected GlowColor to "smoothen" the glow
		//    Enhancement: Progress percentage text can now be aligned usign the TextAlign property
		//    New Feature: Progress percentage calculation can be based on segments - not just the whole control width
		//    New Feature: ColorChangeMode - you can now choose if the whole bar should change color when a threshold is passed - or only the segments as before
		//    New Feature: RightToLeft mode
		//    New Properties: PercentageBasedOn, RightToLeft, ColorChangeMode, TextAlign
		//    Custom Visual Studio toobox icon
		//    Custom icon for Demo application
		//Removed:
		//    The six hardcoded colors
		//Bug fix:
		//   Control crashed when Value>0 and Value<0.5
		//Irrelevant inherited base properties hidden from designer property grid
		//Updated demo project

		//VERSION 1.2 (December 4th, 2008)
		//Added:
		//    Animated glow
		//    Progress percentage
		//    New Properties: GlowMode, GlowSpeed, GlowPause, GlowColor, PercentageShow
		//    Progress percetage text can be customized using the NeroBar's Font and ForeColor Properties
		//Updated demo project
		//Code restructured with Regions for better readbility

		//VERSION 1.1 (November 26th, 2008)
		//Added: 
		//    Two new colors: Cyan and Purple
		//    A NeroBarToolStripMenuItem control 
		//    New property: ColorThresholds 
		//Updated demo project

		//VERSION 1.0 (November 25th, 2008) 
		//Initial release 

		#endregion

		#region "Public Enums"

		public enum NeroBarSegments
		{
			One = 1,
			Two = 2,
			Three = 3
		}

		public enum NeroBarGlowModes
		{
			None = 0,
			ProgressOnly = 1,
			WholeBar = 2
		}

		public enum NeroBarPercentageCalculationModes
		{
			Segment_1 = 1,
			Segments_1_2 = 2,
			WholeControl = 3
		}

		public enum NeroBarColorChangeModes
		{
			Segments = 0,
			WholeBar = 1
		}

		#endregion

		#region "Private variables etc"

		private Color _borderColor = Color.FromArgb(178, 178, 178);
		private Color _backRemain1 = Color.FromArgb(202, 202, 202);
		private Color _backRemain2 = Color.FromArgb(234, 234, 234);
		private Color _backRemain3 = Color.FromArgb(219, 219, 219);
		private Color _backRemain4 = Color.FromArgb(243, 243, 243);
		private Color _segment1Color = Color.FromArgb(55, 217, 60);
		private Color _segment2Color = Color.FromArgb(252, 252, 0);
		private Color _segment3Color = Color.FromArgb(252, 0, 0);

		private Color _glowColor = Color.FromArgb(150, 255, 255, 255);
		private NeroBarSegments _segmentCount = NeroBarSegments.Three;
		private double _value = 0;
		private double _maxValue = 100;
		private double _minValue = 0;
		private double _segment2StartThreshold = 80;
		private double _segment3StartThreshold = 90;
		private bool _drawThresholds = true;
		private bool _colorThresholds = false;
		private NeroBarGlowModes _glowMode = NeroBarGlowModes.None;
		private int _glowSpeed = 20;
		private int _glowPause = 5000;
		private int _glowPosition = -60;
		private NeroBarPercentageCalculationModes _percentageMode = NeroBarPercentageCalculationModes.WholeControl;
		private bool _rightToLeft = false;

		private NeroBarColorChangeModes _changeMode = NeroBarColorChangeModes.Segments;

        private FlowBase tmrAnimateGlow = null;
        private FlowBase tmrGlowPause = null;
        private int tmrAnimateGlowInterval = 20;
        private int tmrGlowPauseInterval = 20;

		private Label lblPercent = new Label();

		private string sTooLarge = "";

        private static int _controlIndex = 0;

		#endregion

		#region "Properties"

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(0)]
		[Category("NeroBar")]
		[Description("The NeroBar's value.")]
		public double Value
		{
			get { return _value; }
			set
			{
				_value = value;
				sTooLarge = "";
				if (_value > _maxValue)
				{
					_value = _maxValue;
					sTooLarge = "> ";
				}
				if (_value < _minValue)
				{
					_value = _minValue;
				}
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(100)]
		[Category("NeroBar")]
		[Description("The NeroBar's maximum value.")]
		public double MaxValue
		{
			get { return _maxValue; }
			set
			{
				if (value <= _minValue)
				{
					throw new Exception("You can't set max value to a value than or equal to min value!");
				}
				else
				{
					_maxValue = value;
					if (_value > value)
					{
						_value = value;
					}
					if (_segment2StartThreshold > value)
					{
						_segment2StartThreshold = value;
					}
					if (_segment3StartThreshold > value)
					{
						_segment3StartThreshold = value;
					}
					this.Invalidate();
				}
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(0)]
		[Category("NeroBar")]
		[Description("The NeroBar's minimum value.")]
		public double MinValue
		{
			get { return _minValue; }
			set
			{
				if (value >= _maxValue)
				{
					throw new Exception("You can't set min value to a value higher than or equal to max value!");
				}
				else
				{
					_minValue = value;
					if (_value < value)
					{
						_value = value;
					}
					if (_segment2StartThreshold < value)
					{
						_segment2StartThreshold = value;
					}
					if (_segment3StartThreshold < value)
					{
						_segment3StartThreshold = value;
					}
					this.Invalidate();
				}
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(true)]
		[Category("NeroBar")]
		[Description("Determines if the thresholds should be shown or not.")]
		public bool DrawThresholds
		{
			get { return _drawThresholds; }
			set
			{
				_drawThresholds = value;
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("NeroBar")]
		[Description("Determines if the thresholds should be colored according to the selected segment color or not.")]
		public bool ColorThresholds
		{
			get { return _colorThresholds; }
			set
			{
				_colorThresholds = value;
				this.Invalidate();
			}
		}


		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(typeof(NeroBar.NeroBarSegments), "Three")]
		[Category("NeroBar")]
		[Description("The number of segments - between one and three.")]
		public NeroBarSegments SegmentCount
		{
			get { return _segmentCount; }
			set
			{
				_segmentCount = value;
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[Category("NeroBar")]
		[DefaultValue(typeof(Color), "55, 217, 60")]
		[Description("The color of the first segment.")]
		public Color Segment1Color
		{
			get { return _segment1Color; }
			set
			{
				_segment1Color = value;
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[Category("NeroBar")]
		[DefaultValue(typeof(Color), "252, 252, 0")]
		[Description("The color of the second segment.")]
		public Color Segment2Color
		{
			get { return _segment2Color; }
			set
			{
				_segment2Color = value;
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[Category("NeroBar")]
		[DefaultValue(typeof(Color), "252, 0, 0")]
		[Description("The color of the third segment.")]
		public Color Segment3Color
		{
			get { return _segment3Color; }
			set
			{
				_segment3Color = value;
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[Category("NeroBar")]
		[DefaultValue(typeof(NeroBarColorChangeModes), "Segments")]
		[Description("Determines if the WHOLE bar should change color when threshold is passed or only the next segment.")]
		public NeroBarColorChangeModes ColorChangeMode
		{
			get { return _changeMode; }
			set
			{
				_changeMode = value;
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(80)]
		[Category("NeroBar")]
		[Description("The lower threshold (starting position) of the second segment.")]
		public double Segment2StartThreshold
		{
			get { return _segment2StartThreshold; }
			set
			{
				if (_segmentCount != NeroBarSegments.One)
				{
					if (value > _maxValue | value < _minValue)
					{
						throw new Exception("Segment 2 Start Threshold must be between min and max value");
					}
					else
					{
						_segment2StartThreshold = value;
						this.Invalidate();
					}
				}
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(90)]
		[Category("NeroBar")]
		[Description("The lower threshold (starting position) of the third segment.")]
		public double Segment3StartThreshold
		{
			get { return _segment3StartThreshold; }
			set
			{
				if (_segmentCount == NeroBarSegments.Three)
				{
					if (value > _maxValue | value < _minValue)
					{
						throw new Exception("Segment 3 Start Threshold must be between min and max value");
					}
					else
					{
						_segment3StartThreshold = value;
						this.Invalidate();
					}
				}
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(typeof(NeroBarGlowModes), "None")]
		[Category("NeroBar")]
		[Description("Determines if the NeroBar should have an animated glow or not.")]
		public NeroBarGlowModes GlowMode
		{
			get { return _glowMode; }
			set
			{
				_glowMode = value;
				if (_glowMode != NeroBarGlowModes.None)
				{
					tmrAnimateGlow.Start();
					tmrGlowPause.Stop();
				}
				else
				{
					if (_rightToLeft)
					{
						_glowPosition = this.Width + 60;
					}
					else
					{
						_glowPosition = -60;
					}
                    tmrAnimateGlow.Stop();
                    tmrGlowPause.Stop();
				}
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(20)]
		[Category("NeroBar")]
		[Description("The time between the glow advances in milliseconds. Lower value -> Higher Speed etc.")]
		public int GlowSpeed
		{
			get { return _glowSpeed; }
			set
			{
				if (value <= 0)
				{
					throw new Exception("The GlowSpeed value cannot be zero or negative");
				}
				_glowSpeed = value;
				if (tmrAnimateGlow.IsRunning)
				{
					tmrAnimateGlow.Stop();
					tmrAnimateGlowInterval = _glowSpeed;
					tmrAnimateGlow.Start();
				}
				else
				{
					tmrAnimateGlowInterval = _glowSpeed;
				}
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(5000)]
		[Category("NeroBar")]
		[Description("The pause between the glow animations in milliseconds.")]
		public int GlowPause
		{
			get { return _glowPause; }
			set
			{
				if (value <= 0)
				{
					throw new Exception("The GlowPause value cannot be zero or negative");
				}
				_glowPause = value;
				if (tmrGlowPause.IsRunning)
				{
					tmrGlowPause.Stop();
					tmrGlowPauseInterval = _glowPause;
					tmrGlowPause.Start();
				}
				else
				{
					tmrGlowPauseInterval = _glowPause;
				}
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[Category("NeroBar")]
		[DefaultValue(typeof(Color), "150, 255, 255, 255")]
		[Description("The color of the animated glow.")]
		public Color GlowColor
		{
			get { return _glowColor; }
			set
			{
				_glowColor = Color.FromArgb(150, value.R, value.G, value.B);
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("NeroBar")]
		[Description("Determines if progress percentage should be shown or not.")]
		public bool PercentageShow
		{
			get { return lblPercent.Visible; }
			set
			{
				lblPercent.Visible = value;
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(typeof(NeroBarPercentageCalculationModes), "Segments_1_2_3")]
		[Category("NeroBar")]
		[Description("Determines if progress percentage should be calculated based on the segment first segment(s) or the whole control width.")]
		public NeroBarPercentageCalculationModes PercentageBasedOn
		{
			get { return _percentageMode; }
			set
			{
				_percentageMode = value;
				this.Invalidate();
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(typeof(Color), "ControlText")]
		[Category("Appearance")]
		[Description("The forecolor of the Percentage text.")]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set
			{
				base.ForeColor = value;
				lblPercent.ForeColor = value;
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[Category("Appearance")]
		[Description("The Percentage text font.")]
		public override Font Font
		{
			get { return base.Font; }
			set
			{
				base.Font = value;
				lblPercent.Font = value;
			}
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[Category("Appearance")]
		[Description("The alignment of the Percentage text.")]
		public System.Drawing.ContentAlignment TextAlign
		{
			get { return lblPercent.TextAlign; }
			set { lblPercent.TextAlign = value; }
		}

		[Localizable(false)]
		[Bindable(false)]
		[Browsable(true)]
		[DefaultValue(false)]
		[Category("Nerobar")]
		[Description("If true, the bar will be filled from right to left instead of left to right.")]
		public new bool RightToLeft
		{
			get { return _rightToLeft; }
			set
			{
				_rightToLeft = value;
				if (_glowPosition == -60)
					_glowPosition = this.Width + 60;
				this.Invalidate();
			}
		}

		#region "Hide irrelevant base properties"

		[Browsable(false)]
		public override bool AllowDrop
		{
			get { return base.AllowDrop; }
			set { base.AllowDrop = value; }
		}

		[Browsable(false)]
		public override bool AutoScroll
		{
			get { return base.AutoScroll; }
			set { base.AutoScroll = value; }
		}

		[Browsable(false)]
		public new Size AutoScrollMargin
		{
			get { return base.AutoScrollMargin; }
			set { base.AutoScrollMargin = value; }
		}

		[Browsable(false)]
		public new Size AutoScrollMinSize
		{
			get { return base.AutoScrollMinSize; }
			set { base.AutoScrollMinSize = value; }
		}

		[Browsable(false)]
		public override System.Drawing.Point AutoScrollOffset
		{
			get { return base.AutoScrollOffset; }
			set { base.AutoScrollOffset = value; }
		}

		[Browsable(false)]
		public override bool AutoSize
		{
			get { return base.AutoSize; }
			set { base.AutoSize = value; }
		}

		[Browsable(false)]
		public new System.Windows.Forms.AutoSizeMode AutoSizeMode
		{
			get { return base.AutoSizeMode; }
			set { base.AutoSizeMode = value; }
		}

		[Browsable(false)]
		public override System.Windows.Forms.AutoValidate AutoValidate
		{
			get { return base.AutoValidate; }
			set { base.AutoValidate = value; }
		}

		[Browsable(false)]
		public override System.Drawing.Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		[Browsable(false)]
		public override System.Drawing.Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}

		[Browsable(false)]
		public override System.Windows.Forms.ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}

		[Browsable(false)]
		public new System.Windows.Forms.BorderStyle BorderStyle
		{
			get { return base.BorderStyle; }
			set { base.BorderStyle = value; }
		}

		[Browsable(false)]
		public override System.Windows.Forms.ContextMenuStrip ContextMenuStrip
		{
			get { return base.ContextMenuStrip; }
			set { base.ContextMenuStrip = value; }
		}

		[Browsable(false)]
		public new bool CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
		}

		[Browsable(false)]
		public override System.Drawing.Size MaximumSize
		{
			get { return base.MaximumSize; }
			set { base.MaximumSize = value; }
		}

		[Browsable(false)]
		public override System.Drawing.Size MinimumSize
		{
			get { return base.MinimumSize; }
			set { base.MinimumSize = value; }
		}

		#endregion

		#endregion

		#region "Constructor"

		public NeroBar()
		{
			this.Name = "NeroBar";
			this.Size = new System.Drawing.Size(307, 15);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			this.BackColor = Color.Transparent;
            this.tmrAnimateGlow = new FlowBase("AnimateGlow" + _controlIndex, tmrAnimateGlow_Elapsed);
			this.tmrAnimateGlowInterval = _glowSpeed;
            this.tmrGlowPause = new FlowBase("GlowPause" + _controlIndex, tmrGlowPause_Elapsed);
			this.tmrGlowPauseInterval = _glowPause;
            _controlIndex++;
			this.SuspendLayout();
			var _with1 = lblPercent;
			_with1.Dock = System.Windows.Forms.DockStyle.Fill;
			_with1.ForeColor = System.Drawing.SystemColors.ControlText;
			_with1.Location = new System.Drawing.Point(0, 0);
			_with1.Name = "PercentLabel";
			_with1.Size = new System.Drawing.Size(307, 15);
			_with1.TabIndex = 0;
			_with1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			_with1.Visible = false;
			this.Controls.Add(lblPercent);
			this.ResumeLayout(false);

            if (!this.DesignMode)
            {
                FlowControl flowControl = ModulesFactory.FlowControlHelper.GetFlowControl("UI");
                //this.tmrAnimateGlow.Name += this.Name;
                //this.tmrGlowPause.Name += this.Name;
                flowControl.AddFlowBase(this.tmrAnimateGlow);
                flowControl.AddFlowBase(this.tmrGlowPause);
            }
		}

		#endregion

		#region "Overridden Methods"

		protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			Rectangle rectUpper = new Rectangle(0, 0, this.Width, this.Height / 2 + 2);
			Rectangle rectLower = new Rectangle(0, this.Height / 2, this.Width, this.Height - (this.Height / 2));

			GraphicsPath pathLower = GetRoundPath(rectLower, 2);
			GraphicsPath pathUpper = GetRoundPath(rectUpper, 2);

			using (Brush brushUpper = new LinearGradientBrush(rectUpper, _backRemain4, _backRemain3, LinearGradientMode.Vertical))
			{
				e.Graphics.FillPath(brushUpper, pathUpper);
			}

			using (Brush brushLower = new LinearGradientBrush(rectLower, _backRemain1, _backRemain2, LinearGradientMode.Vertical))
			{
				e.Graphics.FillPath(brushLower, pathLower);
			}

			if (_drawThresholds)
			{
				Pen linePen = null;
				int pos = 0;

				if (_segmentCount == NeroBarSegments.Two | SegmentCount == NeroBarSegments.Three)
				{
					if (_colorThresholds)
					{
						linePen = new Pen(_segment2Color, 1);
					}
					else
					{
						linePen = new Pen(_borderColor, 1);
					}

					if (_rightToLeft)
					{
						pos = this.Width - Convert.ToInt32(((Convert.ToDouble(this.Width) - 2) * (_segment2StartThreshold - _minValue)) / (_maxValue - _minValue));
					}
					else
					{
						pos = Convert.ToInt32(((Convert.ToDouble(this.Width) - 2) * (_segment2StartThreshold - _minValue)) / (_maxValue - _minValue));
					}

					e.Graphics.DrawLine(linePen, pos, 0, pos, this.Height);
				}
				if (_segmentCount == NeroBarSegments.Three)
				{
					if (_colorThresholds)
					{
						linePen = new Pen(_segment3Color, 1);
					}
					else
					{
						linePen = new Pen(_borderColor, 1);
					}

					if (_rightToLeft)
					{
						pos = this.Width - Convert.ToInt32(((Convert.ToDouble(this.Width) - 2) * (_segment3StartThreshold - _minValue)) / (_maxValue - _minValue));
					}
					else
					{
						pos = Convert.ToInt32(((Convert.ToDouble(this.Width) - 2) * (_segment3StartThreshold - _minValue)) / (_maxValue - _minValue));
					}

					e.Graphics.DrawLine(linePen, pos, 0, pos, this.Height);
				}
			}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			Color backActive1 = default(Color);
			Color backActive2 = default(Color);
			Color backActive3 = default(Color);
			Color backActive4 = default(Color);
			double width = 0;
			double prevWidth = 0;
			double offset = 0;
			Rectangle rectUpper = default(Rectangle);
			GraphicsPath pathUpper = default(GraphicsPath);
			Rectangle rectLower = default(Rectangle);
			GraphicsPath pathLower = default(GraphicsPath);
			int Percent = 0;

			float corrFactor2To1 = -0.225f;
			float corrFactor2To3 = 0.5f;
			float corrFactor2To4 = 0.8f;

			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

			//Draw segment 1
			width = ((Convert.ToDouble(this.Width) - 2) * (_value - _minValue)) / (_maxValue - _minValue);

			if (_changeMode == NeroBarColorChangeModes.Segments)
			{
				if (_segmentCount == NeroBarSegments.Two | _segmentCount == NeroBarSegments.Three)
				{
					if (_value > _segment2StartThreshold)
					{
						width = ((Convert.ToDouble(this.Width) - 2) * (_segment2StartThreshold - _minValue)) / (_maxValue - _minValue);
					}
				}
			}

			prevWidth = width;

			if (Convert.ToInt32(width) > 0)
			{
				if (_rightToLeft)
				{
					rectUpper = new Rectangle(this.Width - Convert.ToInt32(width), 1, Convert.ToInt32(width), this.Height / 2 + 1);
					if (_segmentCount == NeroBarSegments.Two | _segmentCount == NeroBarSegments.Three)
					{
						if (_value < _segment2StartThreshold)
						{
							pathUpper = GetRoundPath(rectUpper, 1);
						}
						else
						{
							pathUpper = GetRightRoundPath(rectUpper, 1);
						}
					}
					else
					{
						pathUpper = GetRoundPath(rectUpper, 1);
					}

					rectLower = new Rectangle(this.Width - Convert.ToInt32(width), this.Height / 2, Convert.ToInt32(width), this.Height - (this.Height / 2) - 1);
					if (_segmentCount == NeroBarSegments.Two | _segmentCount == NeroBarSegments.Three)
					{
						if (_value < _segment2StartThreshold)
						{
							pathLower = GetRoundPath(rectLower, 1);
						}
						else
						{
							pathLower = GetRightRoundPath(rectLower, 1);
						}
					}
					else
					{
						pathLower = GetRoundPath(rectLower, 1);
					}
				}
				else
				{
					rectUpper = new Rectangle(1, 1, Convert.ToInt32(width), this.Height / 2 + 1);
					if (_segmentCount == NeroBarSegments.Two | _segmentCount == NeroBarSegments.Three)
					{
						if (_value < _segment2StartThreshold)
						{
							pathUpper = GetRoundPath(rectUpper, 1);
						}
						else
						{
							pathUpper = GetLeftRoundPath(rectUpper, 1);
						}
					}
					else
					{
						pathUpper = GetRoundPath(rectUpper, 1);
					}

					rectLower = new Rectangle(1, this.Height / 2, Convert.ToInt32(width), this.Height - (this.Height / 2) - 1);
					if (_segmentCount == NeroBarSegments.Two | _segmentCount == NeroBarSegments.Three)
					{
						if (_value < _segment2StartThreshold)
						{
							pathLower = GetRoundPath(rectLower, 1);
						}
						else
						{
							pathLower = GetLeftRoundPath(rectLower, 1);
						}
					}
					else
					{
						pathLower = GetRoundPath(rectLower, 1);
					}
				}

				backActive1 = CreateColorWithCorrectedLightness(_segment1Color, corrFactor2To1);
				backActive2 = _segment1Color;
				backActive3 = CreateColorWithCorrectedLightness(_segment1Color, corrFactor2To3);
				backActive4 = CreateColorWithCorrectedLightness(_segment1Color, corrFactor2To4);

				if (_changeMode == NeroBarColorChangeModes.WholeBar & _segmentCount != NeroBarSegments.One)
				{
					if (_segmentCount == NeroBarSegments.Two)
					{
						if (_value > _segment2StartThreshold)
						{
							backActive1 = CreateColorWithCorrectedLightness(_segment2Color, corrFactor2To1);
							backActive2 = _segment2Color;
							backActive3 = CreateColorWithCorrectedLightness(_segment2Color, corrFactor2To3);
							backActive4 = CreateColorWithCorrectedLightness(_segment2Color, corrFactor2To4);
						}
					}
					else if (_segmentCount == NeroBarSegments.Three)
					{
						if (_value > _segment2StartThreshold)
						{
							backActive1 = CreateColorWithCorrectedLightness(_segment2Color, corrFactor2To1);
							backActive2 = _segment2Color;
							backActive3 = CreateColorWithCorrectedLightness(_segment2Color, corrFactor2To3);
							backActive4 = CreateColorWithCorrectedLightness(_segment2Color, corrFactor2To4);
						}
						if (_value > _segment3StartThreshold)
						{
							backActive1 = CreateColorWithCorrectedLightness(_segment3Color, corrFactor2To1);
							backActive2 = _segment3Color;
							backActive3 = CreateColorWithCorrectedLightness(_segment3Color, corrFactor2To3);
							backActive4 = CreateColorWithCorrectedLightness(_segment3Color, corrFactor2To4);
						}
					}
				}

				using (Brush brushUpper = new LinearGradientBrush(rectUpper, backActive4, backActive3, LinearGradientMode.Vertical))
				{
					e.Graphics.FillPath(brushUpper, pathUpper);
				}

				using (Brush brushLower = new LinearGradientBrush(rectLower, backActive1, backActive2, LinearGradientMode.Vertical))
				{
					e.Graphics.FillPath(brushLower, pathLower);
				}
			}


			if ((_segmentCount == NeroBarSegments.Two | _segmentCount == NeroBarSegments.Three) & _changeMode == NeroBarColorChangeModes.Segments)
			{
				//Draw segment 2
				width = ((Convert.ToDouble(this.Width) - 2) * (_value - _minValue)) / (_maxValue - _minValue);

				if (_segmentCount == NeroBarSegments.Three)
				{
					if (_value > _segment3StartThreshold)
					{
						width = ((Convert.ToDouble(this.Width) - 2) * (_segment3StartThreshold - _minValue)) / (_maxValue - _minValue);
					}
				}

				width = width - prevWidth;
				offset = prevWidth + 1;
				prevWidth = width + prevWidth;

				if (Convert.ToInt32(width) > 0)
				{
					if (_rightToLeft)
					{
						rectUpper = new Rectangle(Convert.ToInt32(this.Width - width - offset), 1, Convert.ToInt32(width), Convert.ToInt32(this.Height / 2 + 1));
						if (_segmentCount == NeroBarSegments.Three)
						{
							if (_value < _segment3StartThreshold)
							{
								pathUpper = GetLeftRoundPath(rectUpper, 1);
							}
							else
							{
								pathUpper = GetNoRoundPath(rectUpper, 1);
							}
						}
						else
						{
							pathUpper = GetLeftRoundPath(rectUpper, 1);
						}

						rectLower = new Rectangle(Convert.ToInt32(this.Width - width - offset), Convert.ToInt32(this.Height / 2), Convert.ToInt32(width), Convert.ToInt32(this.Height - this.Height / 2 - 1));
						if (_segmentCount == NeroBarSegments.Three)
						{
							if (_value < _segment3StartThreshold)
							{
								pathLower = GetLeftRoundPath(rectLower, 1);
							}
							else
							{
								pathLower = GetNoRoundPath(rectLower, 1);
							}
						}
						else
						{
							pathLower = GetLeftRoundPath(rectLower, 1);
						}
					}
					else
					{
						rectUpper = new Rectangle(Convert.ToInt32(offset), 1, Convert.ToInt32(width), Convert.ToInt32(this.Height / 2 + 1));
						if (_segmentCount == NeroBarSegments.Three)
						{
							if (_value < _segment3StartThreshold)
							{
								pathUpper = GetRightRoundPath(rectUpper, 1);
							}
							else
							{
								pathUpper = GetNoRoundPath(rectUpper, 1);
							}
						}
						else
						{
							pathUpper = GetRightRoundPath(rectUpper, 1);
						}

						rectLower = new Rectangle(Convert.ToInt32(offset), Convert.ToInt32(this.Height / 2), Convert.ToInt32(width), Convert.ToInt32(this.Height - this.Height / 2 - 1));
						if (_segmentCount == NeroBarSegments.Three)
						{
							if (_value < _segment3StartThreshold)
							{
								pathLower = GetRightRoundPath(rectLower, 1);
							}
							else
							{
								pathLower = GetNoRoundPath(rectLower, 1);
							}
						}
						else
						{
							pathLower = GetRightRoundPath(rectLower, 1);
						}
					}

					backActive1 = CreateColorWithCorrectedLightness(_segment2Color, corrFactor2To1);
					backActive2 = _segment2Color;
					backActive3 = CreateColorWithCorrectedLightness(_segment2Color, corrFactor2To3);
					backActive4 = CreateColorWithCorrectedLightness(_segment2Color, corrFactor2To4);

					using (Brush brushUpper = new LinearGradientBrush(rectUpper, backActive4, backActive3, LinearGradientMode.Vertical))
					{
						e.Graphics.FillPath(brushUpper, pathUpper);
					}

					using (Brush brushLower = new LinearGradientBrush(rectLower, backActive1, backActive2, LinearGradientMode.Vertical))
					{
						e.Graphics.FillPath(brushLower, pathLower);
					}
				}
			}


			if (_segmentCount == NeroBarSegments.Three & _changeMode == NeroBarColorChangeModes.Segments)
			{
				//Draw segment 3
				width = ((Convert.ToDouble(this.Width) - 2) * (_value - _minValue)) / (_maxValue - _minValue);
				width = width - prevWidth;

				offset = prevWidth + 1;

				if (Convert.ToInt32(width) > 0)
				{
					if (_rightToLeft)
					{
						rectUpper = new Rectangle(Convert.ToInt32(this.Width - width - offset), 1, Convert.ToInt32(width), Convert.ToInt32(this.Height / 2) + 1);
						pathUpper = GetLeftRoundPath(rectUpper, 1);

						rectLower = new Rectangle(Convert.ToInt32(this.Width - width - offset), this.Height / 2, Convert.ToInt32(width), Convert.ToInt32(this.Height - this.Height / 2 - 1));
						pathLower = GetLeftRoundPath(rectLower, 1);
					}
					else
					{
						rectUpper = new Rectangle(Convert.ToInt32(offset), 1, Convert.ToInt32(width), Convert.ToInt32(this.Height / 2 + 1));
						pathUpper = GetRightRoundPath(rectUpper, 1);

						rectLower = new Rectangle(Convert.ToInt32(offset), Convert.ToInt32(this.Height / 2), Convert.ToInt32(width), Convert.ToInt32(this.Height - this.Height / 2 - 1));
						pathLower = GetRightRoundPath(rectLower, 1);
					}

					backActive1 = CreateColorWithCorrectedLightness(_segment3Color, corrFactor2To1);
					backActive2 = _segment3Color;
					backActive3 = CreateColorWithCorrectedLightness(_segment3Color, corrFactor2To3);
					backActive4 = CreateColorWithCorrectedLightness(_segment3Color, corrFactor2To4);

					using (Brush brushUpper = new LinearGradientBrush(rectUpper, backActive4, backActive3, LinearGradientMode.Vertical))
					{
						e.Graphics.FillPath(brushUpper, pathUpper);
					}

					using (Brush brushLower = new LinearGradientBrush(rectLower, backActive1, backActive2, LinearGradientMode.Vertical))
					{
						e.Graphics.FillPath(brushLower, pathLower);
					}
				}
			}

			//Draw border
			Rectangle rectFull = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
			GraphicsPath pathFull = GetRoundPath(rectFull, 2);

			using (Pen pen = new Pen(_borderColor))
			{
				e.Graphics.DrawPath(pen, pathFull);
			}

			//Draw glow
			if (_glowMode != NeroBarGlowModes.None & !this.DesignMode)
			{
				Rectangle r = new Rectangle(_glowPosition, 0, 60, this.Height);
				LinearGradientBrush lgb = new LinearGradientBrush(r, Color.White, Color.White, LinearGradientMode.Horizontal);

				ColorBlend cb = new ColorBlend(4);
				cb.Colors = new Color[] {
				Color.Transparent,
				_glowColor,
				_glowColor,
				Color.Transparent
			};
				cb.Positions = new float[] {
				0f,
				0.5f,
				0.6f,
				1f
			};
				lgb.InterpolationColors = cb;

				Rectangle clip = new Rectangle(1, 2, this.Width - 3, this.Height - 3);

				clip.Width = Convert.ToInt32(_value * 1f / (_maxValue - _minValue) * this.Width);
				if (_rightToLeft)
				{
					clip.X = this.Width - clip.Width;
				}

				if (clip.Width > 0 & clip.Height > 0 & r.Width > 0 & r.Height > 0)
				{
					if (GlowMode == NeroBarGlowModes.ProgressOnly)
					{
						e.Graphics.SetClip(clip);
					}
					e.Graphics.FillRectangle(lgb, r);
					e.Graphics.ResetClip();
				}
			}

			if (lblPercent.Visible)
			{
				try
				{
					switch (_percentageMode)
					{
						case NeroBarPercentageCalculationModes.Segment_1:
							if (_segmentCount == NeroBarSegments.One)
							{
								Percent = Convert.ToInt32((_value * 100) / (_maxValue - _minValue));
							}
							else
							{
								Percent = Convert.ToInt32((_value * 100) / (_segment2StartThreshold - _minValue));
							}
							break;
						case NeroBarPercentageCalculationModes.Segments_1_2:
							if (_segmentCount != NeroBarSegments.Three)
							{
								Percent = Convert.ToInt32((_value * 100) / (_maxValue - _minValue));
							}
							else
							{
								Percent = Convert.ToInt32((_value * 100) / (_segment3StartThreshold - _minValue));
							}
							break;
						case NeroBarPercentageCalculationModes.WholeControl:
							Percent = Convert.ToInt32((_value * 100) / (_maxValue - _minValue));
							break;
					}
				}
				catch
				{
					//Shouldn't happen, but just in case
				}

				lblPercent.Text = sTooLarge + string.Format("{0}%", Percent);
				lblPercent.Width = this.Width;
			}
		}

		public override System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)
		{
			if (proposedSize.Width < 100)
				proposedSize.Width = 100;
			proposedSize.Height = 16;
			return proposedSize;
		}

		#endregion

		#region "Private Methods"

		private GraphicsPath GetLeftRoundPath(Rectangle r, int depth)
		{
			GraphicsPath graphPath = new GraphicsPath();

			graphPath.AddArc(r.X, r.Y, depth, depth, 180, 90);
			graphPath.AddLine(r.X + r.Width - depth, r.Y, r.X + r.Width, r.Y);
			graphPath.AddLine(r.X + r.Width, r.Y, r.X + r.Width, r.Y + depth);
			graphPath.AddLine(r.X + r.Width, r.Y + r.Height - depth, r.X + r.Width, r.Y + r.Height);
			graphPath.AddLine(r.X + r.Width, r.Y + r.Height, r.X + r.Width - depth, r.Y + r.Height);
			graphPath.AddArc(r.X, r.Y + r.Height - depth, depth, depth, 90, 90);

			return graphPath;
		}

		private GraphicsPath GetRightRoundPath(Rectangle r, int depth)
		{
			GraphicsPath graphPath = new GraphicsPath();

			graphPath.AddLine(r.X, r.Y + depth, r.X, r.Y);
			graphPath.AddLine(r.X, r.Y, r.X + depth, r.Y);
			graphPath.AddArc(r.X + r.Width - depth, r.Y, depth, depth, 270, 90);
			graphPath.AddArc(r.X + r.Width - depth, r.Y + r.Height - depth, depth, depth, 0, 90);
			graphPath.AddLine(r.X + depth, r.Y + r.Height, r.X, r.Y + r.Height);
			graphPath.AddLine(r.X, r.Y + r.Height, r.X, r.Y + r.Height - depth);

			return graphPath;
		}

		private GraphicsPath GetNoRoundPath(Rectangle r, int depth)
		{
			GraphicsPath graphPath = new GraphicsPath();

			graphPath.AddLine(r.X, r.Y + depth, r.X, r.Y);
			graphPath.AddLine(r.X, r.Y, r.X + depth, r.Y);
			graphPath.AddLine(r.X + r.Width - depth, r.Y, r.X + r.Width, r.Y);
			graphPath.AddLine(r.X + r.Width, r.Y, r.X + r.Width, r.Y + depth);
			graphPath.AddLine(r.X + r.Width, r.Y + r.Height - depth, r.X + r.Width, r.Y + r.Height);
			graphPath.AddLine(r.X + r.Width, r.Y + r.Height, r.X + r.Width - depth, r.Y + r.Height);
			graphPath.AddLine(r.X + depth, r.Y + r.Height, r.X, r.Y + r.Height);
			graphPath.AddLine(r.X, r.Y + r.Height, r.X, r.Y + r.Height - depth);

			return graphPath;
		}

		private GraphicsPath GetRoundPath(Rectangle r, int depth)
		{
			GraphicsPath graphPath = new GraphicsPath();

			graphPath.AddArc(r.X, r.Y, depth, depth, 180, 90);
			graphPath.AddArc(r.X + r.Width - depth, r.Y, depth, depth, 270, 90);
			graphPath.AddArc(r.X + r.Width - depth, r.Y + r.Height - depth, depth, depth, 0, 90);
			graphPath.AddArc(r.X, r.Y + r.Height - depth, depth, depth, 90, 90);
			graphPath.AddLine(r.X, r.Y + r.Height - depth, r.X, r.Y + depth / 2);

			return graphPath;
		}

		private Color CreateColorWithCorrectedLightness(Color originalColor, float correctionFactor)
		{
			if (correctionFactor == 0)
			{
				return originalColor;
			}

			float red = Convert.ToSingle(originalColor.R);
			float green = Convert.ToSingle(originalColor.G);
			float blue = Convert.ToSingle(originalColor.B);

			if (correctionFactor < 0)
			{
				red = red * (1 + correctionFactor);
				green = green * (1 + correctionFactor);
				blue = blue * (1 + correctionFactor);
			}
			else
			{
				red = (255 - red) * correctionFactor + red;
				green = (255 - green) * correctionFactor + green;
				blue = (255 - blue) * correctionFactor + blue;
			}

			if (red > 255)
				red = 255;
			if (green > 255)
				green = 255;
			if (blue > 255)
				blue = 255;

			return Color.FromArgb(originalColor.A, Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue));
		}

		#endregion

		#region "Timer Event Handlers"

        private void tmrGlowPause_Elapsed(FlowVar flowVar)
		{
            if (!tmrGlowPause.Timer1.IsRunning)
            {
                tmrGlowPause.Timer1.Restart();
            }
            if (tmrGlowPause.Timer1.ElapsedMilliseconds < tmrGlowPauseInterval)
            {
                return;
            }
			tmrGlowPause.Stop();
			if (_glowMode != NeroBarGlowModes.None)
			{
                tmrAnimateGlow.Start();
			}
			else
			{
                tmrAnimateGlow.Stop();
                tmrGlowPause.Stop();
				if (_rightToLeft)
				{
					_glowPosition = this.Width + 60;
				}
				else
				{
					_glowPosition = -60;
				}
			}
		}

        private void tmrAnimateGlow_Elapsed(FlowVar flowVar)
		{
            if (!tmrAnimateGlow.Timer1.IsRunning)
            {
                tmrAnimateGlow.Timer1.Restart();
            }
            if (tmrAnimateGlow.Timer1.ElapsedMilliseconds < tmrAnimateGlowInterval)
            {
                return;
            }
            tmrAnimateGlow.Stop();
			if (_glowMode != NeroBarGlowModes.None)
			{
				if (_rightToLeft)
				{
					_glowPosition -= 2;
					if (_glowPosition < -60)
					{
						_glowPosition = this.Width + 60;
                        tmrGlowPause.Start();
					}
					else
					{
                        tmrAnimateGlow.Start();
					}
				}
				else
				{
					_glowPosition += 2;
					if (_glowPosition > this.Width)
					{
						_glowPosition = -60;
                        tmrGlowPause.Start();
					}
					else
					{
                        tmrAnimateGlow.Start();
					}
				}
				this.Invalidate();
			}
			else
			{
                tmrAnimateGlow.Stop();
                tmrGlowPause.Stop();
				if (_rightToLeft)
				{
					_glowPosition = this.Width + 60;
				}
				else
				{
					_glowPosition = -60;
				}
			}
		}

		#endregion
	}
}