@ECHO OFF

%MSBuildRoot%\msbuild.exe "build.msbuild" /l:FileLogger,Microsoft.Build.Engine;logfile=build.log /t:Rebuild /p:Configuration=Debug

%MSBuildRoot%\msbuild.exe "runner.msbuild" /l:FileLogger,Microsoft.Build.Engine;logfile=runner.log;verbosity=detailed /t:Rebuild;Analyze /p:Configuration=Debug
if "%1" NEQ "noprompt" PAUSE
