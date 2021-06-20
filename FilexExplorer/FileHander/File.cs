using CoreLib.FileHander;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilexExplorer
{
    public class File : IFile
    {
        private string path;
        public virtual string Path
        {
            get
            {
                return path;
            }
            set
            {
                Console.WriteLine(value);
                if (System.IO.File.Exists(value))
                    path = value;
                else
                    throw new Exception();
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
