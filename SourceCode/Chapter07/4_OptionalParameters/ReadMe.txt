SAMPLE CODE: READ ME

For these instructions, it is assumed that the same code for this chaper is found in your C:\Samples\Ch07 folder.
In the instructions that follow your installation folder is referred to by a dollar symbol ($).

For the sake of simplicity there are a few batch files that use MSBuild to run database scripts, automate the build, and automate running the tests. These batch files assume you defined the following environment variables:
• MSBuildRoot – the path to MSBuild.exe
For example, C:\Windows\Microsoft.NET\Framework64\v4.0.30319

• NUnitRoot – the path to nunit-console.exe
For example, C:\Tools\NUnit\v2.5.10.11092\bin\net-2.0

1. To start, run the "all.bat" command file. The compiles all projects.

2. Run the "test.bat" file. All tests pass and there are no errors.

3. Open this file: $\4_OptionalParameters\Lender.Slos\LoanValidator.cs

4. Change the number 17500 to 18000 in two places. It should be as follows:
line  5:    public static readonly int StaticReadonlyLoanLimit = 18000;
line 12:    public int LoanConstCeiling(int loanAmount = 18000)

5. Save the LoanValidator.cs file and run the "build.bat" file (not the all.bat). This only compiles the Lender.Slos.dll assembly.

6. Run the "test.bat" file.

There should be one test that fails with output:
Errors and Failures:
1) Test Failure : Tests.Unit.Lender.Slos.OptionalParameters.OptionalParametersTests.LoanConstCeiling_WithNoParameter_ExpectProperValue
  Expected: 18000
  But was:  17500
  
 