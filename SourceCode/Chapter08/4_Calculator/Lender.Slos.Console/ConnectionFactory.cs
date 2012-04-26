namespace Lender.Slos.ConsoleApp
{
    using System;
    using System.Configuration;

    public static class ConnectionFactory
    {
        public static string GetConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Lender.Slos.Express"];

            if (connectionString == null || 
                string.IsNullOrWhiteSpace(connectionString.ConnectionString))
            {
                throw new InvalidOperationException(
                    "ConnectionString 'Lender.Slos.Express' was not found or is not properly configured.");
            }

            return connectionString.ConnectionString;
        }
    }
}