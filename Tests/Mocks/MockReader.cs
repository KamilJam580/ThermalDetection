﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ThermalOperations;

namespace Core.FileHander
{
    public class MockReader : IThermalReader
    {

        public ThermalFile Read(string filePath)
        {
            ThermalFile thermalFile = new ThermalFile();
            thermalFile.path = "somewhere.tof";
            thermalFile.count = 10;
            thermalFile.raw = MakeRawData(10);

            thermalFile.TemperatureData = DataConverting.RawDataToArray(thermalFile.raw, 120, 160);
            thermalFile.images = DataConverting.CreateThermalImages(thermalFile.intMatrices,thermalFile.minTemperature, thermalFile.maxTemperature);
            //thermalFile.count = thermalFile.images.Count;
            return thermalFile;
        }
        private List<string> MakeRawData(int count)
        {
            List<string> raw = new List<string>();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < count; i++)
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
