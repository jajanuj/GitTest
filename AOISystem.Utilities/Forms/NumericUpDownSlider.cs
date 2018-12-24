using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public partial class NumericUpDownSlider : UserControl
    {
        public NumericUpDownSlider()
        {
            InitializeComponent();
        }

        [Localizable(true)]
        public string NodeName
        {
            get { return this.lblName.Text; }
            set
            {
                if (this.lblName.Text != value)
                {
                    this.lblName.Text = value;
                }
            }
        }

        private decimal _maximum = 100;
        public decimal Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = value;
                this.trbValue.Maximum = _maximum;
                this.nudValue.Maximum = _maximum;
            }
        }

        private decimal _minimum = 0;
        public decimal Minimum
        {
            get { return _minimum; }
            set
            {
                _minimum = value;
                this.trbValue.Minimum = _minimum;
                this.nudValue.Minimum = _minimum;
            }
        }

        private decimal _increment = 1;
        public decimal Increment
        {
            get { return _increment; }
            set
            {
                _increment = value;
                this.trbValue.SmallChange = (int)_increment;
                this.nudValue.Increment = _increment;
            }
        }

        private int _decimalPlaces = 0;
        public int DecimalPlaces
        {
            get { return _decimalPlaces; }
            set
            {
                _decimalPlaces = value;
                this.trbValue.DecimalPlaces = _decimalPlaces;
                this.nudValue.DecimalPlaces = _decimalPlaces;
            }
        }

        private decimal _value = 10;
        public decimal Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    if (this.trbValue.Value != _value)
                    {
                        this.trbValue.Value = _value;   
                    }
                    if (this.nudValue.Value != _value)
                    {
                        this.nudValue.Value = _value;   
                    }
                    OnValueChanged();
                }
            }
        }

        [Localizable(true)]
        public string LeftLabel
        {
            get { return this.lblLeftLabel.Text; }
            set
            {
                if (this.lblLeftLabel.Text != value)
                {
                    this.lblLeftLabel.Text = value;
                }
            }
        }

        [Localizable(true)]
        public string RightLabel
        {
            get { return this.lblRightLabel.Text; }
            set
            {
                if (this.lblRightLabel.Text != value)
                {
                    this.lblRightLabel.Text = value;
                }
            }
        }

        public bool WaitMouseDownUp
        {
            get { return this.trbValue.WaitMouseDownUp; }
            set { this.trbValue.WaitMouseDownUp = value; }
        }

        public event EventHandler ValueChanged;

        private void OnValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, new EventArgs());
            }
        }

        private void nudValue_ValueChanged(object sender, EventArgs e)
        {
            this.Value = this.nudValue.Value;
        }

        private void trbValue_ValueChanged(object sender, EventArgs e)
        {
            this.Value = this.trbValue.Value;
        }
    }
}
