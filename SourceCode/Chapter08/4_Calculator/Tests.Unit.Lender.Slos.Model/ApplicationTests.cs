namespace Tests.Unit.Lender.Slos.Model
{
    using System;

    using global::Lender.Slos.Model;

    using NUnit.Framework;

    public class ApplicationTests
    {
        [TestCase(7499, 1.79, 113, 72.16)]
        [TestCase(8753, 6.53, 139, 89.93)]
        [TestCase(61331, 7.09, 359, 412.07)]
        public void ComputePayment_WithProvidedApplicationData_ExpectProperMonthlyPayment(
            decimal principal,
            decimal annualPercentageRate,
            int termInMonths,
            decimal expectedPaymentAmount)
        {
            // Arrange
            var classUnderTest =
                new Application(null, null, null)
                {
                    Principal = principal,
                    AnnualPercentageRate = annualPercentageRate,
                };

            // Act
            var actual = classUnderTest.ComputePayment(termInMonths);

            // Assert
            Assert.AreEqual(expectedPaymentAmount, actual);
        }

        [TestCase(7499, 1.79, 0, 72.16)]
        [TestCase(7499, 1.79, -1, 72.16)]
        [TestCase(7499, 1.79, -2, 72.16)]
        [TestCase(7499, 1.79, int.MinValue, 72.16)]
        [TestCase(7499, 1.79, 361, 72.16)]
        [TestCase(7499, 1.79, 362, 72.16)]
        [TestCase(7499, 1.79, int.MaxValue, 72.16)]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(
            decimal principal,
            decimal annualPercentageRate,
            int termInMonths,
            decimal expectedPaymentPerPeriod)
        {
            // Arrange
            var classUnderTest =
                new Application(null, null, null)
                {
                    Principal = principal,
                    AnnualPercentageRate = annualPercentageRate,
                };

            // Act
            TestDelegate act = () => classUnderTest.ComputePayment(termInMonths);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [TestCase(0, 1.79, 360, 72.16)]
        [TestCase(999.99, 1.79, 360, 72.16)]
        [TestCase(1000000, 1.79, 360, 72.16)]
        [TestCase(4999999, 1.79, 360, 72.16)]
        public void ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException(
            decimal principal,
            decimal annualPercentageRate,
            int termInMonths,
            decimal expectedPaymentPerPeriod)
        {
            // Arrange
            var classUnderTest =
                new Application(null, null, null)
                {
                    Principal = principal,
                    AnnualPercentageRate = annualPercentageRate,
                };

            // Act
            TestDelegate act = () => classUnderTest.ComputePayment(termInMonths);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(20.0)]
        [TestCase(21.1)]
        public void RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException(
            decimal annualPercentageRate)
        {
            // Arrange
            decimal ratePerMonth;
            var classUnderTest =
                new Application(null, null, null)
                {
                    AnnualPercentageRate = annualPercentageRate,
                };

            // Act
            TestDelegate act = () => ratePerMonth = classUnderTest.RatePerMonth;

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void MaxTermInMonths_Always_Expect360()
        {
            // Arrange

            // Act
            var actual = Application.MaxTermInMonths;

            // Assert
            Assert.AreEqual(360, actual);
        }
    }
}
