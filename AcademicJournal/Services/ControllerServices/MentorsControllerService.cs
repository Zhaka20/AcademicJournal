using AcademicJournal.Services.Abstractions;
using System.Collections.Generic;
using System.Linq;
using AcademicJournal.ViewModels;
using Microsoft.AspNet.Identity;
using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.DAL.Context;
using System.Threading.Tasks;
using System.Data.Entity;
using AcademicJournal.Extensions;
using AcademicJournal.DataModel.Models;
using System;
using AcademicJournal.ViewModels.Mentors;
using AcademicJournal.ViewModels.Students;

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

        public async Task<MentorDetailsViewModel> GetDetailsViewModelAsync(string mentorId)
        {
            Mentor mentor = await service.GetByIdAsync(mentorId);
            if (mentor == null)
            {
                return null;
            }
            MentorDetailsViewModel viewModel = mentor.ToMentorDetailsVM();
            return viewModel;
        }

        public async Task<MentorAcceptStudentViewModel> GetAcceptStudentViewModelAsync(string mentorId)
        {
            IEnumerable<Student> students = await studentService.GetAllAsync(s => s.Mentor.Id != mentorId);
            MentorAcceptStudentViewModel viewModel = new MentorAcceptStudentViewModel
            {
                Students = students,
                StudentVM = new ShowStudentViewModel()
            };
            return viewModel;
        }

        public async Task AcceptStudentAsync(string studentId, string mentorId)
        {
            await service.AcceptStudentAsync(studentId, mentorId);
            await service.SaveChangesAsync();
        }

        public async Task<MentorExpelStudentViewModel> GetExpelStudentViewModelAsync(string studentId)
        {
            Student student = await studentService.GetByIdAsync(studentId);
            if (student == null)
            {
                return null;
            }

            MentorExpelStudentViewModel viewModel = new MentorExpelStudentViewModel
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

        public async Task<StudentMentorViewModel> GetStudentViewModelAsync(string studentId)
        {
            Student student = await studentService.GetFirstOrDefaultAsync(s => s.Id == studentId,
                                                   s => s.Mentor,
                                                   s => s.Submissions.Select(sub => sub.SubmitFile),
                                                   s => s.Submissions.Select(sub => sub.Assignment.AssignmentFile));

            if(student == null)
            {
                return null;
            }
            StudentMentorViewModel viewModel = new StudentMentorViewModel
            {
                Student = student,
                AssignmentModel = new Assignment(),
                SubmissionModel = new Submission()
            };
            return viewModel;
        }

        public CreateMentorViewModel GetCreateMentorViewModel()
        {
            CreateMentorViewModel viewModel = new CreateMentorViewModel();
            return viewModel;
        }

        public async Task<IdentityResult> CreateMentorAsync(CreateMentorViewModel viewModel)
        {
            Mentor newMenotor = viewModel.ToMentorModel();
            IdentityResult result = await userManager.CreateAsync(newMenotor, viewModel.Password);
            if (result.Succeeded)
            {
                IdentityResult roleResult = userManager.AddToRole(newMenotor.Id, "Mentor");         
            }
            return result;
        }

        public async Task<EditMentorViewModel> GetEditViewModelAsync(string mentorId)
        {
            Mentor mentor = await service.GetByIdAsync(mentorId);
            if (mentor == null)
            {
                return null;
            }

            EditMentorViewModel viewModel = mentor.ToEditMentorVM();
            return viewModel;
        }

        public async Task UpdateMentorAsync(EditMentorViewModel viewModel)
        {
            Mentor newMentor = viewModel.ToMentorModel();
            service.Update(newMentor,  e => e.UserName,
                                       e => e.FirstName,
                                       e => e.LastName,
                                       e => e.PhoneNumber);
            await service.SaveChangesAsync();
        }

        public async Task<DeleteMentorViewModel> GetDeleteViewModel(string id)
        {
            Mentor mentor = await service.GetByIdAsync(id);
            if (mentor == null)
            {
                return null;
            }
            DeleteMentorViewModel viewModel = mentor.ToDeleteMentorVM();
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

      

      

      
      

       
       
      

      

      
       

    }
}