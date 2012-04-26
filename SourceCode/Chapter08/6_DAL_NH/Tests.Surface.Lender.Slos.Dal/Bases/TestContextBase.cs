namespace Tests.Surface.Lender.Slos.Dal.Bases
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;

    using FluentNHibernate.Cfg;

    using global::Lender.Slos.NHibernate;

    using NDbUnit.Core;
    using NDbUnit.Core.SqlClient;

    using NHibernate;

    [ExcludeFromCodeCoverage]
    public abstract class TestContextBase : IDisposable
    {
        public const string DefaultConnectionString =
            @"Data Source=(local)\SQLExpress;Initial Catalog=Lender.Slos;Integrated Security=True";

        public const string DefaultProjectPath = @"Tests\Surface\Lender.Slos.Dal\Bases";

        public const string DefaultXmlSchemaFilename = @"Lender.Slos.DataSet.xsd";

        public const string DefaultDatabaseInitializationSql = "database.initialization.sql";

        // Constructor
        protected TestContextBase()
        {
            ProjectPath = DefaultProjectPath;
            Provider = DatabaseProvider.SqlClient;
        }

        protected TestContextBase(Type typeOfClassUnderTest)
            : this()
        {
            ClassUnderTest = typeOfClassUnderTest;
        }

        ~TestContextBase()
        {
            Dispose(false);
        }

        public enum DatabaseProvider
        {
            [Description("System.Data.SqlClient")]
            SqlClient = 1,

            [Description("System.Data.SqlServerCe")]
            SqlCe = 2,

            [Description("System.Data.SQLite")]
            SqLite = 3,
        }

        public DatabaseProvider Provider { get; set; }

        public string ConnectionString { get; set; }

        public string ProjectPath { get; set; }

        public string FolderName { get; set; }

        public Type ClassUnderTest { get; set; }

        // NHibernate support
        public ISessionFactory SessionFactory { get; private set; }

        // Override in derived class to provide another DataSet filename.
        public virtual string XmlSchemaFilename
        {
            get
            {
                return null;
            }
        }

        public string GetRunnerConnectionString()
        {
            var connectionString = 
                ConfigurationManager
                .ConnectionStrings["Tests.Surface.Runner"];

            return connectionString != null
                       ? connectionString.ConnectionString
                       : DefaultConnectionString;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NHibernate support
        public void CreateSessionFactory(bool buildSchema = false)
        {
            try
            {
                SessionFactory = NHibernateModule
                    .CreateSessionFactory(ConnectionString, GetMappings(), buildSchema);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Trace.TraceError("Exception: {0}", exception.ToString());
                throw;
            }
        }

        // NHibernate support
        public ISession GetSession()
        {
            return SessionFactory != null
                ? SessionFactory.OpenSession()
                : null;
        }

        public void SetupTestDatabase(
            string xmlDataFilename,
            bool executeCleanupScript = true,
            string cleanupScript = null)
        {
            var database = InitializeDatabase(cleanupScript);

            var xmlSchemaFile = GetFilePath(
                ProjectPath, 
                @"Bases\Data", 
                DefaultXmlSchemaFilename);
            if (!string.IsNullOrEmpty(XmlSchemaFilename))
            {
                xmlSchemaFile = GetSchemaPath(XmlSchemaFilename);
            }

            database.ReadXmlSchema(xmlSchemaFile);

            var xmlFile = GetDataPath(xmlDataFilename);
            database.ReadXml(xmlFile);

            database.PerformDbOperation(DbOperationFlag.CleanInsertIdentity);
        }

        internal void Initialize(
            string projectPath)
        {
            InitializeConnection();
            InitializeDatabase(projectPath);
        }

        internal void InitializeConnection()
        {
            switch (Provider)
            {
                case DatabaseProvider.SqlClient:
                    if (string.IsNullOrEmpty(ConnectionString))
                    {
                        ConnectionString = GetRunnerConnectionString();
                    }

                    break;
                default:
                    throw new InvalidOperationException(
                        string.Format("Provider '{0}' is not supported.", Provider));
            }
        }

        // NHibernate support
        protected virtual Action<MappingConfiguration> GetMappings()
        {
            return null;
        }

        internal TData Retrieve<TData>(
            string columnName,
            string tableName,
            string whereClause)
        {
            if (string.IsNullOrWhiteSpace(columnName)) throw new ArgumentException("IsNullOrWhiteSpace", "whereClause");
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("IsNullOrWhiteSpace", "tableName");
            if (string.IsNullOrWhiteSpace(whereClause)) throw new ArgumentException("IsNullOrWhiteSpace", "whereClause");

            using (var sqlConnection = new SqlConnection(GetRunnerConnectionString()))
            {
                var dataSet = new DataSet();

                var stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat(
                    "SELECT [{0}] FROM [dbo].[{1}] WHERE {2}",
                    columnName,
                    tableName,
                    whereClause);

                var command = sqlConnection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = stringBuilder.ToString();

                sqlConnection.Open();

                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataSet);

                command.Parameters.Clear();

                var returnValue = default(TData);
                if (dataSet.Tables.Count > 0)
                {
                    var table = dataSet.Tables[0];
                    if (table.Rows.Count == 0)
                    {
                        throw new InvalidOperationException("Query returned zero records");
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        returnValue = row.Field<TData>(columnName);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Query returned no results");
                }

                return returnValue;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Clean up all managed resources
            }

            // Clean up any unmanaged resources here.
            // NHibernate support
            if (SessionFactory != null)
            {
                SessionFactory.Dispose();
                SessionFactory = null;
            }
        }

        private static string GetFilePath(
            string projectPath,
            string relativePath, 
            string filename)
        {
            var path = Path.Combine(relativePath, filename);

            // Running in the ReSharper runner.
            var relativeToBinPath = Path.Combine(Environment.CurrentDirectory, @"..\..\");
            var filePath = Path.Combine(relativeToBinPath, path);
            if (File.Exists(filePath)) return filePath;
            filePath = Path.Combine(Environment.CurrentDirectory, path);
            if (File.Exists(filePath)) return filePath;

            // Running in the command line test runner.
            var commandLinePath = Path.Combine(
                Environment.CurrentDirectory,
                projectPath);
            filePath = Path.Combine(commandLinePath, path);

            return File.Exists(filePath) ? filePath : path;
        }

        private INDbUnitTest CreateDbInstance()
        {
            switch (Provider)
            {
                case DatabaseProvider.SqlClient:
                    return new SqlDbUnitTest(ConnectionString);
                default:
                    throw new InvalidOperationException(
                        string.Format("Provider '{0}' is not supported.", Provider));
            }
        }

        private INDbUnitTest InitializeDatabase(
            string cleanupScript = null)
        {
            var database = CreateDbInstance();

            var filename = GetFilePath(
                ProjectPath,
                @"Bases\Scripts",
                cleanupScript ?? DefaultDatabaseInitializationSql);

            if (File.Exists(filename))
            {
                database.Scripts.AddSingle(filename);

                database.ExecuteScripts();
            }

            database.Scripts.ClearAll();

            return database;
        }

        private string GetSchemaPath(
            string filename)
        {
            var xmlSchemaPath = string.Format(@"{0}\Data", FolderName);

            return GetFilePath(ProjectPath, xmlSchemaPath, filename);
        }

        private string GetDataPath(
            string filename)
        {
            var xmlDataPath = string.Format(@"{0}\Data", FolderName ?? ".");

            return GetFilePath(ProjectPath, xmlDataPath, filename);
        }
    }
}