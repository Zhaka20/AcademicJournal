using AcademicJournal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademicJournal.ViewModels;
using System.Threading.Tasks;
using AcademicJournal.DAL.Context;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using AcademicJournal.DataModel.Models;

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

            WorkDaysDetailsVM viewModel = new WorkDaysDetailsVM
            {
                WorkDay = workDay,
                AttendanceModel = new Attendance()
            };
            return viewModel;
        }

        public async Task<int> CreateWorkDayAsync(WorkDayCreateViewModel inputModel)
        {
            WorkDay newWorkDay = new WorkDay
            {
                JournalId = inputModel.JournalId,
                Day = inputModel.Day
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
            string mentorId = HttpContext.Current.User.Identity.GetUserId();
            IQueryable<Student> mentorsAllStudents = db.Students.Where(s => s.MentorId == mentorId);

            IQueryable<Student> presentStudents = from attendance in db.Attendances
                                  where attendance.WorkDayId == workDayId
                                  select attendance.Student;

            List<Student> notPresentStudents = await mentorsAllStudents.Except(presentStudents).ToListAsync();

            WorDayAddAttendeesViewModel viewModel = new WorDayAddAttendeesViewModel
            {
                StudentModel = new Student(),
                Students = notPresentStudents
            };
            return viewModel;
        }

        public async Task AddWorkDayAttendeesAsync(int workDayId, List<string> attendeeIds)
        {
            if (attendeeIds != null)
            {
                WorkDay workDay = await db.WorkDays.FindAsync(workDayId);

                IQueryable<Student> query = from student in db.Students
                            where attendeeIds.Contains(student.Id)
                            select student;

                List<Student> listOfStudents = await query.ToListAsync();

                foreach (Student student in listOfStudents)
                {
                    workDay.Attendances.Add(new Attendance { Student = student, Come = DateTime.Now });
                }
                await db.SaveChangesAsync();
            }

        }

        public async Task CheckAsLeftAsync(int workDayId, List<int> attendaceIds)
        {
            if (attendaceIds != null)
            {
                WorkDay workDay = await db.WorkDays.FindAsync(workDayId);

                IQueryable<Attendance> query = from attenance in db.Attendances
                            where attendaceIds.Contains(attenance.Id)
                            select attenance;

                List<Attendance> listOfAttendees = await query.ToListAsync();

                foreach (Attendance attendee in listOfAttendees)
                {
                    attendee.Left = DateTime.Now;
                }
                await db.SaveChangesAsync();
            }
        }

        public WorkDayCreateViewModel GetCreateWorkDayViewModel(int journalId)
        {
            WorkDayCreateViewModel viewModel = new WorkDayCreateViewModel
            {
                Day = DateTime.Now,
                JournalId = journalId
            };
            return viewModel;
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}