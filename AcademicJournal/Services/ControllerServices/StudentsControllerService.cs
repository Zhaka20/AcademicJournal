using AcademicJournal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademicJournal.ViewModels;
using System.Threading.Tasks;
using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.DAL.Context;
using AcademicJournal.DAL.Models;
using AcademicJournal.Extensions;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace AcademicJournal.Services.ControllerServices
{
    public class StudentsControllerService : IStudentsControllerService
    {
        private ApplicationDbContext db;
        private IStudentService service;
        private ApplicationUserManager userManager;

        public StudentsControllerService(IStudentService service, ApplicationUserManager userManager, ApplicationDbContext db)
        {
            this.db = db;
            this.service = service;
            this.userManager = userManager;
        }

        public async Task<StudentsIndexViewModel> GetIndexViewModelAsync()
        {
            IEnumerable<Student> students = await service.GetAllStudentsAsync();
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
            Student student = await db.Students.Where(s => s.Id == studentId).
                                            Include(m => m.Mentor).
                                            Include(m => m.Submissions.Select(s => s.Assignment.AssignmentFile)).
                                            Include(m => m.Submissions.Select(s => s.SubmitFile)).
                                            FirstOrDefaultAsync();
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
            Student student = await service.GetStudentByIdAsync(studentId);
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
            Student student = await service.GetStudentByIdAsync(studentId);
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
            service.UpdateStudent(newStudent);
            await service.SaveChangesAsync();
        }

        public async Task<DeleteStudentVM> GetDeleteStudentViewModelAsync(string studentId)
        {
            Student student = await service.GetStudentByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            DeleteStudentVM viewModel = student.ToDeleteStudentVM();
            return viewModel;
        }

        public async Task DeleteStudentAsync(string studentId)
        {
            await service.DeleteStudentByIdAsync(studentId);
            await service.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
            service.Dispose();
        }        
    }
}