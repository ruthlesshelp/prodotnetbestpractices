@ECHO OFF

ECHO "Dropping the database: [Lender.Slos] ..."
"%SqlToolsRoot%\sqlcmd.exe" -S localhost\sqlexpress -Q "DROP DATABASE [Lender.Slos]"
ECHO "    done."

IF "%1" NEQ "noprompt" PAUSE
