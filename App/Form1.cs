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

namespace App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.txt";
            ThermalFile thermalFile = ThermalFile.Read(path);
            imageBox1.Image = thermalFile.images[0];
            InitializeComponent();
        }
    }
}
