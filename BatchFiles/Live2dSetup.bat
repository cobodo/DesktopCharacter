echo on

SET SDKPath=F:\Live2D_SDK_OpenGL_2.0.08_2_jp
SET Platform=x64
SET VsVersion=120
SET Output=%~dp0..\live2dfordll\Dependency

echo %Output%
call python ..\tool\Live2dSetup.py %SDKPath% %Platform% %VsVersion% %Output%

pause