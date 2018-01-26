using AcademicJournal.AbstractBLL.AbstractServices;
using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class AssignmentFileService : GenericService<AssignmentFile, int>, IAssignmentFileService
    {
        public AssignmentFileService(IAssignmentFileRepository repository) : base(repository)
        {
        }
    }
}
