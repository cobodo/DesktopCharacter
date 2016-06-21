using CppSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generater
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleDriver.Run(new Live2DLib());

            File.Copy("Live2DWrap.cpp", Path.GetFullPath("../../../Live2D for DLL/Library/Live2DWrap.cpp"), true);
            File.Copy("Live2DWrap.h", Path.GetFullPath("../../../Live2D for DLL/Library/Live2DWrap.h"), true);
        }
    }
}
