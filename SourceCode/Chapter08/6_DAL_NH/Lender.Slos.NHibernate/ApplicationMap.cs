namespace Lender.Slos.NHibernate
{
    using FluentNHibernate.Mapping;

    using Lender.Slos.Dao;

    internal class ApplicationMap 
        : ClassMap<ApplicationEntity>
    {
        public ApplicationMap()
        {
            Table("Application");

            Id(x => x.Id);

            Map(x => x.StudentId);
            Map(x => x.Principal);
            Map(x => x.AnnualPercentageRate);
            Map(x => x.TotalPayments);
        }
    }
}