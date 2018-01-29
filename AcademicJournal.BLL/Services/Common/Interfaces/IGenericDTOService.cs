using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Common.Interfaces
{
    public interface IGenericDTOService<TEntityDTO, TKey>
    {
        IEnumerable<TEntityDTO> GetAll();
        Task<IEnumerable<TEntityDTO>> GetAllAsync();
        Task<TEntityDTO> GetByIdAsync(TKey id);
        void Update(TEntityDTO dto);
        void Create(TEntityDTO dto);
        void Delete(TEntityDTO dto);
        Task DeleteByIdAsync(TKey id);
        Task SaveChangesAsync();
    }
}
