// <copyright file="CalculatorTest.cs" company="Lender Inc.">Copyright © Lender Inc. 2011</copyright>

using System;
using Lender.Slos.Financial;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Pex.Framework.Generated;

namespace Lender.Slos.Financial
{
    [TestClass]
    [PexClass(typeof(Calculator))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class CalculatorTest
    {
        [PexMethod]
        public decimal ComputePaymentPerPeriod(
            decimal principalAmount,
            decimal ratePerPeriod,
            int termInPeriods
        )
        {
            decimal result = Calculator.ComputePaymentPerPeriod(principalAmount, ratePerPeriod, termInPeriods);
            return result;
            // TODO: add assertions to method CalculatorTest.ComputePaymentPerPeriod(Decimal, Decimal, Int32)
        }
        [PexMethod]
        public decimal ComputeRatePerPeriod(decimal annualPercentageRate)
        {
            decimal result = Calculator.ComputeRatePerPeriod(annualPercentageRate);
            return result;
            // TODO: add assertions to method CalculatorTest.ComputeRatePerPeriod(Decimal)
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePaymentPerPeriodThrowsArgumentOutOfRangeException729()
        {
            decimal d;
            d = this.ComputePaymentPerPeriod(default(decimal), default(decimal), 0);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePaymentPerPeriodThrowsArgumentOutOfRangeException61()
        {
            decimal d;
            d = this.ComputePaymentPerPeriod(default(decimal), default(decimal), 1);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ComputePaymentPerPeriodThrowsArgumentOutOfRangeException633()
        {
            decimal d;
            d = this.ComputePaymentPerPeriod(default(decimal), 1M, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ComputePaymentPerPeriodThrowsInvalidOperationException869()
        {
            decimal d;
            d = this.ComputePaymentPerPeriod(default(decimal), 1e-3M, 1);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ComputePaymentPerPeriodThrowsInvalidOperationException652()
        {
            decimal d;
            d = this.ComputePaymentPerPeriod(1965637706M, 1e-3M, 1);
        }
        [TestMethod]
        public void ComputePaymentPerPeriod344()
        {
            decimal d;
            d = this.ComputePaymentPerPeriod(4066384953739624285955310521e-24M, 1e-3M, 1);
            Assert.AreEqual<decimal>(407045e-2M, d);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ComputeRatePerPeriodThrowsInvalidOperationException907()
        {
            decimal d;
            d = this.ComputeRatePerPeriod(default(decimal));
        }
        [TestMethod]
        public void ComputeRatePerPeriod641()
        {
            decimal d;
            d = this.ComputeRatePerPeriod(1M);
            Assert.AreEqual<decimal>(833e-6M, d);
        }
    }
}
