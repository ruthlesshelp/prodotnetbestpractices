using Lender.Slos.Dal;

namespace Tests.Unit.Lender.Slos.Dal
{
    using NDbUnit.Core;
    using NDbUnit.Core.SqlClient;

    using NUnit.Framework;

    public class IndividualDalTests
    {
        private const string ConnectionString =
            @"Data Source=(local)\SQLExpress;Initial Catalog=Lender.Slos;Integrated Security=True";

        private INDbUnitTest _database;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _database = new SqlDbUnitTest(ConnectionString);

            _database.ReadXmlSchema(@"..\..\Data\Lender.Slos.DataSet.xsd");
            _database.ReadXml(@"..\..\Data\IndividualDalTests_Scenario01.xml");
        }

        [TestFixtureTearDown]
        public void FixtureTeardown()
        {
            _database.PerformDbOperation(DbOperationFlag.DeleteAll);
        }

        [SetUp]
        public void TestSetup()
        {
            _database.PerformDbOperation(DbOperationFlag.CleanInsertIdentity);
        }

        [TestCase(1, "Roosevelt")]
        [TestCase(3, "Smith")]
        [TestCase(5, "Truman")]
        public void Retrieve_WithScenarioDataInDatabase_ExpectProperLastName(
            int id,
            string expectedLastName)
        {
            // Arrange
            var classUnderTest = new IndividualDal(ConnectionString);

            // Act
            var actual = classUnderTest.Retrieve(id);

            // Assert
            Assert.NotNull(actual);
            Assert.AreEqual(expectedLastName, actual.LastName);
        }
    }
}
