namespace Lender.Slos.ImplicitTyping
{
    using System;

    public class Application
    {
        public static readonly int MaximumLoanAmount = 17500;

        public static readonly decimal DefaultAnnualPercentageRate = 1.79m;

        public static readonly int DefaultTotalPayments = 360;

        public static readonly DateTime MinimumDateOfBirth = new DateTime(1900, 1, 1);

        public Application(
            int? id)
        {
            if (id.HasValue)
            {
                Id = id.Value;
            }
        }

        public int? Id { get; private set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string Suffix { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime? DateOnApplication { get; set; }

        public decimal? Principal { get; set; }

        public decimal? AnnualPercentageRate { get; set; }

        public int? TotalPayments { get; set; }
    }
}