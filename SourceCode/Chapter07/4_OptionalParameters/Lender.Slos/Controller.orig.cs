namespace Lender.Slos.OptionalParameters
{
    using System;

    public class Controller
    {
        public Application CreateApplication(
            string lastName, 
            string firstName, 
            DateTime dateOfBirth)
        {
            return CreateApplication(
                lastName, 
                firstName,
                dateOfBirth,
                string.Empty,
                string.Empty,
                DateTime.Today,
                Application.MaximumLoanAmount,
                Application.DefaultAnnualPercentageRate,
                Application.DefaultTotalPayments);
        }

        public Application CreateApplication(
            string lastName,
            string firstName,
            DateTime dateOfBirth,
            string middleInitial)
        {
            return CreateApplication(
                lastName,
                firstName,
                dateOfBirth,
                middleInitial,
                string.Empty,
                DateTime.Today,
                Application.MaximumLoanAmount,
                Application.DefaultAnnualPercentageRate,
                Application.DefaultTotalPayments);
        }

        public Application CreateApplication(
            string lastName,
            string firstName,
            DateTime dateOfBirth,
            string middleInitial,
            string suffix)
        {
            return CreateApplication(
                lastName,
                firstName,
                dateOfBirth,
                middleInitial ?? string.Empty,
                suffix ?? string.Empty,
                DateTime.Today,
                Application.MaximumLoanAmount,
                Application.DefaultAnnualPercentageRate,
                Application.DefaultTotalPayments);
        }

        public Application CreateApplication(
            string lastName,
            string firstName,
            DateTime dateOfBirth,
            string middleInitial,
            string suffix,
            DateTime dateOnApplication)
        {
            return CreateApplication(
                lastName,
                firstName,
                dateOfBirth,
                middleInitial ?? string.Empty,
                suffix ?? string.Empty,
                dateOnApplication,
                Application.MaximumLoanAmount,
                Application.DefaultAnnualPercentageRate,
                Application.DefaultTotalPayments);
        }

        public Application CreateApplication(
            string lastName,
            string firstName,
            DateTime dateOfBirth,
            string middleInitial,
            string suffix,
            DateTime dateOnApplication,
            decimal principal)
        {
            return CreateApplication(
                lastName,
                firstName,
                dateOfBirth,
                middleInitial ?? string.Empty,
                suffix ?? string.Empty,
                dateOnApplication,
                principal,
                Application.DefaultAnnualPercentageRate,
                Application.DefaultTotalPayments);
        }

        public Application CreateApplication(
            string lastName,
            string firstName,
            DateTime dateOfBirth,
            string middleInitial,
            string suffix,
            DateTime dateOnApplication,
            decimal principal,
            decimal annualPercentageRate)
        {
            return CreateApplication(
                lastName,
                firstName,
                dateOfBirth,
                middleInitial ?? string.Empty,
                suffix ?? string.Empty,
                dateOnApplication,
                principal,
                annualPercentageRate,
                Application.DefaultTotalPayments);
        }

        public Application CreateApplication(
            string lastName,
            string firstName,
            DateTime dateOfBirth,
            string middleInitial,
            string suffix,
            DateTime dateOnApplication,
            decimal principal,
            decimal annualPercentageRate,
            int totalPayments)
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
                DateOnApplication = dateOnApplication,
                Principal = principal,
                AnnualPercentageRate = annualPercentageRate,
                TotalPayments = totalPayments,
            };
        }
    }
}