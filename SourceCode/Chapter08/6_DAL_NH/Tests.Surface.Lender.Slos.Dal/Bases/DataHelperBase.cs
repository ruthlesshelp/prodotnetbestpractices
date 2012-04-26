namespace Tests.Surface.Lender.Slos.Dal.Bases
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    using NDbUnit.Core.SqlClient;

    [ExcludeFromCodeCoverage]
    public abstract class DataHelperBase<TContext>
        where TContext : TestContextBase, new()
    {
        protected void ExportTestDataFromDatabase(
            string projectPath = null,
            string outputFolderName = null,
            string outputFilename = null,
            string connectionString = null,
            string xmlSchemaFilename = null,
            Type classUnderTest = null)
        {
            using (var context = new TContext())
            {
                context.Provider = TestContextBase.DatabaseProvider.SqlClient;
                context.ConnectionString = connectionString ?? TestContextBase.DefaultConnectionString;

                context.ProjectPath = projectPath ?? TestContextBase.DefaultProjectPath;
                if (outputFolderName != null) context.FolderName = outputFolderName;

                if (classUnderTest != null) context.ClassUnderTest = classUnderTest;

                var database =
                    new SqlDbUnitTest(context.ConnectionString);

                database.ReadXmlSchema(
                    !string.IsNullOrEmpty(xmlSchemaFilename)
                        ? xmlSchemaFilename
                        : Path.Combine(@"..\..\Bases\Data",
                        TestContextBase.DefaultXmlSchemaFilename));

                var dataSet = database.GetDataSetFromDb();

                var fileName = string.Format(
                    @"..\..\{0}\Data\{1}",
                    context.FolderName,
                    outputFilename ?? string.Format(
                        "{0}Tests_ExportedData.xml",
                        context.ClassUnderTest.Name));

                dataSet.WriteXml(fileName);
            }
        }
    }
}