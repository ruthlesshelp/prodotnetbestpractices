@ECHO OFF

ECHO "Creating the database: [Lender.Slos] ..."
"%SqlToolsRoot%\sqlcmd.exe" -S localhost\sqlexpress -Q "CREATE DATABASE [Lender.Slos]"
ECHO "    done."

IF "%1" NEQ "noprompt" PAUSE
