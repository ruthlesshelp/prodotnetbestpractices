namespace Lender.Slos.Model
{
    using System;
    using System.Globalization;

    public class Loan
    {
        public const int MaxTermInMonths = 360;

        public decimal Principal { get; set; }

        public decimal AnnualPercentageRate { get; set; }

        public decimal PaymentPerPeriod { get; set; }

        public int TotalPayments { get; set; }

        public decimal RatePerMonth
        {
            get
            {
                if (this.AnnualPercentageRate < 0.01m ||
                    this.AnnualPercentageRate >= 20.0m)
                {
                    throw new InvalidOperationException(string.Format(
                        "AnnualPercentageRate {0}% is not valid",
                        this.AnnualPercentageRate));
                }

                var ratePerMonth = (this.AnnualPercentageRate / 100m) / 12m;

                return Math.Round(ratePerMonth, 6, MidpointRounding.AwayFromZero);
            }
        }

        public decimal ComputePayment(int termInMonths)
        {
            if (termInMonths <= 0 || termInMonths > MaxTermInMonths)
            {
                throw new ArgumentOutOfRangeException("termInMonths");
            }

            if (this.Principal < 1000m || this.Principal >= 1000000m)
            {
                throw new InvalidOperationException(string.Format(
                    "Principal {0} is not valid",
                    this.Principal.ToString("C", new CultureInfo("EN-us"))));
            }

            var exponentBase = Convert.ToDouble(decimal.One + this.RatePerMonth);
            var exponent = Convert.ToDecimal(Math.Pow(exponentBase, -1 * termInMonths));

            var payment = (this.RatePerMonth * this.Principal) / (1m - exponent);
            payment = Math.Round(payment, 2, MidpointRounding.AwayFromZero);

            return payment;
        }
    }
}
