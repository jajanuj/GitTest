using System;
using System.Diagnostics;

namespace AOISystem.Utilities.Common
{
    public class MathEx
    {
        /// <summary>
        /// 數值轉換(無條件捨去)
        /// </summary>
        /// <param name="Value">數值</param>
        /// <param name="position">小數點位數</param>
        /// <returns></returns>
        public static float ConvertValue(float Value, int position)
        {
            string ConvertPosition = "#";
            if (position > 0)
            {
                ConvertPosition += ".";
                for (int i = 2; i < position; i++)
                    ConvertPosition += "#";
                ConvertPosition += "0";
            }
            return Single.Parse(Value.ToString(ConvertPosition));
        }

        /// <summary>
        /// 數值轉換(四捨五入)
        /// </summary>
        /// <param name="Value">數值</param>
        /// <param name="position">小數點位數</param>
        /// <returns></returns>
        public static float ConvertValue2(float Value, int position)
        {
            Value = Value * (10 ^ position);
            if (Convert.ToInt32(Value.ToString().Substring(Value.ToString().IndexOf('.') + 1, 1)) > 4)
                Value++;
            Value = Value / (10 ^ position);

            string ConvertPosition = "#";
            if (position > 0)
            {
                ConvertPosition += ".";
                for (int i = 2; i < position; i++)
                    ConvertPosition += "#";
                ConvertPosition += "0";
            }
            return Single.Parse(Value.ToString(ConvertPosition));
        }

        /// <summary>
        /// 數值轉換(四捨五入)
        /// </summary>
        /// <param name="value">數值</param>
        /// <param name="digits">小數點位數</param>
        /// <returns></returns>
        public static double Round(double value, int digits)
        {
            return (double)Math.Round((decimal)value, digits, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 得到兩點直線距離
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double GetP2PDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)));
        }

        public static double GetP2LDistance(double x, double y, double x1, double y1, double x2, double y2)
        {
            double a = GetP2PDistance(x1, y1, x2, y2);
            double b = GetP2PDistance(x1, y1, x, y);
            double c = GetP2PDistance(x2, y2, x, y);
            double p = (a + b + c) / 2;
            double dis = 0;
            if (b + c > a)
            {
                double s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
                dis = 2 * s / a;
            }
            else
            {
                dis = 0;
            }
            return dis;
        }

        public static double GetP2LDistanceOld(double x, double y, double x1, double y1, double x2, double y2)
        {
            double dis = 0;
            if (x1 == x2)
            {
                dis = x - x1;
                return dis;
            }
            double lineK = (y2 - y1) / (x2 - x1);
            double lineC = (x2 * y1 - x1 * y2) / (x2 - x1);
            dis = Math.Abs(lineK * x - y + lineC) / (Math.Sqrt(lineK * lineK + 1));
            return dis;
        }

        private bool IsOdd(int n)
        {
            return Convert.ToBoolean(n % 2);
        }

        public static void LinearRegression(double[] values, out double slope, out double intercept)
        {
            double xAvg = 0;
            double yAvg = 0;

            for (int x = 0; x < values.Length; x++)
            {
                xAvg += x;
                yAvg += values[x];
            }

            xAvg = xAvg / values.Length;
            yAvg = yAvg / values.Length;

            double v1 = 0;
            double v2 = 0;

            for (int x = 0; x < values.Length; x++)
            {
                v1 += (x - xAvg) * (values[x] - yAvg);
                v2 += Math.Pow(x - xAvg, 2);
            }

            slope = v1 / v2;
            intercept = yAvg - slope * xAvg;
        }

        /// <summary>
        /// Fits a line to a collection of (x,y) points.
        /// </summary>
        /// <param name="xVals">The x-axis values.</param>
        /// <param name="yVals">The y-axis values.</param>
        /// <param name="inclusiveStart">The inclusive inclusiveStart index.</param>
        /// <param name="exclusiveEnd">The exclusive exclusiveEnd index.</param>
        /// <param name="rsquared">The r^2 value of the line.</param>
        /// <param name="yintercept">The y-intercept value of the line (i.e. y = ax + b, yintercept is b).</param>
        /// <param name="slope">The slop of the line (i.e. y = ax + b, slope is a).</param>
        public static void LinearRegression(double[] xVals, double[] yVals,
                                            int inclusiveStart, int exclusiveEnd,
                                            out double rsquared, out double yintercept,
                                            out double slope)
        {
            Debug.Assert(xVals.Length == yVals.Length);
            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double ssX = 0;
            double ssY = 0;
            double sumCodeviates = 0;
            double sCo = 0;
            double count = exclusiveEnd - inclusiveStart;

            for (int ctr = inclusiveStart; ctr < exclusiveEnd; ctr++)
            {
                double x = xVals[ctr];
                double y = yVals[ctr];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }
            ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            ssY = sumOfYSq - ((sumOfY * sumOfY) / count);
            double RNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            double RDenom = (count * sumOfXSq - (sumOfX * sumOfX))
             * (count * sumOfYSq - (sumOfY * sumOfY));
            sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            double meanX = sumOfX / count;
            double meanY = sumOfY / count;
            double dblR = RNumerator / Math.Sqrt(RDenom);
            rsquared = dblR * dblR;
            yintercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }

        /// <summary>
        /// 比較最大值, 忽略正負號
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static double MaxValueIgnoreSign(double value1, double value2)
        {
            return Math.Abs(value1) > Math.Abs(value2) ? value1 : value2;
        }

        /// <summary>
        /// 比較最小值, 忽略正負號
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static double MinValueIgnoreSign(double value1, double value2)
        {
            return Math.Abs(value1) < Math.Abs(value2) ? value1 : value2;
        }
    }

    public static class MathExtensions
    {
        /// <summary>
        /// 是否為基數
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsOdd(this int value)
        {
            return Convert.ToBoolean(value % 2);
        }

        /// <summary>
        /// 是否為偶數
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEven(this int value)
        {
            return !Convert.ToBoolean(value % 2);
        }
    }
}
