using System;
namespace AcademicJournal.AbstractModels.Interfaces
{
    public interface IAttendanceModel<TKey,TStudentKey,TWorkDayKey>
    {
       TKey Id { get; set; }

       TStudentKey StudentId { get; set; }
       AbstractStudentModel Student { get; set; }

       TWorkDayKey WorkDayId { get; set; }
       IWorkDay Day { get; set; }

       DateTime? Come { get; set; }
       DateTime? Left { get; set; }
    }
}
