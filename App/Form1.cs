
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

using GuiElements;
using FilexExplorer;
using ThermalOperations;
using CoreLib.FileHander;

namespace App
{
    public partial class Form1 : Form
    {
        ThermalFile thermalFile;
        string path = @"D:\ThermalDetection\ThermalDetection\ThermalData\temp223.tof";

        Explorer filexExplorer;
        StringFormat drawFormat = new StringFormat();
        ItemPanel guiElements = new ItemPanel();
        public Form1()
        {
            InitializeComponent();
            loadThermalFile();

            drawFormat.FormatFlags = StringFormatFlags.LineLimit;

            filexExplorer = new Explorer();
            filexExplorer.CurrentPath = @"D:\Dysk USB\2017";

            guiElements.SetFolderClickDelegate(FolderItemClicked);
            guiElements.SetFileClickDelegate(FileItemClicked);

            CreateUpperDirButton();
            DrawTable();
        }
        private void loadThermalFile()
        {
            thermalFile = ThermalFile.Read(path);
            imageBox1.Image = thermalFile.images[0];
            trackBar1.Maximum = thermalFile.count-1;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Images: " + thermalFile.count);
            Console.WriteLine("min: " + thermalFile.minTemperature);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Trace.WriteLine("val: " + trackBar1.Value);
            imageBox1.Image = thermalFile.images[trackBar1.Value];
        }
        private void DrawTable()
        {
            tableLayoutPanel1.Controls.Clear() ;
            FlowLayoutPanel titlePanel = AddressBar.CreateTitleBar(filexExplorer.CurrentPath);

            List<string> dirs = filexExplorer.getDirs();
            List<IFile> files = filexExplorer.getFiles();
            List<IFile> thermofiles = filexExplorer.getThermoFiles();
            files.AddRange(thermofiles);

            FlowLayoutPanel flowLayoutPanel = guiElements.CreatePanelWithItems(dirs, files);

            tableLayoutPanel1.Controls.Add(titlePanel, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel, 0, 1);

            ConfigureTablePanel();
        }
        private void CreateUpperDirButton()
        {
            button1.Text = char.ConvertFromUtf32(0x2191);
            button1.Click += (sender, EventArgs) =>
            {
                filexExplorer.gotoUpperDir();
                DrawTable();
            };
        }
        void FolderItemClicked(object sender, EventArgs e, string name)
        {
            filexExplorer.gotoDir(name);
            DrawTable();
        }
        private void FileItemClicked(object sender, EventArgs e, IFile file)
        {
            Console.WriteLine("event: " + file.getName());
        }
        private void ConfigureTablePanel()
        {
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            tableLayoutPanel1.RowStyles[0].Height = 5;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.AutoScroll = true;
        }
    }
}
