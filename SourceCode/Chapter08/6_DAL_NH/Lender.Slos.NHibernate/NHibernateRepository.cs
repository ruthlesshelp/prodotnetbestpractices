namespace Lender.Slos.NHibernate
{
    using Lender.Slos.Dao;

    using global::NHibernate;

    public class NHibernateRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        public NHibernateRepository(
            ISession session)
        {
            Session = session;
        }

        public ISession Session { get; private set; }

        public int Create(TEntity entity)
        {
            object retval;
            using (var transaction = Session.BeginTransaction())
            {
                retval = Session.Save(entity);
                transaction.Commit();
            }

            Session.Flush();
            Session.Evict(entity);

            return (int)retval;
        }

        public TEntity Retrieve(int id)
        {
            var entity = Session.Get<TEntity>(id);

            Session.Evict(entity);

            return entity;
        }

        public void Update(TEntity entity)
        {
            using (var transaction = Session.BeginTransaction())
            {
                Session.Update(entity);
                transaction.Commit();
            }

            Session.Flush();
            Session.Evict(entity);
        }

        public void Delete(TEntity entity)
        {
            using (var transaction = Session.BeginTransaction())
            {
                Session.Delete(entity);
                transaction.Commit();
            }

            Session.Flush();
            Session.Evict(entity);
        }
    }
}
