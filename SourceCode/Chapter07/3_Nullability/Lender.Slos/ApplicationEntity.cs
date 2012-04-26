namespace Lender.Slos.Nullability
{
    using System;

    public class ApplicationEntity
    {
        public Guid Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string Suffix { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOnApplication { get; set; }

        public decimal Principal { get; set; }

        public decimal AnnualPercentageRate { get; set; }

        public int TotalPayments { get; set; }
    }
}
