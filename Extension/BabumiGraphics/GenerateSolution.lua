-- premake5.lua

solution "BabumiGraphics"
    configurations { "Debug", "Release" }
    platforms { "x64" }
    location ("./")
    startproject "BabumiGraphics"

project "BabumiGraphics"
    location ("./")
    kind "SharedLib"
    language "C#"
    platforms { "x64" }
    framework ("4.5.2")
    removeplatforms { "Any CPU" }
    links {  "../Lib/$(Configuration)/Live2DforDLL.dll" }
    targetdir "..\\Lib\\$(Configuration)\\"
    
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
        "System.Drawing",
        "System.Data.DataSetExtensions",
        "System.Net.Http",
        "System.Xaml",
        "System.Xml",
        "System.Xml.Linq",
        "System.Runtime.Serialization",
        "Microsoft.CSharp",
        "PresentationFramework",
        "../Dependency/SharpGL/$(Configuration)/SharpGL.dll",
        "../Dependency/SharpGL/$(Configuration)/SharpGL.SceneGraph.dll",
        "../Dependency/SharpGL/$(Configuration)/SharpGL.WPF.dll",
    }
    
    configuration { "Debug*" }
        defines { "DEBUG", "TRACE" }
        flags   { "Symbols" }

        
    configuration { "Release*" }
        defines { "NDEBUG" }
        optimize "On"
        
        