using AcademicJournal.ViewModels.Shared.EntityViewModels;

namespace AcademicJournal.ViewModels.Controller.WorkDays
{
    public class DetailsViewModel
    {
        public WorkDayViewModel WorkDay { get; set; }
        public AttendanceViewModel AttendanceModel { get; set; }
    }
}