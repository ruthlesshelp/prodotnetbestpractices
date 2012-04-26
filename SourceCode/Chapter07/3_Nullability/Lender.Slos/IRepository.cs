namespace Lender.Slos.Nullability
{
    using System;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        Guid Create(TEntity entity);

        TEntity Retrieve(int id);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
