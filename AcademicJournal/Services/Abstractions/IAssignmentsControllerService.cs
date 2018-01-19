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
    public interface IAssignmentsControllerService : IDisposable
    {
        Task<AssignmentsIndexViewModel> GetAssignmentsIndexViewModelAsync();
        Task<AssignmentDetailsViewModel> GetAssignmentsDetailsViewModelAsync(int assignmentId);
        Task<MentorAssignmentsVM> GetMentorAssignmentsViewModelAsync(string mentorId);
        CreateAssigmentVM GetCreateAssignmentViewModel();
        Task<int> CreateAssignmentAsync(Controller controller, string mentorId, CreateAssigmentVM inputModel, HttpPostedFileBase file);
        Task<CreateAndAssignToSingleUserVM> GetCreateAndAssignToSingleUserViewModelAsync(string studentId);
        Task<int> CreateAndAssignToSingleUserAsync(Controller controller, string studentId, CreateAssigmentVM inputModel, HttpPostedFileBase file);
        Task<AssignmentEdtiViewModel> GetAssignmentEdtiViewModelAsync(int assignmentId);
        Task UpdateAssingmentAsync(AssignmentEdtiViewModel inputModel);
        Task<DeleteAssigmentViewModel> GetDeleteAssigmentViewModelAsync(int assignmentId);
        Task DeleteAssignmentAsync(int assignmentId);
        Task<IFileStreamWithInfo> GetAssignmentFileAsync(int assignmentId);
        Task<AssignmentsRemoveStudentVM> GetAssignmentsRemoveStudentVMAsync(int assignmentId, string studentId);
        Task RemoveStudentFromAssignmentAsync(int assignmentId, string studentId);
        Task<AssignToStudentVM> GetAssignToStudentViewModelAsync(string studentId);
        Task AssignToStudentAsync(string id, List<int> assignmentIds);
        Task<AssignToStudentsVM> GetAssignToStudentsViewModelAsync(int assigmentId);
        Task AssignToStudentsAsync(int assigmentId, List<string> studentIds);
        Task<StudentsAndSubmissionsListVM> GetStudentsAndSubmissionsListVMAsync(int assingmentId);
    }
}
