@ECHO OFF

%MSBuildRoot%\msbuild.exe "..\..\database.deploy.msbuild" /t:DbDeploy /p:ScriptDirectory=Create\TestUseOnly /p:ScriptRunOrderFilename=script_run_order.txt
if "%1" NEQ "noprompt" PAUSE
