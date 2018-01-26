using AcademicJournal.ViewModels;
using AcademicJournal.ViewModels.Controller.Assignments;
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
        Task<IndexViewModel> GetAssignmentsIndexViewModelAsync();
        Task<DetailsViewModel> GetAssignmentsDetailsViewModelAsync(int assignmentId);
        Task<MentorViewModel> GetMentorAssignmentsViewModelAsync(string mentorId);
        CreateViewModel GetCreateAssignmentViewModel();
        Task<int> CreateAssignmentAsync(Controller controller, string mentorId, CreateViewModel inputModel, HttpPostedFileBase file);
        Task<CreateAndAssignToSingleUserViewModel> GetCreateAndAssignToSingleUserViewModelAsync(string studentId);
        Task<int> CreateAndAssignToSingleUserAsync(Controller controller, string studentId, CreateViewModel inputModel, HttpPostedFileBase file);
        Task<EdtiViewModel> GetAssignmentEdtiViewModelAsync(int assignmentId);
        Task UpdateAssingmentAsync(EdtiViewModel inputModel);
        Task<DeleteViewModel> GetDeleteAssigmentViewModelAsync(int assignmentId);
        Task DeleteAssignmentAsync(int assignmentId);
        Task<IFileStreamWithInfo> GetAssignmentFileAsync(int assignmentId);
        Task<RemoveStudentViewModel> GetAssignmentsRemoveStudentVMAsync(int assignmentId, string studentId);
        Task RemoveStudentFromAssignmentAsync(int assignmentId, string studentId);
        Task<AssignToStudentViewModel> GetAssignToStudentViewModelAsync(string studentId);
        Task AssignToStudentAsync(string id, List<int> assignmentIds);
        Task<AssignToStudentsViewModel> GetAssignToStudentsViewModelAsync(int assigmentId);
        Task AssignToStudentsAsync(int assigmentId, List<string> studentIds);
        Task<StudentsAndSubmissionsListViewModel> GetStudentsAndSubmissionsListVMAsync(int assingmentId);
    }
}
