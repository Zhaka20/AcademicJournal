using AcademicJournal.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class AssignmentService : GenericService<Assignment, int>, IAssignmentService
    {
        public AssignmentService(IAssignmentRepository repository) : base(repository)
        {
        }
    }
}
