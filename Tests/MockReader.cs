using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ThermalOperations;

namespace Core.FileHander
{
    public class MockReader : IReader
    {
        ThermalFile thermalFile;
        public ThermalFile Read(string filePath)
        {
            thermalFile = new ThermalFile();
            thermalFile.path = "somewhere.tof";
            thermalFile.count = 10;
            thermalFile.raw = MakeRawData();

            thermalFile.temperatureData = DataConverting.RawDataToArray(thermalFile.raw, 120, 160);
            thermalFile = DataConverting.CreateThermalImages(thermalFile);
            return thermalFile;
        }
        private List<string> MakeRawData()
        {
            List<string> raw = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < thermalFile.count; i++)
            {
                for (int x = 0; x < 120; x++)
                {
                    for (int y = 0; y < 160; y++)
                    {
                        stringBuilder.Append(i.ToString()+ " ");
                    }
                }

                raw.Add(stringBuilder.ToString());
                stringBuilder.Clear();
            }
            return raw;
        }


    }
}
