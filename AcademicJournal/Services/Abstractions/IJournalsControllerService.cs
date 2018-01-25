using AcademicJournal.ViewModels.Journals;
using AcademicJournal.ViewModels.WorkDays;
using System;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IJournalsControllerService : IDisposable
    {
        Task<JournalFillVM> GetJournalFillViewModelAsync(int journalId);
        WorkDayCreateViewModel GetCreateWorkDayViewModel(int journalId);
        Task CreateWorkDayAsync(WorkDayCreateViewModel viewModel);
        Task<JournalIndexViewModel> GetJournalsIndexViewModelAsync();
        Task<JournalDetailVM> GetJournalDetailsViewModelAsync(int journalId);
        CreateJournalVM GetCreateJournalViewModel(string mentorId);
        Task<int> CreateJournalAsync(CreateJournalVM viewModel);
        Task<EditJournalVM> GetEditJournalViewModelAsync(int journalId);
        Task UpdateJournalAsync(EditJournalVM viewModel);
        Task<DeleteJournalVM> GetDeleteJournalViewModelAsync(int journalId);
        Task DeleteJournalAsync(int journalId);
    }
}
