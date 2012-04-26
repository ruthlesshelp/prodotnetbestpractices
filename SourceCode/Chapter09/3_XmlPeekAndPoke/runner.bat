@ECHO OFF

%MSBuildRoot%\msbuild.exe "runner.msbuild" /t:XmlPeekAndPoke
if "%1" NEQ "noprompt" PAUSE
