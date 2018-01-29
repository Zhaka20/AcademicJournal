using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Controller.Submissions
{
    public class IndexViewModel
    {
        public IEnumerable<SubmissionViewModel> Submissions { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
        public SubmissionViewModel SubmissionModel { get; set; }
    }
}