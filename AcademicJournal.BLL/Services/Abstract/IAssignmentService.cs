using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Abstract
{
    public interface IAssignmentService : IDisposable
    {
        Task<IEnumerable<Assignment>> GetAllAssignmentsAsync();
        Task<Assignment> GetAssignmentByIdAsync(int id);
        void UpdateAssignment(Assignment assignment);
        void InsertOrUpdateAssignment(Assignment assignment);
        void CreateAssignment(Assignment assignment);
        void DeleteAssignment(Assignment assignment);
        Task DeleteAssignmentByIdWithSubmissionAndAssignmentFilesAsync(int id);
        Task SaveChangesAsync();
    }
}
