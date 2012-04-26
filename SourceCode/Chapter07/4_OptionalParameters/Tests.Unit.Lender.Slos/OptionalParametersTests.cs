namespace Tests.Unit.Lender.Slos.OptionalParameters
{
    using global::Lender.Slos.OptionalParameters;

    using NUnit.Framework;

    public class OptionalParametersTests
    {
        [Test]
        public void LoanConstCeiling_WithNoParameter_ExpectProperValue()
        {
            // Arrange
            var expected = LoanValidator.StaticReadonlyLoanLimit;
            var classUnderTest = new LoanValidator();

            // Act
            var actual = classUnderTest.LoanConstCeiling();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoanStaticReadonlyCeiling_WithNoParameter_ExpectProperValue()
        {
            // Arrange
            var expected = LoanValidator.StaticReadonlyLoanLimit;
            var classUnderTest = new LoanValidator();

            // Act
            var actual = classUnderTest.LoanStaticReadonlyCeiling();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
