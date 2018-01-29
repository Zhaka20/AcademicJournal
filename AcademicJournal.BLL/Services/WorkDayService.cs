using AcademicJournal.AbstractBLL.AbstractServices;
using AcademicJournal.AbstractBLL.AbstractServices.Common;
using AcademicJournal.BLL.Services.Common;
using AcademicJournal.BLL.Services.Common.Interfaces;
using AcademicJournal.BLL.Services.Concrete;
using AcademicJournal.DataModel.Models;
using AcademicJournal.FrontToBLL_DTOs.DTOs;
using System;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class WorkDayService : BasicCRUDService<WorkDayDTO, int>, IWorkDayService, IDisposable
    {
        protected readonly IGenericService<WorkDay, int> service;
        public WorkDayService(IGenericService<WorkDay, int> service, IGenericDTOService<WorkDayDTO, int> dtoService) : base(dtoService)
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
            if(dispose != null)
            {
                dispose.Dispose();
            }
        }
    }
}
