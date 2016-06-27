SET MSBUILD="C:/Program Files (x86)/MSBuild/14.0/Bin/MSBuild.exe"
SET NUGET="%~dp0../Tool/NuGet.exe"
SET EnableNuGetPackageRestore=true

SET Configuration=%1
if "%Configuration%" == "" SET Configuration="Debug"

call %NUGET% restore "%~dp0../Extension/Live2DWrapping/Live2DWrapping.sln"
call %MSBUILD% "%~dp0../Extension/Live2DWrapping/Live2DWrapping.sln" /t:Build /p:Platform=x64 /property:Configuration=%Configuration%
call %MSBUILD% "%~dp0../Extension/Generater/Generater.sln" /t:Build /p:Platform="Any CPU" /property:Configuration=%Configuration%
call %MSBUILD% "%~dp0../Extension/Live2DforDLL/Live2DforDLL.sln" /t:Build /p:Platform=x64 /property:Configuration=%Configuration%
call %NUGET% restore "%~dp0../Extension/BabumiGraphics/BabumiGraphics.sln"
call %MSBUILD% "%~dp0../Extension/BabumiGraphics/BabumiGraphics.sln" /t:Build /p:Platform=x64 /property:Configuration=%Configuration%

pause
