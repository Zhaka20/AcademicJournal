using AcademicJournal.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using AcademicJournal.AbstractBLL.AbstractServices;
using System.Threading.Tasks;
using AcademicJournal.Extensions;
using AcademicJournal.DataModel.Models;
using System;
using AcademicJournal.ViewModels.Controller.Mentors;
using AcademicJournal.ViewModels.Controller.Students;

namespace AcademicJournal.Services.ControllerServices
{
    public class MentorsControllerService : IMentorsControllerService
    {
        protected readonly IMentorService service;
        protected readonly ApplicationUserManager userManager;
        protected readonly IStudentService studentService;

        public MentorsControllerService(IMentorService service,ApplicationUserManager userManager, IStudentService studentService)
        {
            this.studentService = studentService;
            this.service = service;
            this.userManager = userManager;
        }

        public async Task<MentorsHomeViewModel> GetHomeViewModelAsync(string mentorId)
        {
            Mentor mentor = await service.GetFirstOrDefaultAsync(m => m.Id == mentorId,
                                                                 m => m.Students,
                                                                 m => m.Assignments);

            MentorsHomeViewModel viewModel = new MentorsHomeViewModel
            {
                Mentor = mentor,
                JournalVM = new Journal()
            };
            return viewModel;
        }

        public async Task<MentorsListViewModel> GetMentorsListViewModelAsync()
        {
            MentorsListViewModel viewModel = new MentorsListViewModel
            {
                Mentors = await service.GetAllAsync()
            };
            return viewModel;
        }

        public async Task<ViewModels.Mentors.DetailsViewModel> GetDetailsViewModelAsync(string mentorId)
        {
            Mentor mentor = await service.GetByIdAsync(mentorId);
            if (mentor == null)
            {
                return null;
            }
            ViewModels.Mentors.DetailsViewModel viewModel = mentor.ToMentorDetailsVM();
            return viewModel;
        }

        public async Task<AcceptStudentViewModel> GetAcceptStudentViewModelAsync(string mentorId)
        {
            IEnumerable<Student> students = await studentService.GetAllAsync(s => s.Mentor.Id != mentorId);
            AcceptStudentViewModel viewModel = new AcceptStudentViewModel
            {
                Students = students,
                StudentVM = new ViewModels.Students.ShowViewModel()
            };
            return viewModel;
        }

        public async Task AcceptStudentAsync(string studentId, string mentorId)
        {
            await service.AcceptStudentAsync(studentId, mentorId);
            await service.SaveChangesAsync();
        }

        public async Task<ExpelStudentViewModel> GetExpelStudentViewModelAsync(string studentId)
        {
            Student student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            ExpelStudentViewModel viewModel = new ExpelStudentViewModel
            {
                Student = student.ToShowStudentVM(),
                MentorId = student.MentorId
            };
            return viewModel;
        }

        public async Task RemoveStudentAsync(string studentId,string mentorId)
        {
            await service.RemoveStudentAsync(studentId,mentorId);
            await service.SaveChangesAsync();
        }

        public async Task<MyStudentViewModel> GetStudentViewModelAsync(string studentId)
        {
            Student student = await studentService.GetFirstOrDefaultAsync(s => s.Id == studentId,
                                                   s => s.Mentor,
                                                   s => s.Submissions.Select(sub => sub.SubmitFile),
                                                   s => s.Submissions.Select(sub => sub.Assignment.AssignmentFile));

            if(student == null)
            {
                return null;
            }
            MyStudentViewModel viewModel = new MyStudentViewModel
            {
                Student = student,
                AssignmentModel = new Assignment(),
                SubmissionModel = new Submission()
            };
            return viewModel;
        }

        public ViewModels.Mentors.CreateViewModel GetCreateMentorViewModel()
        {
            ViewModels.Mentors.CreateViewModel viewModel = new ViewModels.Mentors.CreateViewModel();
            return viewModel;
        }

        public async Task<IdentityResult> CreateMentorAsync(ViewModels.Mentors.CreateViewModel viewModel)
        {
            Mentor newMenotor = viewModel.ToMentorModel();
            IdentityResult result = await userManager.CreateAsync(newMenotor, viewModel.Password);
            if (result.Succeeded)
            {
                IdentityResult roleResult = userManager.AddToRole(newMenotor.Id, "Mentor");         
            }
            return result;
        }

        public async Task<ViewModels.Mentors.EditViewModel> GetEditViewModelAsync(string mentorId)
        {
            Mentor mentor = await service.GetByIdAsync(mentorId);
            if (mentor == null)
            {
                return null;
            }

            ViewModels.Mentors.EditViewModel viewModel = mentor.ToEditMentorVM();
            return viewModel;
        }

        public async Task UpdateMentorAsync(ViewModels.Mentors.EditViewModel viewModel)
        {
            Mentor newMentor = viewModel.ToMentorModel();
            service.Update(newMentor,  e => e.UserName,
                                       e => e.FirstName,
                                       e => e.LastName,
                                       e => e.PhoneNumber);
            await service.SaveChangesAsync();
        }

        public async Task<ViewModels.Mentors.DeleteViewModel> GetDeleteViewModel(string id)
        {
            Mentor mentor = await service.GetByIdAsync(id);
            if (mentor == null)
            {
                return null;
            }
            ViewModels.Mentors.DeleteViewModel viewModel = mentor.ToDeleteMentorVM();
            return viewModel;
        }


        public async Task DeleteMenotorAsync(string id)
        {
            await service.DeleteByIdAsync(id);
            await service.SaveChangesAsync();
        }

        public void Dispose()
        {
            userManager.Dispose();
            IDisposable dispose = service as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
            dispose = studentService as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
        }

        ViewModels.Mentors.CreateViewModel IMentorsControllerService.GetCreateMentorViewModel()
        {
            throw new NotImplementedException();
        }
    }
}