SET MSBUILD="C:/Program Files (x86)/MSBuild/14.0/Bin/MSBuild.exe"
SET NUGET="%~dp0../live2dfordll/.nuget/NuGet.exe"
SET EnableNuGetPackageRestore=true

call %NUGET% restore "%~dp0../Extension/Live2D for DLL.sln"
call %MSBUILD% "%~dp0../Extension/Live2D for DLL.sln" /t:Build /p:Platform=x64 /property:Configuration=Debug

pause
