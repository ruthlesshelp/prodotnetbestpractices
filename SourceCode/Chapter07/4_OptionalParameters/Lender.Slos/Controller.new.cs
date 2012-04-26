namespace Lender.Slos.OptionalParameters
{
    using System;

    public class Controller
    {
        public Application CreateApplication(
            string lastName,
            string firstName,
            DateTime dateOfBirth,
            string middleInitial = null,
            string suffix = null,
            DateTime? dateOnApplication = null,
            decimal? principal = null,
            decimal? annualPercentageRate = null,
            int? totalPayments = null)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentNullException("lastName");
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentNullException("firstName");
            }

            if (dateOfBirth < Application.MinimumDateOfBirth)
            {
                throw new ArgumentOutOfRangeException("dateOfBirth");
            }

            return new Application(null)
            {
                LastName = lastName,
                FirstName = firstName,
                MiddleInitial = middleInitial ?? string.Empty,
                Suffix = suffix ?? string.Empty,
                DateOfBirth = dateOfBirth,
                DateOnApplication = dateOnApplication ?? DateTime.Today,
                Principal = principal ?? Application.MaximumLoanAmount,
                AnnualPercentageRate = annualPercentageRate ?? Application.DefaultAnnualPercentageRate,
                TotalPayments = totalPayments ?? Application.DefaultTotalPayments,
            };
        }
    }
}