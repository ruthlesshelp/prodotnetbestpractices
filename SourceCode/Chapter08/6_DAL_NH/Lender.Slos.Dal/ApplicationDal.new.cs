namespace Lender.Slos.Dal
{
    using System;

    using Lender.Slos.Dao;
    using Lender.Slos.NHibernate;

    using global::NHibernate;

    public class ApplicationDal : IRepository<ApplicationEntity>
    {
        private readonly NHibernateRepository<ApplicationEntity> repository;

        public ApplicationDal(string connectionString)
            : this(NHibernateModule.OpenSession(connectionString))
        {
        }

        internal ApplicationDal(ISession session)
        {
            repository = new NHibernateRepository<ApplicationEntity>(session);
        }

        public int Create(ApplicationEntity entity)
        {
            // Guard against invalid arguments.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity.Id > 0)
            {
                throw new InvalidOperationException("Entity is invalid, the Id must be 0.");
            }

            try
            {
                // Returns createdId if a record was created.
                return this.repository.Create(entity);
            }
            catch (Exception exception)
            {
                // Throw an exception if the Id was not returned.
                throw new InvalidOperationException("Failed to create.", exception);
            }
        }

        public ApplicationEntity Retrieve(int id)
        {
            // Guard against invalid arguments.
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            try
            {
                // Retrieve returns null if a record isn't found.
                return repository.Retrieve(id);
            }
            catch (Exception exception)
            {
                // Throw an exception if the Id was not returned.
                throw new InvalidOperationException("Failed to retrieve.", exception);
            }
        }

        public void Update(ApplicationEntity entity)
        {
            // Guard against invalid arguments.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity.Id <= 0)
            {
                throw new InvalidOperationException("Entity is invalid, the Id must be greater than 0.");
            }

            try
            {
                repository.Update(entity);
            }
            catch (Exception exception)
            {
                // Throw an exception if the Id was not returned.
                throw new InvalidOperationException("Failed to update.", exception);
            }
        }

        public void Delete(ApplicationEntity entity)
        {
            // Guard against invalid arguments.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity.Id <= 0)
            {
                throw new InvalidOperationException("Entity is invalid, the Id must be greater than 0.");
            }

            var entityToDelete = this.Retrieve(entity.Id);
            if (entityToDelete == null)
            {
                throw new InvalidOperationException("Entity is invalid, entity not found.");
            }

            try
            {
                repository.Delete(entityToDelete);
            }
            catch (Exception exception)
            {
                // Throw an exception if the Id was not returned.
                throw new InvalidOperationException("Failed to delete.", exception);
            }
        }
    }
}
