namespace Lender.Slos.OptionalParameters
{
    public class LoanValidator
    {
        public static readonly int StaticReadonlyLoanLimit = 17500;

        public int LoanStaticReadonlyCeiling(int? loanAmount = null)
        {
            return loanAmount ?? StaticReadonlyLoanLimit;
        }

        public int LoanConstCeiling(int loanAmount = 17500)
        {
            return loanAmount;
        }
    }
}