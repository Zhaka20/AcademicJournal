using AcademicJournal.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.Extensions;
using Microsoft.AspNet.Identity;
using AcademicJournal.DataModel.Models;
using System;
using AcademicJournal.ViewModels.Students;

namespace AcademicJournal.Services.ControllerServices
{
    public class StudentsControllerService : IStudentsControllerService
    {
        private IStudentService service;
        private ApplicationUserManager userManager;

        public StudentsControllerService(IStudentService service, ApplicationUserManager userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        public async Task<StudentsIndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<Student> students = await service.GetAllAsync();
            IEnumerable<ShowStudentViewModel> studentListVM = students.ToShowStudentVMList();
            StudentsIndexViewModel viewModel = new StudentsIndexViewModel
            {
                StudentModel = new ShowStudentViewModel(),
                Students = studentListVM
            };
            return viewModel;
        }

        public async Task<StudentsHomeViewModel> GetHomeViewModelAsync(string studentId)
        {
            Student student = await service.GetFirstOrDefaultAsync(
                                            s => s.Id == studentId,
                                            s => s.Mentor,
                                            s => s.Submissions.Select(sub => sub.Assignment.AssignmentFile),
                                            s => s.Submissions.Select(sub => sub.SubmitFile)
                                            );

            StudentsHomeViewModel viewModel = new StudentsHomeViewModel
            {
                Student = student,
                AssignmentModel = new Assignment(),
                SubmissionModel = new Submission(),
                Submissions = student.Submissions
            };
            return viewModel;
        }


        public async Task<StudentDetailsViewModel> GetDetailsViewModelAsync(string studentId)
        {
            Student student = await service.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }
            StudentDetailsViewModel viewModel = student.ToStudentDetailsVM();
            return viewModel;
        }

        public CreateStudentViewModel GetCreateStudentViewModel()
        {
            CreateStudentViewModel viewModel = new CreateStudentViewModel();
            return viewModel;
        }

        public async Task<IdentityResult> CreateStudentAsync(CreateStudentViewModel viewModel)
        {
            Student newStudent = viewModel.ToStudentModel();

            IdentityResult result = await userManager.CreateAsync(newStudent, viewModel.Password);
            if (result.Succeeded)
            {
                IdentityResult roleResult = userManager.AddToRole(newStudent.Id, "Student");
            }
            return result;
        }

        public async Task<EditStudentViewModel> GetEditStudentViewModelAsync(string studentId)
        {
            Student student = await service.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            EditStudentViewModel viewModel = student.ToEditStudentVM();
            return viewModel;
        }

        public async Task UpdateStudentAsync(EditStudentViewModel viewModel)
        {
            Student newStudent = viewModel.ToStudentModel();
            service.Update(newStudent,
                           e => e.Email,
                           e => e.UserName,
                           e => e.FirstName,
                           e => e.LastName,
                           e => e.PhoneNumber
                         );
            await service.SaveChangesAsync();
        }

        public async Task<DeleteStudentViewModel> GetDeleteStudentViewModelAsync(string studentId)
        {
            Student student = await service.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            DeleteStudentViewModel viewModel = student.ToDeleteStudentVM();
            return viewModel;
        }

        public async Task DeleteStudentAsync(string studentId)
        {
            await service.DeleteByIdAsync(studentId);
            await service.SaveChangesAsync();
        }

        public void Dispose()
        {
            IDisposable dispose = service as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
            userManager.Dispose();
        }        
    }
}