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
        [TestMethod]
        public void ReadThermalFile()
        {
            // Arrage
            string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.tof";
            // Act
            IThermalReader thermalFileReader = new MockReader();
            ThermalFile thermalFile = thermalFileReader.Read(path);
            // Assert
            int imagesCount = thermalFile.images.Count;
            Assert.AreNotEqual(0, imagesCount);
        }
        [TestMethod]
        public void CheckImagesCountInFile()
        {
            // Arrage
            int quantity = 10;
            string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.tof";

            IThermalReader thermalFileReader = new MockReader();
            int imagecount = 0;
            // Act
            for (int i = 0; i < quantity; i++)
            {
                ThermalFile thermalFile = thermalFileReader.Read(path);
                imagecount = thermalFile.images.Count;
            }
            // Assert

            Assert.AreNotEqual(0, imagecount);

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

        private ThermalFile GenerateThermalFileWithBrokenImage(ThermalFile file)
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
        private int CalculateEqualsItems(List<ThermalFile> thermalFiles)
        {
            int equalsCount = 0;
            foreach (var thermal in thermalFiles)
                if (thermal == thermalFiles[0])
                    equalsCount++;

            return equalsCount;
        }

        [TestMethod]
        public void CheckImagesCountWithWrongPath()
        {
            // Arrage
            int quantity = 10;
            int equalsCount = 0;
            string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.tof";

            List<ThermalFile> thermalFiles = new List<ThermalFile>();
            ThermalFile.thermalreader = new MockReader();
            ThermalFile thermalFile = ThermalFile.Read(path);
            // Act
            for (int i = 0; i < quantity; i++)
            {

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
        public void ComprareThermalFiles()
        {
            // Arrage
            int quantity = 10;
            int equalsCount = 0;
            string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.tof";

            List<ThermalFile> thermalFiles = new List<ThermalFile>();
            ThermalFile.thermalreader = new MockReader();
            ThermalFile thermalFile = ThermalFile.Read(path);
            // Act
            for (int i = 0; i < quantity; i++)
            {

                thermalFiles.Add(thermalFile);
            }
            equalsCount = CalculateEqualsItems(thermalFiles);
            // Assert
            Assert.AreEqual(quantity, equalsCount);
        }
        [TestMethod]
        public void NotEqualsThermalFilesWithBrokenPath()
        {
            // Arrage
            int quantity = 10;
            int badComprared = 0;
            string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.tof";

            List<ThermalFile> thermalFiles = new List<ThermalFile>();
            ThermalFile.thermalreader = new MockReader();
            ThermalFile thermalFile = ThermalFile.Read(path);

            // Act
            for (int i = 0; i < quantity; i++)
            {

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
            int quantity = 10;
            int equalsCount = 0;
            string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.tof";

            List<ThermalFile> thermalFiles = new List<ThermalFile>();
            ThermalFile.thermalreader = new MockReader();
            ThermalFile thermalFile = ThermalFile.Read(path);
            // Act
            for (int i = 0; i < quantity; i++)
            {
                thermalFiles.Add(thermalFile);
            }
            ThermalFile badFile = GenerateThermalFileWithBrokenImage(thermalFiles[0]);
            thermalFiles.Add(badFile);

            foreach (var thermal in thermalFiles)
                if (thermal == thermalFiles[1])
                    equalsCount++;
            // Assert
            Assert.AreEqual(quantity, equalsCount);
        }



        [TestMethod]
        public void WriteFileTest()
        {
            // Arrage
            List<ThermalFile> thermalFiles = new List<ThermalFile>();
            ThermalFile.thermalreader = new MockReader();

            string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.tof";
            string path2 = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp225.tof";

            // Act
            ThermalFile file = ThermalFile.Read(path);

            File.Delete(path2);
            ThermalFile.Write(path2, file);
            ThermalFile file2 = ThermalFile.Read(path2);

            int excepted = 1;
            if (file == file2)
            {
                excepted = 1;
            }
            // Assert
            Assert.AreEqual(1, excepted);
        }

    }

}
