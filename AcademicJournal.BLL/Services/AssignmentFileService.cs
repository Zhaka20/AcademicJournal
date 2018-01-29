using AcademicJournal.AbstractBLL.AbstractServices;
using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.FrontToBLL_DTOs.DTOs;
using AcademicJournal.AbstractBLL.AbstractServices.Common;
using AcademicJournal.BLL.Services.Common.Interfaces;
using System;
using AcademicJournal.BLL.Services.Common;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class AssignmentFileService : BasicCRUDService<AssignmentFileDTO, int>,IAssignmentFileService, IDisposable
    {
        protected readonly IGenericService<AssignmentFile, int> service;
        public AssignmentFileService(IGenericService<AssignmentFile, int> service, IGenericDTOService<AssignmentFileDTO, int> dtoService) : base(dtoService)
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
