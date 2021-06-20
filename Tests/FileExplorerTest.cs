using System;
using System.Collections.Generic;
using System.IO;
using CoreLib.FileHander;
using FilexExplorer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThermalOperationsTests
{
    [TestClass]
    public class FileExplorerTest
    {
        public void MakeTempDirsFiles()
        {

            IExplorer fileExplorer = new Explorer();
            string path = @"C:\";
            fileExplorer.CurrentPath = path;

            fileExplorer.CreateNewDir("xxx2");
            fileExplorer.gotoDir("xxx2");

            System.IO.File.WriteAllLines(fileExplorer.CurrentPath+@"\xx.tof", new string[0]);
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
        public void CheckSettingDirs()
        {
            MakeTempDirsFiles();

            IExplorer fileExplorer = new Explorer();
            string path = @"C:\xxx2";
            fileExplorer.CurrentPath = path;

            Assert.AreEqual(@"C:\xxx2", fileExplorer.CurrentPath);
            DeleteTempDirsFiles();
        }

        [TestMethod]
        public void GoToEachDirTest()
        {
            MakeTempDirsFiles();

            IExplorer fileExplorer = new Explorer();
            string path = @"C:\xxx2";
            fileExplorer.CurrentPath = path;
            List<string> dirs = fileExplorer.getDirs();
            foreach (var dir in dirs)
            {
                fileExplorer.gotoDir(dir);
                fileExplorer.gotoUpperDir();
            }
            DeleteTempDirsFiles();
        }
        [TestMethod]
        public void GoUpTooMuch()
        {
            MakeTempDirsFiles();

            IExplorer fileExplorer = new Explorer();
            string path = @"C:\xxx2";
            fileExplorer.CurrentPath = path;
            fileExplorer.gotoUpperDir();
            fileExplorer.gotoUpperDir();
            fileExplorer.gotoUpperDir();
            fileExplorer.gotoUpperDir();
            Console.WriteLine(fileExplorer.CurrentPath);
            DeleteTempDirsFiles();
        }
        [TestMethod]
        public void CheckFilesQuantity()
        {
            MakeTempDirsFiles();

            IExplorer fileExplorer = new Explorer();
            string path = @"C:\xxx2";
            fileExplorer.CurrentPath = path;
            List<IFile> files = fileExplorer.getFiles();
            Assert.AreEqual(4, files.Count);

            DeleteTempDirsFiles();
        }

        [TestMethod]
        public void CheckFilesName()
        {
            MakeTempDirsFiles();

            IExplorer fileExplorer = new Explorer();
            string path = @"C:\xxx2";
            fileExplorer.CurrentPath = path;
            List<IFile> files = fileExplorer.getFiles();
            Assert.AreEqual("xx.tof", files[0].getName());

            DeleteTempDirsFiles();
        }

        [TestMethod]
        public void SearchTermoFilesTest()
        {
            MakeTempDirsFiles();

            IExplorer fileExplorer = new Explorer();
            string path = @"C:\xxx2";
            fileExplorer.CurrentPath = path;
            List<IFile> files = fileExplorer.getThermoFiles();
            Assert.AreEqual(3, files.Count);

            DeleteTempDirsFiles();
        }



    }
}
