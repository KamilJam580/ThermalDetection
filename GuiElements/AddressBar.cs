using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuiElements
{
    public static class AddressBar
    {
        public static FlowLayoutPanel CreateTitleBar(string path)
        {
            Image folderImageI = Properties.Resources.folderIcon.GetThumbnailImage(20, 20, null, IntPtr.Zero);
            TextBox textBox = new TextBox();
            textBox.Text = path;
            textBox.MinimumSize = new Size(900, 20);
            textBox.BorderStyle = BorderStyle.None;
            textBox.Margin = new Padding(25, 0, 0, 0);
            textBox.Font = new Font("Arial", 10, FontStyle.Regular);
            FlowLayoutPanel titlePanel = new FlowLayoutPanel();

            titlePanel.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                g.DrawImageUnscaled(folderImageI, 0, 0);
            };

            titlePanel.Controls.Add(textBox);
            titlePanel.FlowDirection = FlowDirection.LeftToRight;
            titlePanel.BorderStyle = BorderStyle.FixedSingle;
            titlePanel.Size = new Size(900, 25);
            return titlePanel;
        }
    }
}
