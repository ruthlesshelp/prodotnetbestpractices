namespace Tests.Surface.Lender.Slos.Dal
{
    using global::Lender.Slos.Dal;
    using global::Lender.Slos.Dao;

    using Tests.Surface.Lender.Slos.Dal.Bases;
    using Tests.Surface.Lender.Slos.Dal.Helpers;

    public class ApplicationDalTestsContext 
        : TestContextBase
    {
        public ApplicationDalTestsContext()
            : base(typeof(ApplicationDal))
        {
        }

        internal ApplicationEntity CreateValidEntity(int studentId)
        {
            return new ApplicationEntity
            {
                StudentId = studentId,
                Principal = TestDataHelper.BuildMoney(),
                AnnualPercentageRate = TestDataHelper.BuildPercentageRate(),
                TotalPayments = TestDataHelper.BuildCount(),
            };
        }

        internal ApplicationDal CreateInstance()
        {
            return new ApplicationDal(this.GetRunnerConnectionString());
        }
    }
}