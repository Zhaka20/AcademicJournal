using AcademicJournal.ViewModels.Students;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IStudentsControllerService : IDisposable
    {
        Task<StudentsIndexViewModel> GetIndexViewModelAsync();
        Task<StudentsHomeVM> GetHomeViewModelAsync(string studentId);
        Task<StudentDetailsVM> GetDetailsViewModelAsync(string studentId);
        CreateStudentVM GetCreateStudentViewModel();
        Task<IdentityResult> CreateStudentAsync(CreateStudentVM student);
        Task<EditStudentVM> GetEditStudentViewModelAsync(string studentId);
        Task UpdateStudentAsync(EditStudentVM studentViewModel);
        Task<DeleteStudentVM> GetDeleteStudentViewModelAsync(string studentId);
        Task DeleteStudentAsync(string id);
    }
}
