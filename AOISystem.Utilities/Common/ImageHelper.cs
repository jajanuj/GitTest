using System;
using System.Drawing;
using System.IO;
using AOISystem.Utilities.Forms;

namespace AOISystem.Utilities.Common
{
    public class ImageHelper
    {
        /// <summary>
        /// 將Image 物件轉 Base64 String
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ImageToBase64String(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // 將圖片轉成byte[]
                image.Save(ms, image.RawFormat);
                byte[] imageBytes = ms.ToArray();

                // 將 byte[] 轉 base64
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /// <summary>
        /// 將Base64 String 轉 Image 物件
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static Image Base64StringToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                return Image.FromStream(ms);
            }
        }

        /// <summary>
        /// 將Image 物件轉 Base64 String 工具視窗
        /// </summary>
        public static void ImageToBase64StringConverter()
        {
            ImageToBase64StringForm imageToBase64StringForm = new ImageToBase64StringForm();
            imageToBase64StringForm.Show();
        }
    }
}
