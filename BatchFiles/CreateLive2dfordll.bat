SET MSBUILD="C:/Program Files (x86)/MSBuild/14.0/Bin/MSBuild.exe"
SET NUGET="%~dp0../Tool/NuGet.exe"
SET EnableNuGetPackageRestore=true

call %NUGET% restore "%~dp0../Extension/Live2DWrapping/Live2DWrapping.sln"
call %MSBUILD% "%~dp0../Extension/Live2DWrapping/Live2DWrapping.sln" /t:Build /p:Platform=x64 /property:Configuration=Debug

call %MSBUILD% "%~dp0../Extension/Generater/Generater.sln" /t:Build /p:Platform="Any CPU" /property:Configuration=Debug

call %MSBUILD% "%~dp0../Extension/Live2DforDLL/Live2DforDLL.sln" /t:Build /p:Platform=x64 /property:Configuration=Debug

call %NUGET% restore "%~dp0../Extension/BabumiGraphics/BabumiGraphics.sln"
call %MSBUILD% "%~dp0../Extension/BabumiGraphics/BabumiGraphics.sln" /t:Build /p:Platform=x64 /property:Configuration=Debug

pause
