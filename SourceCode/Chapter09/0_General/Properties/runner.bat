@ECHO OFF

%MSBuildRoot%\msbuild.exe "runner.msbuild" /l:FileLogger,Microsoft.Build.Engine;logfile=output.log
if "%1" NEQ "noprompt" PAUSE
