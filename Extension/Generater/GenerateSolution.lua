-- premake5.lua

solution "Generater"
    configurations { "Debug", "Release" }
    platforms { "Any CPU" }
    location ("./")
    startproject "BabumiGraphics"

project "Generater"
    location ("./")
    kind "ConsoleApp"
    language "C#"
    platforms { "Any CPU" }
    framework ("4.5.2")
    targetdir "bin\\$(Configuration)\\"


    files {
         "./**.cs", 
         "./**.xaml", 
         "./**.config",
    }
    
    excludes  { 
        "./obj/**.*",
        "./bin/**.*",
    }
    
    links { 
        "System",
        "System.Data",
        "System.Core",
        "System.Data.DataSetExtensions",
        "System.Net.Http",
        "System.Xaml",
        "System.Xml",
        "System.Xml.Linq",
        "Microsoft.CSharp",
        "../Dependency/CppSharp/CppSharp.AST.dll",
        "../Dependency/CppSharp/CppSharp.dll",
        "../Dependency/CppSharp/CppSharp.Generator.dll",
        "../Dependency/CppSharp/CppSharp.Parser.CLI.dll",
        "../Dependency/CppSharp/CppSharp.Parser.CSharp.dll",
        "../Dependency/CppSharp/CppSharp.Runtime.dll",
    }
    
    postbuildcommands {  
        'copy "$(SolutionDir)..\\Dependency\\CppSharp\\" "$(ProjectDir)bin\\$(Configuration)\\"'
    }
    
    configuration { "Debug*" }
        defines { "DEBUG", "TRACE" }
        flags   { "Symbols" }
        
    configuration { "Release*" }
        defines { "NDEBUG" }
        optimize "On"
        