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
                }
                else
                {
                    throw new Exception();
                }
            }
        }
        public void gotoDir(string dir)
        {
                string path = CurrentPath + (@"\" + dir);
                CurrentPath = path;
        }

        public string gotoUpperDir()
        {
            string path = CurrentPath;
            try
            {
                int index = path.LastIndexOf(@"\".ToCharArray()[0]);
                string newpath = CurrentPath.Substring(0, index);
                if (Directory.Exists(newpath))
                {
                    if (newpath.EndsWith(":"))
                    {
                        newpath+=@"\";
                    }
                    CurrentPath = newpath;
                }
                return CurrentPath;
            }
            catch (Exception)
            {

                CurrentPath = path;
                return CurrentPath;
            }

        }

        public List<string> getDirs()
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
        public void CreateNewDir(string name)
        {
            string path = CurrentPath + (@"\" + name);
            Directory.CreateDirectory(path);
        }

        public void DeleteDir(string name)
        {
            string path = CurrentPath + (@"\" + name);
            Directory.Delete(path, true);
        }

        public List<IFile> getFiles()
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


        public List<IFile> getThermoFiles()
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


    }
}
