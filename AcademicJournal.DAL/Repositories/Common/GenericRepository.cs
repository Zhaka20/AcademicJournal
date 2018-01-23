using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using AcademicJournal.DAL.Context;
using System.Data.Entity;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;

namespace AcademicJournal.DAL.Repositories.Common
{
    public abstract class GenericRepository<TEntity, TKey> : IDisposable, IGenericRepository<TEntity, TKey> where TEntity : class
                            
    {
        protected readonly ApplicationDbContext db;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(ApplicationDbContext db)
        {
            this.db = db;
            dbSet = db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity,
            bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            GetQueryable(filter, orderBy, skip, take);
            return query.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            GetQueryable(filter,orderBy,skip,take);
            return await query.ToListAsync();
        }

        public virtual TEntity GetSingleById(TKey id)
        {
            return dbSet.Find(id);
        }
        public virtual Task<TEntity> GetSingleByIdAsync(TKey id)
        {
            return dbSet.FindAsync(id);
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Update(TEntity entity)
        {
            if(db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            db.Entry(entity).State = EntityState.Modified;
        }
        public virtual void UpdateSelectedProperties(TEntity entity, params Expression<Func<TEntity, object>>[] updateProperties)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            foreach (Expression<Func<TEntity, object>> property in updateProperties)
            {
                db.Entry(entity).Property(property).IsModified = true;
            }
        }
        public virtual void Delete(TEntity entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        public virtual async Task Delete(TKey id)
        {
            TEntity entity = await dbSet.FindAsync(id);
            Delete(entity);
        }
        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }
        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }
        public virtual bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }
        public virtual Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).AnyAsync();
        }
        public virtual void SaveChanges()
        {
            db.SaveChanges();
        }
        public virtual Task SaveChangesAsync()
        {
            return db.SaveChangesAsync();
        }

        protected virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity,
            bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (Expression<Func<TEntity, object>> include in includeProperties)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if(includeProperties != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includeProperties)
                    query = query.Include(include);
            }           
            return query.FirstOrDefault(filter);
        }

        public virtual Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includeProperties)
                    query = query.Include(include);
            }
            return query.FirstOrDefaultAsync(filter);
        }

        public virtual void Dispose()
        {
            db.Dispose();
        }
    }
}
