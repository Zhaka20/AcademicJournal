using AcademicJournal.AbstractBLL.AbstractServices;
using AcademicJournal.AbstractBLL.AbstractServices.Common;
using AcademicJournal.DataModel.Models;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class WorkDayService : IWorkDayService
    {
        protected readonly IGenericService<WorkDay, int> genericService;
        public WorkDayService(IGenericService<WorkDay, int> genericService)
        {
            this.genericService = genericService;
        }


    }
}
