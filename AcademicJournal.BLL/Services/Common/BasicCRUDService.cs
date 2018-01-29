using AcademicJournal.AbstractBLL.AbstractServices.Common;
using AcademicJournal.BLL.Services.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DALAbstraction.AbstractRepositories;

namespace AcademicJournal.BLL.Services.Common
{
    public abstract class BasicCRUDService<TEntity, TKey> : IBasicCRUDService<TEntity, TKey>
    {
        protected readonly IGenericDTOService<TEntity, TKey> dtoService;
        private ISubmissionRepository repository;

        public BasicCRUDService(ISubmissionRepository repository)
        {
            this.repository = repository;
        }

        public BasicCRUDService(IGenericDTOService<TEntity, TKey> dtoService)
        {
            this.dtoService = dtoService;
        }

        public void Create(TEntity dto)
        {
            dtoService.Create(dto);
        }

        public void Delete(TEntity dto)
        {
            dtoService.Delete(dto);
        }

        public Task DeleteByIdAsync(TKey id)
        {
            return dtoService.DeleteByIdAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dtoService.GetAll();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return dtoService.GetAllAsync();
        }

        public Task<TEntity> GetByIdAsync(TKey id)
        {
            return dtoService.GetByIdAsync(id);
        }

        public Task SaveChangesAsync()
        {
            return dtoService.SaveChangesAsync();
        }

        public void Update(TEntity dto)
        {
            dtoService.Update(dto);
        }
    }
}
