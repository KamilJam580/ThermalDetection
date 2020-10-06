using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.FileExplorer
{
    public class FileExplorer : IExplorer
    {
        private static string thermoExtension = "*.tof";
        public static List<string> dirs = new List<string>();
        public static List<string> files = new List<string>();
        public static List<string> thermofiles = new List<string>();

        private static string currentPath = Directory.GetCurrentDirectory();

        public string CurrentPath
        {
            get { return currentPath; }
            set 
            {
                if (Directory.Exists(value))
                {
                    currentPath = value;
                    files = getFiles();
                    thermofiles = getThermoFiles();
                    dirs = getDirs();
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public string gotoUpperDir()
        {
            if (Directory.Exists(currentPath))
            {
                DirectoryInfo directoryInfo = Directory.GetParent(currentPath);
                currentPath = directoryInfo.Parent.FullName + "\\";
                return currentPath;
            }
            else
            {
                throw new Exception();
            }
        }

        private List<string> getFiles()
        {
            Console.WriteLine(currentPath);
            if (Directory.Exists(currentPath))
            {
                string[] files2 = Directory.GetFiles(currentPath);
                files = new List<string>(files2);
                return files;
            }
            else
            {
                throw new Exception();
            }
        }
        private List<string> getThermoFiles()
        {
            if (Directory.Exists(currentPath))
            {
                string[] fileslist = Directory.GetFiles(currentPath, thermoExtension);
                thermofiles = new List<string>(fileslist);
                return thermofiles;
            }
            else
            {
                throw new Exception();
            }
        }
        private List<string> getDirs()
        {
            string[] dirs2 = Directory.GetDirectories(currentPath);
            dirs = new List<string>(dirs2);
            return dirs;
        }



    }
}
