using AcademicJournal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IWorkDaysControllerService : IDisposable
    {
        Task<WorkDayIndexViewModel> GetWorkDaysIndexViewModel();
        Task<WorkDaysDetailsVM> GetWorkDayDetailsViewModelAsync(int workDayId);
        WorkDayCreateViewModel GetCreateWorkDayViewModel(int journalId);
        Task<int> CreateWorkDayAsync(WorkDaysDetailsVM inputModel);
        Task<WorkDayEditViewModel> GetWorkDayEditViewModelAsync(int workDayId);
        Task WorkDayUpdateAsync(WorkDayEditViewModel inputModel);
        Task<WorkDayDeleteViewModel> GetWorkDayDeleteViewModelAsync(int id);
        Task WorkDayDeleteAsync(int workDayId);
        Task<WorDayAddAttendeesViewModel> GetWorDayAddAttendeesViewModelAsync(int workDayId);
        Task AddWorkDayAttendeesAsync(int workDayId, List<string> attendeeIds);
        Task CheckAsLeftAsync(int workDayId, List<int> attendaceIds);
    }
}
