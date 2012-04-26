@ECHO OFF

%MSBuildRoot%\msbuild.exe "runner.msbuild" /t:Rebuild;Test /p:Configuration=Debug
if "%1" NEQ "noprompt" PAUSE
