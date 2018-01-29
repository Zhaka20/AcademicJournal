using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademicJournal.AbstractBLL.AbstractServices.Common
{
    public interface IBasicCRUDService<TEntity, TKey>
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        void Update(TEntity dto);
        void Create(TEntity dto);
        void Delete(TEntity dto);
        Task DeleteByIdAsync(TKey id);
        Task SaveChangesAsync();
    }
}
