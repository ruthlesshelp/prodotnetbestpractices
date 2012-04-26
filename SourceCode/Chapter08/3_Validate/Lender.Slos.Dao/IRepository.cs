namespace Lender.Slos.Dao
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        int Create(TEntity entity);

        TEntity Retrieve(int id);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
