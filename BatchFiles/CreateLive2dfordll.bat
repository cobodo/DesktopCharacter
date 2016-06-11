SET MSBUILD="C:/Program Files (x86)/MSBuild/14.0/Bin/MSBuild.exe"
SET EnableNuGetPackageRestore=true

call %MSBUILD% "%~dp0../live2dfordll/Live2D for DLL.sln" /t:Build /p:Platform=x64 /property:Configuration=Debug
