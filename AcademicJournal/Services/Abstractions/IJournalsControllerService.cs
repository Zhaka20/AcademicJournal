using AcademicJournal.ViewModels.Journals;
using AcademicJournal.ViewModels.WorkDays;
using System;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IJournalsControllerService : IDisposable
    {
        Task<JournalFillViewModel> GetJournalFillViewModelAsync(int journalId);
        WorkDayCreateViewModel GetCreateWorkDayViewModel(int journalId);
        Task CreateWorkDayAsync(WorkDayCreateViewModel viewModel);
        Task<JournalIndexViewModel> GetJournalsIndexViewModelAsync();
        Task<JournalDetailViewModel> GetJournalDetailsViewModelAsync(int journalId);
        CreateJournalViewModel GetCreateJournalViewModel(string mentorId);
        Task<int> CreateJournalAsync(CreateJournalViewModel viewModel);
        Task<EditJournalViewModel> GetEditJournalViewModelAsync(int journalId);
        Task UpdateJournalAsync(EditJournalViewModel viewModel);
        Task<DeleteJournalViewModel> GetDeleteJournalViewModelAsync(int journalId);
        Task DeleteJournalAsync(int journalId);
    }
}
