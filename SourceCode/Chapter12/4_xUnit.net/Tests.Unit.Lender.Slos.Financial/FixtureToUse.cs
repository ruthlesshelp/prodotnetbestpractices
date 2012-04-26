namespace Tests.Unit.Lender.Slos.Financial
{
    using System;

    public class FixtureToUse : IDisposable
    {
        public bool IsSetup { get; private set; }

        public void Setup()
        {
            IsSetup = true;
            Console.WriteLine("Fixture setup");
        }

        public void Dispose()
        {
            Console.WriteLine();
            Console.WriteLine("Fixture teardown");
        }
    }
}