@ECHO OFF

%MSBuildRoot%\msbuild.exe "test.msbuild" /p:Configuration=Debug
if "%1" NEQ "noprompt" PAUSE
