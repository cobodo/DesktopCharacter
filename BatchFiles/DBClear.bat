echo off

SET DataBase="db.sqlite"

choice /c:yn /m "y=Às n=I—¹"

if %errorlevel% == 1 goto y
if %errorlevel% == 2 goto n

:y
del %~dp0..\DesktopCharacter\bin\Debug\%DataBase%
del %~dp0..\DesktopCharacter\bin\Release\%DataBase% 
echo Database‚ğíœ‚µ‚Ü‚µ‚½
GOTO END

:n
echo I—¹‚µ‚Ü‚·
GOTO END

:END
pause