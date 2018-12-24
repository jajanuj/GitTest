using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AOISystem.Utilities.Encryption
{
    public class AESEncryption
    {
        /// <summary>
        /// AES 產生Key函數
        /// </summary>
        /// <returns></returns>
        public static string GenerateKey()
        {
            AesManaged aesM = new AesManaged();
            aesM.GenerateKey();
            byte[] result = aesM.Key;
            string hexString = ConvertToHexString(result);
            return hexString;
        }

        /// <summary>
        /// AES 加密函數
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESEncoder(string source, string key)
        {
            byte[] bkey = new byte[32];
            byte[] iv = new byte[16];
            byte[] shaBuffer = new SHA384Managed().ComputeHash(Encoding.UTF8.GetBytes(key));
            RijndaelManaged aes = new RijndaelManaged();

            Array.Copy(shaBuffer, 0, bkey, 0, 32);
            Array.Copy(shaBuffer, 32, iv, 0, 16);

            ICryptoTransform ict = aes.CreateEncryptor(bkey, iv);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
            byte[] rawData = UTF8Encoding.UTF8.GetBytes(source);

            cs.Write(rawData, 0, rawData.Length);
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// AES 解密函數
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESDecoder(string source, string key)
        {
            byte[] bkey = new byte[32];
            byte[] iv = new byte[16];
            byte[] shaBuffer = new SHA384Managed().ComputeHash(Encoding.UTF8.GetBytes(key));
            RijndaelManaged aes = new RijndaelManaged();

            Array.Copy(shaBuffer, 0, bkey, 0, 32);
            Array.Copy(shaBuffer, 32, iv, 0, 16);

            ICryptoTransform ict = aes.CreateDecryptor(bkey, iv);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
            byte[] rawData = Convert.FromBase64String(source);
            cs.Write(rawData, 0, rawData.Length);
            cs.Close();
            return UTF8Encoding.UTF8.GetString(ms.ToArray());
        }

        private static string ConvertToHexString(Byte[] byteArray)
        {
            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder(byteArray.Length);

            for (int i = 0; i < byteArray.Length; i++)
            {
                sb.Append(byteArray[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
