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
    public class AssignmentService : GenericService<Assignment, int>
    {
        public AssignmentService(IGenericRepository<Assignment, int> repository) : base(repository)
        {
        }
    }
}
