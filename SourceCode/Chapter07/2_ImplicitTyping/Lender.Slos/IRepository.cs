namespace Lender.Slos.ImplicitTyping
{
    using System.Linq;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        int Create(TEntity entity);

        TEntity Retrieve(int id);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<ApplicationEntity> Query<T>();
    }
}
