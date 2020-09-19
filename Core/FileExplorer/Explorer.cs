using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.FileExplorer
{
    public static class FileExplorer
    {
        private static string thermoExtension = "*.tof";
        private static string currentDir;
        public static string[] getFiles(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                return files;
            }
            else
            {
                throw new Exception();
            }
        }
        public static string[] getThermoFiles(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path, thermoExtension);
                return files;
            }
            else
            {
                throw new Exception();
            }
        }
        public static string getUpperDir(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = Directory.GetParent(path);
                return directoryInfo.Parent.FullName + "\\";
            }
            else
            {
                throw new Exception();
            }
        }
        public static string[] getDirs(string path)
        {
            string[] dirs = Directory.GetDirectories(path);
            
            return dirs;
        }


    }
}
