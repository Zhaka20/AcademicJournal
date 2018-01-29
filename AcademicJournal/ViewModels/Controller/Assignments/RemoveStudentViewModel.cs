using AcademicJournal.ViewModels.Shared.EntityViewModels;

namespace AcademicJournal.ViewModels.Controller.Assignments
{
    public class RemoveStudentViewModel
    {
        public AssignmentViewModel Assignment { get; set; }
        public StudentViewModel Student { get; set; }
    }
}