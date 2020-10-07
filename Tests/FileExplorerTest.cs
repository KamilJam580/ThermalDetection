using System;
using System.Collections.Generic;
using CoreLib.FileHander;
using FilexExplorer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThermalOperationsTests
{
    [TestClass]
    public class FileExplorerTest
    {
        string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\";

        [TestMethod]
        public void GetFilesInDir()
        {
            IExplorer fileExplorer = new MockExplorer();
            fileExplorer.CurrentPath = path;
            List<IFile> filesName = fileExplorer.getFiles();
            Assert.AreEqual(10, filesName.Count);
        }

        [TestMethod]
        public void GetThremoFilesInDir()
        {
            IExplorer fileExplorer = new MockExplorer();
            fileExplorer.CurrentPath = path;
            Console.WriteLine(fileExplorer.CurrentPath);
            List<IFile> filesName = fileExplorer.getThermoFiles();
            Assert.AreEqual(10, filesName.Count);
        }
        [TestMethod]
        public void GetUpperDirectoryPath()
        {
            IExplorer fileExplorer = new MockExplorer();
            string excepted = @"D:\ThermalDetection\ThermalDetection\ThermalData\";
            fileExplorer.CurrentPath = path;
            string upperDir = fileExplorer.gotoUpperDir();
            Assert.AreEqual(excepted, upperDir);
        }
        [TestMethod]
        public void CountDirsTest()
        {
            IExplorer fileExplorer = new MockExplorer();
            fileExplorer.CurrentPath = path;
            List<string> upperDir = fileExplorer.getDirs();
            Console.WriteLine(upperDir.Count);
            Assert.AreEqual(10, upperDir.Count);
        }
        [TestMethod]
        public void CountDirsTest2()
        {
            IExplorer fileExplorer = new MockExplorer();
            string path = @"D:\ThermalDetection\ThermalDetection\";
            fileExplorer.CurrentPath = path;

            Assert.AreEqual(10, fileExplorer.getDirs().Count);
        }
        [TestMethod]
        public void CountDirsTest3()
        {
            IExplorer fileExplorer = new MockExplorer();
            fileExplorer.CurrentPath = @"D:\ThermalDetection\ThermalDetection\Tests\bin\Debug";
        Assert.AreEqual(fileExplorer.CurrentPath, @"D:\ThermalDetection\ThermalDetection\Tests\bin\Debug");
        }
        [TestMethod]
        public void CountDirsTest4()
        {
            IExplorer fileExplorer = new MockExplorer();
            string path = @"D:\ThermalDetection\ThermalDetection";
            fileExplorer.CurrentPath = path;
            List<string> dirs = fileExplorer.getDirs();
            fileExplorer.CurrentPath = dirs[0];
            Assert.AreEqual(dirs[0], fileExplorer.CurrentPath);
        }
    }
}
