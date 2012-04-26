namespace Lender.Slos.ImplicitTyping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ApplicationRepository : IRepository<ApplicationEntity>
    {
        private readonly List<ApplicationEntity> applications;

        public ApplicationRepository()
        {
            applications = BuildList();
        }

        public int Create(ApplicationEntity entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationEntity Retrieve(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ApplicationEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ApplicationEntity> Query<T>()
        {
            return applications.AsQueryable();
        }

        private List<ApplicationEntity> BuildList()
        {
            var list = new List<ApplicationEntity>();

            list.Add(
                new ApplicationEntity
                    {
                        Id = 1,
                        LastName = "Public",
                        FirstName = "John",
                        MiddleInitial = "Q",
                        Suffix = "Sr.",
                        DateOfBirth = new DateTime(1993, 11, 13),
                        DateOnApplication = DateTime.Today.AddDays(-5),
                        Principal = 6337,
                        AnnualPercentageRate = 1.93m,
                        TotalPayments = 360,
                    });
            list.Add(
                new ApplicationEntity
                {
                    Id = 1,
                    LastName = "Public",
                    FirstName = "Jane",
                    MiddleInitial = "P",
                    DateOfBirth = new DateTime(1991, 7, 11),
                    DateOnApplication = DateTime.Today.AddDays(-1),
                    Principal = 7883,
                    AnnualPercentageRate = 1.79m,
                    TotalPayments = 360,
                });

            list.Add(
                new ApplicationEntity
                {
                    Id = 1,
                    LastName = "Smith",
                    FirstName = "Jane",
                    MiddleInitial = "R",
                    DateOfBirth = new DateTime(1993, 11, 13),
                    DateOnApplication = DateTime.Today.AddDays(-3),
                    Principal = 2203,
                    AnnualPercentageRate = 2.71m,
                    TotalPayments = 360,
                });

            return list;
        }
    }
}