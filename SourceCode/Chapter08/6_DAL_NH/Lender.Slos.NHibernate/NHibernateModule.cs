namespace Lender.Slos.NHibernate
{
    using System;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using global::NHibernate;
    using global::NHibernate.Tool.hbm2ddl;

    public class NHibernateModule
    {
        public static ISessionFactory CreateSessionFactory(
            string connectionString,
            Action<MappingConfiguration> mappings = null, 
            bool buildSchema = false)
        {
            IPersistenceConfigurer database = 
                MsSqlConfiguration.MsSql2008.ConnectionString(connectionString);

            var fluentConfiguration = Fluently
                .Configure()
                .Database(database)
                .Mappings(mappings ?? GetMappings());

            if (buildSchema)
            {
                fluentConfiguration = fluentConfiguration
                    .ExposeConfiguration(
                        config => new SchemaExport(config).Create(false, true));
            }

            return fluentConfiguration.BuildSessionFactory();
        }

        public static ISession OpenSession(string connectionString)
        {
            return CreateSessionFactory(connectionString).OpenSession();
        }

        private static Action<MappingConfiguration> GetMappings()
        {
            return m =>
            {
                m.FluentMappings.Add(typeof(IndividualMap));
                m.FluentMappings.Add(typeof(StudentMap));
                m.FluentMappings.Add(typeof(ApplicationMap));
            };
        }
    }
}
