using AcademicJournal.ViewModels.Controller.Journals;
using System;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IJournalsControllerService : IDisposable
    {
        Task<FillViewModel> GetJournalFillViewModelAsync(int journalId);
        ViewModels.WorkDays.CreateViewModel GetCreateWorkDayViewModel(int journalId);
        Task CreateWorkDayAsync(ViewModels.WorkDays.CreateViewModel viewModel);
        Task<ViewModels.Journals.IndexViewModel> GetJournalsIndexViewModelAsync();
        Task<ViewModels.Journals.DetailsViewModel> GetJournalDetailsViewModelAsync(int journalId);
        CreateViewModel GetCreateJournalViewModel(string mentorId);
        Task<int> CreateJournalAsync(ViewModels.Journals.CreateViewModel viewModel);
        Task<ViewModels.Journals.EditViewModel> GetEditJournalViewModelAsync(int journalId);
        Task UpdateJournalAsync(ViewModels.Journals.EditViewModel viewModel);
        Task<ViewModels.Journals.DeleteViewModel> GetDeleteJournalViewModelAsync(int journalId);
        Task DeleteJournalAsync(int journalId);
    }
}
