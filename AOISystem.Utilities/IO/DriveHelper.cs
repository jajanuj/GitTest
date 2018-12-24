using System.IO;

namespace AOISystem.Utilities.IO
{
    public class DriveHelper
    {
        /// <summary>
        /// 得到磁碟上的可用空間量(GB)
        /// </summary>
        /// <param name="driveName"></param>
        /// <returns></returns>
        public static double GetAvailableFreeSpace(string driveName)
        {
            driveName = Path.GetPathRoot(driveName);
            DriveInfo driveInfo = new DriveInfo(driveName);
            return (double)driveInfo.AvailableFreeSpace / 1024 / 1024 / 1024;
        }

        /// <summary>
        /// 得到磁碟上已使用空間比例(%)
        /// </summary>
        /// <param name="driveName"></param>
        /// <returns></returns>
        public static double GetUsedSpaceRatio(string driveName)
        {
            driveName = Path.GetPathRoot(driveName);
            DriveInfo driveInfo = new DriveInfo(driveName);
            return (1 - (double)driveInfo.AvailableFreeSpace / driveInfo.TotalSize) * 100;
        }
    }
}
