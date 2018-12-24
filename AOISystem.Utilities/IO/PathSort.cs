using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AOISystem.Utilities.Logging;

namespace AOISystem.Utilities.IO
{
    public class PathSort
    {
        public static string[] Sort(string[] paths, int subFileNameIndex)
        {
            try
            {
                List<PathInfo> pathInfoCollection = new List<PathInfo>();
                foreach (string path in paths)
                {
                    PathInfo pathInfo = new PathInfo();
                    string[] subPaths = path.Split(new string[] { @"\" }, StringSplitOptions.None);
                    string[] subFileNames = subPaths[subPaths.Length - 1].Split(new string[] { @"_" }, StringSplitOptions.None);
                    string subFileName = subFileNames[subFileNameIndex];
                    if (subFileNameIndex == 0)
                    {
                        Match m = Regex.Match(subFileName, @"\d+");
                        while (m.Success)
                        {
                            pathInfo.Path = path;
                            pathInfo.Number = int.Parse(m.Value);
                            m = m.NextMatch();
                        }
                    }
                    if (pathInfo.Path == null)
                    {
                        pathInfo.Path = path;
                        pathInfo.Number = Convert.ToInt32(subFileName, 16);
                    }
                    pathInfoCollection.Add(pathInfo);
                }
                pathInfoCollection.Sort((x, y) => { return x.Number.CompareTo(y.Number); });
                string[] newPaths = new string[paths.Length];
                for (int i = 0; i < newPaths.Length; i++)
                {
                    newPaths[i] = pathInfoCollection[i].Path;
                }
                return newPaths;
            }
            catch(Exception e)
            {
                LogHelper.Exception(e);
                return paths;
            }
        }

        class PathInfo
        {
            public int Number { get; set; }

            public string Path { get; set; }
        }
    }
}
