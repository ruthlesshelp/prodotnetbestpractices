namespace Tests.Unit.Lender.Slos.Financial
{
    using System;

    using global::Lender.Slos.Financial;

    using Xunit;
    using Xunit.Extensions;

    public class CalculatorTests
    {
        [Theory]
        [InlineData(7499, 0.001492, 113, 72.16)]
        [InlineData(8753, 0.005442, 139, 89.93)]
        [InlineData(61331, 0.005908, 359, 412.07)]
        public void ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment(
            double principal,
            double ratePerPeriod,
            int termInPeriods,
            double expectedPaymentAmount)
        {
            // Arrange

            // Act
            var actual = Calculator.ComputePaymentPerPeriod(
                (decimal)principal, 
                (decimal)ratePerPeriod, 
                termInPeriods);

            // Assert
            Assert.Equal((decimal)expectedPaymentAmount, actual);
        }

        [Theory]
        [InlineData(7499, 0.001492, 0, 72.16)]
        [InlineData(7499, 0.001492, -1, 72.16)]
        [InlineData(7499, 0.001492, -2, 72.16)]
        [InlineData(7499, 0.001492, int.MinValue, 72.16)]
        [InlineData(7499, 0.001492, 361, 72.16)]
        [InlineData(7499, 0.001492, 362, 72.16)]
        [InlineData(7499, 0.001492, int.MaxValue, 72.16)]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(
            double principal,
            double ratePerPeriod,
            int termInPeriods,
            double expectedPaymentPerPeriod)
        {
            // Arrange

            // Act
            Assert.ThrowsDelegate act = () => Calculator.ComputePaymentPerPeriod(
                (decimal)principal, 
                (decimal)ratePerPeriod, 
                termInPeriods);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Theory]
        [InlineData(0, 0.001492, 360, 72.16)]
        [InlineData(999.99, 0.001492, 360, 72.16)]
        [InlineData(1000000, 0.001492, 360, 72.16)]
        [InlineData(4999999, 0.001492, 360, 72.16)]
        public void ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException(
            double principal,
            double ratePerPeriod,
            int termInMonths,
            double expectedPaymentPerPeriod)
        {
            // Arrange

            // Act
            Assert.ThrowsDelegate act = () => Calculator.ComputePaymentPerPeriod(
                (decimal)principal,
                (decimal)ratePerPeriod, 
                termInMonths);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Theory]
        [InlineData(1.79, 0.001492)]
        [InlineData(6.53, 0.005442)]
        [InlineData(7.09, 0.005908)]
        public void RatePerMonth_WithValidAnnualPercentageRate_ExpectProperRatePerPeriod(
            double annualPercentageRate, 
            double expectedRatePerPeriod)
        {
            // Arrange

            // Act
            var actual = Calculator.ComputeRatePerPeriod(
                (decimal)annualPercentageRate);

            // Assert
            Assert.Equal((decimal)expectedRatePerPeriod, actual);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(20.0)]
        [InlineData(21.1)]
        public void RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException(
            double annualPercentageRate)
        {
            // Arrange

            // Act
            Assert.ThrowsDelegate act = () => Calculator.ComputeRatePerPeriod(
                (decimal)annualPercentageRate);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void MaxTermInMonths_Always_Expect360()
        {
            // Arrange

            // Act
            var actual = Calculator.MaxTermInMonths;

            // Assert
            Assert.Equal(360, actual);
        }

        [Fact]
        public void ComputePayment_WithDecimalDefaultForRatePerPeriod_ExpectArgumentOutOfRangeException()
        {
            // Arrange

            // Act
            Assert.ThrowsDelegate act = () => Calculator.ComputePaymentPerPeriod(
                4066384953739624285955310521e-24M, 
                default(decimal), 
                1);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void ComputePayment_WithDecimalMinValueForRatePerPeriod_ExpectArgumentOutOfRangeException()
        {
            // Arrange

            // Act
            Assert.ThrowsDelegate act = () => Calculator.ComputePaymentPerPeriod(
                22046654230654627912396946053e-25M, 
                decimal.MinValue, 
                1);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void ComputePayment_WithDecimalMaxValueForRatePerPeriod_ExpectArgumentOutOfRangeException()
        {
            // Arrange

            // Act
            Assert.ThrowsDelegate act = () => Calculator.ComputePaymentPerPeriod(4066384953739624285955310521e-24M, 
                decimal.MaxValue, 
                1);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Theory]
        [InlineData(7499, 0.0, 113, 72.16)]
        [InlineData(7499, 0.0173, 113, 72.16)]
        [InlineData(7499, 0.7919, 113, 72.16)]
        [InlineData(7499, -0.0173, 113, 72.16)]
        [InlineData(7499, -0.7919, 113, 72.16)]
        public void ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException(
            double principal,
            double ratePerPeriod,
            int termInPeriods,
            double expectedPaymentPerPeriod)
        {
            // Arrange

            // Act
            Assert.ThrowsDelegate act = () => Calculator.ComputePaymentPerPeriod(
                (decimal)principal,
                (decimal)ratePerPeriod, 
                termInPeriods);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }
    }
}
