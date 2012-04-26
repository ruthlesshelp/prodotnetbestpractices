namespace Tests.Surface.Lender.Slos.Dal.Bases
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SurfaceTestingBase<TContext> : IDisposable
        where TContext : TestContextBase, new()
    {
        ~SurfaceTestingBase()
        {
            Dispose(false);
        }

        protected TContext TestFixtureContext { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Clean up all managed resources
                if (TestFixtureContext != null) TestFixtureContext.Dispose();
            }

            // Clean up any unmanaged resources here.
        }

        protected void SetUpTestFixture(
            TContext context = null, 
            bool buildSchema = false)
        {
            TestFixtureContext = context ?? new TContext();

            TestFixtureContext.InitializeConnection();
        }
    }
}