using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ThermalOperations
{
    public static class ThermalWriter
    {
        public static void Write(string path, ThermalFile file)
        {
            using (StreamWriter writer =
            new System.IO.StreamWriter(@path, true))
                foreach (string line in file.raw)
                    writer.WriteLine(line);
            
        }
    }
}
