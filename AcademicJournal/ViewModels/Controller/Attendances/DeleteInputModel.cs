using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.ViewModels.Controller.Attendances
{
    public class DeleteInputModel
    {
        [Required]
        public int Id { get; set; }
        public AttendanceViewModel Attendance { get; set; }
    }
}