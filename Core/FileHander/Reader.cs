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
            List<string> allData = File.ReadLines(@thermalFile.path).ToList<string>();
            thermalFile.width = int.Parse(allData[0]);
            thermalFile.height = int.Parse(allData[1]);
            return allData.GetRange(2,allData.Count-2);
        }
        
    }
}
