namespace Lender.Slos.ImplicitTyping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Controller
    {
        private readonly IRepository<ApplicationEntity> _repository;

        public Controller(IRepository<ApplicationEntity> repository)
        {
            _repository = repository;
        }

        public ApplicationCollection Search(string lastNameCriteria)
        {
            if (lastNameCriteria == null)
            {
                throw new ArgumentNullException("lastNameCriteria");
            }

            var entities = _repository
                .Query<ApplicationEntity>()
                .Where(s => s.LastName.StartsWith(lastNameCriteria));

            return Convert(entities.ToList());
        }

        private static ApplicationCollection Convert(IEnumerable<ApplicationEntity> entities)
        {
            var collection = new ApplicationCollection();

            foreach (var entity in entities)
            {
                collection.Add(
                    new Application(entity.Id)
                        {
                            LastName = entity.LastName,
                            FirstName = entity.FirstName,
                            MiddleInitial = entity.MiddleInitial,
                            Suffix = entity.Suffix,
                            DateOfBirth = entity.DateOfBirth,
                            DateOnApplication = entity.DateOnApplication,
                            Principal = entity.Principal,
                            AnnualPercentageRate = entity.AnnualPercentageRate,
                            TotalPayments = entity.TotalPayments,
                        });
            }

            return collection;
        }
    }
}