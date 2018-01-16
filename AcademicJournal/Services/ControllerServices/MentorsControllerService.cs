using AcademicJournal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademicJournal.ViewModels;
using Microsoft.AspNet.Identity;
using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.DAL.Context;
using System.Threading.Tasks;
using System.Data.Entity;
using AcademicJournal.DAL.Models;
using AcademicJournal.Extensions;
using System.Web.Mvc;

namespace AcademicJournal.Services.ControllerServices
{
    public class MentorsControllerService : IMentorsControllerService
    {
        IMentorService service;
        ApplicationUserManager userManager;
        ApplicationDbContext db;

        public MentorsControllerService(IMentorService service,ApplicationUserManager userManager, ApplicationDbContext db)
        {
            this.db = db;
            this.service = service;
            this.userManager = userManager;
        }

        public async Task<MentorsHomeVM> GetHomeViewModelAsync(string mentorId)
        {
            var mentor = await db.Mentors.Where(m => m.Id == mentorId).Include(m => m.Students).Include(m => m.Assignments).FirstOrDefaultAsync();
            MentorsHomeVM viewModel = new MentorsHomeVM
            {
                Mentor = mentor,
                JournalVM = new Journal()
            };
            return viewModel;
        }

        public async Task<MentorsListVM> GetMentorsListViewModelAsync()
        {
            MentorsListVM viewModel = new MentorsListVM
            {
                Mentors = await service.GetAllMentorsAsync()
            };
            return viewModel;
        }

        public async Task<MentorDetailsVM> GetDetailsViewModelAsync(string mentorId)
        {
            Mentor mentor = await service.GetMentorByIdAsync(mentorId);
            if (mentor == null)
            {
                return null;
            }
            MentorDetailsVM viewModel = mentor.ToMentorDetailsVM();
            return viewModel;
        }

        public async Task<MentorAcceptStudentVM> GetAcceptStudentViewModelAsync(string mentorId)
        {
            var students = await db.Students.Where(s => s.Mentor.Id != mentorId).ToListAsync();
            MentorAcceptStudentVM viewModel = new MentorAcceptStudentVM
            {
                Students = students,
                StudentVM = new ShowStudentVM()
            };
            return viewModel;
        }

        public async Task AcceptStudentAsync(string studentId, string mentorId)
        {
            await service.AcceptStudentAsync(studentId, mentorId);
            await service.SaveChangesAsync();
        }

        public async Task<MentorExpelStudentVM> GetExpelStudentViewModelAsync(string studentId)
        {
            Student student = await db.Students.FindAsync(studentId);
            if (student == null)
            {
                return null;
            }

            MentorExpelStudentVM viewModel = new MentorExpelStudentVM
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

        public async Task<StudentMentorVM> GetStudentViewModelAsync(string studentId)
        {
            var student = await db.Students.Where(s => s.Id == studentId).
                                            Include(m => m.Mentor).
                                            Include(s => s.Submissions.Select(sub => sub.SubmitFile)).
                                            Include(s => s.Submissions.Select(sub => sub.Assignment.AssignmentFile)).
                                            FirstOrDefaultAsync();
            StudentMentorVM viewModel = new StudentMentorVM
            {
                Student = student,
                AssignmentModel = new Assignment(),
                SubmissionModel = new Submission()
            };
            return viewModel;
        }

        public CreateMentorVM GetCreateMentorViewModel()
        {
            CreateMentorVM viewModel = new CreateMentorVM();
            return viewModel;
        }

        public async Task<IdentityResult> CreateMentorAsync(CreateMentorVM viewModel)
        {
            Mentor newMenotor = viewModel.ToMentorModel();
            var result = await userManager.CreateAsync(newMenotor, viewModel.Password);
            if (result.Succeeded)
            {
                var roleResult = userManager.AddToRole(newMenotor.Id, "Mentor");         
            }
            return result;
        }

        public async Task<EditMentorVM> GetEditViewModelAsync(string mentorId)
        {
            Mentor mentor = await service.GetMentorByIdAsync(mentorId);
            if (mentor == null)
            {
                return null;
            }

            EditMentorVM viewModel = mentor.ToEditMentorVM();
            return viewModel;
        }

        public async Task UpdateMentorAsync(EditMentorVM vm)
        {
            Mentor newMentor = vm.ToMentorModel();
            service.UpdateMentor(newMentor);
            await service.SaveChangesAsync();
        }

        public async Task<DeleteMentorVM> GetDeleteViewModel(string id)
        {
            Mentor mentor = await service.GetMentorByIdAsync(id);
            if (mentor == null)
            {
                return null;
            }
            DeleteMentorVM viewModel = mentor.ToDeleteMentorVM();
            return viewModel;
        }


        public async Task DeleteMenotorAsync(string id)
        {
            await service.DeleteMentorByIdAsync(id);
            await service.SaveChangesAsync();
        }

       

        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

      

      

      
      

       
       
      

      

      
       

    }
}