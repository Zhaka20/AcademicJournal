using AcademicJournal.ViewModels.Shared.EntityViewModels;

namespace AcademicJournal.ViewModels.Controller.Submissions
{
    public class DeleteViewModel
    {
        public SubmissionViewModel Submission { get; set; }
        public StudentViewModel StudentModel { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
    }
}