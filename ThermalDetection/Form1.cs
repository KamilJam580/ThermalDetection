using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThermalOperations;

namespace ThermalDetection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            ThermalFileHandler thermalFileReader = new ThermalFileHandler();
            string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.txt";
            List<Emgu.CV.UMat> images = thermalFileReader.Read(path);
            InitializeComponent();

        }

    }
}
