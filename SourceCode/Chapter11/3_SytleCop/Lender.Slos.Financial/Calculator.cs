namespace Lender.Slos.Financial
{
    using System;
    using System.Globalization;

    public static class Calculator
    {
        public const int MaxTermInMonths = 360;

        public const decimal MinPrincipalAmount = 1000m;
        public const decimal MaxPrincipalAmount = 1000000m;

        public const decimal PeriodsPerYear = 12m;

        // Violates the StyleCop rule SA1308: VariableNamesMustNotBePrefixed
        //     More information in the StyleCop help file: StyleCop.chm
        private static DateTime s_lastCalculationTime;

        public static decimal ComputeRatePerPeriod(
            decimal annualPercentageRate)
        {
            s_lastCalculationTime = DateTime.Now;

            if (annualPercentageRate < 0.01m ||
                annualPercentageRate >= 20.0m)
            {
                throw new InvalidOperationException(string.Format(
                    "AnnualPercentageRate {0}% is not valid",
                    annualPercentageRate));
            }

            var ratePerMonth = (annualPercentageRate / 100m) / PeriodsPerYear;

            return Math.Round(ratePerMonth, 6, MidpointRounding.AwayFromZero);
        }

        public static decimal ComputePaymentPerPeriod(
            decimal principalAmount,
            decimal ratePerPeriod,
            int termInPeriods)
        {
            s_lastCalculationTime = DateTime.Now;

            if (termInPeriods <= 0 || termInPeriods > MaxTermInMonths)
            {
                throw new ArgumentOutOfRangeException("termInPeriods");
            }

            if (principalAmount < MinPrincipalAmount || 
                principalAmount >= MaxPrincipalAmount)
            {
                // Violates the FxCop rule a: b
                throw new InvalidOperationException(string.Format(
                    "Principal {0} is not valid", 
                    principalAmount.ToString("C", new CultureInfo("EN-us"))));
            }

            TooManyVariables();

            try
            {
                // Violates the FxCop rule CA1804: RemoveUnusedLocals
                //     http://msdn.microsoft.com/en-us/library/ms182278.aspx
                var unusedLocal = new IncorrectlyImplementsIDisposable();

                var exponentBase = Convert.ToDouble(decimal.One + ratePerPeriod);
                var exponent = Convert.ToDecimal(Math.Pow(exponentBase, -1 * termInPeriods));

                var payment = (ratePerPeriod * principalAmount) / (1m - exponent);
                payment = Math.Round(payment, 2, MidpointRounding.AwayFromZero);

                // Violates the StyleCop rule SA1507: CodeMustNotContainMultipleBlankLinesInARow
                //     More information in the StyleCop help file: StyleCop.chm


                return payment;
            }
            catch (ArithmeticException arithmeticException)
            {
                // Violates the FxCop rule CA2200: RethrowToPreserveStackDetails
                //     http://msdn.microsoft.com/en-us/library/ms182363(v=VS.100).aspx
                throw arithmeticException;
            }
        }

        private static void TooManyVariables()
        {
            System.Diagnostics.Trace.WriteLine(s_lastCalculationTime);

            #pragma warning disable 0168
            
            // Violates the FxCop rule CA1809: AvoidExcessiveLocals
            //     http://msdn.microsoft.com/en-US/library/ms182263(v=VS.100).aspx
            // Violates the StyleCop rule SA1107: CodeMustNotContainMultipleStatementsOnOneLine
            //     More information in the StyleCop help file: StyleCop.chm
            int v1; int v2; int v3; int v4; int v5; int v6; int v7; int v8; int v9; int v10; int v11; int v12; int v13; int v14; int v15; int v16; int v17; int v18; int v19; int v20; int v21; int v22; int v23; int v24; int v25; int v26; int v27; int v28; int v29; int v30; int v31; int v32; int v33; int v34; int v35; int v36; int v37; int v38; int v39; int v40; int v41; int v42; int v43; int v44; int v45; int v46; int v47; int v48; int v49; int v50; int v51; int v52; int v53; int v54; int v55; int v56; int v57; int v58; int v59; int v60; int v61; int v62; int v63; int v64; int v65;
            
            #pragma warning restore 0168
        }
    }
}
