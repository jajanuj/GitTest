using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AOISystem.Utilities
{
    /// <summary>
    /// FileIOUtility 的摘要描述
    /// </summary>
    public static class FileIOUtility
    {
        #region Enviroment應用
        /// <summary>
        /// 顯示主機的環境.
        /// </summary>
        /// <returns></returns>
        public static string ShowEnviroment()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("處理序的命令列：" + Environment.CommandLine);
            sb.AppendLine("工作目錄的完整路徑：" + Environment.CurrentDirectory);
            sb.AppendLine("處理序的結束代碼：" + Environment.ExitCode);
            sb.AppendLine("是否正常關機：" + Environment.HasShutdownStarted);
            sb.AppendLine("NetBIOS名稱：" + Environment.MachineName);
            sb.AppendLine("環境定義的新字串：" + Environment.NewLine);
            sb.AppendLine("作業系統平台：" + Environment.OSVersion.Platform);
            sb.AppendLine("Service Pack版本：" + Environment.OSVersion.ServicePack);
            sb.AppendLine("作業系統版本：" + Environment.OSVersion.Version);
            sb.AppendLine("串連字串表示：" + Environment.OSVersion.VersionString);
            sb.AppendLine("處理器數目：" + Environment.ProcessorCount);
            sb.AppendLine("堆疊追蹤資訊：" + Environment.StackTrace);
            sb.AppendLine("系統目錄完整路徑：" + Environment.SystemDirectory);
            sb.AppendLine("系統啟動後的毫秒數：" + Environment.TickCount);
            sb.AppendLine("使用者網域名稱：" + Environment.UserDomainName);
            sb.AppendLine("處理序是否與使用者互動：" + Environment.UserInteractive);
            sb.AppendLine("使用者名稱：" + Environment.UserName);
            sb.AppendLine("Version：" + Environment.Version);
            sb.AppendLine("組件元件值：" + Environment.Version.Build);
            sb.AppendLine("主要元件值：" + Environment.Version.Major);
            sb.AppendLine("修訂編號的高 16 位元：" + Environment.Version.MajorRevision);
            sb.AppendLine("次要元件值：" + Environment.Version.Minor);
            sb.AppendLine("修訂編號的低 16 位元：" + Environment.Version.MinorRevision);
            sb.AppendLine("修訂元件值：" + Environment.Version.Revision);
            sb.AppendLine("實際記憶體數量：" + Environment.WorkingSet);

            string strFinal;
            string strQuery = "系統磁碟機：%SystemDrive% 與 系統根目錄：%SystemRoot%";
            strFinal = Environment.ExpandEnvironmentVariables(strQuery);
            sb.AppendLine(strFinal);

            string[] arguments = Environment.GetCommandLineArgs();
            sb.AppendLine(string.Format("取得命令列的Args: {0}", string.Join(", ", arguments)));

            sb.AppendLine("系統特殊資料夾的路徑：" + Environment.GetFolderPath(Environment.SpecialFolder.System));

            string[] drives = Environment.GetLogicalDrives();
            sb.AppendLine(string.Format("系統磁碟機：{0}", string.Join(", ", drives)));
            return sb.ToString();

        }
        #endregion
        #region 檔案讀寫及操作
        /// <summary>
        /// 以行為單位讀取整個文字檔案的內容.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        public static string[] ReadFileLine(string FileName)
        {
            //以行為單位讀取整個文字檔案的內容
            return File.ReadAllLines(FileName);
        }
        /// <summary>
        /// 以字串的方式回傳整個檔案的內容.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        public static string ReadFileString(string FileName)
        {
            //以字串的方式回傳整個檔案的內容
            return File.ReadAllText(FileName);
        }
        /// <summary>
        /// 新建檔案.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        public static void CreateFile(string FileName)
        {
            //新建檔案       
            File.Create(FileName);
        }
        /// <summary>
        /// 把內容寫到目的檔案，若檔案存在則覆寫之(原本檔案會被覆蓋過去).
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="Content">The content.</param>
        public static void OverwriteFile(string FileName, string Content)
        {
            //把內容寫到目的檔案，若檔案存在則覆寫之(原本檔案會被覆蓋過去)

            File.WriteAllText(FileName, Content);
        }
        /// <summary>
        /// 把內容寫到目的檔案，若檔案存在則附加在原本內容之後.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="Content">The content.</param>
        public static void AppendFile(string FileName, string Content)
        {
            //把內容寫到目的檔案，若檔案存在則附加在原本內容之後
            //if (File.Exists(FileName) == false)
            //    File.Create(FileName);
            File.AppendAllText(FileName, Content);
        }

        /// <summary>
        /// 以行為單位讀取整個文字檔案的內容，並指定編碼方式.
        /// </summary>
        /// <param name="InFName">Name of the in F.</param>
        /// <param name="EncodingType">Type of the encoding.</param>
        /// <returns></returns>
        public static string[] ImportData(string InFName, string EncodingType)
        {
            try
            {
                FileStream fs = new FileStream(InFName, FileMode.Open, FileAccess.Read);
                StreamReader sr;
                if (EncodingType == string.Empty || EncodingType.ToUpper() == "DEFAULT")
                    sr = new StreamReader(fs, UnicodeEncoding.Default);
                else
                    sr = new StreamReader(fs, UnicodeEncoding.GetEncoding(EncodingType));
                StreamWriter sw;
                List<string> result = new List<string>();
                while (sr.Peek() >= 0)
                {
                    string temp = sr.ReadLine();
                    result.Add(temp);
                }
                sr.Close();
                return result.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 以行為單位匯出文字檔案，並指定編碼方式.
        /// </summary>
        /// <param name="OutFName">Name of the output File Name.</param>
        /// <param name="InValue">The input value arrya.</param>
        /// <returns></returns>
        public static bool ExportData(string OutFName, string[] InValue)
        {
            try
            {
                FileInfo f1 = new FileInfo(OutFName);
                StreamWriter sw = f1.AppendText();
                for (int i = 0; i < InValue.Length; i++)
                    sw.WriteLine(InValue[i].ToString());
                sw.Flush();
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// 判斷檔案是否存在.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        public static bool CheckFileExist(string FileName)
        {
            // 判斷檔案是否存在            
            return (System.IO.File.Exists(FileName)) ? true : false;
        }

        /// <summary>
        /// 批次重新命名檔案，放置於同資料夾下.
        /// </summary>
        /// <param name="DirectoryPath">The directory path.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="Start">The start.</param>
        /// <param name="End">The end.</param>
        public static void BatchRenameFile(string DirectoryPath, string FileName, int Start)
        {

            DirectoryInfo di = new DirectoryInfo(DirectoryPath);
            int StartCount = Start;
            foreach (FileInfo fi in di.GetFiles())
            {
                String NewFileName = FileName + StartCount;
                //重新命名
                fi.MoveTo(Path.Combine(fi.DirectoryName, NewFileName + fi.Extension));
                StartCount++;
            }

        }
        /// <summary>
        /// 取得檔案建立日期.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetFileCreateDate(string FileName)
        {
            FileInfo fs = new FileInfo(FileName);
            if (!fs.Exists) return " Not Found!";
            else return fs.CreationTime.ToString("yyyyMMdd");
        }
        /// <summary>
        /// 取得檔案最後存取日期.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetFileLastAccessDate(string FileName)
        {
            FileInfo fs = new FileInfo(FileName);
            if (!fs.Exists) return " Not Found!";
            else return fs.LastAccessTime.ToString("yyyyMMdd");
        }
        /// <summary>
        /// 取得檔案最後修改日期.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns></returns>
        public static string GetFileLastWriteDate(string FileName)
        {
            FileInfo fs = new FileInfo(FileName);
            if (!fs.Exists) return " Not Found!";
            else return fs.LastWriteTime.ToString("yyyyMMdd");
        }
        /// <summary>
        /// 刪除檔案.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        public static void DeleteFile(string FileName)
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
        }
        /// <summary>
        /// 比較檔案內容.
        /// </summary>
        /// <param name="SourceFile1">The source file1.</param>
        /// <param name="SourceFile2">The source file2.</param>
        /// <returns></returns>
        public static bool CompareFile(string SourceFile1, string SourceFile2)
        {

            FileStream fs1 = File.OpenRead(SourceFile1);
            FileStream fs2 = File.OpenRead(SourceFile2);
            //1.檢查文件大小
            if (fs1.Length != fs2.Length)
            {
                //大小相同
                fs2.Dispose();
                fs2.Dispose();
                return false;
            }

            //2.比對內容，逐一找出每一字元，法一
            int FileByte1 = 0;
            int FileByte2 = 0;
            do
            {
                FileByte1 = fs1.ReadByte();
                FileByte2 = fs2.ReadByte();
            }
            //若發現字元不同則離開迴圈且非檔尾
            while ((FileByte1 == FileByte2) && ((FileByte1 != -1) || (FileByte2 != -1)));

            if ((FileByte1 - FileByte2) == 0)
            {
                //return true;
            }
            else
            {
                return false;
            }

            //3.比對內容，逐一找出每一字元，法二
            byte[] Byte1 = File.ReadAllBytes(SourceFile1);
            byte[] Byte2 = File.ReadAllBytes(SourceFile2);
            int i = 0;
            do
            {
                if (Byte1[i] != Byte2[i])
                {
                    return false;
                }
                i++;

            } while (i < Byte1.Length);
            return true;
        }

        //public static void GetFileShortNameFromDir()
        //{
        //    int pos;
        //    string shortName;
        //    Computer MyComputer = new Computer();

        //    foreach (string FlagFileName in MyComputer.FileSystem.GetFiles(
        //    MyComputer.FileSystem.CurrentDirectory))
        //    {
        //        pos = FlagFileName.LastIndexOf(@"") + 1;

        //        // 取得移除前置路徑之後的簡短檔案名稱。
        //        shortName = FlagFileName.Substring(pos);
        //        MessageBox.Show(FlagFileName.Replace(MyComputer.FileSystem.CurrentDirectory, string.Empty).Replace("\\", string.Empty));
        //    }

        //}
        #endregion
    }
}
