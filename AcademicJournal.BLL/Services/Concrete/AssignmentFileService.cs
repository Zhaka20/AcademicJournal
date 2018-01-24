using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class AssignmentFileService : GenericService<AssignmentFile, int>, IAssignmentFileService
    {
        public AssignmentFileService(IGenericRepository<AssignmentFile, int> repository) : base(repository)
        {
        }
    }
}
