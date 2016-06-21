echo on

SET SDKPath='F:\Live2D_SDK_OpenGL_2.0.08_2_jp'
SET Platform='x64'
SET VsVersion='120'
SET Output='%~dp0..\Live2D for DLL\Dependency\'

echo %Output%
cd Impl
call Live2dSetup.exe %SDKPath% %Platform% %VsVersion% %Output%

pause