using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOISystem.Utilities.IO
{
    public class PathHelper
    {
        //參考路徑範例 D:\COG_AOI_Data\20130725
        public static string[] DeleteOverdueDirectory(string path, string dateTimeFormate, int saveDays, string[] exceptionDirectory)
        {
            string[] directories = null;
            List<string> deletedDirectories = new List<string>();
            List<string> saveDirectories = new List<string>();
            if (!Directory.Exists(path))
            {
                return deletedDirectories.ToArray();
            }
            directories = Directory.GetDirectories(path);
            List<string> directoryList = directories.ToList();
            for (int i = 0; i < saveDays; i++)
            {
                string saveDayString = DateTime.Now.AddDays(i * -1).ToString("yyyyMMdd");
                int count = directoryList.Count;
                for (int j = count - 1; j >= 0; j--)
                {
                    string dateString = directoryList[j].Split('\\').Last();
                    if (dateString.Contains(saveDayString))
                    {
                        saveDirectories.Add(directoryList[j]);
                        directoryList.RemoveAt(j);
                    }
                }
            }
            for (int i = 0; i < exceptionDirectory.Length; i++)
            {
                exceptionDirectory[i] = path + "\\" + exceptionDirectory[i];
            }
            saveDirectories.AddRange(exceptionDirectory);

            foreach (string directory in directories)
            {
                if (!saveDirectories.Contains(directory))
                {
                    DirectoryInfo di = new DirectoryInfo(directory);
                    di.Delete(true);
                    deletedDirectories.Add(directory);
                }
            }
            return deletedDirectories.ToArray();
        }

        public static string[] DeleteOverdueDirectory(string path, string dateTimeFormate, int saveDays, double availableDISKMinFreeSpace, string[] exceptionDirectory)
        {
            List<string> deletedDirectories = new List<string>();
            deletedDirectories.AddRange(DeleteOverdueDirectory(path, dateTimeFormate, saveDays, exceptionDirectory));
            if (saveDays > 0 && availableDISKMinFreeSpace != 0)
            {
                double availableFreeSpace = DriveHelper.GetAvailableFreeSpace(path);
                if (availableFreeSpace < availableDISKMinFreeSpace)
                {
                    deletedDirectories.AddRange(DeleteOverdueDirectory(path, dateTimeFormate, saveDays - 1, exceptionDirectory));
                }
            }
            return deletedDirectories.ToArray();
        }

        /// <summary>
        /// 修正檔案名稱副檔名
        /// </summary>
        /// <param name="fullName">完整路徑</param>
        /// <param name="extension">修正附檔名(.csv)</param>
        public static void FixFileNameExtension(ref string fullName, string extension)
        {
            fullName = Path.GetFullPath(fullName);
            var path = Path.GetDirectoryName(fullName);
            if (path != null && !Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!String.Equals(Path.GetExtension(fullName), extension))
                fullName += extension;
        }
    }
}
