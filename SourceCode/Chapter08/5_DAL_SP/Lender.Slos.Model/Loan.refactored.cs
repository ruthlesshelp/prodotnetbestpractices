namespace Lender.Slos.Model
{
    using Lender.Slos.Financial;

    public class Loan
    {
        public static int MaxTermInMonths
        {
            get
            {
                return Calculator.MaxTermInMonths;
            }
        }

        public decimal Principal { get; set; }

        public decimal AnnualPercentageRate { get; set; }

        public decimal PaymentPerPeriod { get; set; }

        public int TotalPayments { get; set; }

        public decimal RatePerMonth
        {
            get
            {
                return Calculator
                    .ComputeRatePerPeriod(this.AnnualPercentageRate);
            }
        }

        public decimal ComputePayment(int termInMonths)
        {
            return Calculator
                .ComputePaymentPerPeriod(this.Principal, this.RatePerMonth, termInMonths);
        }
    }
}
