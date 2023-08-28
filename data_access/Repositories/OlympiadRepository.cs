using data_access.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace data_access.Repositories
{
    public class OlympiadRepository<TEntity> : IRepository<TEntity>  where TEntity : class
    {
        private static OlympiadDBContext? context;
        private DbSet<TEntity> dbSet;
        private bool disposedValue;
        private static int instanceCoutn = 0;

        public OlympiadRepository()
        {
            instanceCoutn++;
            context ??= new OlympiadDBContext();
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity? GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity? entityToDelete = dbSet.Find(id);
            if(entityToDelete != null) Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context?.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            if(context!= null) context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void SaveChanges(TEntity entityToUpdate)
        {
            context?.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    instanceCoutn--;
                    if (instanceCoutn == 0) context?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
