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
using AcademicJournal.ViewModels.Shared.EntityViewModels;
using AcademicJournal.Services.Common;

namespace AcademicJournal.Services.ControllerServices
{
    public class WorkDaysControllerService : IWorkDaysControllerService
    {
        protected readonly IWorkDayService workDayService;
        protected readonly IStudentService studentService;
        protected readonly IAttendanceService attendanceService;
        protected readonly IObjectMapper mapper;

        public WorkDaysControllerService(IWorkDayService service, IStudentService studentService, IAttendanceService attendanceService, IObjectToObjectMapper mapper)
        {
            this.attendanceService = attendanceService;
            this.studentService = studentService;
            this.workDayService = service;
            this.mapper = mapper;
        }

        public async Task<IndexViewModel> GetWorkDaysIndexViewModel()
        {
            IEnumerable<WorkDayDTO> workDayDTOs = await workDayService.GetAllAsync();
            IEnumerable<WorkDayViewModel> workDayViewModels = mapper.Map<IEnumerable<WorkDayViewModel>, IEnumerable<WorkDayDTO>>(workDayDTOs);
            IndexViewModel viewModel = new IndexViewModel
            {
                WorkDay = new WorkDayViewModel(),
                WorkDays = workDayViewModels
            };
            return viewModel;
        }

        public async Task<DetailsViewModel> GetWorkDayDetailsViewModelAsync(int workDayId)
        {
            WorkDayDTO workDayDTO = await workDayService.GetFirstOrDefaultAsync(w => w.Id == workDayId, w => w.Attendances);

            if (workDayDTO == null)
            {
                return null;
            }
            WorkDayViewModel workDayViewModel = mapper.Map<WorkDayViewModel, WorkDayDTO>(workDayDTO);
            DetailsViewModel viewModel = new DetailsViewModel
            {
                WorkDay = workDayViewModel,
                AttendanceModel = new AttendanceViewModel()
            };
            return viewModel;
        }

        public async Task<int> CreateWorkDayAsync(CreateViewModel inputModel)
        {
            WorkDayDTO newWorkDay = new WorkDayDTO
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
            WorkDayDTO workDayDTO = await workDayService.GetByIdAsync(workDayId);
            if (workDayDTO == null)
            {
                return null;
            }
            var workDayViewModel = mapper.Map<WorkDayViewModel, WorkDayDTO>(workDayDTO);

            EditViewModel viewModel = new EditViewModel
            {
                WorkDayToEdit = workDayDTO
            };
            return viewModel;
        }

        public async Task WorkDayUpdateAsync(EditViewModel inputModel)
        {
            //WorkDayDTO updatedWorkDay = new WorkDayDTO
            //{
            //    Id = inputModel.WorkDayToEdit.Id,
            //    Day = inputModel.WorkDayToEdit.Day
            //};
            WorkDayDTO updatedWorkDay = mapper.Map<WorkDayDTO, WorkDayViewModel>(inputModel.WorkDayToEdit);
            workDayService.Update();
            await workDayService.SaveChangesAsync();
        }

        public async Task<DeleteViewModel> GetWorkDayDeleteViewModelAsync(int id)
        {
            WorkDayDTO workDayDTO = await workDayService.GetByIdAsync(id);
            if (workDayDTO == null)
            {
                return null;
            }

            var workDayViewModel = mapper.Map<WorkDayViewModel, WorkDayDTO>(workDayDTO);

            DeleteViewModel viewModel = new DeleteViewModel
            {
                WorkDayToDelete = workDayViewModel
            };
            return viewModel;
        }

        public async Task WorkDayDeleteAsync(int workDayId)
        {
            WorkDayDTO workDay = await workDayService.GetByIdAsync(workDayId);
            workDayService.Delete(workDay);
            await workDayService.SaveChangesAsync();
        }

        public async Task<AddAttendeesViewModel> GetWorDayAddAttendeesViewModelAsync(int workDayId)
        {
            string mentorId = HttpContext.Current.User.Identity.GetUserId();
            //IQueryable<Student> mentorsAllStudents = db.Students.Where(s => s.MentorId == mentorId);

            IEnumerable<StudentDTO> mentorsAllStudents = await studentService.GetAllAsync(s => s.MentorId == mentorId);

            //IQueryable<Student> presentStudents = from attendance in db.Attendances
            //                      where attendance.WorkDayId == workDayId
            //                      select attendance.Student;

            IEnumerable<AttendanceDTO> attendances = await attendanceService.GetAllAsync(a => a.WorkDayId == workDayId);

            List<StudentDTO> presentStudents = new List<StudentDTO>();
            foreach (var attendance in attendances)
            {
                presentStudents.Add(attendance.Student);
            }

            //List<Student> notPresentStudents = await mentorsAllStudents.Except(presentStudents).ToListAsync();

            IEnumerable<StudentDTO> notPresentStudents = mentorsAllStudents.Except(presentStudents);
            IEnumerable<StudentViewModel> notPresentStudentViewModels = mapper.Map<IEnumerable<StudentViewModel>, IEnumerable<StudentDTO>>(notPresentStudents);
            AddAttendeesViewModel viewModel = new AddAttendeesViewModel
            {
                StudentModel = new StudentViewModel(),
                NotPresentStudents = notPresentStudentViewModels
            };
            return viewModel;
        }

        public async Task AddWorkDayAttendeesAsync(int workDayId, List<string> attendeeIds)
        {
            if (attendeeIds != null)
            {
                WorkDayDTO workDay = await workDayService.GetByIdAsync(workDayId);

                //IQueryable<Student> query = from student in db.Students
                //            where attendeeIds.Contains(student.Id)
                //            select student;

                IEnumerable<StudentDTO> students = await studentService.GetAllAsync(s => attendeeIds.Contains(s.Id));
                //List<Student> listOfStudents = await query.ToListAsync();


                //WORKDAYSERVICE.ADDATTENDEESIMPLEMENT NEEDED
                foreach (StudentDTO student in students)
                {
                    workDay.Attendances.Add(new AttendanceDTO { Student = student, Come = DateTime.Now });
                }
                await workDayService.SaveChangesAsync();
            }

        }

        public async Task CheckAsLeftAsync(int workDayId, List<int> attendaceIds)
        {
            if (attendaceIds != null)
            {
                WorkDayDTO workDay = await workDayService.GetByIdAsync(workDayId);
                //WorkDay workDay = await db.WorkDays.FindAsync(workDayId);

                //IQueryable<Attendance> query = from attenance in db.Attendances
                //            where attendaceIds.Contains(attenance.Id)
                //            select attenance;
                //List<Attendance> listOfAttendees = await query.ToListAsync();
                IEnumerable<AttendanceDTO> attendances = await attendanceService.GetAllAsync(a => attendaceIds.Contains(a.Id));

                //foreach (Attendance attendee in listOfAttendees)
                //{
                //    attendee.Left = DateTime.Now;
                //}

                foreach (AttendanceDTO attendee in attendances)
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
            if (disposable != null)
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