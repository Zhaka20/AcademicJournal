using AcademicJournal.Services.Abstractions;
using System.Collections.Generic;
using AcademicJournal.ViewModels;
using System.Threading.Tasks;
using AcademicJournal.DAL.Context;
using System.Data.Entity;
using AcademicJournal.DataModel.Models;

namespace AcademicJournal.Services.ControllerServices
{
    public class AttendancesControllerService : IAttendancesControllerService
    {
        private ApplicationDbContext db;
        public AttendancesControllerService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<AttendanceIndexViewModel> GetAttendancesIndexViewModelAsync()
        {
            List<Attendance> attendances = await db.Attendances.
                                       Include(a => a.Day).
                                       Include(a => a.Student).
                                       ToListAsync();
            AttendanceIndexViewModel viewModel = new AttendanceIndexViewModel
            {
                Attendances = attendances,
                AttendanceModel = new Attendance(),
                StudentModel = new Student()
            };
            return viewModel;
        }
        public async Task<AttendancesDetailsViewModel> GetAttendancesDetailsViewModelAsync(int attendanceId)
        {
            Attendance attendance = await db.Attendances.FindAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            AttendancesDetailsViewModel viewModel = new AttendancesDetailsViewModel
            {
                Attendance = attendance
            };
            return viewModel;
        }
        public async Task<EditAttendanceViewModel> GetEditAttendanceViewModelAsync(int attendanceId)
        {
            Attendance attendance = await db.Attendances.FindAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            EditAttendanceViewModel viewModel = new EditAttendanceViewModel
            {
                Come = attendance.Come,
                Left = attendance.Left,
                Id = attendance.Id
            };
            return viewModel;
        }
        public async Task UpdateAttendanceAsync(EditAttendanceViewModel inputModel)
        {
            Attendance updatedAttendance = new Attendance
            {
                Id = inputModel.Id,
                Left = inputModel.Left,
                Come = inputModel.Come
            };
            db.Attendances.Attach(updatedAttendance);
            db.Entry(updatedAttendance).Property(e => e.Left).IsModified = true;
            db.Entry(updatedAttendance).Property(e => e.Come).IsModified = true;
            await db.SaveChangesAsync();
        }
        public async Task<DeleteAttendanceViewModel> GetDeleteAttendanceViewModelAsync(int attendanceId)
        {
            Attendance attendance = await db.Attendances.FindAsync(attendanceId);
            if (attendance == null)
            {
                return null;
            }
            DeleteAttendanceViewModel viewModel = new DeleteAttendanceViewModel
            {
                Attendance = attendance
            };
            return viewModel;
        }
        public async Task DeleteAttendanceAsync(DeleteAttendanceInputModel inputModel)
        {
            Attendance attendanceToRemove = new Attendance { Id = inputModel.Id };
            db.Attendances.Attach(attendanceToRemove);
            db.Attendances.Remove(attendanceToRemove);
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}