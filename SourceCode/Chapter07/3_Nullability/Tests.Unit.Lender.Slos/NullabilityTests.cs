namespace Tests.Unit.Lender.Slos.Nullability
{
    using System;

    using NUnit.Framework;

    public class NullabilityTests
    {
        [Test]
        public void Save_WithNoDateOnApplication_ExpectToday()
        {
            // Arrange
            var classUnderTest = NullabilityTestsHelper.CreateApplication();

            // Act
            classUnderTest.Save();

            // Assert
            Assert.AreEqual(DateTime.Today, classUnderTest.DateOnApplication);
        }

        [Test]
        public void Save_WithNoMiddleName_ExpectEmptyString()
        {
            // Arrange
            var classUnderTest = NullabilityTestsHelper.CreateApplication();

            // Act
            classUnderTest.Save();

            // Assert
            Assert.AreEqual(string.Empty, classUnderTest.MiddleInitial);
        }

        [Test]
        public void Save_WithNoId_ExpectNextId()
        {
            // Arrange
            var expectedNextId = new Guid("62C78CED-8765-4B99-A3F8-366AAF5B7C22");

            NullabilityTestsHelper.NextId = expectedNextId;

            var classUnderTest = NullabilityTestsHelper.CreateApplication();

            // Act
            classUnderTest.Save();

            // Assert
            Assert.AreEqual(expectedNextId, classUnderTest.Id);
        }

        [Test]
        public void Save_WithValidId_ExpectSameId()
        {
            // Arrange
            NullabilityTestsHelper.NextId = new Guid("62C78CED-8765-4B99-A3F8-366AAF5B7C22");

            var expectedId = new Guid("95C90124-4CBE-4465-9A31-10EEE35E51D8");
            var classUnderTest = NullabilityTestsHelper.CreateApplication(expectedId);

            // Act
            classUnderTest.Save();

            // Assert
            Assert.AreEqual(expectedId, classUnderTest.Id);
        }
    }
}
