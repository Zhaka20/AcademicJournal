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
    public class SubmissionService : GenericService<Submission, int>
    {
        public SubmissionService(IGenericRepository<Submission, int> repository) : base(repository)
        {
        }
    }
}
