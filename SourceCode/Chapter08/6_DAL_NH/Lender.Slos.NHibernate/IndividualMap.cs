namespace Lender.Slos.NHibernate
{
    using FluentNHibernate.Mapping;

    using Lender.Slos.Dao;

    internal class IndividualMap
        : ClassMap<IndividualEntity>
    {
        public IndividualMap()
        {
            Table("Individual");

            Id(c => c.Id);

            Map(c => c.LastName);
            Map(c => c.FirstName);
            Map(c => c.MiddleName);
            Map(c => c.Suffix);
            Map(c => c.DateOfBirth);
        }
    }
}