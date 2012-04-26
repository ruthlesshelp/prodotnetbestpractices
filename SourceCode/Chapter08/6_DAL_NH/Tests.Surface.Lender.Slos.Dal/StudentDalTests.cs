namespace Tests.Surface.Lender.Slos.Dal
{
    using System;

    using NUnit.Framework;

    using Tests.Surface.Lender.Slos.Dal.Bases;

    public class StudentDalTests
        : SurfaceTestingBase<StudentDalTestsContext>
    {
        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            this.SetUpTestFixture();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            Dispose();
        }

        [TestCase("StudentDalTests_Scenario01.xml", 7, 7)]
        public void Create_WithValidStudent_ExpectProperIdIsReturned(
            string xmlDataFilename,
            int studentId,
            int expectedId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(studentId);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            var actual = classUnderTest.Create(entity);

            // Assert
            Assert.AreEqual(expectedId, actual);
        }

        [TestCase("StudentDalTests_Scenario01.xml")]
        public void Create_WithNullEntity_ExpectArgumentNullException(
            string xmlDataFilename)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Create(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [TestCase("StudentDalTests_Scenario01.xml", 2)]
        public void Create_WithIdAlreadySet_ExpectInvalidOperationException(
            string xmlDataFilename,
            int studentId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(studentId);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Create(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("StudentDalTests_Scenario01.xml", 3)]
        public void Create_WithInvalidHighSchoolState_ExpectInvalidOperationException(
            string xmlDataFilename,
            int studentId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(studentId);
            entity.HighSchoolState = "Invalid";

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Create(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("StudentDalTests_Scenario01.xml", 3)]
        public void Retrieve_WithValidId_ExpectEntityHavingIdIsRetrieved(
            string xmlDataFilename,
            int id)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            var actual = classUnderTest.Retrieve(id);

            // Assert
            Assert.NotNull(actual);
            Assert.AreEqual(id, actual.Id);
        }

        [TestCase("StudentDalTests_Scenario01.xml", "New High School Name", 3)]
        [TestCase("StudentDalTests_Scenario01.xml", "", 3)]
        [TestCase("StudentDalTests_Scenario01.xml", " ", 3)]
        public void Update_WhenHighSchoolNameIsChanged_ExpectProperHighSchoolName(
            string xmlDataFilename,
            string expectedHighSchoolName,
            int studentId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(studentId);
            entity.HighSchoolName = expectedHighSchoolName;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            classUnderTest.Update(entity);

            // Assert
            var actual = TestFixtureContext.Retrieve<string>(
                "HighSchoolName",
                "Student",
                string.Format("[Id] = {0}", studentId));
            Assert.AreEqual(expectedHighSchoolName, actual);
        }

        [TestCase("StudentDalTests_Scenario01.xml")]
        public void Update_WithNullEntity_ExpectArgumentNullException(
            string xmlDataFilename)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Update(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [TestCase("StudentDalTests_Scenario01.xml", 0)]
        [TestCase("StudentDalTests_Scenario01.xml", -1)]
        [TestCase("StudentDalTests_Scenario01.xml", 2)]
        public void Update_WithInvalidId_ExpectInvalidOperationException(
            string xmlDataFilename,
            int studentId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(studentId);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Update(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("StudentDalTests_Scenario01.xml", 3)]
        public void Delete_WithValidId_ExpectInvalidOperationException(
            string xmlDataFilename,
            int studentId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(studentId);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            classUnderTest.Delete(entity);

            // Assert
            TestDelegate retrieve =
                () =>
                TestFixtureContext.Retrieve<string>(
                    "HighSchoolName", 
                    "Student", 
                    string.Format("[Id] = {0}", studentId));
            Assert.Throws<InvalidOperationException>(retrieve);
        }

        [TestCase("StudentDalTests_Scenario01.xml", 0)]
        [TestCase("StudentDalTests_Scenario01.xml", -1)]
        [TestCase("StudentDalTests_Scenario01.xml", 2)]
        public void Delete_WithInvalidId_ExpectInvalidOperationException(
            string xmlDataFilename,
            int studentId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(studentId);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Delete(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("StudentDalTests_Scenario01.xml")]
        public void Delete_WithNullEntity_ExpectArgumentNullException(
            string xmlDataFilename)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Delete(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
