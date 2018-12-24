using System;
using System.Collections;

namespace AOISystem.Utilities
{
    public class BitConverterEx
    {
        /// <summary>將指定變數S內的第Bit位元設定為1</summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        public static void SetB(ref byte S, byte Bit)
        {
            byte buf = 1;
            buf = (buf <<= Bit);
            S = (byte)(S | buf);
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為1
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        public static void SetB(ref short S, byte Bit)
        {
            short buf = 1;
            buf = (buf <<= Bit);
            S = (short)(S | buf);
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為1
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        public static void SetB(ref ushort S, byte Bit)
        {
            ushort buf = 1;
            buf = (buf <<= Bit);
            S = (ushort)(S | buf);
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為1
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        public static void SetB(ref int S, byte Bit)
        {
            int buf = 1;
            buf = (buf <<= Bit);
            S = (S | buf);
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為1
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        public static void SetB(ref long S, byte Bit)
        {
            long buf = 1;
            buf = (buf <<= Bit);
            S = (S | buf);
        }

        /// <summary>將指定變數S內的第Bit位元設定為0</summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        public static void ClrB(ref byte S, byte Bit)
        {
            uint buf = 1;
            buf = (buf <<= Bit);
            buf = (uint)(buf ^ 0x7FFFFFFFFFFFFFFF);
            S = (byte)(S & buf);
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為0
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        public static void ClrB(ref short S, byte Bit)
        {
            short buf = 1;
            buf = (buf <<= Bit);
            buf = (short)(buf ^ 0x7FFF);
            S = (short)(S & buf);
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為0
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        public static void ClrB(ref ushort S, byte Bit)
        {
            ushort buf = 1;
            buf = (buf <<= Bit);
            buf = (ushort)(buf ^ 0x7FFF);
            S = (ushort)(S & buf);
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為0
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        public static void ClrB(ref int S, byte Bit)
        {
            int buf = 1;
            buf = (buf <<= Bit);
            buf = (buf ^ 0x7FFFFFFF);
            S = (S & buf);
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為0
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        public static void ClrB(ref long S, byte Bit)
        {
            long buf = 1;
            buf = (buf <<= Bit);
            buf = (buf ^ 0x7FFFFFFFFFFFFFFF);
            S = (S & buf);
        }

        /// <summary>
        /// 檢查指定變數S內的第Bit位元狀態是否為1
        /// </summary>
        /// <param name="S">指定變數S</param>
        /// <param name="Bit">第Bit位元</param>
        /// <returns>回傳值。true=狀態為1；false=狀態為0</returns>
        public static bool TestB(short S, byte Bit)
        {
            short buf = 1;
            buf = (buf <<= Bit);
            S = (short)(S & buf);
            return (S != 0);
        }

        /// <summary>
        /// 檢查指定變數S內的第Bit位元狀態是否為1
        /// </summary>
        /// <param name="S">指定變數S</param>
        /// <param name="Bit">第Bit位元</param>
        /// <returns>回傳值。true=狀態為1；false=狀態為0</returns>
        public static bool TestB(ushort S, byte Bit)
        {
            ushort buf = 1;
            buf = (buf <<= Bit);
            S = (ushort)(S & buf);
            return (S != 0);
        }

        /// <summary>
        /// 檢查指定變數S內的第Bit位元狀態是否為1
        /// </summary>
        /// <param name="S">指定變數S</param>
        /// <param name="Bit">第Bit位元</param>
        /// <returns>回傳值。true=狀態為1；false=狀態為0</returns>
        public static bool TestB(int S, byte Bit)
        {
            int buf = 1;
            buf = (buf <<= Bit);
            S = (S & buf);
            return (S != 0);
        }

        /// <summary>
        /// 檢查指定變數S內的第Bit位元狀態是否為1
        /// </summary>
        /// <param name="S">指定變數S</param>
        /// <param name="Bit">第Bit位元</param>
        /// <returns>回傳值。true=狀態為1；false=狀態為0</returns>
        public static bool TestB(long S, byte Bit)
        {
            long buf = 1;
            buf = (buf <<= Bit);
            S = (S & buf);
            return (S != 0);
        }

        /// <summary>將指定變數S內的第Bit位元設定為指定的狀態Status</summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        /// <param name="Status">指定的狀態Status</param>
        public static void SetBit(ref byte S, byte Bit, bool Status)
        {
            if (Status)
            {
                SetB(ref S, Bit);
            }
            else
            {
                ClrB(ref S, Bit);
            }
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為指定的狀態Status
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        /// <param name="Status">指定的狀態Status</param>
        public static void SetBit(ref short S, byte Bit, bool Status)
        {
            if (Status)
            {
                SetB(ref S, Bit);
            }
            else
            {
                ClrB(ref S, Bit);
            }
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為指定的狀態Status
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        /// <param name="Status">指定的狀態Status</param>
        public static void SetBit(ref ushort S, byte Bit, bool Status)
        {
            if (Status)
            {
                SetB(ref S, Bit);
            }
            else
            {
                ClrB(ref S, Bit);
            }
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為指定的狀態Status
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        /// <param name="Status">指定的狀態Status</param>
        public static void SetBit(ref int S, byte Bit, bool Status)
        {
            if (Status)
            {
                SetB(ref S, Bit);
            }
            else
            {
                ClrB(ref S, Bit);
            }
        }

        /// <summary>
        /// 將指定變數S內的第Bit位元設定為指定的狀態Status
        /// </summary>
        /// <param name="S">指定變數S(執行完後會回傳結果)</param>
        /// <param name="Bit">第Bit位元</param>
        /// <param name="Status">指定的狀態Status</param>
        public static void SetBit(ref long S, byte Bit, bool Status)
        {
            if (Status)
            {
                SetB(ref S, Bit);
            }
            else
            {
                ClrB(ref S, Bit);
            }
        }

        public static bool GetBit(int value, byte bit)
        {
            return (value >> bit & 1) == 1;
        }

        public static int SetBit(int value, byte bit, bool on)
        {
            return on ? value | (1 << bit) : value & ~(1 << bit);
        }

        public static bool[] GetBits(byte input)
        {
            bool[] bits = new bool[8];
            for (int i = 0; i < bits.Length; i++)
            {
                bits[i] = (input >> i & 1) == 1;
            }
            return bits;
        }

        public static bool[] GetBits(Int16 input)
        {
            bool[] bits = new bool[16];
            for (int i = 0; i < bits.Length; i++)
            {
                bits[i] = (input >> i & 1) == 1;
            }
            return bits;
        }

        public static void SetBit(ref bool[] bools, int value)
        {
            for (int i = 0; i < bools.Length; i++)
            {
                bools[i] = (value >> i & 1) == 1;
            }
        }

        public static Byte ConvertToByte(bool[] bits)
        {
            Byte result = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                result = Convert.ToByte(result + (bits[i] ? 1 << i : 0));
            }
            return result;
        }

        public static SByte ConvertToSByte(bool[] bits)
        {
            SByte result = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                result = Convert.ToSByte(result + (bits[i] ? 1 << i : 0));
            }
            return result;
        }

        public static UInt16 ConvertToUInt16(bool[] bits)
        {
            UInt16 result = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                result = Convert.ToUInt16(result + (bits[i] ? 1 << i : 0));
            }
            return result;
        }

        public static Int16 ConvertToInt16(bool[] bits)
        {
            Int16 result = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                result = Convert.ToInt16(result + (bits[i] ? 1 << i : 0));
            }
            return result;
        }

        public static Int16 GetInt16ByExtractInt16SomeBits(Int16 source, int bitsStart, int bitsLength)
        {
            Int16 result = 0;
            bool[] recombineBits = new bool[16];
            int index = 0;
            if (bitsLength < 16 && bitsStart < 16)
            {
                for (int i = 0; i < recombineBits.Length; i++)
                {
                    if (index >= bitsLength)
                    {
                        break;
                    }
                    if (i >= bitsStart && index < bitsLength)
                    {
                        recombineBits[index] = (source >> i & 1) == 1;
                        index++;
                    }
                }

                for (int i = 0; i < recombineBits.Length; i++)
                {
                    result = Convert.ToInt16(result + (recombineBits[i] ? 1 << i : 0));
                }
            }
            return result;
        }

        public static Int16 GetInt16ByChangeInt16SomeBits(Int16 source, Int16 input, int bitsStart, int bitsLength)
        {
            Int16 result = 0;
            bool[] sourceBits = new bool[16];
            bool[] inputBits = new bool[16];
            bool[] recombineBits = new bool[16];

            if (bitsLength < 16 && bitsStart < 16)
            {
                for (int i = 0; i < sourceBits.Length; i++)
                {
                    sourceBits[i] = (source >> i & 1) == 1;
                }

                for (int i = 0; i < inputBits.Length; i++)
                {
                    inputBits[i] = (input >> i & 1) == 1;
                }

                for (int i = 0; i < recombineBits.Length; i++)
                {
                    if (i < bitsStart)
                    {
                        recombineBits[i] = sourceBits[i];
                    }
                    else if (i >= bitsStart && i <= bitsStart + bitsLength)
                    {
                        recombineBits[i] = inputBits[i - (bitsStart)];
                    }
                    else if (i > bitsStart + bitsLength)
                    {
                        recombineBits[i] = sourceBits[i];
                    }
                }

                for (int i = 0; i < recombineBits.Length; i++)
                {
                    result = Convert.ToInt16(result + (recombineBits[i] ? 1 << i : 0));
                }
            }
            return result;
        }
    }
}