-- premake5.lua

solution "Live2DWrapping"
    configurations { "Debug", "Release" }
    platforms { "x64" }
    location ("./")
    startproject "BabumiGraphics"

project "Live2DWrapping"
    location ("./")
    kind "StaticLib"
    language "C++"
    platforms { "x64" }
    toolset "v120"
    removeplatforms { "Any CPU" }
    targetdir "$(SolutionDir)..\\Lib\\$(Configuration)\\"
    includedirs { 
        "$(ProjectDir)",
        "$(SolutionDir)",
        "$(SolutionDir)..\\Dependency\\Live2D_SDK_OpenGL",
        "$(SolutionDir)..\\Dependency\\libpng\\include",
     }
    
     files {
         "./**.cpp", 
         "./**.h", 
         "./**.config",
     }
    
     excludes  { 
        "./obj/**.*",
        "./bin/**.*",
     }
     
     defines { "DEBUG", "L2D_TARGET_WIN_GL" }
     
     linkoptions {
         '"live2d_opengl.lib"',
         '/LIBPATH:'..'"$(SolutionDir)..\\Dependency\\Live2D_SDK_OpenGL\\lib\\$(Configuration)"'
     }
    
    configuration { "Debug*" }
        defines { "DEBUG", "TRACE" }
        flags   { "Symbols" }
        buildoptions { "/MDd" }
        
    configuration { "Release*" }
        defines { "NDEBUG" }
        buildoptions { "/MD" }
        optimize "On"
