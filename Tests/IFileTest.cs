using CoreLib.FileHander;
using FilexExplorer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ThermalOperationsTests
{
    [TestClass]
    public class IFileTest
    {
        public void MakeTempDirsFiles()
        {

            IExplorer fileExplorer = new Explorer();
            string path = @"C:\";
            fileExplorer.CurrentPath = path;

            fileExplorer.CreateNewDir("xxx2");
            fileExplorer.gotoDir("xxx2");

            System.IO.File.WriteAllLines(fileExplorer.CurrentPath + @"\xx.tof", new string[0]);
            System.IO.File.WriteAllLines(fileExplorer.CurrentPath + @"\xx2.tof", new string[0]);
            System.IO.File.WriteAllLines(fileExplorer.CurrentPath + @"\xx3.tof", new string[0]);
            System.IO.File.WriteAllLines(fileExplorer.CurrentPath + @"\xx3.xyz", new string[0]);



            fileExplorer.CreateNewDir("yyy2");
            fileExplorer.CreateNewDir("yyy3");
            fileExplorer.CreateNewDir("yyy4");
            fileExplorer.gotoDir("yyy2");

            fileExplorer.CreateNewDir("zzz2");


            fileExplorer.gotoUpperDir();
            fileExplorer.gotoUpperDir();
            fileExplorer.gotoUpperDir();

        }
        public void DeleteTempDirsFiles()
        {
            string path = @"C:\";
            IExplorer fileExplorer = new Explorer();
            fileExplorer.CurrentPath = path;
            fileExplorer.DeleteDir("xxx2");
        }
        [TestMethod]
        public void CheckPath()
        {
            MakeTempDirsFiles();

            string path = @"C:\xxx2\xx.tof";
            IFile file = new File();
            file.Path = path;

            Assert.AreEqual(@"C:\xxx2\xx.tof", file.Path);


        }
    }
}
