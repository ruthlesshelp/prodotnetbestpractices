namespace Tests.Unit.Lender.Slos.OptionalParameters
{
    using System;

    using global::Lender.Slos.OptionalParameters;

    using NUnit.Framework;

    public class OptionalParametersListings
    {
        private readonly int _expectedLoanLimit;

        public OptionalParametersListings()
        {
            _expectedLoanLimit = LoanValidator.StaticReadonlyLoanLimit;
        }

        [TestCase("Public", "John", 1993, 11, 13)]
        public void CreateApplication_Listing_7(
            string lastName,
            string firstName,
            int year,
            int month,
            int day)
        {
            var dateOfBirth = new DateTime(year, month, day);

            var controller = new Controller();

            var application = controller.CreateApplication(
                lastName,
                firstName,
                dateOfBirth);

            Assert.NotNull(application);
            Console.WriteLine("{0}, {1}, DOB:{2}",
                application.LastName,
                application.FirstName,
                application.DateOfBirth.ToShortDateString());

            Assert.AreEqual(lastName, application.LastName);
        }

        [Test]
        public void RevisedLoanAmount_Listing_7()
        {
            var validator = new LoanValidator();

            var originalLoanAmount = 5000;

            var revisedLoanAmount = validator.LoanConstCeiling(originalLoanAmount);

            Console.WriteLine("LoanConstCeiling expected: {0}, actual: {1}",
                originalLoanAmount,
                revisedLoanAmount);

            originalLoanAmount = revisedLoanAmount;

            Console.WriteLine("LoanConstCeiling expected: {0}, actual: {1}",
                originalLoanAmount,
                revisedLoanAmount);
        }

        [Test]
        public void RetrieveConstLoanLimit_Listing_7()
        {
            var validator = new LoanValidator();

            var loanLimit = validator.LoanConstCeiling();

            Console.WriteLine("LoanConstCeiling expected: {0}, actual: {1}",
                _expectedLoanLimit,
                loanLimit);
        }

        [Test]
        public void RetrieveStaticReadonlyLoanLimit_Listing_7()
        {
            var validator = new LoanValidator();

            var loanLimit = validator.LoanStaticReadonlyCeiling();

            Console.WriteLine("LoanStaticReadonlyCeiling expected: {0}, actual: {1}",
                _expectedLoanLimit,
                loanLimit);
        }
    }
}