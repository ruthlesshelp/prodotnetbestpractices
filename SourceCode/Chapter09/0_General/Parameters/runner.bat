@ECHO OFF

%MSBuildRoot%\msbuild.exe "runner.msbuild" /p:ParamOne=42;ParamTwo="First;Second;Third";ParamThree="C:\My Documents\\";ParamFour="\"In Quotes\""
if "%1" NEQ "noprompt" PAUSE
