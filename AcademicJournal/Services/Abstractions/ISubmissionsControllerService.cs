using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Controller.Submissions;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AcademicJournal.Services.Abstractions
{
    public interface ISubmissionsControllerService : IDisposable
    {
        Task<IndexViewModel> GetSubmissionsIndexViewModelAsync();
        Task<AssignmentViewModel> GetAssignmentSubmissionsViewModelAsync(int assignmentId);
        Task<DetailsViewModel> GetSubmissionDetailsViewModelAsync(int assignmentId, string studentId);
        Task<EditViewModel> GetEditSubmissionViewModelAsync(int assignmentId, string studentId);
        Task UpdateSubmissionAsync(EditViewModel viewModel);
        Task<DeleteViewModel> GetDeleteSubmissionViewModelAsync(int assignmentId, string studentId);
        Task DeleteSubmissionAsync(int assignmentId, string studentId);
        Task<IFileStreamWithInfo> GetSubmissionFileAsync(Controller server,int assignmentId, string studentId);
        Task<bool> ToggleSubmissionCompleteStatusAsync(int assignmentId, string studentId);
        Task<EvaluateViewModel> GetSubmissionEvaluateViewModelAsync(int assignmentId, string studentId);
        Task EvaluateSubmissionAsync(EvaluateInputModel inputModel);
        Task UploadFileAsync(Controller controller, HttpPostedFileBase file, int assignmentId, string studentId);
        Task<Submission> GetSubmissionAsync(int assignmentId, string studentId);
    }
}
