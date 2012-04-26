namespace Tests.Unit.Lender.Slos.Financial
{
    using System;

    using global::Lender.Slos.Financial;

    using NUnit.Framework;

    public class CalculatorTests
    {
        [TestCase(7499, 0.001492, 113, 72.16)]
        [TestCase(8753, 0.005442, 139, 89.93)]
        [TestCase(61331, 0.005908, 359, 412.07)]
        public void ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment(
            decimal principal,
            decimal ratePerPeriod,
            int termInPeriods,
            decimal expectedPaymentAmount)
        {
            // Arrange

            // Act
            var actual = Calculator
                .ComputePaymentPerPeriod(principal, ratePerPeriod, termInPeriods);

            // Assert
            Assert.AreEqual(expectedPaymentAmount, actual);
        }

        [TestCase(7499, 0.001492, 0, 72.16)]
        [TestCase(7499, 0.001492, -1, 72.16)]
        [TestCase(7499, 0.001492, -2, 72.16)]
        [TestCase(7499, 0.001492, int.MinValue, 72.16)]
        [TestCase(7499, 0.001492, 361, 72.16)]
        [TestCase(7499, 0.001492, 362, 72.16)]
        [TestCase(7499, 0.001492, int.MaxValue, 72.16)]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(
            decimal principal,
            decimal ratePerPeriod,
            int termInPeriods,
            decimal expectedPaymentPerPeriod)
        {
            // Arrange

            // Act
            TestDelegate act = () => Calculator.ComputePaymentPerPeriod(principal, ratePerPeriod, termInPeriods);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [TestCase(0, 0.001492, 360, 72.16)]
        [TestCase(999.99, 0.001492, 360, 72.16)]
        [TestCase(1000000, 0.001492, 360, 72.16)]
        [TestCase(4999999, 0.001492, 360, 72.16)]
        public void ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException(
            decimal principal,
            decimal ratePerPeriod,
            int termInMonths,
            decimal expectedPaymentPerPeriod)
        {
            // Arrange

            // Act
            TestDelegate act = () => Calculator.ComputePaymentPerPeriod(principal, ratePerPeriod, termInMonths);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase(1.79, 0.001492)]
        [TestCase(6.53, 0.005442)]
        [TestCase(7.09, 0.005908)]
        public void RatePerMonth_WithValidAnnualPercentageRate_ExpectProperRatePerPeriod(
            decimal annualPercentageRate, 
            decimal expectedRatePerPeriod)
        {
            // Arrange

            // Act
            var actual = Calculator.ComputeRatePerPeriod(annualPercentageRate);

            // Assert
            Assert.AreEqual(expectedRatePerPeriod, actual);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(20.0)]
        [TestCase(21.1)]
        public void RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException(
            decimal annualPercentageRate)
        {
            // Arrange

            // Act
            TestDelegate act = () => Calculator.ComputeRatePerPeriod(annualPercentageRate);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void MaxTermInMonths_Always_Expect360()
        {
            // Arrange

            // Act
            var actual = Calculator.MaxTermInMonths;

            // Assert
            Assert.AreEqual(360, actual);
        }
    }
}
