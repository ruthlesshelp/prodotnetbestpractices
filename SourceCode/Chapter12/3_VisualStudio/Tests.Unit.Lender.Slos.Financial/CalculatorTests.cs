namespace Tests.Unit.Lender.Slos.Financial
{
    using System;

    using global::Lender.Slos.Financial;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CalculatorTests
    {
        #region ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment

        [TestMethod]
        public void ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment_TC01()
        {
            ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment(7499, 0.001492m, 113, 72.16m);
        }

        [TestMethod]
        public void ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment_TC02()
        {
            ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment(8753, 0.005442m, 139, 89.93m);
        }

        [TestMethod]
        public void ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment_TC03()
        {
            ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment(61331, 0.005908m, 359, 412.07m);
        }

        private void ComputePayment_WithProvidedLoanData_ExpectProperMonthlyPayment(
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

        #endregion

        #region ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException_TC01()
        {
            ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(7499, 0.001492m, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException_TC02()
        {
            ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(7499, 0.001492m, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException_TC03()
        {
            ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(7499, 0.001492m, -2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException_TC04()
        {
            ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(7499, 0.001492m, int.MinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException_TC05()
        {
            ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(7499, 0.001492m, 361);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException_TC06()
        {
            ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(7499, 0.001492m, 362);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException_TC07()
        {
            ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(7499, 0.001492m, int.MaxValue);
        }

        private void ComputePayment_WithInvalidTermInMonths_ExpectArgumentOutOfRangeException(
            decimal principal,
            decimal ratePerPeriod,
            int termInPeriods)
        {
            // Arrange

            // Act
            Calculator.ComputePaymentPerPeriod(principal, ratePerPeriod, termInPeriods);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        #endregion

        #region ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException_TC01()
        {
            ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException(0, 0.001492m, 360);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException_TC02()
        {
            ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException(999.99m, 0.001492m, 360);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException_TC03()
        {
            ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException(1000000, 0.001492m, 360);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException_TC04()
        {
            ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException(4999999, 0.001492m, 360);
        }

        private void ComputePayment_WithInvalidPrincipal_ExpectInvalidOperationException(
            decimal principal,
            decimal ratePerPeriod,
            int termInMonths)
        {
            // Arrange

            // Act
            Calculator.ComputePaymentPerPeriod(principal, ratePerPeriod, termInMonths);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        #endregion

        #region RatePerMonth_WithValidAnnualPercentageRate_ExpectProperRatePerPeriod

        [TestMethod]
        public void RatePerMonth_WithValidAnnualPercentageRate_ExpectProperRatePerPeriod_TC01()
        {
            RatePerMonth_WithValidAnnualPercentageRate_ExpectProperRatePerPeriod(1.79m, 0.001492m);
        }

        [TestMethod]
        public void RatePerMonth_WithValidAnnualPercentageRate_ExpectProperRatePerPeriod_TC02()
        {
            RatePerMonth_WithValidAnnualPercentageRate_ExpectProperRatePerPeriod(6.53m, 0.005442m);
        }

        [TestMethod]
        public void RatePerMonth_WithValidAnnualPercentageRate_ExpectProperRatePerPeriod_TC03()
        {
            RatePerMonth_WithValidAnnualPercentageRate_ExpectProperRatePerPeriod(7.09m, 0.005908m);
        }

        private void RatePerMonth_WithValidAnnualPercentageRate_ExpectProperRatePerPeriod(
            decimal annualPercentageRate,
            decimal expectedRatePerPeriod)
        {
            // Arrange

            // Act
            var actual = Calculator.ComputeRatePerPeriod(annualPercentageRate);

            // Assert
            Assert.AreEqual(expectedRatePerPeriod, actual);
        }

        #endregion

        #region RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException_TC01()
        {
            RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException(0m);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException_TC02()
        {
            RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException(-1m);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException_TC03()
        {
            RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException(20.0m);
            //[TestCase(21.1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException_TC04()
        {
            RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException(21.1m);
        }

        private void RatePerMonth_WhenAnnualPercentageRateIsInvalid_ExpectInvalidOperationException(
            decimal annualPercentageRate)
        {
            // Arrange

            // Act
            Calculator.ComputeRatePerPeriod(annualPercentageRate);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        #endregion

        [TestMethod]
        public void MaxTermInMonths_Always_Expect360()
        {
            // Arrange

            // Act
            var actual = Calculator.MaxTermInMonths;

            // Assert
            Assert.AreEqual(360, actual);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        #region ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException_TC01()
        {
            ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException(7499m, 0.0m, 113);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException_TC02()
        {
            ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException(7499m, 0.0173m, 113);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException_TC03()
        {
            ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException(7499m, 0.7919m, 113);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException_TC04()
        {
            ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException(7499m, -0.0173m, 113);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException_TC05()
        {
            ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException(7499m, -0.7919m, 113);
        }

        private void ComputePayment_WithInvalidRatePerPeriod_ExpectArgumentOutOfRangeException(
            decimal principal,
            decimal ratePerPeriod,
            int termInPeriods)
        {
            // Arrange

            // Act
            Calculator.ComputePaymentPerPeriod(principal, ratePerPeriod, termInPeriods);

            // Assert
            Assert.Fail("Expected exception was not thrown");
        }

        #endregion
    }
}
