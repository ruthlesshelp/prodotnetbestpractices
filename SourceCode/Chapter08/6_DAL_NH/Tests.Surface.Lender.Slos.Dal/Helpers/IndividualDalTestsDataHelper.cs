namespace Tests.Surface.Lender.Slos.Dal.Helpers
{
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using Tests.Surface.Lender.Slos.Dal.Bases;

    [ExcludeFromCodeCoverage]
    public class IndividualDalTestsDataHelper :
        DataHelperBase<IndividualDalTestsContext>
    {
#if !SUPPRESS_MANUAL_TESTS
        [Test]
        [Ignore("Helper to extract data from the specified SQL Server database")]
#endif
        public void HelperMethod_ExportTestDataFromDatabase()
        {
            ExportTestDataFromDatabase();
        }
    }
}