@ECHO OFF

mkdir logs

%MSBuildRoot%\msbuild.exe "runner.msbuild" /l:FileLogger,Microsoft.Build.Engine;logfile=logs\quiet.log;verbosity=quiet

%MSBuildRoot%\msbuild.exe "runner.msbuild" /l:FileLogger,Microsoft.Build.Engine;logfile=logs\minimal.log;verbosity=minimal

%MSBuildRoot%\msbuild.exe "runner.msbuild" /l:FileLogger,Microsoft.Build.Engine;logfile=logs\normal.log

%MSBuildRoot%\msbuild.exe "runner.msbuild" /l:FileLogger,Microsoft.Build.Engine;logfile=logs\detailed.log;verbosity=detailed

%MSBuildRoot%\msbuild.exe "runner.msbuild" /l:FileLogger,Microsoft.Build.Engine;logfile=logs\diagnostic.log;verbosity=diagnostic
if "%1" NEQ "noprompt" PAUSE
