using AcademicJournal.DALAbstraction.AbstractRepositories.Common;
using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DALAbstraction.AbstractRepositories
{
    public interface ICommentRepository : IGenericRepository<Comment, int>
    {
    }
}
