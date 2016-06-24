echo on

SET SDKPath=%1
SET Platform=x64
SET VsVersion=120
SET Output=%~dp0..\Extension\Dependency

if "%SDKPath%" == "" SET SDKPath="none"

echo %Output%
call python %~dp0..\tool\Live2dSetup.py %SDKPath% %Platform% %VsVersion% %Output%

pause
