using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace tinyERP.Dal.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public TEntity Add(TEntity entity)
        {
            return Context.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return Context.Set<TEntity>().AddRange(entities);
        }

        public TEntity Remove(TEntity entity)
        {
            return Context.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            return Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
