using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Core.FileHander;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThermalOperations;

namespace ThermalOperationsTests
{
    [TestClass]
    public class FileHandlerTest
    {
        int quantity = 10;
        string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.tof";
        int equalsCount = 0;
        int badComprared = 0;

        [TestMethod]
        public void ReadThermalFile()
        {
            // Arrage
            // Act
            ThermalFile.thermalreader = new MockReader();
            ThermalFile thermalFile  = ThermalFile.Read(path);
            // Assert
            int imagesCount = thermalFile.images.Count;
            Assert.AreNotEqual(0, imagesCount);
        }
        [TestMethod]
        public void ReadThermalFileALootAndCheckQuantityInFile()
        {
            // Arrage
            ThermalFile thermalFile;
            ThermalFile.thermalreader = new MockReader();
            int imagecount = 0;
            // Act
            for (int i = 0; i < quantity; i++)
            {
                thermalFile = ThermalFile.Read(path);
                imagecount = thermalFile.images.Count;
            }
            // Assert

            Assert.AreNotEqual(0, imagecount);
        }
        [TestMethod]
        public void ComprareThermalFiles()
        {
            // Arrage
            List<ThermalFile> thermalFiles = new List<ThermalFile>();
            ThermalFile.thermalreader = new MockReader();
            // Act
            for (int i = 0; i < quantity; i++)
            {
                ThermalFile thermalFile = ThermalFile.Read(path);
                thermalFiles.Add(thermalFile);
            }
            equalsCount = CalculateEqualsItems(thermalFiles);
            // Assert
            Assert.AreEqual(quantity, equalsCount);
        }

        private int CalculateEqualsItems(List<ThermalFile> thermalFiles)
        {
            foreach (var thermal in thermalFiles)
                if (thermal == thermalFiles[0])
                    equalsCount++;

            return equalsCount;
        }

        [TestMethod]
        public void EqualsThermalFilesWithBrokenPath()
        {
            // Arrage
            List<ThermalFile> thermalFiles = new List<ThermalFile>();
            ThermalFile.thermalreader = new MockReader();
            // Act
            for (int i = 0; i < quantity; i++)
            {
                ThermalFile thermalFile = ThermalFile.Read(path);
                thermalFiles.Add(thermalFile);
            }
            ThermalFile brokenThermalFile = DeclareBadThermalFile(thermalFiles[0]);
            brokenThermalFile.path = @"D:\ThermalDetection\Theata\tem23.tof";
            thermalFiles.Add(brokenThermalFile);

            equalsCount = CalculateEqualsItems(thermalFiles);
            // Assert
            Assert.AreEqual(thermalFiles.Count, equalsCount);
        }

        [TestMethod]
        public void NotEqualsThermalFilesWithBrokenPath()
        {
            // Arrage
            List<ThermalFile> thermalFiles = new List<ThermalFile>();
            ThermalFile.thermalreader = new MockReader();
            // Act
            for (int i = 0; i < quantity; i++)
            {
                ThermalFile thermalFile = ThermalFile.Read(path);
                thermalFiles.Add(thermalFile);
            }
            ThermalFile brokenThermalFile = DeclareBadThermalFile(thermalFiles[0]);
            brokenThermalFile.path = @"D:\ThermalDetection\Theata\tem23.tof";
            thermalFiles.Add(brokenThermalFile);

            foreach (var thermal in thermalFiles)
                if (thermal != thermalFiles[0])
                    badComprared++;

            // Assert
            Assert.AreEqual(0, badComprared);
        }

        [TestMethod]
        public void ComprareThermalFilesWithBrokenImage()
        {
            // Arrage
            List<ThermalFile> thermalFiles = new List<ThermalFile>();
            ThermalFile.thermalreader = new MockReader();
            // Act
            for (int i = 0; i < quantity; i++)
            {
                ThermalFile thermalFile = ThermalFile.Read(path);
                thermalFiles.Add(thermalFile);
            }
            ThermalFile badFile = GenerateThermalFileWithBrokenImage(thermalFiles[0]);
            thermalFiles.Add(badFile);

            foreach (var thermal in thermalFiles)
                if (thermal == thermalFiles[1])
                    equalsCount++;
            // Assert
            Assert.AreEqual(quantity , equalsCount);
        }

        private ThermalFile DeclareBadThermalFile(ThermalFile thermalFile)
        {
            ThermalFile badThermalFile = new ThermalFile();
            badThermalFile.count = thermalFile.count;
            badThermalFile.minTemperature = thermalFile.minTemperature;
            badThermalFile.maxTemperature = thermalFile.maxTemperature;
            badThermalFile.images = new List<Emgu.CV.UMat>(thermalFile.images);
            badThermalFile.TemperatureData = new List<int[,]>(thermalFile.TemperatureData);
            return badThermalFile;
        }

        private ThermalFile GenerateThermalFileWithBrokenImage(ThermalFile file )
        {
            ThermalFile brokenThermalFile = DeclareBadThermalFile(file);

            List<Emgu.CV.UMat> imgs = new List<Emgu.CV.UMat>();
            Emgu.CV.UMat img = file.images[0];
            imgs.Add(img);
            imgs.Add(img);
            for (int i = 2; i < file.count; i++)
                imgs.Add(file.images[i]);

            brokenThermalFile.images = new List<Emgu.CV.UMat>(imgs);
            return brokenThermalFile;
        }

        [TestMethod]
        public void WriteFileTest()
        {
            // Arrage
            List<ThermalFile> thermalFiles = new List<ThermalFile>();
            ThermalFile.thermalreader = new MockReader();
            // Act
            ThermalFile file = ThermalFile.Read(path);

            string path2 = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp225.tof";
            File.Delete(path2);
            ThermalFile.Write(path2, file);
            ThermalFile file2 = ThermalFile.Read(path2);

            int excepted = 0;
            if (file==file2)
            {
                excepted = 1;
            }
            // Assert
            Assert.AreEqual(1, excepted);
        }

    }

}
