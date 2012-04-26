namespace Lender.Slos.Model
{
    using System;

    using Lender.Slos.Dao;
    using Lender.Slos.Financial;

    public class Application
    {
        private readonly IRepository<ApplicationEntity> _applicationRepo;

        public static int MaxTermInMonths
        {
            get
            {
                return Calculator.MaxTermInMonths;
            }
        }

        public Application()
            : this(RepositoryFactory.IndividualRepo, RepositoryFactory.StudentRepo, RepositoryFactory.ApplicationRepo)
        {
        }

        internal Application(
            IRepository<IndividualEntity> individualRepo,
            IRepository<StudentEntity> studentRepo,
            IRepository<ApplicationEntity> applicationRepo)
        {
            _applicationRepo = applicationRepo;

            this.Student = new Student(individualRepo, studentRepo);
        }

        public int Id { get; private set; }

        public Student Student { get; private set; }

        public decimal Principal { get; set; }

        public decimal AnnualPercentageRate { get; set; }

        public int TotalPayments { get; set; }

        public decimal RatePerMonth
        {
            get
            {
                return Calculator
                    .ComputeRatePerPeriod(this.AnnualPercentageRate);
            }
        }

        public static Application FindById(int id)
        {
            var application = new Application();

            application.Get(id);

            return application;
        }

        public void Get(int id)
        {
            var applicationEntity = _applicationRepo.Retrieve(id);
            if (applicationEntity == null)
            {
                throw new InvalidOperationException("Application not found.");
            }

            this.Student.Get(applicationEntity.StudentId);

            this.LoadData(applicationEntity);
        }

        public void Save()
        {
            this.Student.Save();

            var applicationEntity =
                new ApplicationEntity
                    {
                        Id = Id,
                        StudentId = Student.Id,
                        AnnualPercentageRate = AnnualPercentageRate,
                        Principal = Principal,
                        TotalPayments = TotalPayments
                    };

            if (applicationEntity.Id > 0)
            {
                _applicationRepo.Update(applicationEntity);
            }
            else
            {
                Id = _applicationRepo.Create(applicationEntity);
            }
        }

        public void Remove()
        {
            var applicationId = this.Id;
            if (applicationId <= 0)
            {
                throw new InvalidOperationException("Application Id is invalid.");
            }

            var applicationEntity = _applicationRepo.Retrieve(applicationId);
            _applicationRepo.Delete(applicationEntity);
        }

        public decimal ComputePayment(int termInMonths)
        {
            return Calculator
                .ComputePaymentPerPeriod(this.Principal, this.RatePerMonth, termInMonths);
        }

        internal void LoadData(ApplicationEntity applicationEntity)
        {
            Id = applicationEntity.Id;

            Principal = applicationEntity.Principal;
            AnnualPercentageRate = applicationEntity.AnnualPercentageRate;
            TotalPayments = applicationEntity.TotalPayments;
        }
    }
}
