@ECHO OFF

%MSBuildRoot%\msbuild.exe "runner.msbuild" /l:FileLogger,Microsoft.Build.Engine;logfile=runner.log /t:Rebuild;Test /p:Configuration=Debug
if "%1" NEQ "noprompt" PAUSE
