using System;

namespace AOISystem.Utilities.Data
{
    [Serializable]
    public class CompareItem
    {
        public CompareItem()
        { 
        
        }

        public bool Enable { get; set; }

        public virtual bool CompareTo(double value)
        {
            return true;
        }
    }

    [Serializable]
    public class GreaterItem : CompareItem
    {
        public GreaterItem()
        { 
        
        }

        public double Value { get; set; }

        public override bool CompareTo(double value)
        {
            bool result = true;
            if (this.Enable)
            {
                result = value > this.Value;   
            }
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Enable, this.Value);
        }
    }

    [Serializable]
    public class LessItem : CompareItem
    {
        public LessItem()
        {

        }

        public double Value { get; set; }

        public override bool CompareTo(double value)
        {
            bool result = true;
            if (this.Enable)
            {
                result = value < this.Value;
            }
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Enable, this.Value);
        }
    }

    [Serializable]
    public class BetweenItem : CompareItem
    {
        public BetweenItem()
        {

        }

        public double Up { get; set; }

        public double Low { get; set; }

        public override bool CompareTo(double value)
        {
            bool result = true;
            if (this.Enable)
            {
                result = value <= this.Up && value >= this.Low;
            }
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", this.Enable, this.Up, this.Low);
        }
    }
}
