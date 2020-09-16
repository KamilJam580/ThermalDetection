using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ThermalOperations
{
    public static class Reader
    {
        static ThermalFile thermalFile;
        public static ThermalFile Read(string filePath)
        {
            if (File.Exists(filePath))
            {
                thermalFile = new ThermalFile(filePath);
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

        static List<string> ReadAllLines()
        {
            return File.ReadLines(@thermalFile.path).ToList<string>();
        }
        
    }
}
