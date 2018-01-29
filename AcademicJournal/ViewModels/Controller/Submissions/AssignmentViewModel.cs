using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Controller.Submissions
{
    public class AssignmentViewModel
    {
        public AssignmentViewModel Assignment { get; set; }
        public IEnumerable<SubmissionViewModel> Submissions { get; set; }
        public SubmissionViewModel SubmissionModel { get; set; }
        public StudentViewModel StudentModel { get; set; }
    }
}