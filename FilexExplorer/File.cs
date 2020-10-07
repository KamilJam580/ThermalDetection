using CoreLib.FileHander;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilexExplorer
{
    public class File : IFile
    {
        private string path;
        public string Path
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

        public string getName()
        {
            return System.IO.Path.GetFileName(Path);
        }

        public Image getThumbnail()
        {
            Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(path);
            return icon.ToBitmap();
        }

    }
}
