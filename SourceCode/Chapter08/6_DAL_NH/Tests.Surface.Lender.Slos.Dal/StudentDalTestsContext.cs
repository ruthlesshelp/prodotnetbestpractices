namespace Tests.Surface.Lender.Slos.Dal
{
    using global::Lender.Slos.Dal;
    using global::Lender.Slos.Dao;

    using Tests.Surface.Lender.Slos.Dal.Bases;
    using Tests.Surface.Lender.Slos.Dal.Helpers;

    public class StudentDalTestsContext : TestContextBase
    {
        public StudentDalTestsContext()
            : base(typeof(StudentDal))
        {
        }

        internal StudentEntity CreateValidEntity(int individualId)
        {
            return new StudentEntity
            {
                Id = individualId,
                HighSchoolName = TestDataHelper.BuildNameString(40),
                HighSchoolCity = TestDataHelper.BuildNameString(40),
                HighSchoolState = TestDataHelper.BuildNameString(2),
            };
        }

        internal StudentDal CreateInstance()
        {
            // Original Dal: 
            //     return new StudentDal(this.GetRunnerConnectionString());
            return new StudentDal(this.GetSession());
        }
    }
}