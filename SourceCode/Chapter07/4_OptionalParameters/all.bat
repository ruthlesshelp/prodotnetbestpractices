@ECHO OFF

%MSBuildRoot%\msbuild.exe "all.msbuild" /p:Configuration=Debug
if "%1" NEQ "noprompt" PAUSE
