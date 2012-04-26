namespace Lender.Slos.Model
{
    using Lender.Slos.Dal;
    using Lender.Slos.Dao;

    // Caution: This factory implementation is intended only as an example.
    public static class RepositoryFactory
    {
        public static string ConnectionString { private get; set; }

        internal static IRepository<IndividualEntity> IndividualRepo
        {
            get
            {
                return new IndividualDal(ConnectionString); 
            }
        }

        internal static IRepository<StudentEntity> StudentRepo
        {
            get
            {
                return new StudentDal(ConnectionString); 
            }
        }

        internal static IRepository<ApplicationEntity> ApplicationRepo
        {
            get
            {
                return new ApplicationDal(ConnectionString); 
            }
        }
    }
}