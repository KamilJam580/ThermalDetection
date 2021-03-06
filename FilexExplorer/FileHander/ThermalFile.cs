﻿using Core.FileHander;
using CoreLib.FileHander;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThermalOperations
{
    public class ThermalFile : FilexExplorer.File
    {
        public double minTemperature;
        public double maxTemperature;
        public string path;
        public List<string> raw;
        public List<Emgu.CV.UMat> images;
        public int count;
        public static IThermalReader thermalreader = new ThermalReader();
        public int height;
        public int width;
        public List<Emgu.CV.Matrix<int>> intMatrices;
        private List<int[,]> temperatureData;

        public event PropertyChangedEventHandler PropertyChanged;

        public static ThermalFile Read(string filePath)
        {
            ThermalFile thermalFile;
            thermalFile = thermalreader.Read(filePath);
            return thermalFile;
        }
        public static void Write(string path, ThermalFile file)
        {
            ThermalWriter.Write(path, file);
        }

        public List<int[,]> TemperatureData
        {
            get { return temperatureData; }
            set 
            {
                temperatureData = value;
                intMatrices = DataConverting.ScaleIntensity(temperatureData, out minTemperature, out maxTemperature);
            }
        }

        public static bool operator == (ThermalFile file1, ThermalFile file2)
        {
            if (file1.count != file2.count)
                return false;

            if (file1.minTemperature != file2.minTemperature)
                return false;

            if (file1.maxTemperature != file2.maxTemperature)
                return false;

            for (int i = 0; i < file1.count; i++)
                if (!file1.images[i].Equals(file2.images[i]))
                    return false;
            return true;
        }
        public static bool operator != (ThermalFile file1, ThermalFile file2)
        {
            Boolean bcount = false;
            if (file1.count == file2.count)
                bcount = true;
            Boolean bminTemperature = false;
            if (file1.minTemperature == file2.minTemperature)
                bminTemperature = true;
            Boolean bmaxTemperature = false;
            if (file1.maxTemperature == file2.maxTemperature)
                bmaxTemperature = true;
            Boolean bimages = false;
            for (int i = 0; i < file1.count; i++)
                if (file1.images[i].Equals(file2.images[i]))
                    bimages = true;
            if (!bcount || !bminTemperature || !bmaxTemperature || !bimages)
            {
                return true;
            }
            return false ;
        }


    }
}
