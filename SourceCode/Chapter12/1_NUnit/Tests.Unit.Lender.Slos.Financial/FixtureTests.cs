namespace Tests.Unit.Lender.Slos.Financial
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class FixtureTests
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            Console.WriteLine("Fixture setup");
        }

        [TestFixtureTearDown]
        public void FixtureTeardown()
        {
            Console.WriteLine("Fixture teardown");
        }

        [SetUp]
        public void TestSetup()
        {
            Console.WriteLine("Before-test");
        }

        [TearDown]
        public void TestTeardown()
        {
            Console.WriteLine("After-test");
        }

        [Test]
        public void TestMethod_NoParameters()
        {
            Console.WriteLine("Executing 'TestMethod_NoParameters'");
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void TestMethod_WithParameters(int index)
        {
            Console.WriteLine("Executing 'TestMethod_WithParameters' {0}", index);
        }
    }
}
