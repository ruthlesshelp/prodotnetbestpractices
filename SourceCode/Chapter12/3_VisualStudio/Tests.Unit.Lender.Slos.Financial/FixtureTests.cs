namespace Tests.Unit.Lender.Slos.Financial
{
    using System.Diagnostics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FixtureTests
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Trace.WriteLine("Test assembly initialize");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Trace.WriteLine("Test assembly cleanup");
        }

        [ClassInitialize]
        public static void FixtureSetup(TestContext context)
        {
            Trace.WriteLine("Fixture setup");
        }

        [ClassCleanup]
        public static void FixtureTeardown()
        {
            Trace.WriteLine("Fixture teardown");
        }

        [TestInitialize]
        public void TestSetup()
        {
            Trace.WriteLine("Before-test");
        }

        [TestCleanup]
        public void TestTeardown()
        {
            Trace.WriteLine("After-test");
        }

        [TestMethod]
        public void TestMethod_NoParameters()
        {
            Trace.WriteLine("Executing 'TestMethod_NoParameters'");
        }

        [TestMethod]
        public void TestMethod_WithParameters_0()
        {
            TestMethod_WithParameters(0);
        }

        [TestMethod]
        public void TestMethod_WithParameters_1()
        {
            TestMethod_WithParameters(1);
        }

        [TestMethod]
        public void TestMethod_WithParameters_2()
        {
            TestMethod_WithParameters(2);
        }

        private void TestMethod_WithParameters(int index)
        {
            Trace.WriteLine(string.Format("Executing 'TestMethod_WithParameters' {0}", index));
        }
    }
}
