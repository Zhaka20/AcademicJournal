using AcademicJournal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    interface IJournalsControllerService : IDisposable
    {
        Task<JournalFillVM> GetJournalFillViewModelAsync(int id);
        CreateWorkDayViewModel GetCreateWorkDayViewModelAsync();
        Task CreateWorkDayAsync(CreateWorkDayViewModel viewModel);
        Task<JournalIndexViewModel> GetJournalsIndexViewModelAsync();
        Task<JournalDetailVM> GetJournalDetailsViewModelAsync(int journalId);
        Task<CreateJournalVM> GetCreateJournalViewModelAsync();
        Task CreateJournalAsync(CreateJournalVM viewModel);
        Task<EditJournalVM> GetEditJournalViewModelAsync(int journalId);
        Task UpdateJournalAsync(EditJournalVM viewModel);
        Task<DeleteJournalVM> GetDeleteJournalViewModelAsync(int journalId);
        Task DeleteJournalAsync(int journalId);
    }
}
