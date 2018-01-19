using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Abstract
{
    public interface IAssignmentService : IDisposable
    {
        Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();
        Task<Assignment> GetAssignmentByIdAsync(int id);
        Task<IEnumerable<Assignment>> FindAsync(Expression<Func<Assignment, bool>> predicate);
        Assignment FindSingle(Expression<Func<Assignment, bool>> predicate);
        void UpdateAssignment(Assignment assignment);
        void CreateAssignment(Assignment assignment);
        void DeleteAssignment(Assignment assignment);
        IQueryable<Assignment> AsQueriable();
        Task SaveChangesAsync();
    }
}
