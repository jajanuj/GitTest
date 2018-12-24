using System;
using System.Runtime.InteropServices;

namespace AOISystem.Utilities
{
    public unsafe class PointerConvert
    {
        public PointerConvert() 
        {
        }

        /// <summary>轉換Int數據到數組</summary>
        /// <param name="data"></param> 
        /// <returns></returns> 
        public static byte[] ToByte(int data)
        {
            unsafe
            {
                byte* pdata = (byte*)&data;
                byte[] byteArray = new byte[sizeof(int)];
                for (int i = 0; i < sizeof(int); ++i)
                    byteArray[i] = *pdata++;
                return byteArray;
            }
        }
        /// <summary>轉換float數據到數組</summary>
        /// <param name="data"></param> 
        /// <returns></returns> 
        public static byte[] ToByte(float data)
        {
            unsafe
            {
                byte* pdata = (byte*)&data;
                byte[] byteArray = new byte[sizeof(float)];
                for (int i = 0; i < sizeof(float); ++i)
                    byteArray[i] = *pdata++;
                return byteArray;
            }
        }
        /// <summary>轉換double數據到數組</summary> 
        /// <param name="data"></param> 
        /// <returns></returns> 
        public static byte[] ToByte(double data)
        {
            unsafe
            {
                byte* pdata = (byte*)&data;
                byte[] byteArray = new byte[sizeof(double)];
                for (int i = 0; i < sizeof(double); ++i)
                    byteArray[i] = *pdata++;
                return byteArray;
            }
        }
        /// <summary>轉換數組為整形</summary> 
        /// <param name="data"></param> 
        /// <returns></returns> 
        public static int ToInt(byte[] data)
        {
            unsafe
            {
                int n = 0;
                fixed (byte* p = data)
                {
                    n = Marshal.ReadInt32((IntPtr)p);
                }
                return n;
            }
        }
        /// <summary>轉換數組為float</summary> 
        /// <param name="data"></param> 
        /// <returns></returns> 
        public static float ToFloat(byte[] data)
        {
            float a = 0;
            byte i;

            byte[] x = data;
            void* pf;
            fixed (byte* px = x)
            {
                pf = &a;
                for (i = 0; i < data.Length; i++)
                {
                    *((byte*)pf + i) = *(px + i);
                }
            }

            return a;
        }
        /// <summary>轉換數組為Double</summary> 
        /// <param name="data"></param> 
        /// <returns></returns> 
        public static double ToDouble(byte[] data)
        {
            double a = 0;
            byte i;

            byte[] x = data;
            void* pf;
            fixed (byte* px = x)
            {
                pf = &a;
                for (i = 0; i < data.Length; i++)
                {
                    *((byte*)pf + i) = *(px + i);
                }
            }
            return a;
        }
    } 
}
