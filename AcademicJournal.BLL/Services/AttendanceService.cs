using AcademicJournal.DataModel.Models;
using System;
using AcademicJournal.AbstractBLL.AbstractServices;
using AcademicJournal.FrontToBLL_DTOs.DTOs;
using AcademicJournal.BLL.Services.Common;
using AcademicJournal.BLL.Services.Common.Interfaces;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class AttendanceService : BasicCRUDService<AttendanceDTO, int>, IAttendanceService, IDisposable
    {
        protected readonly IGenericService<Attendance, int> service;
        public AttendanceService(IGenericService<Attendance, int> service, IGenericDTOService<AttendanceDTO, int> dtoService) : base(dtoService)
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
