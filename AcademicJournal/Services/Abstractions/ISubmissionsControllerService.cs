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
    public interface ISubmissionsControllerService : IDisposable
    {
        Task<SubmissionsIndexVM> GetSubmissionsIndexViewModelAsync();
        Task<AssignmentSumbissionsVM> GetAssignmentSubmissionsViewModelAsync(int assignmentId);
        Task<SubmissionDetailsVM> GetSubmissionDetailsViewModelAsync(int assignmentId, string studentId);
        Task<EditSubmissionVM> GetEditSubmissionViewModelAsync(int assignmentId, string studentId);
        Task UpdateSubmissionAsync(EditSubmissionVM viewModel);
        Task<DeleteSubmissionVM> GetDeleteSubmissionViewModelAsync(int assignmentId, string studentId);
        Task DeleteSubmissionAsync(int assignmentId, string studentId);
        Task<IFileStreamWithName> GetSubmissionFileAsync(Controller server,int assignmentId, string studentId);
        Task<bool> ToggleSubmissionCompleteStatusAsync(int assignmentId, string studentId);
        Task<EvaluateSubmissionVM> GetSubmissionEvaluateViewModelAsync(int assignmentId, string studentId);
        Task EvaluateSubmissionAsync(EvaluateSubmissionInputModel inputModel);
        Task UploadFileAsync(Controller controller, HttpPostedFileBase file, int assignmentId, string studentId);
    }
}
