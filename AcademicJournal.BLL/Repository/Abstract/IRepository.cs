using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Repository.Abstract
{
    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        void Add(TEntity item);
        void Delete(TEntity item);
        Task Delete(TKey item);
        void Update(TEntity item);
        Task SaveChangesAsync();
    }
}
