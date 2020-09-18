using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThermalOperations;

namespace App
{
    public partial class Form1 : Form
    {
        ThermalFile thermalFile;
        string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.tof";
        public Form1()
        {
            InitializeComponent();
            loadThermalFile();
        }
        private void loadThermalFile()
        {
            thermalFile = ThermalFile.Read(path);
            imageBox1.Image = thermalFile.images[0];
            trackBar1.Maximum = thermalFile.count-1;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Trace.WriteLine("val: " + trackBar1.Value);
            imageBox1.Image = thermalFile.images[trackBar1.Value];
        }
    }
}
