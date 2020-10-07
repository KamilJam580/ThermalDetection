﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.FileHander
{
    public interface IFile
    {
        string Path
        {
            get;
            set;
        }
        string getName();
        Image getThumbnail();
    }
}