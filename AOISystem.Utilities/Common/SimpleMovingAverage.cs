using System;
using System.Linq;
using AOISystem.Utilities.Collections;

namespace AOISystem.Utilities.Common
{
    public class SimpleMovingAverage
    {
        CircularList<double> samples;
        protected double total;

        /// <summary>
        /// Get the average for the current number of samples.
        /// </summary>
        public double Average
        {
            get
            {
                return samples.Count == 0 ? 0 : total / samples.Count;
            }
        }

        /// <summary>
        /// Get the all items of samples
        /// </summary>
        public double[] Items
        {
            get
            {
                return samples.ToArray();
            }
        }

        /// <summary>
        /// Get the max value of samples
        /// </summary>
        public double Max
        {
            get
            {
                return samples.Count == 0 ? 0 : samples.Max();
            }
        }

        /// <summary>
        /// Get the min value of samples
        /// </summary>
        public double Min
        {
            get
            {
                return samples.Count == 0 ? 0 : samples.Min();
            }
        }

        /// <summary>
        /// Constructor, initializing the sample size to the specified number.
        /// </summary>
        public SimpleMovingAverage(int numSamples)
        {
            if (numSamples <= 0)
            {
                throw new ArgumentOutOfRangeException("numSamples can't be negative or 0.");
            }

            samples = new CircularList<double>(numSamples);
            total = 0;
        }

        /// <summary>
        /// Adds a sample to the sample collection.
        /// </summary>
        public void AddSample(double val)
        {
            if (samples.Count == samples.Length)
            {
                total -= samples.Value;
            }

            samples.Value = val;
            total += val;
            samples.Next();
        }

        /// <summary>
        /// Clears all samples to 0.
        /// </summary>
        public void ClearSamples()
        {
            total = 0;
            samples.Clear();
        }

        /// <summary>
        /// Initializes all samples to the specified value.
        /// </summary>
        public void InitializeSamples(double v)
        {
            samples.SetAll(v);
            total = v * samples.Length;
        }
    }
}
