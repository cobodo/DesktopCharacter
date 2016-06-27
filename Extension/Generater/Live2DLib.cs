using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;
using System.IO;

namespace Generater
{
    class Live2DLib : ILibrary
    {
        public void Postprocess(Driver driver, ASTContext ctx)
        {

        }

        public void Preprocess(Driver driver, ASTContext ctx)
        {

        }

        public void Setup(Driver driver)
        {
            var options = driver.Options;
            options.GeneratorKind = GeneratorKind.CLI;
            options.LibraryName = "Live2DWrap";
            options.Headers.Add("Live2DWrap.h");
            options.Libraries.Add("Live2DWrapping.lib");
            options.addIncludeDirs("../../../Live2DWrapping");
#if (DEBUG)
            options.addLibraryDirs("../../../Lib/Debug");
#elif (NDEBUG)
            options.addLibraryDirs("../../../Lib/Release");
#endif
        }

        public void SetupPasses(Driver driver)
        {

        }
    }
}
