SAMPLE CODE: READ ME

For these instructions, it is assumed that the same code for this chaper is found in your C:\Samples\Ch12 folder.
In the instructions that follow your installation folder is referred to by a dollar symbol ($).

The Database folder contains the database scripts to create the database.

For the sake of simplicity there are a few batch files that use MSBuild to run database scripts, automate the build, and automate running the tests. These batch files assume you defined the following environment variables:
• MSBuildRoot – the path to MSBuild.exe
For example, C:\Windows\Microsoft.NET\Framework64\v4.0.30319

• SqlToolsRoot – the path to sqlcmd.exe
For example, C:\Program Files\Microsoft SQL Server\100\Tools\Binn

The DbCreate.SqlExpress.Lender.Slos.bat command file creates the local SQLExpress database.

With the database created and the environment variables set, run the Lender.Slos.CreateScripts.bat batch to execute all the SQL create scripts in the correct order. If you prefer to run the scripts manually then you will find them in the $\Deploy\Database\Scripts\Create folder. The script_run_order.txt file lists the proper order to run the scripts. If all the scripts run properly there will be three tables (Individual, Student and Application) and twelve stored procedures (a set of four CRUD stored procedures for each of the tables) in the database.

