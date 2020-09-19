using Core.FileHander;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ThermalOperations
{
    public class Reader : IReader
    {
        ThermalFile thermalFile;
        public ThermalFile Read(string filePath)
        {
            if (File.Exists(filePath))
            {
                thermalFile = new ThermalFile();
                thermalFile.path = filePath;
                Trace.WriteLine("File: " + thermalFile.path);
                thermalFile.raw = ReadAllLines();
                thermalFile.temperatureData = DataConverting.RawDataToArray(thermalFile.raw);
                thermalFile = DataConverting.CreateThermalImages(thermalFile);
                return thermalFile;
            }
            else
            {
                Trace.WriteLine("File dont exist");
                return null;
            }     
        }

         List<string> ReadAllLines()
        {
            return File.ReadLines(@thermalFile.path).ToList<string>();
        }
        
    }
}
