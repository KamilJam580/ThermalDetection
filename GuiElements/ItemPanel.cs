using CoreLib.FileHander;
using FilexExplorer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiElements
{
    public class ItemPanel
    {
        Font font = new Font("Arial", 8, FontStyle.Regular);
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        Image folderImageI = Properties.Resources.folderIcon.GetThumbnailImage(60, 60, null, IntPtr.Zero);

        public FlowLayoutPanel CreatePanelWithItems(List<string> dirs, List<IFile> files)
        {
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            var foldersP = getFoldersPanels(dirs);
            var filesP = getFilesPanels(files);

            List<Panel> dirsPanels = new List<Panel>();
            dirsPanels.AddRange(foldersP);
            dirsPanels.AddRange(filesP);

            foreach (var item in dirsPanels)
            {
                flowLayoutPanel.Controls.Add(item);
            }
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.Width = 900;
            flowLayoutPanel.Height = 500;
            flowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            return flowLayoutPanel;
        }

        public Action<object, EventArgs, string> FolderClickCallback;
        public Action<object, EventArgs, IFile> FoleClickCallback;
        public void setFolderClickDelegate(Action<object, EventArgs, string> folderClickCallback)
        {
            FolderClickCallback = folderClickCallback;
        }
        public void setFileClickDelegate(Action<object, EventArgs, IFile> fileClickCallback)
        {
            FoleClickCallback  = fileClickCallback;
        }
        public List<Panel> getFoldersPanels(List<string> dirs)
        {
            List<Panel> panels = new List<Panel>();
            foreach (var name in dirs)
            {
                Panel drawPanel = DrawPanel(name, folderImageI);
                drawPanel.Click += (sender, EventArgs) =>
                {
                    FolderClickCallback.Invoke(sender, EventArgs, name);
                };
                ResizeAndSetPadding(drawPanel);
                panels.Add(drawPanel);
            }
            return panels;
        }

        private List<Panel> getFilesPanels(List<IFile> files)
        {
            List<Panel> panels = new List<Panel>();
            foreach (var file in files)
            {
                Image itemImage = file.getThumbnail();
                Bitmap itemBitmap = new Bitmap(itemImage, 60, 60);

                Panel drawPanel = DrawPanel(file.getName(), itemBitmap);
                drawPanel.Click += (sender, EventArgs) =>
                {
                    FoleClickCallback.Invoke(sender, EventArgs, file);
                };
                ResizeAndSetPadding(drawPanel);
                panels.Add(drawPanel);
            }
            return panels;
        }

        private static void ResizeAndSetPadding(Panel drawPanel)
        {
            drawPanel.Size = new Size(62, 125);
            drawPanel.Padding = new Padding(0, 0, 0, 0);
            drawPanel.Margin = new Padding(15, 10, 0, 0);
            drawPanel.BorderStyle = BorderStyle.None;
        }

        private Panel DrawPanel(string name, Image image)
        {
            Panel drawPanel = new Panel();
            drawPanel.Paint += (sender, e) =>
            {
                e.Graphics.DrawImageUnscaled(image, 0, 0);
                RectangleF rectF1 = new RectangleF(0, 62, 60, 70);
                e.Graphics.DrawString(name, font, drawBrush, rectF1);
            };
            return drawPanel;
        }






    }
}
