using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class SubmitFileService : GenericService<SubmitFile, int>, ISubmitFileService
    {
        public SubmitFileService(ISubmitFileRepository repository) : base(repository)
        {
        }
    }
}
