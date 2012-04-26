namespace Lender.Slos.Nullability
{
    using System;

    public class Application
    {
        public static readonly int MaximumLoanAmount = 17500;

        public static readonly decimal DefaultAnnualPercentageRate = 1.79m;

        public static readonly int DefaultTotalPayments = 360;

        public static readonly DateTime MinimumDateOfBirth = new DateTime(1900, 1, 1);

        private readonly IRepository<ApplicationEntity> _applicationRepo;

        public Application(
            IRepository<ApplicationEntity> applicationRepo,
            Guid? id)
        {
            _applicationRepo = applicationRepo;

            Id = id;
        }

        public Guid? Id { get; private set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string Suffix { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime? DateOnApplication { get; set; }

        public decimal? Principal { get; set; }

        public decimal? AnnualPercentageRate { get; set; }

        public int? TotalPayments { get; set; }

        public void Retrieve(int id)
        {
            var entity = _applicationRepo.Retrieve(id);

            Syncronize(entity);
        }

        public void Save()
        {
            Validate();

            var entity = CreateEntity();

            if (!Id.HasValue)
            {
                entity.Id = _applicationRepo.Create(entity);
            }
            else
            {
                entity.Id = Id.Value;
                _applicationRepo.Update(entity);
            }

            Syncronize(entity);
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(LastName))
            {
                throw new InvalidOperationException("LastName is required.");
            }

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                throw new InvalidOperationException("FirstName is required.");
            }

            if (DateOfBirth < MinimumDateOfBirth)
            {
                throw new InvalidOperationException("DateOfBirth is invalid.");
            }

            if (DateOfBirth >= DateTime.Today)
            {
                throw new InvalidOperationException("DateOfBirth is invalid.");
            }
        }

        private ApplicationEntity CreateEntity()
        {
            return new ApplicationEntity
                {
                    LastName = LastName,
                    FirstName = FirstName,
                    MiddleInitial = MiddleInitial ?? string.Empty,
                    Suffix = Suffix ?? string.Empty,
                    DateOfBirth = DateOfBirth,
                    DateOnApplication = DateOnApplication ?? DateTime.Today,
                    Principal = Principal ?? MaximumLoanAmount,
                    AnnualPercentageRate = AnnualPercentageRate ?? DefaultAnnualPercentageRate,
                    TotalPayments = TotalPayments ?? DefaultTotalPayments,
                };
        }

        private void Syncronize(ApplicationEntity entity)
        {
            Id = entity.Id;
            LastName = entity.LastName;
            FirstName = entity.FirstName;
            MiddleInitial = entity.MiddleInitial;
            Suffix = entity.Suffix;
            DateOfBirth = entity.DateOfBirth;
            DateOnApplication = entity.DateOnApplication;
            Principal = entity.Principal;
            AnnualPercentageRate = entity.AnnualPercentageRate;
            TotalPayments = entity.TotalPayments;
        }
    }
}