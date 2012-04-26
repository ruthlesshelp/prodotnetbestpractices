@ECHO OFF

%MSBuildRoot%\msbuild.exe "runner.msbuild" /t:Rebuild /p:Configuration=Debug
if "%1" NEQ "noprompt" PAUSE
