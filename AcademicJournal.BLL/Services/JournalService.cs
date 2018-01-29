using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;
using AcademicJournal.AbstractBLL.AbstractServices;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.BLL.Services.Common;
using AcademicJournal.BLL.Services.Common.Interfaces;
using AcademicJournal.FrontToBLL_DTOs.DTOs;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class JournalService : BasicCRUDService<JournalDTO, int>, IJournalService, IDisposable
    {
        protected readonly IGenericService<Journal, int> service;
        public JournalService(IGenericService<Journal, int> service,IGenericDTOService<JournalDTO, int> dtoService) : base(dtoService)
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
