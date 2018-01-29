using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Controller.Assignments
{
    public class StudentsAndSubmissionsListViewModel
    {
        public IEnumerable<SubmissionViewModel> Submissions { get; set; }
        public StudentViewModel StudentModel { get; set; }
        public AssignmentViewModel Assignment { get; set; }
    }

}