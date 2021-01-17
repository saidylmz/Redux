using Redux.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Redux.CORE.DAL.EntityFramework
{
    public class EFRepositoryBase<TEntity, TContext> : IRepository<TEntity>
         where TEntity : BaseEntity, new()
         where TContext : DbContext, new()
    {
        private readonly TContext context = EFSingletonContext<TContext>.Instance;

        public void Add(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return context.Set<TEntity>().Where(filter).SingleOrDefault();
        }
        public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null)
            {
                return context.Set<TEntity>().ToList();
            }
            else
            {
                return context.Set<TEntity>().Where(filter).ToList();
            }
        }
        public void Remove(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
