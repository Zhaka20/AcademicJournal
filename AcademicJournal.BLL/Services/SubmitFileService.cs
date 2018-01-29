using AcademicJournal.AbstractBLL.AbstractServices;
using System;
using AcademicJournal.BLL.Services.Common;
using AcademicJournal.BLL.Services.Common.Interfaces;
using AcademicJournal.FrontToBLL_DTOs.DTOs;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class SubmitFileService : BasicCRUDService<SubmitFileDTO, int>, ISubmitFileService, IDisposable
    {
        protected readonly IGenericService<SubmitFileService, int> service;
        public SubmitFileService(IGenericService<SubmitFileService, int> service, IGenericDTOService<SubmitFileDTO, int> dtoService) : base(dtoService)
        {
            this.service = service;
        }

        public void Dispose()
        {
            IDisposable dispose = dtoService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
            dispose = service as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
        }
    }
}
