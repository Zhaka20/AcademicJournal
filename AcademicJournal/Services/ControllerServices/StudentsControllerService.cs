using AcademicJournal.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using AcademicJournal.ViewModels;
using System.Threading.Tasks;
using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.DAL.Context;
using AcademicJournal.Extensions;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using AcademicJournal.DataModel.Models;
using System;

namespace AcademicJournal.Services.ControllerServices
{
    public class StudentsControllerService : IStudentsControllerService
    {
        private IStudentService service;
        private ApplicationUserManager userManager;

        public StudentsControllerService(IStudentService service, ApplicationUserManager userManager, ApplicationDbContext db)
        {
            this.service = service;
            this.userManager = userManager;
        }

        public async Task<StudentsIndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<Student> students = await service.GetAllAsync();
            IEnumerable<ShowStudentVM> studentListVM = students.ToShowStudentVMList();
            StudentsIndexViewModel viewModel = new StudentsIndexViewModel
            {
                StudentModel = new ShowStudentVM(),
                Students = studentListVM
            };
            return viewModel;
        }

        public async Task<StudentsHomeVM> GetHomeViewModelAsync(string studentId)
        {
            Student student = await service.GetFirstOrDefaultAsync(
                                            s => s.Id == studentId,
                                            s => s.Mentor,
                                            s => s.Submissions.Select(sub => sub.Assignment.AssignmentFile),
                                            s => s.Submissions.Select(sub => sub.SubmitFile)
                                            );

            StudentsHomeVM viewModel = new StudentsHomeVM
            {
                Student = student,
                AssignmentModel = new Assignment(),
                SubmissionModel = new Submission(),
                Submissions = student.Submissions
            };
            return viewModel;
        }


        public async Task<StudentDetailsVM> GetDetailsViewModelAsync(string studentId)
        {
            Student student = await service.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }
            StudentDetailsVM viewModel = student.ToStudentDetailsVM();
            return viewModel;
        }

        public CreateStudentVM GetCreateStudentViewModel()
        {
            CreateStudentVM viewModel = new CreateStudentVM();
            return viewModel;
        }

        public async Task<IdentityResult> CreateStudentAsync(CreateStudentVM viewModel)
        {
            Student newStudent = viewModel.ToStudentModel();

            IdentityResult result = await userManager.CreateAsync(newStudent, viewModel.Password);
            if (result.Succeeded)
            {
                IdentityResult roleResult = userManager.AddToRole(newStudent.Id, "Student");
            }
            return result;
        }

        public async Task<EditStudentVM> GetEditStudentViewModelAsync(string studentId)
        {
            Student student = await service.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            EditStudentVM viewModel = student.ToEditStudentVM();
            return viewModel;
        }

        public async Task UpdateStudentAsync(EditStudentVM viewModel)
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

        public async Task<DeleteStudentVM> GetDeleteStudentViewModelAsync(string studentId)
        {
            Student student = await service.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            DeleteStudentVM viewModel = student.ToDeleteStudentVM();
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