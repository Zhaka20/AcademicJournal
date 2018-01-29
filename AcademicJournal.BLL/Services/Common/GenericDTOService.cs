using AcademicJournal.BLL.Services.Common.Interfaces;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;
using AcademicJournal.Services.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Common
{
    abstract class GenericDTOService<TEntityDTO, TEntity, TKey>: IDisposable
    {
        protected readonly IGenericService<TEntity, TKey> baseService;
        protected readonly IObjectMapper mapper;
        public GenericDTOService(IGenericService<TEntity, TKey> baseService, IObjectMapper mapper)
        {
            this.baseService = baseService;
            this.mapper = mapper;
        }

        public void Create(TEntityDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }
            TEntity model = mapper.Map<TEntityDTO, TEntity>(dto);
            if(model == null)
            {
                throw new ArgumentNullException("could not convert argument type to a proper entity.");
            }
            baseService.Create(model);
        }

        public void Delete(TEntityDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("dto");
            }
            TEntity model = mapper.Map<TEntityDTO, TEntity>(dto);
            if (model == null)
            {
                throw new ArgumentNullException("could not convert argument type to a proper entity.");
            }
            baseService.Delete(model);
        }

        public Task DeleteByIdAsync(TKey id)
        {
            return baseService.DeleteByIdAsync(id);
        }

        public IEnumerable<TEntityDTO> GetAll()
        {
            IEnumerable<TEntity> entities = baseService.GetAll();
            if (entities == null)
            {
                return null;
            }
            IEnumerable<TEntityDTO> result = mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDTO>>(entities);
            return result;
        }

        public async Task<IEnumerable<TEntityDTO>> GetAllAsync()
        {
            IEnumerable<TEntity> entities = await baseService.GetAllAsync();
            if (entities == null)
            {
                return null;
            }
            IEnumerable<TEntityDTO> result = mapper.Map<IEnumerable<TEntity>, IEnumerable<TEntityDTO>>(entities);
            return result;
        }

        public async Task<TEntityDTO> GetByIdAsync(TKey id)
        {
            var entity = await baseService.GetByIdAsync(id);
            if(entity == null)
            {
                return default(TEntityDTO);
            }
            var result = mapper.Map<TEntity, TEntityDTO>(entity);
            return result;
        }

        public Task SaveChangesAsync()
        {
            return baseService.SaveChangesAsync();
        }

        public void Update(TEntityDTO dto)
        {
            if(dto == null)
            {
                throw new ArgumentNullException("dto");
            }
            var entity = mapper.Map<TEntityDTO, TEntity>(dto);
             if (entity == null)
            {
                throw new ArgumentNullException("could not conver the argument to a proper entity object");
            }
            baseService.Update(entity);
        }

        public void Dispose()
        {
            IDisposable dispose = baseService as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
        }
    }
}
