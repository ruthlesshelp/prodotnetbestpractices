namespace Tests.Surface.Lender.Slos.Dal
{
    using System;

    using NUnit.Framework;

    using Tests.Surface.Lender.Slos.Dal.Bases;

    public class IndividualDalTests
        : SurfaceTestingBase<IndividualDalTestsContext>
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

        [TestCase("IndividualDalTests_Scenario01.xml", 4)]
        public void Create_WithValidIndividual_ExpectProperIdIsReturned(
            string xmlDataFilename,
            int expectedId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity();

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            var actual = classUnderTest.Create(entity);

            // Assert
            Assert.AreEqual(expectedId, actual);
        }

        [TestCase("IndividualDalTests_Scenario01.xml")]
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

        [TestCase("IndividualDalTests_Scenario01.xml", 2)]
        public void Create_WithIdAlreadySet_ExpectInvalidOperationException(
            string xmlDataFilename,
            int individualId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity();
            entity.Id = individualId;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Create(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("IndividualDalTests_Scenario01.xml")]
        public void Create_WithInvalidLastName_ExpectInvalidOperationException(
            string xmlDataFilename)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity();
            entity.LastName = null;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Create(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("IndividualDalTests_Scenario01.xml", 3)]
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

        [TestCase("IndividualDalTests_Scenario01.xml", "New Last Name", 3)]
        public void Update_WhenLastNameIsChanged_ExpectProperLastName(
            string xmlDataFilename,
            string expectedLastName,
            int individualId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity();
            entity.Id = individualId;
            entity.LastName = expectedLastName;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            classUnderTest.Update(entity);

            // Assert
            var actual = TestFixtureContext.Retrieve<string>(
                "LastName",
                "Individual",
                string.Format("[Id] = {0}", individualId));
            Assert.AreEqual(expectedLastName, actual);
        }

        [TestCase("IndividualDalTests_Scenario01.xml")]
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

        [TestCase("IndividualDalTests_Scenario01.xml", 0)]
        [TestCase("IndividualDalTests_Scenario01.xml", -1)]
        [TestCase("IndividualDalTests_Scenario01.xml", 2)]
        public void Update_WithInvalidId_ExpectInvalidOperationException(
            string xmlDataFilename,
            int individualId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity();
            entity.Id = individualId;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Update(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("IndividualDalTests_Scenario01.xml", 3)]
        public void Delete_WithValidId_ExpectInvalidOperationException(
            string xmlDataFilename,
            int individualId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity();
            entity.Id = individualId;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            classUnderTest.Delete(entity);

            // Assert
            TestDelegate retrieve =
                () =>
                TestFixtureContext.Retrieve<string>(
                    "LastName",
                    "Individual",
                    string.Format("[Id] = {0}", individualId));
            Assert.Throws<InvalidOperationException>(retrieve);
        }

        [TestCase("IndividualDalTests_Scenario01.xml", 0)]
        [TestCase("IndividualDalTests_Scenario01.xml", -1)]
        [TestCase("IndividualDalTests_Scenario01.xml", 2)]
        public void Delete_WithInvalidId_ExpectInvalidOperationException(
            string xmlDataFilename,
            int individualId)
        {
            // Arrange
            TestFixtureContext.SetupTestDatabase(xmlDataFilename);

            var entity = TestFixtureContext.CreateValidEntity();
            entity.Id = individualId;

            var classUnderTest = TestFixtureContext.CreateInstance();

            // Act
            TestDelegate act = () => classUnderTest.Delete(entity);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [TestCase("IndividualDalTests_Scenario01.xml")]
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
