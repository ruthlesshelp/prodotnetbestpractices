@ECHO OFF

%MSBuildRoot%\msbuild.exe "runner.msbuild" /t:Rebuild /p:Configuration=Debug;BUILD_NUMBER=1.3.5.7
if "%1" NEQ "noprompt" PAUSE
