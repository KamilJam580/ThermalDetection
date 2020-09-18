using System;
using Core.FileExplorer;
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
            string[] filesName = FileExplorer.getFiles(path);
            Assert.AreEqual(3, filesName.Length);
        }
        [TestMethod]
        public void GetFilesInDirDontExist()
        {
            string path = @"D:\ThermalDetection\Thermalion\ThermalData23\";
            Assert.ThrowsException<Exception>(() =>
            FileExplorer.getFiles(path) );
        }
        [TestMethod]
        public void GetThremoFilesInDir()
        {
            string[] filesName = FileExplorer.getThermoFiles(path);
            Assert.AreEqual(2, filesName.Length);
        }
        [TestMethod]
        public void GetUpperDirectoryPath()
        {
            string excepted = @"D:\ThermalDetection\ThermalDetection\";
            string upperDir = FileExplorer.getUpperDir(path);
            Assert.AreEqual(excepted, upperDir);
        }
        [TestMethod]
        public void CountDirsTest()
        {
            string[] upperDir = FileExplorer.getDirs(path);
            Assert.AreEqual(0, upperDir.Length);
        }
        [TestMethod]
        public void CountDirsTest2()
        {
            string path = @"D:\ThermalDetection\ThermalDetection";
            string[] upperDir = FileExplorer.getDirs(path);
            Assert.AreEqual(9, upperDir.Length);
        }

    }
}
