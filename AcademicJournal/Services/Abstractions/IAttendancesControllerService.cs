using AcademicJournal.ViewModels.Attendances;
using System;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IAttendancesControllerService : IDisposable
    {
        Task<AttendanceIndexViewModel> GetAttendancesIndexViewModelAsync();
        Task<AttendancesDetailsViewModel> GetAttendancesDetailsViewModelAsync(int attendanceId);
        Task<EditAttendanceViewModel> GetEditAttendanceViewModelAsync(int attendanceId);
        Task UpdateAttendanceAsync(EditAttendanceViewModel inputModel);
        Task<DeleteAttendanceViewModel> GetDeleteAttendanceViewModelAsync(int attendanceId);
        Task DeleteAttendanceAsync(DeleteAttendanceInputModel inputModel);
    }
}
