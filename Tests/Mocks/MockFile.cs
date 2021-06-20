using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermalOperations;

namespace ThermalOperationsTests.Mocks
{
    class MockFile : ThermalFile
    {
        public override string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }
    }
}
