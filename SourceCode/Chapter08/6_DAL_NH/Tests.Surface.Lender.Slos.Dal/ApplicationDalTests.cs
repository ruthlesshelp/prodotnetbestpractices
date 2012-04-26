namespace Tests.Surface.Lender.Slos.Dal
{
    using System;
    using System.Data.SqlTypes;

    using global::Lender.Slos.Dal;
    using global::Lender.Slos.Dao;

    using NUnit.Framework;

    using Tests.Surface.Lender.Slos.Dal.Bases;

    public class ApplicationDalTests 
        : SurfaceTestingBase<ApplicationDalTestsContext>
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

        [TestCase("ApplicationDalTests_Scenario01.xml", 3, 6)]
        public void Create_WithValidApplication_ExpectProperIdReturned(
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

        [TestCase("ApplicationDalTests_Scenario01.xml")]
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

        [TestCase("ApplicationDalTests_Scenario01.xml", 3, 2)]
        public void Create_WithIdAlreadySet_ExpectInvalidOperationException(
            string xmlDataFilename,
            int studentId,
            int applicationId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(studentId);
            entity.Id = applicationId;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Create(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("ApplicationDalTests_Scenario01.xml", 0)]
        public void Create_WithInvalidStudentId_ExpectInvalidOperationException(
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

        [TestCase("ApplicationDalTests_Scenario01.xml", 3)]
        public void Create_WithInvalidHighSchoolState_ExpectInvalidOperationException(
            string xmlDataFilename,
            int studentId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(studentId);
            entity.Principal = SqlMoney.MaxValue.ToDecimal();

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Create(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("ApplicationDalTests_Scenario01.xml", 5)]
        public void Retrieve_WhenEntityWithIdStoredInDatabase_ExpectEntityHavingIdIsRetrieved(
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

        [TestCase("ApplicationDalTests_Scenario01.xml", 7)]
        public void Retrieve_WhenEntityWithIdNotStoredInDatabase_ExpectEntityIsNull(
            string xmlDataFilename,
            int id)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            var actual = classUnderTest.Retrieve(id);

            // Assert
            Assert.IsNull(actual);
        }

        [TestCase("ApplicationDalTests_Scenario01.xml", 0)]
        [TestCase("ApplicationDalTests_Scenario01.xml", -1)]
        [TestCase("ApplicationDalTests_Scenario01.xml", -2)]
        public void Retrieve_WithInvalidId_ExpectArgumentOutOfRangeException(
            string xmlDataFilename,
            int id)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Retrieve(id);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [TestCase("ApplicationDalTests_Scenario01.xml", 3, 5, 12346.03)]
        public void Update_WhenPrincipalIsChanged_ExpectProperPrincipal(
            string xmlDataFilename,
            int individualId,
            int applicationId,
            decimal expectedPrincipal)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(individualId);
            entity.Id = applicationId;
            entity.Principal = expectedPrincipal;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            classUnderTest.Update(entity);

            // Assert
            var actual = TestFixtureContext.Retrieve<decimal>(
                "Principal",
                "Application",
                string.Format("[Id] = {0}", applicationId));
            Assert.AreEqual(expectedPrincipal, actual);
        }

        [TestCase("ApplicationDalTests_Scenario01.xml")]
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

        [TestCase("ApplicationDalTests_Scenario01.xml", 3, 0)]
        [TestCase("ApplicationDalTests_Scenario01.xml", 3, -1)]
        [TestCase("ApplicationDalTests_Scenario01.xml", 3, 2)]
        public void Update_WithInvalidId_ExpectInvalidOperationException(
            string xmlDataFilename,
            int studentId,
            int applicationId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(studentId);
            entity.Id = applicationId;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Update(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("ApplicationDalTests_Scenario01.xml", 3, 5)]
        public void Delete_WithValidId_ExpectInvalidOperationException(
            string xmlDataFilename,
            int individualId,
            int applicationId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(individualId);
            entity.Id = applicationId;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            classUnderTest.Delete(entity);

            // Assert
            TestDelegate retrieve =
                () =>
                TestFixtureContext.Retrieve<decimal>(
                    "Principal",
                    "Application",
                    string.Format("[Id] = {0}", applicationId));
            Assert.Throws<InvalidOperationException>(retrieve);
        }

        [TestCase("ApplicationDalTests_Scenario01.xml", 0)]
        [TestCase("ApplicationDalTests_Scenario01.xml", -1)]
        [TestCase("ApplicationDalTests_Scenario01.xml", 2)]
        public void Delete_WithInvalidId_ExpectInvalidOperationException(
            string xmlDataFilename,
            int applicationId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity(0);
            entity.Id = applicationId;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Delete(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("ApplicationDalTests_Scenario01.xml")]
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
