using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Abstract.Common
{
    public interface IGenericService<TEntity, TKey>
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

        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetByCompositeKeysAsync(params object[] keys);
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] updateProperties);
        void Create(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(TKey id);
        Task SaveChangesAsync();
    }
}
