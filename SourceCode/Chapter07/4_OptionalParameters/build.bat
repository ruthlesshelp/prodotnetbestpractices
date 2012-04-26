@ECHO OFF

%MSBuildRoot%\msbuild.exe "build.msbuild" /p:Configuration=Debug
if "%1" NEQ "noprompt" PAUSE
