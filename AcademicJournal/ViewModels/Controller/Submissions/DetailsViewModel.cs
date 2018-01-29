using AcademicJournal.ViewModels.Shared.EntityViewModels;

namespace AcademicJournal.ViewModels.Controller.Submissions
{
    public class DetailsViewModel
    {
        public SubmissionViewModel Submission { get; set; }
        public StudentViewModel StudentModel { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
    }
}