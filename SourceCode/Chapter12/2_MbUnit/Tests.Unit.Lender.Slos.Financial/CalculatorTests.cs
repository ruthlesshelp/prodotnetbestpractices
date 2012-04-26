namespace Tests.Unit.Lender.Slos.Financial
{
    using System;

    using global::Lender.Slos.Financial;

    using MbUnit.Framework;

    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        [Row(7499, 0.001492, 113, 72.16)]
        [Row(8753, 0.005442, 139, 89.93)]
        [Row(61331, 0.005908, 359, 412.07)]
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

        [Test]
        [Row(7499, 0.001492, 0, 72.16)]
        [Row(7499, 0.001492, -1, 72.16)]
        [Row(7499, 0.001492, -2, 72.16)]
        [Row(7499, 0.001492, int.MinValue, 72.16)]
        [Row(7499, 0.001492, 361, 72.16)]
        [Row(7499, 0.001492, 362, 72.16)]
        [Row(7499, 0.001492, int.MaxValue, 72.16)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(
            decimal principal,
            decimal ratePerPeriod,
            int termInPeriods,
            decimal expectedPaymentPerPeriod)
        {
            // Arrange

            // Act
            Calculator.ComputePaymentPerPeriod(principal, ratePerPeriod, termInPeriods);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        [Test]
        [Row(0, 0.001492, 360, 72.16)]
        [Row(999.99, 0.001492, 360, 72.16)]
        [Row(1000000, 0.001492, 360, 72.16)]
        [Row(4999999, 0.001492, 360, 72.16)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException(
            decimal principal,
            decimal ratePerPeriod,
            int termInMonths,
            decimal expectedPaymentPerPeriod)
        {
            // Arrange

            // Act
            Calculator.ComputePaymentPerPeriod(principal, ratePerPeriod, termInMonths);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        [Test]
        [Row(1.79, 0.001492)]
        [Row(6.53, 0.005442)]
        [Row(7.09, 0.005908)]
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

        [Test]
        [Row(0)]
        [Row(-1)]
        [Row(20.0)]
        [Row(21.1)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException(
            decimal annualPercentageRate)
        {
            // Arrange

            // Act
            Calculator.ComputeRatePerPeriod(annualPercentageRate);

            // Assert
            Assert.Fail("Expected exception was not thrown");
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

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithDecimalDefaultForRatePerPeriod_ExpectArgumentOutOfRangeException()
        {
            // Arrange

            // Act
            Calculator.ComputePaymentPerPeriod(
                4066384953739624285955310521e-24M, 
                default(decimal), 
                1);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithDecimalMinValueForRatePerPeriod_ExpectArgumentOutOfRangeException()
        {
            // Arrange

            // Act
            Calculator.ComputePaymentPerPeriod(
                22046654230654627912396946053e-25M, 
                decimal.MinValue, 
                1);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithDecimalMaxValueForRatePerPeriod_ExpectArgumentOutOfRangeException()
        {
            // Arrange

            // Act
            Calculator.ComputePaymentPerPeriod(4066384953739624285955310521e-24M, 
                decimal.MaxValue, 
                1);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        [Test]
        [Row(7499, 0.0, 113, 72.16)]
        [Row(7499, 0.0173, 113, 72.16)]
        [Row(7499, 0.7919, 113, 72.16)]
        [Row(7499, -0.0173, 113, 72.16)]
        [Row(7499, -0.7919, 113, 72.16)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException(
            decimal principal,
            decimal ratePerPeriod,
            int termInPeriods,
            decimal expectedPaymentPerPeriod)
        {
            // Arrange

            // Act
            Calculator.ComputePaymentPerPeriod(principal, ratePerPeriod, termInPeriods);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }
    }
}
