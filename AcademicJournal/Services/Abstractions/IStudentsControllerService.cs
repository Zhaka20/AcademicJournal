using AcademicJournal.ViewModels.Controller.Students;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IStudentsControllerService : IDisposable
    {
        Task<IndexViewModel> GetIndexViewModelAsync();
        Task<HomeViewModel> GetHomeViewModelAsync(string studentId);
        Task<DetailsViewModel> GetDetailsViewModelAsync(string studentId);
        CreateViewModel GetCreateStudentViewModel();
        Task<IdentityResult> CreateStudentAsync(CreateViewModel student);
        Task<EditViewModel> GetEditStudentViewModelAsync(string studentId);
        Task UpdateStudentAsync(EditViewModel studentViewModel);
        Task<DeleteViewModel> GetDeleteStudentViewModelAsync(string studentId);
        Task DeleteStudentAsync(string id);
    }
}
