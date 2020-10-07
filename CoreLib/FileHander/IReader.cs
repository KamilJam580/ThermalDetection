using System;
using System.Collections.Generic;
using System.Text;
using ThermalOperations;

namespace Core.FileHander
{
     public interface IReader
     {
        ThermalFile Read(string filePath);
    }
}
