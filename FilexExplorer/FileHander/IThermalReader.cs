using System;
using System.Collections.Generic;
using System.Text;
using ThermalOperations;

namespace Core.FileHander
{
     public interface IThermalReader
     {
        ThermalFile Read(string filePath);
    }
}
