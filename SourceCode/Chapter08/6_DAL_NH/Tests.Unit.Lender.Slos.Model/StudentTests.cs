namespace Tests.Unit.Lender.Slos.Model
{
    using System;

    using global::Lender.Slos.Dao;
    using global::Lender.Slos.Model;

    using Moq;

    using NUnit.Framework;

    public class StudentTests
    {
        [TestCase(1999, 5, 17, 2015, 5, 17)]
        [TestCase(1999, 5, 16, 2015, 5, 17)]
        [TestCase(1999, 3, 19, 2015, 5, 17)]
        [TestCase(1993, 5, 17, 2015, 5, 17)]
        [TestCase(1989, 5, 18, 2015, 5, 17)]
        [TestCase(1989, 5, 19, 2015, 5, 17)]
        public void Validate_WithValidDateOfBirth_ExpectNoException(
            int year,
            int month,
            int dayOfMonth,
            int todayYear,
            int todayMonth,
            int todayDayOfMonth)
        {
            // Arrange
            var classUnderTest =
                new Student(null, null)
                {
                    Today = new DateTime(todayYear, todayMonth, todayDayOfMonth),
                    DateOfBirth = new DateTime(year, month, dayOfMonth),
                };

            // Act
            classUnderTest.Validate();

            // Assert
            Assert.Pass("No exception thrown.");
        }

        [TestCase(1999, 5, 18, 2015, 5, 17)]
        [TestCase(2003, 7, 19, 2015, 5, 17)]
        [TestCase(1989, 5, 17, 2015, 5, 17)]
        [TestCase(1989, 5, 16, 2015, 5, 17)]
        [TestCase(1951, 7, 19, 2015, 5, 17)]
        [TestCase(2015, 5, 17, 2015, 5, 17)]
        [TestCase(2015, 5, 18, 2015, 5, 17)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Validate_WithInvalidDateOfBirth_ExpectInvalidOperationException_v1(
            int year,
            int month,
            int dayOfMonth,
            int todayYear,
            int todayMonth,
            int todayDayOfMonth)
        {
            // Arrange
            var classUnderTest =
                new Student(null, null)
                {
                    Today = new DateTime(todayYear, todayMonth, todayDayOfMonth),
                    DateOfBirth = new DateTime(year, month, dayOfMonth),
                };

            // Act
            classUnderTest.Validate();

            // Assert
            Assert.Fail(
                "Exception was not thrown for DOB {0} applying on {1}", 
                classUnderTest.DateOfBirth.ToShortDateString(),
                classUnderTest.Today.ToShortDateString());
        }

        [TestCase(1999, 5, 18, 2015, 5, 17)]
        [TestCase(2003, 7, 19, 2015, 5, 17)]
        [TestCase(1989, 5, 17, 2015, 5, 17)]
        [TestCase(1989, 5, 16, 2015, 5, 17)]
        [TestCase(1951, 7, 19, 2015, 5, 17)]
        [TestCase(2015, 5, 17, 2015, 5, 17)]
        [TestCase(2015, 5, 18, 2015, 5, 17)]
        public void Validate_WithInvalidDateOfBirth_ExpectInvalidOperationException_v2(
            int year,
            int month,
            int dayOfMonth,
            int todayYear,
            int todayMonth,
            int todayDayOfMonth)
        {
            // Arrange
            var classUnderTest =
                new Student(null, null)
                {
                    Today = new DateTime(todayYear, todayMonth, todayDayOfMonth),
                    DateOfBirth = new DateTime(year, month, dayOfMonth),
                };

            // Act
            TestDelegate act = () => classUnderTest.Validate();

            // Assert
            Assert.Throws<InvalidOperationException>(act,
                "Exception was not thrown for DOB {0} applying on {1}",
                classUnderTest.DateOfBirth.ToShortDateString(),
                classUnderTest.Today.ToShortDateString());
        }

        [TestCase(13)]
        [TestCase(47)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Save_WithInvalidDateOfBirth_ExpectInvalidOperationException_v1(
            int age)
        {
            // Arrange
            var today = new DateTime(2003, 5, 17);

            var classUnderTest =
                new Student(null, null)
                {
                    Today = today,
                    DateOfBirth = today.AddYears(-1 * age),
                };

            // Act
            classUnderTest.Save();

            // Assert
            Assert.Fail(
                "Exception was not thrown for age {0}", age);
        }

        [TestCase(13)]
        [TestCase(47)]
        public void Save_WithInvalidDateOfBirth_ExpectInvalidOperationException_v2(
            int age)
        {
            // Arrange
            var today = new DateTime(2003, 5, 17);

            var classUnderTest =
                new Student(null, null)
                {
                    Today = today,
                    DateOfBirth = today.AddYears(-1 * age),
                };

            // Act
            TestDelegate act = () => classUnderTest.Save();

            // Assert
            Assert.Throws<InvalidOperationException>(act,
                "Exception was not thrown for age {0}", age);
        }

        [Test]
        public void Save_WithValidNewStudent_ExpectIndividualDalCreateIsCalledOnce()
        {
            // Arrange
            var today = new DateTime(2003, 5, 17);

            const int ExpectedStudentId = 897931;

            var stubStudentRepo = new Mock<IRepository<StudentEntity>>();
            stubStudentRepo
                .Setup(e => e.Retrieve(ExpectedStudentId))
                .Returns(default(StudentEntity));
            stubStudentRepo
                .Setup(e => e.Create(It.IsAny<StudentEntity>()))
                .Returns(ExpectedStudentId);

            var mockIndividualRepo = new Mock<IRepository<IndividualEntity>>();
            mockIndividualRepo
                .Setup(e => e.Create(It.IsAny<IndividualEntity>()))
                .Returns(ExpectedStudentId);

            var classUnderTest =
                new Student(mockIndividualRepo.Object, stubStudentRepo.Object)
                {
                    Today = today,
                    DateOfBirth = today.AddYears(-19),
                };

            // Act
            classUnderTest.Save();

            // Assert
            Assert.AreEqual(ExpectedStudentId, classUnderTest.Id);
            mockIndividualRepo
                .Verify(e => e.Create(It.IsAny<IndividualEntity>()), Times.Once());
        }

        [Test]
        public void Save_WithAnExistingStudent_ExpectIndividualDalUpdateIsCalledOnce()
        {
            // Arrange
            var today = new DateTime(2003, 5, 17);

            const int ExpectedStudentId = 897931;
            var studentEntity = new StudentEntity { Id = ExpectedStudentId, };

            var stubStudentRepo = new Mock<IRepository<StudentEntity>>();
            stubStudentRepo
                .Setup(e => e.Retrieve(ExpectedStudentId))
                .Returns(studentEntity);

            var mockIndividualRepo = new Mock<IRepository<IndividualEntity>>();
            mockIndividualRepo
                .Setup(e => e.Update(It.IsAny<IndividualEntity>()));

            var classUnderTest =
                new Student(mockIndividualRepo.Object, stubStudentRepo.Object)
                {
                    Id = ExpectedStudentId,
                    Today = today,
                    DateOfBirth = today.AddYears(-19),
                };

            // Act
            classUnderTest.Save();

            // Assert
            Assert.AreEqual(ExpectedStudentId, classUnderTest.Id);
            mockIndividualRepo
                .Verify(e => e.Update(It.IsAny<IndividualEntity>()), Times.Once());
        }

        [Test]
        public void Save_WithAnExistingStudentImproperlyCreated_ExpectInvalidOperationException()
        {
            // Arrange
            var today = new DateTime(2003, 5, 17);

            const int ExpectedStudentId = 897931;

            var stubIndividualRepo = new Mock<IRepository<IndividualEntity>>();
            stubIndividualRepo
                .Setup(e => e.Update(It.IsAny<IndividualEntity>()));

            var stubStudentRepo = new Mock<IRepository<StudentEntity>>();
            stubStudentRepo
                .Setup(e => e.Retrieve(ExpectedStudentId))
                .Returns(default(StudentEntity));
            stubStudentRepo
                .Setup(e => e.Create(It.IsAny<StudentEntity>()))
                .Returns(23);

            var classUnderTest =
                new Student(stubIndividualRepo.Object, stubStudentRepo.Object)
                {
                    Id = ExpectedStudentId,
                    Today = today,
                    DateOfBirth = today.AddYears(-19),
                };

            // Act
            TestDelegate act = () => classUnderTest.Save();

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
