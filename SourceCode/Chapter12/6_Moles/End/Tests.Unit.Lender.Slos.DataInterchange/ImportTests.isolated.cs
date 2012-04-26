using Lender.Slos.DataInterchange;
using Lender.Slos.DataInterchange.Moles;

namespace Tests.Unit.Lender.Slos.DataInterchange
{
    using System;
    using System.IO;
    using Microsoft.Moles.Framework;

    using NUnit.Framework;

    public class ImportTests
    {
        [TestCase("1FBF377361CD.dat", "{BEB5C694-8302-4397-990E-D1CA29C163F1}")]
        [TestCase("A72498755DD2.dat", "{4E9C15FD-5966-4F69-8263-16E11F239873}")]
        public void Load_WithValidFile_ExpectProperData(
            string filename, 
            string data)
        {
            using (MolesContext.Create())
            {
                // Arrange
                var expectedData = data;
                var fileInfo = new FileInfo(filename);

                MFileSystem.ReadAllTextFileInfo = info =>
                    {
                        Console.WriteLine("filename: {0}", info.Name);
                        Assert.IsTrue(info.Name == filename);
                        return data;
                    };

                var classUnderTest = new Import();

                // Act
                classUnderTest.Load(fileInfo);

                // Assert
                Assert.AreEqual(expectedData, classUnderTest.Data);
            }
        }
    }
}
