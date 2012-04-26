using Lender.Slos.ImplicitTyping;

namespace Tests.Unit.Lender.Slos.ImplicitTyping
{
    using NUnit.Framework;

    public class ImplicitTypingTests
    {
        [TestCase("P", 2)]
        public void Search_WithLastNameCriteria_ExpectProperCount(
            string lastNameCriteria,
            int expectedCount)
        {
            // Arrange
            var classUnderTest = ImplicitTypingTestsHelper.CreateController();

            // Act
            var results = classUnderTest.Search(lastNameCriteria);

            // Assert
            Assert.AreEqual(expectedCount, results.Count);
        }
    }
}
