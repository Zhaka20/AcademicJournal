using AcademicJournal.ViewModels.Shared.EntityViewModels;

namespace AcademicJournal.ViewModels.Controller.Mentors
{
    public class MyStudentViewModel
    {
        public StudentViewModel Student { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
        public SubmissionViewModel SubmissionModel { get; set; }
    }
}