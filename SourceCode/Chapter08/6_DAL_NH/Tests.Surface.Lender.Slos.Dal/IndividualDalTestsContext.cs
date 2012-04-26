namespace Tests.Surface.Lender.Slos.Dal
{
    using System;

    using global::Lender.Slos.Dal;
    using global::Lender.Slos.Dao;

    using Tests.Surface.Lender.Slos.Dal.Bases;
    using Tests.Surface.Lender.Slos.Dal.Helpers;

    public class IndividualDalTestsContext : TestContextBase
    {
        public IndividualDalTestsContext()
            : base(typeof(IndividualDal))
        {
        }

        internal IndividualEntity CreateValidEntity()
        {
            return new IndividualEntity
            {
                LastName = TestDataHelper.BuildNameString(150),
                FirstName = TestDataHelper.BuildNameString(),
                MiddleName = TestDataHelper.BuildNameString(),
                Suffix = TestDataHelper.BuildNameString(),
                DateOfBirth =
                    TestDataHelper
                    .BuildSqlDateTime(DateTime.Today.AddYears(-26), DateTime.Today.AddYears(-18))
                    .Value
                    .Date,
            };
        }

        internal IndividualDal CreateInstance()
        {
            // Original Dal: 
            //     return new IndividualDal(this.GetRunnerConnectionString());
            return new IndividualDal(this.GetSession());
        }
    }
}