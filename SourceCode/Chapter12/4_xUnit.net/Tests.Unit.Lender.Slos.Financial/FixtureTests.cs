namespace Tests.Unit.Lender.Slos.Financial
{
    using System;

    using Xunit;
    using Xunit.Extensions;

    public class FixtureTests
        : IDisposable, IUseFixture<FixtureToUse>
    {
        private FixtureToUse _data;

        public FixtureTests()
        {
            Console.WriteLine("Before-test");
            if (_data == null)
            {
                Console.WriteLine("Fixture is not set.");
            }
        }

        [Fact]
        public void TestMethod_NoParameters()
        {
            if (_data != null)
            {
                Console.WriteLine("Now, the fixture is set.");
            }

            Console.WriteLine("Executing 'TestMethod_NoParameters'");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void TestMethod_WithParameters(int index)
        {
            if (_data != null)
            {
                Console.WriteLine("Now, the fixture is set.");
            }

            Console.WriteLine("Executing 'TestMethod_WithParameters' {0}", index);
        }

        public void Dispose()
        {
            Console.WriteLine("After-test");
        }

        public void SetFixture(FixtureToUse data)
        {
            if (!data.IsSetup)
            {
                data.Setup();
            }

            _data = data;
        }
    }
}
