namespace Tests.Unit.Lender.Slos.Financial
{
    using System;

    using MbUnit.Framework;

    [TestFixture]
    public class FixtureTests
    {
        [FixtureSetUp]
        public void FixtureSetup()
        {
            Console.WriteLine("Fixture setup");
        }

        [FixtureTearDown]
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

        [Test]
        [Row(0)]
        [Row(1)]
        [Row(2)]
        public void TestMethod_WithParameters(int index)
        {
            Console.WriteLine("Executing 'TestMethod_WithParameters' {0}", index);
        }
    }
}
