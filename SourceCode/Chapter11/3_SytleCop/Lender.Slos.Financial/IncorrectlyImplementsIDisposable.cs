namespace Lender.Slos.Financial
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    // Violates the FxCop rule CA1063: ImplementIDisposableCorrectly
    //     http://msdn.microsoft.com/en-us/library/ms244737(v=VS.100).aspx
    public class IncorrectlyImplementsIDisposable : IDisposable
    {
        private StringBuilder stringBuilder = new StringBuilder(20);
        private IntPtr unmanagedMemory = Marshal.AllocHGlobal(100);

        public IncorrectlyImplementsIDisposable()
        {
            stringBuilder.AppendFormat("HashCode: {0}", unmanagedMemory.GetHashCode());
        }

        ~IncorrectlyImplementsIDisposable()
        {
            if (string.IsNullOrEmpty(stringBuilder.ToString()))
            {
                // Violates the FxCop rule CA1065: DoNotRaiseExceptionsInUnexpectedLocations
                //     http://msdn.microsoft.com/en-us/library/bb386039.aspx
                throw new InvalidOperationException("DoNotRaiseExceptionsInUnexpectedLocations	CA1065");
            }

            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Dispose(bool) should be protected, virtual, or sealed.
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Clean up all managed resources
                if (stringBuilder != null)
                {
                    stringBuilder.Clear();
                    stringBuilder = null;
                }
            }

            // Clean up any unmanaged resources here.
            if (unmanagedMemory != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(unmanagedMemory);
                unmanagedMemory = IntPtr.Zero;
            }
        }
    }
}
