using AcademicJournal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AcademicJournal.Services.Abstractions
{
    interface IAssignmentsController : IDisposable
    {
        Task<AssignmentsIndexViewModel> GetAssignmentsIndexViewModelAsync();
        Task<AssignmentDetailsViewModel> GetAssignmentsDetailsViewModelAsync(int assignmentId);
        Task<MentorAssignmentsVM> GetMentorAssignmentsViewModelAsync(string mentorId);
        CreateAssigmentVM GetCreateAssignmentViewModel();
        Task CreateAssignmentAsync(Controller controller, string mentorId, CreateAssigmentVM inputModel, HttpPostedFileBase file);
        Task<CreateAndAssignToSingleUserVM> GetCreateAndAssignToSingleUserViewModelAsync(string studentId);
        Task CreateAndAssignToSingleUserAsync(Controller controller, string studentId,CreateAndAssignToSingleUserVM inputModel, HttpPostedFileBase file);
        Task<AssignmentEdtiViewModel> GetAssignmentEdtiViewModelAsync(int assignmentId);
        Task UpdateAssingmentAsync(AssignmentEdtiViewModel inputModel);
        Task<DeleteAssigmentViewModel> GetDeleteAssigmentViewModelAsync(int assignmentId);
        Task DeleteAssignmentAsync(int assignmentId);
        Task<IFileStreamWithName> GetAssignmentFileAsync(Controller server, int assignmentId);
        Task<AssignmentsRemoveStudentVM> GetAssignmentsRemoveStudentVMAsync(int assignmentId, string studentId);
        Task RemoveStudentFromAssignmentAsync(int assignmentId, string studentId);
        Task<AssignToStudentVM> GetAssignToStudentViewModelAsync(string studentId);
        Task AssignToStudentAsync(string id, List<int> assignmentIds);
        Task<AssignToStudentsVM> GetAssignToStudentsViewModelAsync(int assigmentIds);
        Task AssignToStudentsAsync(int assigmentId, List<string> studentIds);
    }
}
