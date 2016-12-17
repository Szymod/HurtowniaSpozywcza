using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext context;
        protected DbSet<T> dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual T Create()
        {
            return dbSet.Create();
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual T GetById(long id)
        {
            return dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = context.Entry(entity);

            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;

            else
                dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = context.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
                dbSet.Attach(entity);

            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = context.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
                dbEntityEntry.State = EntityState.Deleted;

            else
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            Delete(entity);
        }

        public virtual void Delete(long id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            Delete(entity);
        }

       public void Refresh(T entity)
       {
            context.Entry(entity).Reload();
       }

    }
}

