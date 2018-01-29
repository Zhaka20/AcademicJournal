using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Controller.Students
{
    public class HomeViewModel
    {
        public StudentViewModel Student { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
        public SubmissionViewModel SubmissionModel { get; set; }
        public IEnumerable<SubmissionViewModel> Submissions { get; set; }
    }
}