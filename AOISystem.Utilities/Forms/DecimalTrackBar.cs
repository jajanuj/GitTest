using System;
using System.Windows.Forms;

namespace AOISystem.Utilities.Forms
{
    public class DecimalTrackBar : TrackBar
    {
        private int _decimalPlaces = 0;
        public int DecimalPlaces
        {
            get { return _decimalPlaces; }
            set
            {
                _decimalPlaces = value;
                //base.LargeChange = (int)(base.LargeChange / GetPrecision());
                //base.SmallChange = (int)(base.SmallChange / GetPrecision());
                base.Maximum = (int)(base.Maximum / GetPrecision());
                base.Minimum = (int)(base.Minimum / GetPrecision());
                base.Value = (int)(base.Value / GetPrecision());
                base.TickFrequency = (base.Maximum - base.Minimum + 5) / 10;
            }
        }

        //public new decimal LargeChange
        //{ 
        //    get 
        //    { 
        //        return base.LargeChange * GetPrecision(); 
        //    } 
        //    set 
        //    { 
        //        base.LargeChange = (int)(value / GetPrecision()); 
        //    } 
        //}

        //public new decimal SmallChange
        //{
        //    get
        //    {
        //        return base.SmallChange * GetPrecision();
        //    }
        //    set
        //    {
        //        base.SmallChange = (int)(value / GetPrecision());
        //    }
        //}

        public new decimal Maximum
        {
            get
            {
                return base.Maximum * GetPrecision();
            }
            set
            {
                base.Maximum = (int)(value / GetPrecision());
                base.TickFrequency = (base.Maximum - base.Minimum + 5) / 10;
            }
        }

        public new decimal Minimum
        {
            get
            {
                return base.Minimum * GetPrecision();
            }
            set
            {
                base.Minimum = (int)(value / GetPrecision());
                base.TickFrequency = (base.Maximum - base.Minimum + 5) / 10;
            }
        }

        public new decimal Value
        {
            get
            {
                return base.Value * GetPrecision();
            }
            set
            {
                base.Value = (int)(value / GetPrecision());
            }
        }

        private decimal GetPrecision()
        {
            return (decimal)Math.Pow(0.1, _decimalPlaces);
        }

        private bool _waitMouseDownUp = false;
        public bool WaitMouseDownUp
        {
            get { return _waitMouseDownUp; }
            set { _waitMouseDownUp = value; }
        }

        private bool _mouseKeyPress = false;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mouseKeyPress = true;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _mouseKeyPress = false;
            if (this.WaitMouseDownUp)
            {
                OnValueChanged(e);
            }
            base.OnMouseUp(e);
        }

        protected override void OnValueChanged(EventArgs e)
        {
            if (this.WaitMouseDownUp && _mouseKeyPress)
            {
                return;
            }
            base.OnValueChanged(e);
        }
    }
}
