-- premake5.lua

solution "Live2D for DLL"
    configurations { "Debug", "Release" }
    platforms { "x64", "Any CPU" }
    location ("./")

project "Live2DWrapping"
    location ("./Live2DWrapping")
    kind "StaticLib"
    language "C++"
    platforms { "x64" }
    toolset "v120"
    removeplatforms { "Any CPU" }
    targetdir "$(SolutionDir)Lib\\$(Configuration)\\"
    includedirs { 
        "$(ProjectDir)",
        "$(SolutionDir)",
        "$(SolutionDir)Dependency\\Live2D_SDK_OpenGL",
        "$(SolutionDir)Dependency\\libpng\\include",
     }
    
     files {
         "Live2DWrapping/**.cpp", 
         "Live2DWrapping/**.h", 
         "Live2DWrapping/**.config",
     }
    
     excludes  { 
        "Live2DWrapping/obj/**.*",
        "Live2DWrapping/bin/**.*",
     }
     
     defines { "DEBUG", "L2D_TARGET_WIN_GL" }
     
     linkoptions {
         '"live2d_opengl.lib"',
         '/LIBPATH:'..'"$(SolutionDir)Dependency\\Live2D_SDK_OpenGL\\lib\\$(Configuration)"'
     }
     configuration { "Debug" }
        buildoptions { "/MDd" }

project "Generater"
    location ("./Generater")
    kind "ConsoleApp"
    language "C#"
    platforms { "Any CPU" }
    framework ("4.5.2")
    defines { "DEBUG", "TRACE" }
    buildoptions{'"copy "$(SolutionDir)Dependency\\CppSharp\\" "$(ProjectDir)bin\\$(Configuration)\\"'}
    targetdir "$(ProjectDir)bin\\$(Configuration)\\"
    dependson {"Live2DWrapping"}
    
    removeplatforms { "x64" }
    
    files {
         "Generater/**.cs", 
         "Generater/**.xaml", 
         "Generater/**.config",
    }
    
    excludes  { 
        "Generater/obj/**.*",
        "Generater/bin/**.*",
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
        "Dependency/CppSharp/CppSharp.AST.dll",
        "Dependency/CppSharp/CppSharp.dll",
        "Dependency/CppSharp/CppSharp.Generator.dll",
        "Dependency/CppSharp/CppSharp.Parser.CLI.dll",
        "Dependency/CppSharp/CppSharp.Parser.CSharp.dll",
        "Dependency/CppSharp/CppSharp.Runtime.dll",
    }
    
    postbuildcommands {  
        'copy "$(SolutionDir)Dependency\\CppSharp\\" "$(ProjectDir)bin\\$(Configuration)\\"'
    }
    
project "Live2D for DLL"  
    location ("./Live2D for DLL")
    kind "SharedLib"
    language "C++"
    platforms { "x64" }  
    toolset "v120"
    buildoptions { "/clr" }
    targetdir "$(SolutionDir)Lib\\$(Configuration)\\"
    includedirs { "$(ProjectDir)", "$(SolutionDir)" }
    libdirs { '$(SolutionDir)Lib\\$(Configuration)\\' }
    links { "Live2DWrapping.lib", "opengl32.lib" }
    defines { "_WINDLL", "_MBCS" }
    dependson {"Generater"}
    removeplatforms { "Any CPU" }
    clr "On"
    
    prebuildcommands {  
        'cd $(SolutionDir)Generater\\bin\\$(Configuration)',
        'call $(SolutionDir)Generater\\bin\\$(Configuration)\\Generater.exe'
    }

     files {
         "Live2D for DLL/**.cpp", 
         "Live2D for DLL/**.h", 
         "Live2D for DLL/**.config",
     }
    
     excludes  { 
        "Live2DWrapping/obj/**.*",
        "Live2DWrapping/bin/**.*",
     }
     configuration { "Debug" }
        buildoptions { "/MDd" }
        
project "BabumiGraphics"
    location ("./BabumiGraphics")
    kind "SharedLib"
    language "C#"
    platforms { "x64" }
    framework ("4.5.2")
    defines { "DEBUG", "TRACE" }
    dependson {"Live2D for DLL"}
    removeplatforms { "Any CPU" }
        
    files {
         "BabumiGraphics/**.cs", 
         "BabumiGraphics/**.xaml", 
         "BabumiGraphics/**.config",
    }
    
    excludes  { 
        "BabumiGraphics/obj/**.*",
        "BabumiGraphics/bin/**.*",
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
        "Dependency/SharpGL/SharpGL.dll",
        "Dependency/SharpGL/SharpGL.SceneGraph.dll",
        "Dependency/SharpGL/SharpGL.WPF.dll",
    }
    
    configuration { "Debug*" }
        defines { "DEBUG", "TRACE" }
        flags   { "Symbols" }
        links {  "Lib/Debug/Live2D for DLL.dll" }
        targetdir "Lib\\Debug\\"
        
    configuration { "Release*" }
        optimize "On"
        