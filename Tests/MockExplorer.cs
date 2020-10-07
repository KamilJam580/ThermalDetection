
using CoreLib.FileHander;
using FilexExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermalOperations;

namespace ThermalOperationsTests
{
    class MockExplorer : IExplorer
    {
        private static string thermoExtension = "*.tof";
        private List<string> dirs = new List<string>();
        private List<IFile> files = new List<IFile>();
        private List<IFile> thermofiles = new List<IFile>();

        private string currentPath = @"C:\\program\data\";
        public string CurrentPath 
        {
            get { return currentPath; }
            set 
            {
                    currentPath = value;
                    dirs = searchDirs();
                     files = searchFiles();
                    thermofiles = searchThermoFiles();
                    
            }
        }

        public List<string> searchDirs()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                list.Add("dir" + i.ToString());
            }
            return list;
        }

        public List<IFile> searchThermoFiles()
        {
            List<IFile> list = new List<IFile>();
            for (int i = 0; i < 10; i++)
            {
                ThermalFile thermalFile = new ThermalFile();
                thermalFile.Path = "thermo" + i.ToString() + ".tof";
                list.Add(thermalFile);
            }
            return list;
        }

        public List<IFile> searchFiles()
        {
            List<IFile> list = new List<IFile>();
            for (int i = 0; i < 10; i++)
            {
                IFile file = new File();
                string name = "file" + i.ToString() + ".xc";
                file.Path = name;
                list.Add(file);
            }
            return list;
        }

        public string gotoDir(string dir)
        {
            if (dirs.Contains(dir))
            {
                return CurrentPath;
            }
            else
                throw new Exception();
        }

        public string gotoUpperDir()
        {
            CurrentPath = CurrentPath;
            return CurrentPath;
        }
        public List<string> getDirs()
        {
            return dirs;
        }

        public List<IFile> getThermoFiles()
        {
            return thermofiles;
        }



        List<IFile> IExplorer.getFiles()
        {
            return files;
        }

    }
}
