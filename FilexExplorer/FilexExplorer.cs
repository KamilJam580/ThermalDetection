using CoreLib.FileHander;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermalOperations;

namespace FilexExplorer
{
    public class Explorer : IExplorer
    {
        private static string thermoExtension = "*.tof";
        public List<string> dirs = new List<string>();
        public List<IFile> files = new List<IFile>();
        public List<IFile> thermofiles = new List<IFile>();

        private static string currentPath;

        public Explorer()
        {
            CurrentPath = Directory.GetCurrentDirectory();
        }
        public string CurrentPath
        {
            get { return currentPath; }
            set
            {
                if (Directory.Exists(value))
                {
                    currentPath = value;
                    files = searchFiles();
                    thermofiles = searchThermoFiles();
                    dirs = searchDirs();
                }
                else
                {
                    throw new Exception();
                }
            }
        }


        public string gotoUpperDir()
        {
            if (Directory.Exists(CurrentPath))
            {
                string path = CurrentPath;
                int index = path.LastIndexOf(@"\".ToCharArray()[0]);
                path = CurrentPath.Substring(0, index);
                CurrentPath = path;
                return CurrentPath;
            }
            else
            {
                throw new Exception();
            }
        }

        public string gotoDir(string dir)
        {
            if (dirs.Contains(dir))
            {
                CurrentPath += (@"\" + dir);
                return CurrentPath;
            }
            else
                throw new Exception();
        }

        private List<IFile> searchFiles()
        {
            if (Directory.Exists(currentPath))
            {
                string[] filesNames = Directory.GetFiles(currentPath);

                files = new List<IFile>();
                foreach (var fileName in filesNames)
                {
                    IFile file = new File();
                    file.Path = fileName;
                    files.Add(file);
                }
                return files;
            }
            else
            {
                throw new Exception();
            }
        }
        private List<IFile> searchThermoFiles()
        {
            if (Directory.Exists(currentPath))
            {
                string[] fileslist = Directory.GetFiles(currentPath, thermoExtension);
                foreach (var filename in fileslist)
                {
                    IFile file = new ThermalFile();
                    file.Path = filename;
                    thermofiles.Add(file);
                }

                return thermofiles;
            }
            else
            {
                throw new Exception();
            }
        }
        private List<string> searchDirs()
        {
            string[] dirs2 = Directory.GetDirectories(currentPath);

            List<string> FolderNames = new List<string>();
            foreach (var item in dirs2)
            {
                string FolderName = new DirectoryInfo(item).Name;
                FolderNames.Add(FolderName);
            }
            return FolderNames;
        }

        public List<string> getDirs()
        {
            return dirs;
        }

        public List<IFile> getThermoFiles()
        {
            return thermofiles;
        }

        public List<IFile> getFiles()
        {
            return files;
        }
    }
}
