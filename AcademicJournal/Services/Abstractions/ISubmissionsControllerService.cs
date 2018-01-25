using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Submissions;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AcademicJournal.Services.Abstractions
{
    public interface ISubmissionsControllerService : IDisposable
    {
        Task<SubmissionsIndexViewModel> GetSubmissionsIndexViewModelAsync();
        Task<AssignmentSumbissionsViewModel> GetAssignmentSubmissionsViewModelAsync(int assignmentId);
        Task<SubmissionDetailsViewModel> GetSubmissionDetailsViewModelAsync(int assignmentId, string studentId);
        Task<EditSubmissionViewModel> GetEditSubmissionViewModelAsync(int assignmentId, string studentId);
        Task UpdateSubmissionAsync(EditSubmissionViewModel viewModel);
        Task<DeleteSubmissionViewModel> GetDeleteSubmissionViewModelAsync(int assignmentId, string studentId);
        Task DeleteSubmissionAsync(int assignmentId, string studentId);
        Task<IFileStreamWithInfo> GetSubmissionFileAsync(Controller server,int assignmentId, string studentId);
        Task<bool> ToggleSubmissionCompleteStatusAsync(int assignmentId, string studentId);
        Task<EvaluateSubmissionViewModel> GetSubmissionEvaluateViewModelAsync(int assignmentId, string studentId);
        Task EvaluateSubmissionAsync(EvaluateSubmissionInputModel inputModel);
        Task UploadFileAsync(Controller controller, HttpPostedFileBase file, int assignmentId, string studentId);
        Task<Submission> GetSubmissionAsync(int assignmentId, string studentId);
    }
}
