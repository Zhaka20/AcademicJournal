using AcademicJournal.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AcademicJournal.Services.Abstractions
{
    public interface IMentorsControllerService : IDisposable
    {
        Task<MentorsHomeVM> GetHomeViewModelAsync(string mentorId);
        Task<MentorsListVM> GetMentorsListViewModelAsync();
        Task<MentorDetailsVM> GetDetailsViewModelAsync(string mentorId);
        Task<MentorAcceptStudentVM> GetAcceptStudentViewModelAsync(string mentorId);
        Task AcceptStudentAsync(string studentId,string mentorId);
        Task<MentorExpelStudentVM> GetExpelStudentViewModelAsync(string studentId);
        Task RemoveStudentAsync(string studentId,string mentorId);
        Task<StudentMentorVM> GetStudentViewModelAsync(string id);
        CreateMentorVM GetCreateMentorViewModel();
        Task<IdentityResult> CreateMentorAsync(CreateMentorVM viewModel);
        Task<EditMentorVM> GetEditViewModelAsync(string id);
        Task UpdateMentorAsync(EditMentorVM vm);
        Task<DeleteMentorVM> GetDeleteViewModel(string id);
        Task DeleteMenotorAsync(string id);

    }
}
