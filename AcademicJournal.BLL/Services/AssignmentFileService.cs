using AcademicJournal.AbstractBLL.AbstractServices;
using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.FrontToBLL_DTOs.DTOs;
using AcademicJournal.AbstractBLL.AbstractServices.Common;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class AssignmentFileService : IAssignmentFileService
    {
        protected readonly IGenericService<AssignmentFile, int> genericService;
        public AssignmentFileService(IAssignmentFileRepository repository) : base(repository)
        {
        }
    }
}
