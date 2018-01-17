using AcademicJournal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IJournalsControllerService : IDisposable
    {
        Task<JournalFillVM> GetJournalFillViewModelAsync(int journalId);
        CreateWorkDayViewModel GetCreateWorkDayViewModel(int journalId);
        Task CreateWorkDayAsync(CreateWorkDayViewModel viewModel);
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
