using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Controller.Attendances
{
    public class IndexViewModel
    {
        public IEnumerable<AttendanceViewModel> Attendances { get; set; }
        public StudentViewModel StudentModel { get; set; }
        public AttendanceViewModel AttendanceModel { get; set; }
    }
}