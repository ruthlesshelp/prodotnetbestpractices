namespace Lender.Slos.NHibernate
{
    using FluentNHibernate.Mapping;

    using Lender.Slos.Dao;

    internal class StudentMap 
        : ClassMap<StudentEntity>
    {
        public StudentMap()
        {
            Table("Student");

            Id(x => x.Id).GeneratedBy.Assigned();

            Map(x => x.HighSchoolName);
            Map(x => x.HighSchoolCity);
            Map(x => x.HighSchoolState);
        }
    }
}