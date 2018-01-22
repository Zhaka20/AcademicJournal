using AcademicJournal.DALAbstraction.AbstractRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using AcademicJournal.DAL.Context;
using System.Data.Entity;

namespace AcademicJournal.DAL.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class, IDisposable
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
            int? skip = default(int?), int? take = default(int?),
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            GetQueryable(filter, orderBy, skip, take);
            return query.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? skip = default(int?), int? take = default(int?), params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            GetQueryable(filter,orderBy,skip,take);
            return await query.ToListAsync();
        }

        public TEntity GetSingleById(TKey id)
        {
            return dbSet.Find(id);
        }
        public Task<TEntity> GetSingleByIdAsync(TKey id)
        {
            return dbSet.FindAsync(id);
        }
        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public void Update(TEntity entity)
        {
            if(db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            db.Entry(entity).State = EntityState.Modified;
        }
        public void UpdateSelectedProperties(TEntity entity, params Expression<Func<TEntity, object>>[] updateProperties)
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
        public void Delete(TEntity entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        public async Task Delete(TKey id)
        {
            TEntity entity = await dbSet.FindAsync(id);
            Delete(entity);
        }
        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }
        public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }
        public bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }
        public Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).AnyAsync();
        }
        //_________________________________




















        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity,
            bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = default(int?), int? take = default(int?),
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
    }
}
