using AcademicJournal.ViewModels.Students;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IStudentsControllerService : IDisposable
    {
        Task<StudentsIndexViewModel> GetIndexViewModelAsync();
        Task<StudentsHomeViewModel> GetHomeViewModelAsync(string studentId);
        Task<StudentDetailsViewModel> GetDetailsViewModelAsync(string studentId);
        CreateStudentViewModel GetCreateStudentViewModel();
        Task<IdentityResult> CreateStudentAsync(CreateStudentViewModel student);
        Task<EditStudentViewModel> GetEditStudentViewModelAsync(string studentId);
        Task UpdateStudentAsync(EditStudentViewModel studentViewModel);
        Task<DeleteStudentViewModel> GetDeleteStudentViewModelAsync(string studentId);
        Task DeleteStudentAsync(string id);
    }
}
