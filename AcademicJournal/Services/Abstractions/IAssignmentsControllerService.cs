using AcademicJournal.ViewModels;
using AcademicJournal.ViewModels.Assignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AcademicJournal.Services.Abstractions
{
    public interface IAssignmentsControllerService : IDisposable
    {
        Task<AssignmentsIndexViewModel> GetAssignmentsIndexViewModelAsync();
        Task<AssignmentDetailsViewModel> GetAssignmentsDetailsViewModelAsync(int assignmentId);
        Task<MentorAssignmentsViewModel> GetMentorAssignmentsViewModelAsync(string mentorId);
        CreateAssigmentViewModel GetCreateAssignmentViewModel();
        Task<int> CreateAssignmentAsync(Controller controller, string mentorId, CreateAssigmentViewModel inputModel, HttpPostedFileBase file);
        Task<CreateAndAssignToSingleUserViewModel> GetCreateAndAssignToSingleUserViewModelAsync(string studentId);
        Task<int> CreateAndAssignToSingleUserAsync(Controller controller, string studentId, CreateAssigmentViewModel inputModel, HttpPostedFileBase file);
        Task<AssignmentEdtiViewModel> GetAssignmentEdtiViewModelAsync(int assignmentId);
        Task UpdateAssingmentAsync(AssignmentEdtiViewModel inputModel);
        Task<DeleteAssigmentViewModel> GetDeleteAssigmentViewModelAsync(int assignmentId);
        Task DeleteAssignmentAsync(int assignmentId);
        Task<IFileStreamWithInfo> GetAssignmentFileAsync(int assignmentId);
        Task<AssignmentsRemoveStudentViewModel> GetAssignmentsRemoveStudentVMAsync(int assignmentId, string studentId);
        Task RemoveStudentFromAssignmentAsync(int assignmentId, string studentId);
        Task<AssignToStudentViewModel> GetAssignToStudentViewModelAsync(string studentId);
        Task AssignToStudentAsync(string id, List<int> assignmentIds);
        Task<AssignToStudentsViewModel> GetAssignToStudentsViewModelAsync(int assigmentId);
        Task AssignToStudentsAsync(int assigmentId, List<string> studentIds);
        Task<StudentsAndSubmissionsListViewModel> GetStudentsAndSubmissionsListVMAsync(int assingmentId);
    }
}
