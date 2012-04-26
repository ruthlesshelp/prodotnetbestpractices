@ECHO OFF

%MSBuildRoot%\msbuild.exe "runner.msbuild" /t:DateAndTime
if "%1" NEQ "noprompt" PAUSE
