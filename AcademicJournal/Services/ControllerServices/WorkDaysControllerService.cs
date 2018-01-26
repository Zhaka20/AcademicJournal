using AcademicJournal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademicJournal.ViewModels.Controller.WorkDays;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using AcademicJournal.AbstractBLL.AbstractServices;
using AcademicJournal.FrontToBLL_DTOs.DTOs;

namespace AcademicJournal.Services.ControllerServices
{
    public class WorkDaysControllerService : IWorkDaysControllerService
    {
        protected readonly IWorkDayService workDayService;
        protected readonly IStudentService studentService;
        protected readonly IAttendanceService attendanceService;

        public WorkDaysControllerService(IWorkDayService service, IStudentService studentService, IAttendanceService attendanceService)
        {
            this.attendanceService = attendanceService;
            this.studentService = studentService;
            this.workDayService = service;
        }

        public async Task<IndexViewModel> GetWorkDaysIndexViewModel()
        {
            IEnumerable<WorkDayDTO> workDays = await workDayService.GetAllAsync();
            IndexViewModel viewModel = new IndexViewModel
            {
                WorkDayDTO = new WorkDayDTO(),
                WorkDays = workDays
            };
            return viewModel;
        }

        public async Task<DetailsViewModel> GetWorkDayDetailsViewModelAsync(int workDayId)
        {
            WorkDay workDay = await workDayService.GetFirstOrDefaultAsync(w => w.Id == workDayId, w => w.Attendances);
    
            if (workDay == null)
            {
                return null;
            }

            DetailsViewModel viewModel = new DetailsViewModel
            {
                WorkDay = workDay,
                AttendanceModel = new Attendance()
            };
            return viewModel;
        }

        public async Task<int> CreateWorkDayAsync(CreateViewModel inputModel)
        {
            WorkDay newWorkDay = new WorkDay
            {
                JournalId = inputModel.JournalId,
                Day = inputModel.Day
            };
            workDayService.Create(newWorkDay);
            await workDayService.SaveChangesAsync();
            return newWorkDay.Id;
        }

        public async Task<EditViewModel> GetWorkDayEditViewModelAsync(int workDayId)
        {
            WorkDay workDay = await workDayService.GetByIdAsync(workDayId);
            if (workDay == null)
            {
                return null;
            }
            EditViewModel viewModel = new EditViewModel
            {
                WorkDay = workDay
            };
            return viewModel;
        }

        public async Task WorkDayUpdateAsync(EditViewModel inputModel)
        {
            WorkDay updatedWorkDay = new WorkDay
            {
                Id = inputModel.WorkDay.Id,
                Day = inputModel.WorkDay.Day
            };
            workDayService.Update(updatedWorkDay, w => w.Day);          
            await workDayService.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetWorkDayDeleteViewModelAsync(int id)
        {
            WorkDay workDay = await workDayService.GetByIdAsync(id);
            if (workDay == null)
            {
                return null;
            }
            DeleteViewModel viewModel = new DeleteViewModel
            {
                WorkDay = workDay
            };
            return viewModel;
        }

        public async Task WorkDayDeleteAsync(int workDayId)
        {
            WorkDay workDay = await workDayService.GetByIdAsync(workDayId);
            workDayService.Delete(workDay);
            await workDayService.SaveChangesAsync();
        }

        public async Task<AddAttendeesViewModel> GetWorDayAddAttendeesViewModelAsync(int workDayId)
        {
            string mentorId = HttpContext.Current.User.Identity.GetUserId();
            //IQueryable<Student> mentorsAllStudents = db.Students.Where(s => s.MentorId == mentorId);

            IEnumerable<Student> mentorsAllStudents = await studentService.GetAllAsync(s => s.MentorId == mentorId);

            //IQueryable<Student> presentStudents = from attendance in db.Attendances
            //                      where attendance.WorkDayId == workDayId
            //                      select attendance.Student;

            IEnumerable<Attendance> attendances = await attendanceService.GetAllAsync(a => a.WorkDayId == workDayId);

            List<Student> presentStudents = new List<Student>();
            foreach(var attendance in attendances)
            {
                presentStudents.Add(attendance.Student);
            }

            //List<Student> notPresentStudents = await mentorsAllStudents.Except(presentStudents).ToListAsync();

            IEnumerable<Student> notPresentStudents = mentorsAllStudents.Except(presentStudents);

            AddAttendeesViewModel viewModel = new AddAttendeesViewModel
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
                WorkDay workDay = await workDayService.GetByIdAsync(workDayId);

                //IQueryable<Student> query = from student in db.Students
                //            where attendeeIds.Contains(student.Id)
                //            select student;

                IEnumerable<Student> students =  await studentService.GetAllAsync(s => attendeeIds.Contains(s.Id));
                //List<Student> listOfStudents = await query.ToListAsync();


                //WORKDAYSERVICE.ADDATTENDEESIMPLEMENT NEEDED
                foreach (Student student in students)
                {
                    workDay.Attendances.Add(new Attendance { Student = student, Come = DateTime.Now });
                }
                await workDayService.SaveChangesAsync();
            }

        }

        public async Task CheckAsLeftAsync(int workDayId, List<int> attendaceIds)
        {
            if (attendaceIds != null)
            {
                WorkDay workDay = await workDayService.GetByIdAsync(workDayId);
                //WorkDay workDay = await db.WorkDays.FindAsync(workDayId);

                //IQueryable<Attendance> query = from attenance in db.Attendances
                //            where attendaceIds.Contains(attenance.Id)
                //            select attenance;
                //List<Attendance> listOfAttendees = await query.ToListAsync();
                var attendances = await attendanceService.GetAllAsync(a => attendaceIds.Contains(a.Id));

                //foreach (Attendance attendee in listOfAttendees)
                //{
                //    attendee.Left = DateTime.Now;
                //}

                foreach (Attendance attendee in attendances)
                {
                    attendee.Left = DateTime.Now;
                }

                await workDayService.SaveChangesAsync();
                //await db.SaveChangesAsync();
            }
        }

        public CreateViewModel GetCreateWorkDayViewModel(int journalId)
        {
            CreateViewModel viewModel = new CreateViewModel
            {
                Day = DateTime.Now,
                JournalId = journalId
            };
            return viewModel;
        }

        public void Dispose()
        {
            IDisposable disposable = attendanceService as IDisposable;
            if(disposable != null)
            {
                disposable.Dispose();
            }
            disposable = studentService as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
            disposable = workDayService as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

    }
}