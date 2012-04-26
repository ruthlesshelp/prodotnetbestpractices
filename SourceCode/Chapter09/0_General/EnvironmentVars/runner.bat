@ECHO OFF

%MSBuildRoot%\msbuild.exe "runner.msbuild"
if "%1" NEQ "noprompt" PAUSE
