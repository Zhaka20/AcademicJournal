using AcademicJournal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademicJournal.ViewModels;
using System.Threading.Tasks;
using AcademicJournal.DAL.Models;
using AcademicJournal.DAL.Context;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace AcademicJournal.Services.ControllerServices
{
    public class WorkDaysControllerService : IWorkDaysControllerService
    {
        private ApplicationDbContext db;

        public WorkDaysControllerService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<WorkDayIndexViewModel> GetWorkDaysIndexViewModel()
        {
            List<WorkDay> workDays = await db.WorkDays.ToListAsync();
            WorkDayIndexViewModel viewModel = new WorkDayIndexViewModel
            {
                WorkDayModel = new WorkDay(),
                WorkDays = workDays
            };
            return viewModel;
        }

        public async Task<WorkDaysDetailsVM> GetWorkDayDetailsViewModelAsync(int workDayId)
        {
            WorkDay workDay = await db.WorkDays.Include(w => w.Attendances).
                                                FirstOrDefaultAsync(w => w.Id == workDayId);
            if (workDay == null)
            {
                return null;
            }

            var viewModel = new WorkDaysDetailsVM
            {
                WorkDay = workDay,
                AttendanceModel = new Attendance()
            };
            return viewModel;
        }

        public async Task<int> CreateWorkDayAsync(WorkDaysDetailsVM inputModel)
        {
            WorkDay newWorkDay = new WorkDay
            {
                JournalId = inputModel.WorkDay.JournalId,
                Day = inputModel.WorkDay.Day
            };
            db.WorkDays.Add(newWorkDay);
            await db.SaveChangesAsync();
            return newWorkDay.Id;
        }

        public async Task<WorkDayEditViewModel> GetWorkDayEditViewModelAsync(int workDayId)
        {
            WorkDay workDay = await db.WorkDays.FindAsync(workDayId);
            if (workDay == null)
            {
                return null;
            }
            WorkDayEditViewModel viewModel = new WorkDayEditViewModel
            {
                WorkDay = workDay
            };
            return viewModel;
        }

        public async Task WorkDayUpdateAsync(WorkDayEditViewModel inputModel)
        {
            WorkDay updatedWorkDay = new WorkDay
            {
                Id = inputModel.WorkDay.Id,
                Day = inputModel.WorkDay.Day
            };
            db.WorkDays.Attach(updatedWorkDay);
            db.Entry(updatedWorkDay).Property(w => w.Day).IsModified = true;
            await db.SaveChangesAsync();
        }

        public async Task<WorkDayDeleteViewModel> GetWorkDayDeleteViewModelAsync(int id)
        {
            WorkDay workDay = await db.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return null;
            }
            WorkDayDeleteViewModel viewModel = new WorkDayDeleteViewModel
            {
                WorkDay = workDay
            };
            return viewModel;
        }

        public async Task WorkDayDeleteAsync(int workDayId)
        {
            WorkDay workDay = await db.WorkDays.FindAsync(workDayId);
            db.WorkDays.Remove(workDay);
            await db.SaveChangesAsync();
        }

        public async Task<WorDayAddAttendeesViewModel> GetWorDayAddAttendeesViewModelAsync(int workDayId)
        {
            var mentorId = HttpContext.Current.User.Identity.GetUserId();
            var mentorsAllStudents = db.Students.Where(s => s.MentorId == mentorId);

            var presentStudents = from attendance in db.Attendances
                                  where attendance.WorkDayId == workDayId
                                  select attendance.Student;

            var notPresentStudents = await mentorsAllStudents.Except(presentStudents).ToListAsync();

            WorDayAddAttendeesViewModel viewModel = new WorDayAddAttendeesViewModel
            {
                StudentModel = new Student(),
                Students = notPresentStudents
            };
            return viewModel;
        }

        ///_______________________________________________________________________________

        public Task AddWorkDayAttendeesAsync(int workDayId, List<string> attendeeIds)
        {
            throw new NotImplementedException();
        }

        public Task CheckAsLeftAsync(int workDayId, List<string> attendeeIds)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public WorkDayCreateViewModel GetCreateWorkDayViewModel()
        {
            throw new NotImplementedException();
        }






    }
}