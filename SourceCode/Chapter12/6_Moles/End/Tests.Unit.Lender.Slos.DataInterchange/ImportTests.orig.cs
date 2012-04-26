using Lender.Slos.DataInterchange;

namespace Tests.Unit.Lender.Slos.DataInterchange
{
    using NUnit.Framework;

    public class ImportTests
    {
        private const string FileName = "temporary.dat";

        private const string Data = "{BEB5C694-8302-4397-990E-D1CA29C163F1}";

        [SetUp]
        public void TestSetup()
        {
            System.IO.File.WriteAllText(FileName, Data);
        }

        [TearDown]
        public void TestTeardown()
        {
            if (System.IO.File.Exists(FileName))
            {
                System.IO.File.Delete(FileName);
            }
        }

        [Test]
        public void Load_WithValidFile_ExpectProperData()
        {
            // Arrange
            var fileInfo = new System.IO.FileInfo(FileName);

            var classUnderTest = new Import();

            // Act
            classUnderTest.Load(fileInfo);

            // Assert
            Assert.AreEqual(Data, classUnderTest.Data);
        }
    }
}
