using AcademicJournal.AbstractBLL.AbstractServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;
using AcademicJournal.FrontToBLL_DTOs.DTOs;
using AcademicJournal.AbstractBLL.AbstractServices.Common;
using AcademicJournal.BLL.Services.Common;
using AcademicJournal.BLL.Services.Common.Interfaces;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class AssignmentService : BasicCRUDService<AssignmentDTO, int>, IAssignmentService, IDisposable
    {
        protected readonly IGenericService<Assignment, int> service;

        public AssignmentService(IGenericService<Assignment, int> service,IGenericDTOService<AssignmentDTO, int> dtoService) : base(dtoService)
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
