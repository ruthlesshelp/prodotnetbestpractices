@ECHO OFF

%MSBuildRoot%\msbuild.exe "runner.msbuild" /t:Rebuild;Analyze /p:Configuration=Debug
if "%1" NEQ "noprompt" PAUSE
