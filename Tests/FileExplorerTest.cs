using System;
using System.Collections.Generic;
using Core.FileExplorer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThermalOperationsTests
{
    [TestClass]
    public class FileExplorerTest
    {
        string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\";
        FileExplorer fileExplorer = new FileExplorer();
        [TestMethod]
        public void GetFilesInDir()
        {
            fileExplorer.CurrentPath = path;
            List<string> filesName = FileExplorer.files;
            Assert.AreEqual(3, filesName.Count);
        }
        [TestMethod]
        public void GetFilesInDirDontExist()
        {
            string path = @"D:\ThermalDetection\Thermalion\ThermalData23\";
            Assert.ThrowsException<Exception>(() =>
            fileExplorer.CurrentPath = path);

        }
        [TestMethod]
        public void GetThremoFilesInDir()
        {
            fileExplorer.CurrentPath = path;
            Console.WriteLine(fileExplorer.CurrentPath);
            List<string> filesName = FileExplorer.thermofiles;
            Assert.AreEqual(2, filesName.Count);
        }
        [TestMethod]
        public void GetUpperDirectoryPath()
        {
            string excepted = @"D:\ThermalDetection\ThermalDetection\";
            fileExplorer.CurrentPath = path;
            string upperDir = fileExplorer.gotoUpperDir();
            Assert.AreEqual(excepted, upperDir);
        }
        [TestMethod]
        public void CountDirsTest()
        {
            fileExplorer.CurrentPath = path;
            List<string> upperDir = FileExplorer.dirs;
            Assert.AreEqual(0, upperDir.Count);
        }
        [TestMethod]
        public void CountDirsTest2()
        {
            string path = @"D:\ThermalDetection\ThermalDetection";
            fileExplorer.CurrentPath = path;
            List<string> upperDir = FileExplorer.dirs;
            Assert.AreEqual(8, upperDir.Count);
        }
        [TestMethod]
        public void CountDirsTest3()
        {
            fileExplorer.CurrentPath = @"D:\ThermalDetection\ThermalDetection\Tests\bin\Debug";
        Assert.AreEqual(fileExplorer.CurrentPath, @"D:\ThermalDetection\ThermalDetection\Tests\bin\Debug");
        }
        [TestMethod]
        public void CountDirsTest4()
        {
            string path = @"D:\ThermalDetection\ThermalDetection";
            fileExplorer.CurrentPath = path;
            List<string> dirs = FileExplorer.dirs;
            fileExplorer.CurrentPath = dirs[0];
            Assert.AreEqual(@"D:\ThermalDetection\ThermalDetection\.git", fileExplorer.CurrentPath);
        }
    }
}
