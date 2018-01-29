using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DALAbstraction.AbstractRepositories.Common
{
    public interface IGenericRepository<TEntity, TKey>
    {
        IEnumerable<TEntity> GetAll(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             int? skip = null,
             int? take = null,
             params Expression<Func<TEntity, object>>[] includeProperties
             );

        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includeProperties
            );

        TEntity GetFirstOrDefault(
                Expression<Func<TEntity, bool>> filter = null,
                params Expression<Func<TEntity, object>>[] includeProperties
                );

        Task<TEntity> GetFirstOrDefaultAsync(
                Expression<Func<TEntity, bool>> filter = null,
                params Expression<Func<TEntity, object>>[] includeProperties
                );

        TEntity GetSingleById(TKey id);
        Task<TEntity> GetSingleByIdAsync(TKey id);
        TEntity GetSingleById(params object[] keys);
        Task<TEntity> GetSingleByIdAsync(params object[] keys);

        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);

        bool GetExists(Expression<Func<TEntity, bool>> filter = null);
        Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null);

        void Insert(TEntity entity);
        void Update(TEntity entity);
        void UpdateSelectedProperties(TEntity entity, params Expression<Func<TEntity, object>>[] includeProperties);

        Task Delete(TKey id);
        void Delete(TEntity entity);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
