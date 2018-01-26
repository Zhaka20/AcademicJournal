using AcademicJournal.ViewModels.Controller.Attendances;
using System;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IAttendancesControllerService : IDisposable
    {
        Task<IndexViewModel> GetAttendancesIndexViewModelAsync();
        Task<DetailsViewModel> GetAttendancesDetailsViewModelAsync(int attendanceId);
        Task<EditViewModel> GetEditAttendanceViewModelAsync(int attendanceId);
        Task UpdateAttendanceAsync(EditViewModel inputModel);
        Task<DeleteViewModel> GetDeleteAttendanceViewModelAsync(int attendanceId);
        Task DeleteAttendanceAsync(DeleteInputModel inputModel);
    }
}
