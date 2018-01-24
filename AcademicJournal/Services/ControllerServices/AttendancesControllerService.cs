using AcademicJournal.Services.Abstractions;
using System.Collections.Generic;
using AcademicJournal.ViewModels;
using System.Threading.Tasks;
using AcademicJournal.DAL.Context;
using System.Data.Entity;
using AcademicJournal.DataModel.Models;
using AcademicJournal.BLL.Services.Abstract;
using System;

namespace AcademicJournal.Services.ControllerServices
{
    public class AttendancesControllerService : IAttendancesControllerService
    {
        protected readonly IAttendanceService service;
        public AttendancesControllerService(IAttendanceService service)
        {
            this.service = service;
        }


        public async Task<AttendanceIndexViewModel> GetAttendancesIndexViewModelAsync()
        {
            IEnumerable<Attendance> attendances = await service.GetAllAsync(null, null, null, null, a => a.Day, a => a.Student );
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
            Attendance attendance = await service.GetByIdAsync(attendanceId);
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
            Attendance attendance = await service.GetByIdAsync(attendanceId);
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
            service.Update(updatedAttendance, e => e.Left, e => e.Come);
            await service.SaveChangesAsync();
        }
        public async Task<DeleteAttendanceViewModel> GetDeleteAttendanceViewModelAsync(int attendanceId)
        {
            Attendance attendance = await service.GetByIdAsync(attendanceId);
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
            service.Delete(attendanceToRemove);
            await service.SaveChangesAsync();
        }

        public void Dispose()
        {
            IDisposable dispose = service as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
        }

    }
}