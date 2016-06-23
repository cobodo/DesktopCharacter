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
    defines { "DEBUG", "TRACE" }
    --dependson {"Live2D for DLL"}
    removeplatforms { "Any CPU" }
        
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
        "../Dependency/SharpGL/SharpGL.dll",
        "../Dependency/SharpGL/SharpGL.SceneGraph.dll",
        "../Dependency/SharpGL/SharpGL.WPF.dll",
    }
    
    configuration { "Debug*" }
        defines { "DEBUG", "TRACE" }
        flags   { "Symbols" }
        links {  "../Lib/Debug/Live2DforDLL.dll" }
        targetdir "..\\Lib\\Debug\\"
        
    configuration { "Release*" }
        optimize "On"
        