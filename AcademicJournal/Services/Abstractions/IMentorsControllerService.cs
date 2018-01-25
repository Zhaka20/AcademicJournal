using AcademicJournal.ViewModels.Mentors;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IMentorsControllerService : IDisposable
    {
        Task<MentorsHomeViewModel> GetHomeViewModelAsync(string mentorId);
        Task<MentorsListViewModel> GetMentorsListViewModelAsync();
        Task<MentorDetailsViewModel> GetDetailsViewModelAsync(string mentorId);
        Task<MentorAcceptStudentViewModel> GetAcceptStudentViewModelAsync(string mentorId);
        Task AcceptStudentAsync(string studentId,string mentorId);
        Task<MentorExpelStudentViewModel> GetExpelStudentViewModelAsync(string studentId);
        Task RemoveStudentAsync(string studentId,string mentorId);
        Task<StudentMentorViewModel> GetStudentViewModelAsync(string id);
        CreateMentorViewModel GetCreateMentorViewModel();
        Task<IdentityResult> CreateMentorAsync(CreateMentorViewModel viewModel);
        Task<EditMentorViewModel> GetEditViewModelAsync(string id);
        Task UpdateMentorAsync(EditMentorViewModel vm);
        Task<DeleteMentorViewModel> GetDeleteViewModel(string id);
        Task DeleteMenotorAsync(string id);

    }
}
