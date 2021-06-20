using CoreLib.FileHander;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilexExplorer
{
    public interface IExplorer
    {
        string gotoUpperDir();
        void gotoDir(string dir);
        string CurrentPath
        {
            get;
            set;
        }
        List<string> getDirs();
        List<IFile> getThermoFiles();
        List<IFile> getFiles();
        void CreateNewDir(string name);
        void DeleteDir(string name);
    }
}
